using palota_func_countries_assessment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using palota_func_countries_assessment.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Palota.Assessment.Countries.Models;

namespace palota_func_countries_assessment.Logic
{
    public class Logic
    {
        public static string Baseurl = "https://restcountries.com/v2/";
        public static List<CountryResponse> countryresponseList = new List<CountryResponse>();
        public static async Task<List<CountryResponse>> GetCountriesAsync() {
            string path = "all";
            countryresponseList = await getClientResponseAsync(path);
            
            return countryresponseList;
        }

        public static async Task<Country> getCountryByIsoAsync(string isocode) {            
            Country country = new Country();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("alpha/"+isocode+"");
                
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    country = JsonConvert.DeserializeObject<Country>(EmpResponse);
                    return country;
                }

            }
            return country;
        }
        
        public static async Task<List<CountryResponse>> getContinentCountries(string continent) {

            string path = "continent/" + continent + "";
            countryresponseList = await getClientResponseAsync(path);

            return countryresponseList;

        }

        public static async Task<List<CountryResponse>> getClientResponseAsync(string path) 
        {
            List<CountryResponse> countryresponseList = new List<CountryResponse>();
            List<Country> countryInfo = new List<Country>();
            using (var client = new HttpClient())
            {
               
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync(path);

                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    countryInfo = JsonConvert.DeserializeObject<List<Country>>(Response);

                    foreach (var item in countryInfo)
                    {
                        CountryResponse countryresponse = new CountryResponse();
                        Location loc = new Location();

                        countryresponse.name = item.name;
                        countryresponse.iso3Code = item.name;
                        countryresponse.capital = item.capital;
                        countryresponse.subregion = item.subregion;
                        countryresponse.region = item.region;
                        countryresponse.population = item.population;

                        if (item.latlng != null)
                        {
                            loc.lattitude = item.latlng[0];
                            loc.longitude = item.latlng[1];
                        }

                        countryresponse.location = loc;
                        countryresponse.demonym = item.demonym;
                        countryresponse.nativeName = item.nativeName;
                        countryresponse.numericCode = item.numericCode;
                        countryresponse.flag = item.flag;

                        countryresponseList.Add(countryresponse);

                    }

                }
                return countryresponseList;
            }
        }
    }
}
