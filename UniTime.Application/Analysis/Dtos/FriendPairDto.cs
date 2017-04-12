using Abp.AutoMapper;
using UniTime.Users;

namespace UniTime.Analysis.Dtos
{
    [AutoMapFrom(typeof(FriendPair))]
    public class FriendPairDto
    {
        public long LeftId { get; set; }

        public long RightId { get; set; }
    }
}