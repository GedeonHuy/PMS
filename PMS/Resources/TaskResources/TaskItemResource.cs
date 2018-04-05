namespace PMS.Resources.TaskResources
{
    public class TaskItemResource
    {
        public int TaskItemId { get; set; }
        public int? TaskId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDone { get; set; }
        public TaskResource Task { get; set; }
        public TaskItemResource()
        {
            IsDeleted = false;
            IsDone = false;
        }
    }
}