using System.Collections.Generic;
using UniTime.Activities;

namespace UniTime.Tags
{
    public class AbstractActivityTag : Tag
    {
        public virtual ICollection<AbstractActivity> Activities { get; set; }
    }
}