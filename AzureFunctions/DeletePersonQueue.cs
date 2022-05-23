using System;
using System.Text.Json;
using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureFunctions
{
    public class DeletePersonQueue
    {
        [FunctionName("DeletePersonQueue")]
        public static void Run(
            [QueueTrigger("DeletePerson", Connection = "AzureWebJobsStorage")] 
            string queueItem,
            [Table("Person")] TableClient tableClient,
            ILogger log)
        {
            log.LogInformation($"DeletePersonQueue trigger function started.");

            var data = JsonSerializer.Deserialize<Person>(queueItem);

            tableClient.DeleteEntity(data?.PartitionKey, data?.RowKey);

            log.LogInformation($"DeletePersonQueue trigger function finished.");
        }
    }
}
