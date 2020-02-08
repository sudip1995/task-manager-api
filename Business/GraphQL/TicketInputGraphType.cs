using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class TicketInputGraphType: InputObjectGraphType<Ticket>
    {
        public TicketInputGraphType()
        {
            Field<StringGraphType>("title");
            Field<StringGraphType>("columnId");
            Field<StringGraphType>("description");
            Field<IntGraphType>("order");
        }
    }
}