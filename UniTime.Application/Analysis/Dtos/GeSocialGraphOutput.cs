using System.Collections.Generic;
using UniTime.Users.Dtos;

namespace UniTime.Analysis.Dtos
{
    public class GeSocialGraphOutput
    {
        public ICollection<FriendPairDto> Edges { get; set; }

        public ICollection<UserListDto> Nodes { get; set; }
    }
}