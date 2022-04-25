using System.Collections.Generic;
using System.ComponentModel;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        [DisplayName("Hide completed")]
        public bool HideCompleted { get; }
        public bool OrderByRank { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }

        public TodoListDetailViewmodel(int todoListId, string title, bool hideCompleted, bool orderByRank, ICollection<TodoItemSummaryViewmodel> items)
        {
            Items = items;
            TodoListId = todoListId;
            Title = title;
            HideCompleted = hideCompleted;
            OrderByRank = orderByRank;
        }
    }
}