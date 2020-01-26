namespace TaskManager.Contracts.Models
{
    public class BoardStoreDatabaseSettings : IBoardStoreDatabaseSettings
    {
        public string BoardCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBoardStoreDatabaseSettings
    {
        string BoardCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}