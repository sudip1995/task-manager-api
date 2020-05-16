using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Grpc.Core;
using Newtonsoft.Json;

namespace TaskManager.Services
{
    public class GraphqlService: GraphQLService.GraphQLServiceBase
    {
        public ISchema TaskManagerSchema { get; set; }
        public GraphqlService(ISchema taskManagerSchema)
        {
            TaskManagerSchema = taskManagerSchema;
        }

        public override async Task Execute(Request request, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            var executionOptions = new ExecutionOptions
            {
                Schema = TaskManagerSchema,
                Query = request.Query
            };
            if (!String.IsNullOrWhiteSpace(request.Inputs))
            {
                executionOptions.Inputs = request.Inputs.ToInputs();
            }
            var result = await new DocumentExecuter().ExecuteAsync(executionOptions);

            var response = new Response();

            if (result.Errors?.Count > 0)
            {
                foreach (var executionError in result.Errors)
                {
                    var error = new Error
                    {
                        Message = executionError.Message
                    };
                    if (executionError.Locations != null)
                    {
                        foreach (var executionErrorLocation in executionError?.Locations)
                        {
                            error.Locations.Add(new SourceLocation
                            {
                                Line = executionErrorLocation.Line,
                                Column = executionErrorLocation.Column
                            });
                        }
                    }
                    

                    if (executionError.Path != null)
                    {
                        error.Path.AddRange(executionError.Path);
                    }
                    
                    response.Errors.Add(error);
                }
                await responseStream.WriteAsync(response);
            }

            var jsonString = JsonConvert.SerializeObject(result.Data);

            response.Data = "{ \"data\": " + jsonString + "}";

            await responseStream.WriteAsync(response);

        }
    }
}
