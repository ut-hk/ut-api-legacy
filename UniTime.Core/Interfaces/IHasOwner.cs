using UniTime.Users;

namespace UniTime.Interfaces
{
    public interface IHasOwner
    {
        User Owner { get; }

        long OwnerId { get; }
    }
}