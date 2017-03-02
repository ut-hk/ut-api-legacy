namespace UniTime.Activities.Dtos
{
    public class CreateActivityPlanInput
    {
        public string Name { get; set; }

        public long[] DescriptionIds { get; set; }
    }
}