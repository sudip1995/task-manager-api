using Newtonsoft.Json.Linq;

namespace TaskManager.Contracts.Models
{
    public class GraphQlQuery
    {
        public string OperationName { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}
