using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class CheckListItemInputGraphType : InputObjectGraphType<CheckListItem>
    {
        public CheckListItemInputGraphType()
        {
            Field<StringGraphType>("title");
            Field<BooleanGraphType>("isChecked");
        }
    }
}
