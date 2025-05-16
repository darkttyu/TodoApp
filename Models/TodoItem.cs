namespace TodoApp.Models
{
	public enum Priority { Low, Medium, High }

	public class TodoItem
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public required string Category { get; set; }
		public Priority Priority { get; set; }
		public bool IsCompleted { get; set; }
		public bool IsHidden { get; set; }
	}
}
