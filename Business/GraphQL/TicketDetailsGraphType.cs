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
            Field<ListGraphType<AttachmentGraphType>>("attachments", resolve: ctx => ctx.Source.Attachments);
        }
    }

    public class AttachmentGraphType : ObjectGraphType<Attachment>
    {
        public AttachmentGraphType()
        {
            Name = "Attachment";
            Field(o => o.Id);
            Field(o => o.FileName);
            Field(o => o.FileSize);
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