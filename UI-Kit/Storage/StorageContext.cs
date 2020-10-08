using System;
using System.IO;
using Azure.Storage.Blobs;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs.Models;

namespace UI_Kit.Storage
{
    public class StorageContext
    {
        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=kuviocreative;AccountKey=KLS5GllFAdTF2Vp6ogzMpFjkEEM+bg/NJMFdKzWU4jOP+4hCIxyyXAUkhmW8SajS50o6wNd8D780HerrOn2iZQ==;EndpointSuffix=core.windows.net";
        private readonly string containerName = "uikit";
        private readonly string baseUrl = "https://kuviocreative.blob.core.windows.net/";

        public StorageContext()
        {

        }

        public StorageContext(IConfiguration config)
        {
            //connectionString = config["BlobStorage:ConnectionString"];
        }

        public async Task<string> Upload(Stream stream, string fileName, int userId)
        {
            var encodeFileName = Uri.EscapeUriString(fileName);//);DateTime.Now.Ticks + fileName);
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            BlobClient blob = container.GetBlobClient(encodeFileName);//userId + "/" + 
            
            await blob.UploadAsync(stream, new BlobHttpHeaders { ContentType = "text/css;" });
            string filePath = baseUrl + containerName + "/" + encodeFileName;//+ userId + "/" 
            //if upload succeeds
            return filePath;
        }
    }
}
