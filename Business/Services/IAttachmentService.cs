using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TaskManager.Business.Services
{
    public interface IAttachmentService
    {
        Task<object> Upload(string ticketId, IFormFile data);
    }
}