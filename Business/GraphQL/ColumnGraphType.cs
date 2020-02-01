using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class ColumnGraphType: ObjectGraphType<Column>
    {
        public ColumnGraphType()
        {
            Name = "Column";
            Field(o => o.Id);
            Field(o => o.Title);
            Field(o => o.BoardId);
            Field(o => o.Order);
        }
    }
}
