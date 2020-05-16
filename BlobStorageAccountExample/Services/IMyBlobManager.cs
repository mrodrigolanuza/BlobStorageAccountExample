using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlobStorageAccountExample.Services
{
    public interface IMyBlobManager
    {
        Task<string> UploadFile(IFormFile myFile);
        Task<List<IListBlobItem>> BlobList();
        Task<bool> DeleteBlob(string absoluteUri);
    }
}
