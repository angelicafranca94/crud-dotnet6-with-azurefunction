using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AzureFunctions
{
    public class EditPersonQueue
    {
        [FunctionName("EditPersonQueue")]
        public static void Run(
            [QueueTrigger("EditPerson", Connection = "AzureWebJobsStorage")] string queueItem,
            [Table("Person")] TableClient tableClient,
            ILogger log)
        {
            log.LogInformation($"EditPersonQueue trigger function started.");

            var data = JsonSerializer.Deserialize<Person>(queueItem);

            tableClient.UpsertEntity(data);

            log.LogInformation($"EditPersonQueue trigger function finished.");
        }
    }
}
