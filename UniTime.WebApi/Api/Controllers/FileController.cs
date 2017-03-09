using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Web.Models;
using Abp.WebApi.Authorization;
using UniTime.Files;
using UniTime.Files.Dtos;
using UniTime.Files.Managers;
using File = UniTime.Files.File;

namespace UniTime.Api.Controllers
{
    public class FileController : UniTimeApiControllerBase
    {
        private readonly IFileManager _fileManager;

        public FileController(
            IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet]
        public virtual async Task<HttpResponseMessage> GetFile(Guid id)
        {
            var file = await _fileManager.GetAsync(id);
            var stream = _fileManager.GetStream(file);

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StreamContent(stream)
            };

            // Content headers
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = file.OriginalFileName
            };
            response.Content.Headers.ContentLength = stream.Length;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(File.ContentTypes[Path.GetExtension(file.OriginalFileName)]);

            // Response headers
            response.Headers.CacheControl = new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromDays(14)
            };

            return response;
        }

        [HttpPost]
        [UnitOfWork]
        [AbpApiAuthorize]
        [WrapResult]
        public virtual async Task<IReadOnlyList<FileDto>> PostFile()
        {
            if (AbpSession.UserId == null)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            try
            {
                var currentUser = await GetCurrentUserAsync();

                if (!Request.Content.IsMimeMultipartContent("form-data"))
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

                var provider = await Request.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider());
                var fileContents = provider.Contents;

                var files = new List<File>();

                foreach (var fileContent in fileContents)
                {
                    var rawFileName = fileContent?.Headers.ContentDisposition.FileName;
                    if (rawFileName == null) throw new HttpResponseException(HttpStatusCode.BadRequest);

                    var fileName = string.Join("", rawFileName.Split(Path.GetInvalidFileNameChars()));
                    fileName = Path.ChangeExtension(fileName, Path.GetExtension(fileName).ToLower());

                    if (Image.AllowedExtensions.Contains(Path.GetExtension(fileName)))
                        using (var fileStream = await fileContent.ReadAsStreamAsync())
                        {
                            var image = await _fileManager.CreateImageAsync(Image.Create(fileName, currentUser), fileStream, currentUser);

                            files.Add(image);
                        }
                }

                return files.MapTo<List<FileDto>>();
            }
            catch (Exception exception)
            {
                throw new UserFriendlyException(exception.GetBaseException().Message);
            }
        }
    }
}