using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using palota_func_countries_assessment.Models;

namespace palota_func_countries_assessment.Functions
{
    public static class CountryByIso
    {
        [FunctionName("CountryByIso")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "countries/{iso3Code}")] HttpRequest req, string iso3Code,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function to get country by ISOcode.");

            try
            {
                Country country = await Logic.Logic.getCountryByIsoAsync(iso3Code);
                return new OkObjectResult(country);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            

        }
    }
}
