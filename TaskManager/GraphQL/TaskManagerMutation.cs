using System;
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
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "title"}),
                resolve: ctx =>
                {
                    var title = ctx.GetArgument<string>("title");
                    return taskManagerDataMutator.AddBoard(title);
                });
            Field<ColumnGraphType>(
                "addColumn",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "boardId"},
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "title" }),
                resolve: ctx =>
                {
                    var boardId = ctx.GetArgument<string>("boardId");
                    var title = ctx.GetArgument<string>("title");
                    return taskManagerDataMutator.AddColumn(title, boardId);
                });
            Field<TicketGraphType>(
                "addTicket",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "columnId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "title" }),
                resolve: ctx =>
                {
                    var columnId = ctx.GetArgument<string>("columnId");
                    var title = ctx.GetArgument<string>("title");
                    return taskManagerDataMutator.AddTicket(title, columnId);
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
            Field<BoardGraphType>(
                "moveColumn",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "fromBoardId" },
                    new QueryArgument<StringGraphType> { Name = "toBoardId" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "previousIndex" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> {Name = "currentIndex" }),
                resolve: ctx =>
                {
                    var fromBoardId = ctx.GetArgument<string>("fromBoardId");
                    var toBoardId = ctx.GetArgument<string>("toBoardId");
                    var previousIndex = ctx.GetArgument<int>("previousIndex");
                    var currentIndex = ctx.GetArgument<int>("currentIndex");
                    return taskManagerDataMutator.MoveColumn(fromBoardId, toBoardId, previousIndex, currentIndex);
                });
            Field<BoardGraphType>(
                "moveTicket",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "fromBoardId" },
                    new QueryArgument<StringGraphType> { Name = "toBoardId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "fromColumnId" },
                    new QueryArgument<StringGraphType> { Name = "toColumnId" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "previousIndex" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> {Name = "currentIndex" }),
                resolve: ctx =>
                {
                    var fromBoardId = ctx.GetArgument<string>("fromBoardId");
                    var toBoardId = ctx.GetArgument<string>("toBoardId");
                    var fromColumnId = ctx.GetArgument<string>("fromColumnId");
                    var toColumnId = ctx.GetArgument<string>("toColumnId");
                    var previousIndex = ctx.GetArgument<int>("previousIndex");
                    var currentIndex = ctx.GetArgument<int>("currentIndex");
                    return taskManagerDataMutator.MovTicket(fromBoardId, toBoardId, fromColumnId, toColumnId, previousIndex, currentIndex);
                });
            Field<CheckListGraphType>(
                "addChecklist",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "ticketId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "title" }),
                resolve: ctx =>
                {
                    var ticketId = ctx.GetArgument<string>("ticketId");
                    var title = ctx.GetArgument<string>("title");
                    return taskManagerDataMutator.AddCheckList(title, ticketId);
                });
            Field<CheckListItemGraphType>(
                "addChecklistItem",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "checklistId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "title" }),
                resolve: ctx =>
                {
                    var checklistId = ctx.GetArgument<string>("checklistId");
                    var title = ctx.GetArgument<string>("title");
                    return taskManagerDataMutator.AddCheckListItem(title, checklistId);
                });
        }
    }
}