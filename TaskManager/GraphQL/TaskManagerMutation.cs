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
            Field<ColumnGraphType>(
                "addColumn",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "boardId"},
                    new QueryArgument<NonNullGraphType<ColumnInputGraphType>> {Name = "column"}),
                resolve: ctx =>
                {
                    var boardId = ctx.GetArgument<string>("boardId");
                    var column = ctx.GetArgument<Column>("column");
                    return taskManagerDataMutator.AddColumn(column, boardId);
                });
            Field<TicketGraphType>(
                "addTicket",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "columnId" },
                    new QueryArgument<NonNullGraphType<TicketInputGraphType>> {Name = "ticket"}),
                resolve: ctx =>
                {
                    var columnId = ctx.GetArgument<string>("columnId");
                    var ticket = ctx.GetArgument<Ticket>("ticket");
                    return taskManagerDataMutator.AddTicket(ticket, columnId);
                });
            Field<BoardGraphType>(
                "updateBoard",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id"},
                    new QueryArgument<NonNullGraphType<BoardInputGraphType>> {Name = "board"}),
                resolve: ctx =>
                {
                    var id = ctx.GetArgument<string>("id");
                    var board = ctx.GetArgument<Board>("board");
                    return taskManagerDataMutator.UpdateBoard(id, board);
                });
            Field<ColumnGraphType>(
                "updateColumn",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ColumnInputGraphType>> {Name = "column"}),
                resolve: ctx =>
                {
                    var id = ctx.GetArgument<string>("id");
                    var column = ctx.GetArgument<Column>("column");
                    return taskManagerDataMutator.UpdateColumn(id, column);
                });
            Field<TicketGraphType>(
                "updateTicket",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<TicketInputGraphType>> {Name = "ticket"}),
                resolve: ctx =>
                {
                    var id = ctx.GetArgument<string>("id");
                    var ticket = ctx.GetArgument<Ticket>("ticket");
                    return taskManagerDataMutator.UpdateTicket(id, ticket);
                });
        }
    }
}