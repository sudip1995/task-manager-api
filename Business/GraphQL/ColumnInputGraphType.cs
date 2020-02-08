using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class ColumnInputGraphType: InputObjectGraphType<Column>
    {
        public ColumnInputGraphType()
        {
            Field<StringGraphType>("title");
            Field<StringGraphType>("boardId");
            Field<IntGraphType>("order");
        }
    }
}
