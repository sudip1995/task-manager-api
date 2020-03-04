using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class ColumnGraphType: ObjectGraphType<Column>
    {
        public ColumnGraphType(ITaskManagerDataProvider taskManagerDataProvider)
        {
            Name = "Column";
            Field(o => o.Id);
            Field(o => o.Title);
            Field(o => o.BoardId);
            Field<ListGraphType<TicketGraphType>>("tickets",
                resolve: ctx => taskManagerDataProvider.GetTickets(ctx.Source.Id));
        }
    }
}
