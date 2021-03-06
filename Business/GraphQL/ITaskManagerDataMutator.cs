﻿using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public interface ITaskManagerDataMutator
    {
        Board AddBoard(string title);
        Column AddColumn(string title, string boardId);
        Ticket AddTicket(string title, string columnId);
        Board UpdateBoard(string id, Board board);
        Column UpdateColumn(string id, Column column);
        Ticket UpdateTicket(string id, Ticket ticket);
        Board MoveColumn(string fromBoardId, string toBoardId, int previousIndex, int currentIndex);
        Board MovTicket(string fromBoardId, string toBoardId, string fromColumnId, string toColumnId, int previousIndex, int currentIndex);
        CheckList AddCheckList(string title, string ticketId);
        CheckListItem AddCheckListItem(string title, string checklistId);
        CheckList UpdateCheckList(string id, CheckList checklist);
        CheckListItem UpdateCheckListItem(string id, CheckListItem checklistItem);
    }
}