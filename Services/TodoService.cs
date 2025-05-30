using TodoApp.Models;
using TodoApp.Data;

namespace TodoApp.Services
{
	public class TodoService
	{
		private readonly TodoDbContext _context;

		public TodoService(TodoDbContext context)
		{
			_context = context;
		}

		public List<TodoItem> GetAll(string? category = null, Priority? priority = null)
		{
			var query = _context.TodoItems.AsQueryable();
			if (!string.IsNullOrEmpty(category))
				query = query.Where(i => i.Category == category);
			if (priority.HasValue)
				query = query.Where(i => i.Priority == priority.Value);
			// Only show non-hidden items
			query = query.Where(i => !i.IsHidden);
			return query.ToList();
		}

		public void Add(string title, string category, Priority priority)
		{
			_context.TodoItems.Add(new TodoItem
			{
				Title = title,
				Category = category,
				Priority = priority,
				IsCompleted = false,
				IsHidden = false
			});
			_context.SaveChanges();
		}

		public void MarkAsComplete(int id)
		{
			var item = _context.TodoItems.FirstOrDefault(i => i.Id == id);
			if (item != null)
			{
				item.IsCompleted = true;
				_context.SaveChanges();
			}
		}

		public void Hide(int id)
		{
			var item = _context.TodoItems.FirstOrDefault(i => i.Id == id);
			if (item != null)
			{
				item.IsHidden = true;
				_context.SaveChanges();
			}
		}

		public List<string> GetCategories()
		{
			return _context.TodoItems.Select(i => i.Category).Distinct().ToList();
		}
	}
}
