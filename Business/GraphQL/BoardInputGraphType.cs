using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class BoardInputGraphType: InputObjectGraphType<Board>
    {
        public BoardInputGraphType()
        {
            Field<StringGraphType>("title");
        }
    }
}
