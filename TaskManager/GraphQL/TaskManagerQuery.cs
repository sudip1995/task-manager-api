using System.Linq;
using System.Security.Claims;
using GraphQL.Types;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using TaskManager.Business.GraphQL;

namespace TaskManager.GraphQL
{
    public class TaskManagerQuery : ObjectGraphType
    {
        public TaskManagerQuery(ITaskManagerDataProvider dataProvider)
        {
            Field<ListGraphType<BoardGraphType>>(
                "boards",
                resolve: ctx =>
                {
                    var user = (ClaimsPrincipal)ctx.UserContext;
                    var isUserAuthenticated = ((ClaimsIdentity)user.Identity).IsAuthenticated;
                    return dataProvider.GetBoards();
                });
            Field<BoardGraphType>(
                "board",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> {Name = "id"}),
            resolve: ctx =>
            {
                var user = (ClaimsPrincipal)ctx.UserContext;
                var gg = user.Claims.ToList();
                var isUserAuthenticated = ((ClaimsIdentity)user.Identity).IsAuthenticated;
                return dataProvider.GetBoard(ctx.GetArgument<string>("id"));
            });
        }
    }
}
