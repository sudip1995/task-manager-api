using System.Collections.Generic;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.Services
{
    public interface ITicketService
    {
        Ticket Get(string id);
        List<Ticket> GetAll(string columnId);
        Ticket Add(string title, string columnId);
        Ticket Update(string id, Ticket ticket);
        TicketDetails GetDetails(string id);
        List<CheckList> GetCheckLists(string ticketId);
        CheckList AddCheckList(string title, string ticketId);
        CheckListItem AddCheckListItem(string title, string checklistId);
        CheckList UpdateCheckList(string id, CheckList checklist);
        CheckListItem UpdateCheckListItem(string id, CheckListItem checklistItem);
        Attachment AddAttachment(string ticketId, string objectId, string fileName, long streamLength);
    }
}