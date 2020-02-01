using System.Collections.Generic;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.Services
{
    public interface ITicketService
    {
        List<Ticket> GetAll(string columnId);
        Ticket Add(Ticket ticket);
    }
}