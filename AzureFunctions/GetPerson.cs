using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Data.Tables;

namespace AzureFunctions
{
    public static class GetPerson
    {
        [FunctionName("GetPerson")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] 
            HttpRequest req,
            [Table("Person")] TableClient tableClient,
            ILogger log)
        {
            log.LogInformation("GetPerson function started a request.");

            var rowKey = req.Query["id"];
            var entity = tableClient.QueryAsync<TableEntity>(filter: $"RowKey eq '{rowKey}'");

            log.LogInformation("GetPerson function finished a request.");

            return new OkObjectResult(entity);
        }
    }
}
