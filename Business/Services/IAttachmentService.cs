using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Business.Services
{
    public interface IAttachmentService
    {
        Task<object> Upload(string ticketId, IFormFile data);
        Task<FileStreamResult> Download(string id);
    }
}