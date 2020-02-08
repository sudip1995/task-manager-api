using System.Collections.Generic;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.Services
{
    public interface ITicketService
    {
        Ticket Get(string id);
        List<Ticket> GetAll(string columnId);
        Ticket Add(Ticket ticket, string columnId);
        Ticket Update(string id, Ticket ticket);
    }
}