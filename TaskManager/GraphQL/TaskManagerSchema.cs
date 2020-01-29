using GraphQL;
using GraphQL.Types;

namespace TaskManager.GraphQL
{
    public class TaskManagerSchema : Schema
    {
        public TaskManagerSchema(IDependencyResolver resolver): base(resolver)
        {
            Query = resolver.Resolve<TaskManagerQuery>();
            Mutation = resolver.Resolve<TaskManagerMutation>();
        }
    }
}

