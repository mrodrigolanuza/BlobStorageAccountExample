using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlobStorageAccountExample.Settings
{
    public class BlobStorageSettings
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public BlobStorageSettings(string connectionString, string containerName) {
            if (!string.IsNullOrEmpty(connectionString) && !string.IsNullOrEmpty(containerName)) {
                ConnectionString = connectionString;
                ContainerName = containerName;
            }
        }
    }
}
