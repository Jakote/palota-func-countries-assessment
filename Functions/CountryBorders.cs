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
    public static class CountryBorders
    {
        [FunctionName("CountryBorders")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "countries/{iso3Code}/borders")] HttpRequest req, string iso3code,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
   
            try
            {
                Country country = await Logic.Logic.getCountryByIsoAsync(iso3code);
                List<Country> countrylist = new List<Country>();

                foreach (string cty in country.borders)
                {
                    Country bordercouintry = await Logic.Logic.getCountryByIsoAsync(cty);
                    countrylist.Add(bordercouintry);
                }
                return new ObjectResult(countrylist);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            
        }
    }
}
