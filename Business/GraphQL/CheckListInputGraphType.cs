using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class CheckListInputGraphType : InputObjectGraphType<CheckList>
    {
        public CheckListInputGraphType()
        {
            Field<StringGraphType>("title");
        }
    }
}
