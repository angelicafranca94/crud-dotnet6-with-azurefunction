using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace AzureFunctions
{
    public class CreatePersonQueue
    {
        [FunctionName("CreatePersonQueue")]
        public static void Run([QueueTrigger("CreatePerson", Connection = "AzureWebJobsStorage")]
            string queueItem,
            [Table("Person")] TableClient tableClient,
            ILogger log)
        {
            log.LogInformation($"CreatePersonQueue trigger function started.");

            var data = JsonSerializer.Deserialize<Person>(queueItem);

            data.PartitionKey = "Person";
            data.RowKey = Guid.NewGuid().ToString();


            tableClient.AddEntity(data);

            log.LogInformation($"CreatePersonQueue trigger function finished.");
        }
    }
}
