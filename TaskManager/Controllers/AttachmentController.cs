using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Services;
using TaskManager.Library.Ioc;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        public IAttachmentService AttachmentService { get; set; }
        public AttachmentController()
        {
            AttachmentService = IocContainer.Instance.Resolve<IAttachmentService>();
        }
        [HttpPost("Upload")]
        public IActionResult Upload([FromForm] IFormFile file, [FromQuery] string ticketId)
        {
            var response = AttachmentService.Upload(ticketId, file);
            return Ok(response);
        }
        [HttpGet("Download")]
        public async Task<FileStreamResult> Download([FromQuery] string id)
        {
            var response = await AttachmentService.Download(id);
            return response;
        }
    }
}
