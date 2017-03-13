using System;

namespace UniTime.Interfaces
{
    public interface ITimeSlot
    {
        DateTime? StartTime { get; }

        DateTime? EndTime { get; }
    }
}