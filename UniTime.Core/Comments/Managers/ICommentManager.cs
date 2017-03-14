using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Comments.Managers
{
    public interface ICommentManager : IDomainService
    {
        Task<Comment> GetAsync(Guid id);

        Task<Comment> CreateAsync(Comment comment);
    }
}