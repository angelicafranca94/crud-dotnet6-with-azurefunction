using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AzureFunctions
{
    public static class DeletePerson
    {
        [FunctionName("DeletePerson")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)]
            HttpRequest req,
            [Queue("DeletePerson", Connection = "AzureWebJobsStorage")]
            IAsyncCollector<string> queueItem,
            ILogger log)
        {
            log.LogInformation("DeletPerson function started a request.");

            await queueItem.AddAsync(
                JsonSerializer.Serialize(
            new Person
            {
                PartitionKey = req.Query["key"],
                RowKey = req.Query["id"]
            }));

            log.LogInformation("DeletePerson function finished a request.");

            return new OkObjectResult($"Obrigado! Seu registro já esta sendo processado.");
        }
    }
}
