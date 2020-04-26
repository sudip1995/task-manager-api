using GraphQL.Types;
using TaskManager.Business.GraphQL;

namespace TaskManager.GraphQL
{
    public class TaskManagerQuery : ObjectGraphType
    {
        public TaskManagerQuery(ITaskManagerDataProvider dataProvider)
        {
            Field<ListGraphType<BoardGraphType>>(
                "boards",
                resolve: ctx => dataProvider.GetBoards());
            Field<BoardGraphType>(
                "board",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "id"}),
            resolve: ctx => dataProvider.GetBoard(ctx.GetArgument<string>("id")));
            Field<TicketDetailsGraphType>(
                "ticketDetails",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
                resolve: ctx => dataProvider.GetTicketDetails(ctx.GetArgument<string>("id")));
        }
    }
}
