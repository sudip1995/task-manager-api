using System.Collections.Generic;
using TaskManager.Library.Database;

namespace TaskManager.Contracts.Models
{
    public class Board : ARepositoryItem
    {
        public string Title { get; set; }
        public List<Column> Columns { get; set; }
        public bool IsFavorite { get; set; }
        public Board()
        {
            Columns = new List<Column>();
        }
        public Board(string title)
        {
            Title = title;
            Columns = new List<Column>();
            IsFavorite = false;
        }

        public void AddColumn(Column column)
        {
            Columns.Add(column);
        }

        public void UpdateColumn(Column column)
        {
            var id = Columns.FindIndex(o => o.Id == column.Id);
            if (id != -1)
            {
                Columns[id] = column;
            }
        }
    }
}