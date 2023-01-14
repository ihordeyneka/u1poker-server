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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] dynamic input,
            ILogger log)
        {
            var competition = input.Competition;
            var accessCode = GenerateAccessCode();

            return new Competition { Name = competition, AccessCode = accessCode, RegistrationActive = true };
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
