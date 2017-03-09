using System.Collections.Generic;
using UniTime.Activities;

namespace UniTime.Locations
{
    public class AbstractActivityLocation : Location
    {
        protected AbstractActivityLocation()
        {
        }

        public virtual ICollection<AbstractActivity> Activities { get; set; }
    }
}