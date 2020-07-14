using System;
using System.Composition;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Minio;
using Minio.Exceptions;
using TaskManager.Library.DataProviders;
using TaskManager.Library.Extensions;
using TaskManager.Library.Helpers;

namespace TaskManager.Business.Services
{
    [Export(typeof(IAttachmentService))]
    public class AttachmentService : IAttachmentService
    {
        [Import] public IUserInfoProvider UserInfoProvider { get; set; }
        [Import] public ITicketService TicketService { get; set; }
        private static MinioClient MinIoClient { get; set; }
        private static readonly object LockObject = new object();
        private const string BucketName = "taskmanager";

        public AttachmentService()
        {
            GetMinIOClient();
        }

        private void GetMinIOClient()
        {
            if (MinIoClient == null)
            {
                lock (LockObject)
                {
                    if (MinIoClient == null)
                    {
                        MinIoClient = new MinioClient(ConfigurationHelper.Instance.GetMinIOEndpoint(),
                            ConfigurationHelper.Instance.GetMinIOAccessKey(),
                            ConfigurationHelper.Instance.GetMinIOSecretKey());
                    }
                }
            }
        }

        public async Task<object> Upload(string ticketId, IFormFile file)
        {
            var user = UserInfoProvider.GetUser();
            try
            {
                bool found = await MinIoClient.BucketExistsAsync(BucketName);
                if (!found)
                {
                    await MinIoClient.MakeBucketAsync(BucketName);
                }

                await using var stream = file.OpenReadStream();
                var fileName = $"{Guid.NewGuid().ToString()}-{user.UserName}-{ticketId}-{stream.Length}-{file.FileName}";
                await MinIoClient.PutObjectAsync(BucketName, fileName, stream, stream.Length);
                var attachment = TicketService.AddAttachment(ticketId, fileName, file.FileName, stream.Length);
                return attachment;
            }
            catch (MinioException e)
            {
                Console.WriteLine("File Upload Error: {0}", e.Message);
                return e.Message;
            }
        }
        public async Task<object> Download(string objectId)
        {
            try
            {
                await MinIoClient.StatObjectAsync(BucketName, objectId);

                await MinIoClient.GetObjectAsync(BucketName, objectId, 
                    (stream) =>
                    {
                        stream.CopyTo(Console.OpenStandardOutput());
                    });
            }
            catch (MinioException e)
            {
                Console.WriteLine("File Download Error: {0}", e.Message);
            }

            return objectId;
        }
    }
}