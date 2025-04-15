namespace TaskManager.API.Entities
{
    public class TaskItem
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Status { get; set; } = "Pending";
        public string? DueDate { get; set; }
        public Guid? AssignedTo { get; set; }
        public string? AssignedBy { get; set; }
        public string? AssigneeName { get; set; }

    }
}
