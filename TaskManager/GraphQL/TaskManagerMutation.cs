using System.Reactive.PlatformServices;
using GraphQL.Types;
using TaskManager.Business.GraphQL;
using TaskManager.Contracts.Models;

namespace TaskManager.GraphQL
{
    public class TaskManagerMutation: ObjectGraphType
    {
        public TaskManagerMutation(ITaskManagerDataMutator taskManagerDataMutator)
        {
            Field<BoardGraphType>(
                "addBoard",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BoardInputGraphType>> {Name = "board"}),
                resolve: ctx =>
                {
                    var board = ctx.GetArgument<Board>("board");
                    return taskManagerDataMutator.AddBoard(board);
                });
        }
    }
}