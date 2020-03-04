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
    }
}