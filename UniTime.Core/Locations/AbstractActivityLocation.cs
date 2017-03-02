using System.Collections.Generic;
using UniTime.Activities;

namespace UniTime.Locations
{
    public class AbstractActivityLocation : Location
    {
        public virtual ICollection<AbstractActivity> Activities { get; set; }
    }
}