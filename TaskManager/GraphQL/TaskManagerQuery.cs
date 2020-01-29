using GraphQL.Types;
using TaskManager.Business.GraphQL;

namespace TaskManager.GraphQL
{
    public class TaskManagerQuery : ObjectGraphType
    {
        public TaskManagerQuery(ITaskManagerDataProvider dataProvider)
        {
            Field<ListGraphType<BoardGraphType>>(
                "boards",
                resolve: ctx => dataProvider.GetBoards());
        }
    }
}
