using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using palota_func_countries_assessment.Models;


namespace palota_func_countries_assessment.Functions
{
    public static class CountryService
    {
        [FunctionName("CountryService")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get","post", Route = "countries")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed to get list of countries");
            
            try
            {
                List<CountryResponse> countriesList = await Logic.Logic.GetCountriesAsync();
                return new OkObjectResult(countriesList);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
           

            
        }
    }
}
