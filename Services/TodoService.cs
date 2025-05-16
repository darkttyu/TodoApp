using System.Collections.Generic;
using System.Linq;
using TodoApp.Models;

namespace TodoApp.Services
{
	public class TodoService
	{
		private static List<TodoItem> _items = new List<TodoItem>();
		private static int _nextId = 1;

		public List<TodoItem> GetAll(string? category = null, Priority? priority = null)
		{
			var query = _items.AsQueryable();
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
			_items.Add(new TodoItem
			{
				Id = _nextId++,
				Title = title,
				Category = category,
				Priority = priority,
				IsCompleted = false,
				IsHidden = false
			});
		}

		public void MarkAsComplete(int id)
		{
			var item = _items.FirstOrDefault(i => i.Id == id);
			if (item != null)
			{
				item.IsCompleted = true;
			}
		}

		public void Hide(int id)
		{
			var item = _items.FirstOrDefault(i => i.Id == id);
			if (item != null)
			{
				item.IsHidden = true;
			}
		}

		public List<string> GetCategories()
		{
			return _items.Select(i => i.Category).Distinct().ToList();
		}
	}
}
