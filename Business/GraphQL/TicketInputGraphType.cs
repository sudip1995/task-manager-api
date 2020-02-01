using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class TicketInputGraphType: InputObjectGraphType<Ticket>
    {
        public TicketInputGraphType()
        {
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<NonNullGraphType<StringGraphType>>("columnId");
            Field<StringGraphType>("description");
            Field<IntGraphType>("order");
        }
    }
}