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
using palota_func_countries_assessment.Models;

namespace palota_func_countries_assessment.Functions
{
    public static class ContinentCountries
    {
        [FunctionName("ContinentCountries")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "continents/{continentName}/countries/")] HttpRequest req,string continentName,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed to get countries in a continent");

            
            try
            {
                string continent = continentName;
                List<CountryResponse> countriesList = await Logic.Logic.getContinentCountries(continent);
                return new OkObjectResult(countriesList);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            
        }
    }
}
