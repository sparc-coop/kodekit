using System;
using System.IO;
using Azure.Storage.Blobs;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs.Models;
using System.Text;

namespace Kodekit.Features
{
    public class UploadService
    {
        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=kodekit;AccountKey=1C/QtEYejRDLCOinYPFO2J7KRMIZzocBIlh4LkdEzMSvE/fq2Olk4oZj0oL0ji6Xv6wgIYs15LL3Qf9ZyaduyQ==;EndpointSuffix=core.windows.net";
        private readonly string containerName = "kits";
        private readonly string baseUrl = "https://kodekit.blob.core.windows.net/";

        public UploadService()
        {

        }

        public UploadService(IConfiguration config)
        {
            //connectionString = config["BlobStorage:ConnectionString"];
        }

        public async Task<string> Upload(Stream stream, string fileName, int? userId)
        {
            var encodeFileName = Uri.EscapeUriString(fileName);//);DateTime.Now.Ticks + fileName);
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            BlobClient blob = container.GetBlobClient(encodeFileName);//userId + "/" + 

            await blob.UploadAsync(stream, new BlobHttpHeaders { ContentType = "text/css;" });
            string filePath = baseUrl + containerName + "/" + encodeFileName;//+ userId + "/" 
            //if upload succeeds
            return filePath;
        }

        public async Task<string> UploadString(string cssString, string fileName)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(cssString);
            MemoryStream stream = new MemoryStream(bytes);

            var encodeFileName = Uri.EscapeUriString(fileName);
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            BlobClient blob = container.GetBlobClient(encodeFileName);

            await blob.UploadAsync(stream, new BlobHttpHeaders { ContentType = "text/css;" });
            string filePath = baseUrl + containerName + "/" + encodeFileName;
            //if upload succeeds
            return filePath;
        }

        public async Task<string> Download(string fileName)
        {
            var encodeFileName = Uri.EscapeUriString(fileName);
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            BlobClient blob = container.GetBlobClient(encodeFileName + ".css");

            Stream stream = await blob.OpenReadAsync();
            var folder = Environment.SpecialFolder.MyDocuments;

            string filePath = Environment.GetFolderPath(folder) + "\\kodekit.css";

            using (var fileStream = File.Create(filePath))
            {
                stream.CopyTo(fileStream);
            }
            return filePath;
        }
    }
}
