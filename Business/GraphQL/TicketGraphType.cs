using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class TicketGraphType : ObjectGraphType<Ticket>
    {
        public TicketGraphType()
        {
            Name = "Ticket";
            Field(o => o.Id);
            Field(o => o.Title);
            Field(o => o.ColumnId);
        }
    }
}