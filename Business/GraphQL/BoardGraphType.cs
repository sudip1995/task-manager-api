using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class BoardGraphType : ObjectGraphType<Board>
    {
        public BoardGraphType()
        {
            Name = "Board";
            Field(o => o.Id);
            Field(o => o.Title);
        }
    }
}