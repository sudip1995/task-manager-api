namespace TaskManager.Contracts.Models
{
    public class TaskManagerStoreDatabaseSettings : ITaskManagerStoreDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string BoardCollectionName { get; set; }
        public string ColumnCollectionName { get; set; }
        public string TicketCollectionName { get; set; }
    }

    public interface ITaskManagerStoreDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string BoardCollectionName { get; set; }
        string ColumnCollectionName { get; set; }
        string TicketCollectionName { get; set; }
    }
}