using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Azure.Data.Tables;

namespace AzureFunctions
{
    public static class ListPeople
    {
        [FunctionName("ListPeople")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] 
        HttpRequest req,
        [Table("Person")] TableClient tableClient,
        ILogger log)
        {
            log.LogInformation("ListPeople function started a request.");

            var table =  tableClient.QueryAsync<Person>();

            log.LogInformation("ListPeople function finished a request.");

            return new OkObjectResult(table);
        }
    }
}
