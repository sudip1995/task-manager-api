using GraphQL.Types;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class TicketDetailsGraphType: ObjectGraphType<TicketDetails>
    {
        public TicketDetailsGraphType()
        {
            Name = "TicketDetails";
            Field(o => o.Id);
            Field(o => o.Title);
            Field(o => o.Description);
            Field<ListGraphType<CheckListGraphType>>("checkLists", resolve: ctx => ctx.Source.CheckLists);
        }
    }

    public class CheckListGraphType : ObjectGraphType<CheckList>
    {
        public CheckListGraphType()
        {
            Name = "CheckList";
            Field(o => o.Id);
            Field(o => o.Title);
            Field<ListGraphType<CheckListItemGraphType>>("checkListItems", resolve: ctx => ctx.Source.CheckListItems);
        }
    }

    public class CheckListItemGraphType : ObjectGraphType<CheckListItem>
    {
        public CheckListItemGraphType()
        {
            Name = "CheckListItem";
            Field(o => o.Id);
            Field(o => o.Title);
            Field(o => o.IsChecked);
        }
    }
}