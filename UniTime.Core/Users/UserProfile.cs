using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using UniTime.Files;
using UniTime.Users.Enums;

namespace UniTime.Users
{
    public class UserProfile : AuditedEntity<long>
    {
        protected UserProfile()
        {
        }

        public virtual User User { get; protected set; }

        [ForeignKey(nameof(User))]
        public override long Id { get; set; }

        public virtual Gender Gender { get; protected set; } = Gender.NotProvided;

        public virtual DateTime? Birthday { get; protected set; }

        [ForeignKey(nameof(CoverId))]
        public virtual Image Cover { get; protected set; }

        public virtual Guid? CoverId { get; protected set; }

        public static UserProfile Create(User user, Gender? gender, DateTime? birthday, Image cover)
        {
            var userProfile = new UserProfile
            {
                Id = user.Id,
                Gender = gender ?? Gender.NotProvided,
                Birthday = birthday
            };

            if (cover != null)
            {
                userProfile.Cover = cover;
                userProfile.CoverId = cover.Id;
            }

            return userProfile;
        }

        internal void EditUserProfile(Gender? gender, DateTime? birthday, Image cover)
        {
            Gender = gender ?? Gender.NotProvided;
            Birthday = birthday;

            if (cover != null)
            {
                Cover = cover;
                CoverId = cover.Id;
            }
        }
    }
}