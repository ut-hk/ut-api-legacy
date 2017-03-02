﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.Activities;
using UniTime.Comments.Dtos;
using UniTime.Comments.Managers;

namespace UniTime.Comments
{
    public interface IReplyAppService : IApplicationService
    {
        Task<GetRepliesOutput> GetReplies();

        Task<EntityDto<long>> CreateReply(CreateReplyInput input);
    }

    public class CreateReplyInput
    {
        public long CommentId { get; set; }

        public string Content { get; set; }
    }

    public class GetRepliesOutput
    {
        public IReadOnlyList<ReplyDto> Replies { get; set; }
    }

    public class ReplyAppService : UniTimeAppServiceBase, IReplyAppService
    {
        private readonly ICommentManager _commentManager;
        private readonly IReplyManager _replyManager;
        private readonly IRepository<Reply, long> _replyRepository;

        public ReplyAppService(
            IRepository<Reply, long> replyRepository,
            ICommentManager commentManager,
            IReplyManager replyManager)
        {
            _replyRepository = replyRepository;
            _commentManager = commentManager;
            _replyManager = replyManager;
        }

        public async Task<GetRepliesOutput> GetReplies()
        {
            var replies = await _replyRepository.GetAllListAsync();

            return new GetRepliesOutput
            {
                Replies = replies.MapTo<List<ReplyDto>>()
            };
        }

        public async Task<EntityDto<long>> CreateReply(CreateReplyInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var comment = await _commentManager.GetCommentAsync(input.CommentId);

            var reply = await _replyManager.CreateReplyAsync(new Reply
            {
                Content = input.Content,
                Comment = comment,
                CommentId = comment.Id,
                Owner = currentUser,
                OwnerId = currentUser.Id
            });

            return new EntityDto<long>(reply.Id);
        }
    }
}