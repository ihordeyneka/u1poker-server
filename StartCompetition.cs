using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure;
using Azure.Data.Tables;
using U1Poker.Schema;

namespace U1Poker.StartCompetition
{
    public static class StartCompetition
    {
        [FunctionName("StartCompetition")]
        [return: Table("Competition", Connection = "AzureWebJobsStorage")]
        public static Competition Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest request,
            ILogger log)
        {
            var competition = request.Query.ContainsKey("Competition") ? request.Query["Competition"].ToString() :
                $"Competition - {DateTime.Today.ToShortDateString()}";
            var accessCode = GenerateAccessCode();

            return new Competition { PartitionKey = Guid.NewGuid().ToString(), RowKey = competition, AccessCode = accessCode, RegistrationActive = true };
        }

        private static string GenerateAccessCode()
        {
            string result = "";
            var random = new Random();

            for (int i = 0; i < 4; i++)
            {
                result += random.Next(10).ToString();
            }

            return result;
        }
    }
}
