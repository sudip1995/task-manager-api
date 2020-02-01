using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class BoardGraphType : ObjectGraphType<Board>
    {
        public BoardGraphType(ITaskManagerDataProvider taskManagerDataProvider)
        {
            Name = "Board";
            Field(o => o.Id);
            Field(o => o.Title);
            Field<ListGraphType<ColumnGraphType>>("columns", resolve: ctx => taskManagerDataProvider.GetColumns(ctx.Source.Id));
        }
    }
}