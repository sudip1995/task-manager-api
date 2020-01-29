using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Contracts.Models;

namespace TaskManager.Controllers
{
    [Route("[controller]")]
    public class GraphQlController: Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecutor;
        public GraphQlController(ISchema schema, 
            IDocumentExecuter documentExecutor)
        {
            _schema = schema;
            _documentExecutor = documentExecutor;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var inputs = query.Variables?.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs
            };

            var result = await _documentExecutor.ExecuteAsync(executionOptions);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
