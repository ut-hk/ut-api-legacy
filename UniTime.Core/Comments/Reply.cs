﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using UniTime.Interfaces;
using UniTime.Users;

namespace UniTime.Comments
{
    public class Reply : FullAuditedEntity<long>, IHasOwner
    {
        public const int MaxContentLength = 512;
        public const int MinContentLength = 1;

        protected Reply()
        {
        }

        [Required]
        [StringLength(MaxContentLength, MinimumLength = MinContentLength)]
        public virtual string Content { get; protected set; }

        [ForeignKey(nameof(CommentId))]
        public virtual Comment Comment { get; protected set; }

        public virtual Guid CommentId { get; protected set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; protected set; }

        public virtual long OwnerId { get; protected set; }

        public static Reply Create(string content, Comment comment, User owner)
        {
            return new Reply
            {
                Content = content,
                Comment = comment,
                CommentId = comment.Id,
                Owner = owner,
                OwnerId = owner.Id
            };
        }
    }
}