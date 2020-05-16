using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlobStorageAccountExample.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BlobStorageAccountExample.Services
{
    public class MyBlobManager : IMyBlobManager
    {
        CloudBlobContainer _blobContainer;

        public MyBlobManager(BlobStorageSettings settings) {
            try {
                var storageAccount = CloudStorageAccount.Parse(settings.ConnectionString);          //Storage Account
                var cloudBlobClient = storageAccount.CreateCloudBlobClient();                       //Cliente blob sobre la storageaccount
                _blobContainer = cloudBlobClient.GetContainerReference(settings.ContainerName);     //Contenedor blob
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<IListBlobItem>> BlobList() {
            var totalResults = new List<IListBlobItem>();
            BlobContinuationToken continuationToken = null;
            do {
                var results = await _blobContainer.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = results.ContinuationToken;
                totalResults.AddRange(results.Results);
            } while (continuationToken!=null);
            return totalResults;
        }

        public async Task<bool> DeleteBlob(string absoluteUri) {
            try {
                var uriObj = new Uri(absoluteUri);
                var blockBlob = _blobContainer.GetBlockBlobReference(Path.GetFileName(uriObj.LocalPath));
                await blockBlob.DeleteAsync();
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<string> UploadFile(IFormFile myFile) {
            var absoluteUri = string.Empty;
            if (myFile == null || myFile.Length == 0)
                return null;
            try {
                var blockBlob = _blobContainer.GetBlockBlobReference(Path.GetFileName(myFile.FileName));
                blockBlob.Properties.ContentType = myFile.ContentType;  //Indicar tipo de contenido que albergará el blob (imágen, fichero..)
                await blockBlob.UploadFromStreamAsync(myFile.OpenReadStream());
                absoluteUri = blockBlob.Uri.AbsoluteUri;
            } catch (Exception ex) {
                throw ex;
            }
            return absoluteUri;
        }
    }
}
