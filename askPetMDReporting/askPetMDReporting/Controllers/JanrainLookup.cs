using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using askPetMDReporting.Models;

namespace askPetMDReporting.Controllers
{
    public class JanrainLookup
    {
        public static Report UserCount()
        {
            var today = DateTime.Today.ToString("MM/dd/yyyy");
            var yesterday = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy");
            var client = new RestClient("https://petmd.us.janraincapture.com/entity.count?client_id=b6ggz2hnm8w7eg6pgpwqbs7apvqehdmr&client_secret=e8u4fwqdkfqcgyw3cbhg37dbqzm6axx4");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("type_name", "user");
            request.AddParameter("filter", "created>='" + yesterday + "' and created<'" + today + "'");
            //request.AddParameter("filter", "created>'2016-05-04'");
            //request.AddUrlSegment("id", 123);
            //request.AddHeader("header", "value");
            var response = client.Execute(request);
            var report = JsonConvert.DeserializeObject<Report>(response.Content);
            var count = report.Count.ToString();
            var csv = new StringBuilder();
            var filePath = @"C: \Users\ddickinson\Documents\Visual Studio 2015\Projects\askPetMDReporting\csv\csv.txt";

            // if (!File.Exists(filePath))
            // {
            //     string newText = string.Format("Date,Accounts Created,Pet Accounts Created");
            //     File.WriteAllText(filePath, newText, Encoding.UTF8);
            // }

            var newLine = string.Format("Accounts Created: " + count);
            File.WriteAllText(filePath, newLine.ToString());

            return report;
        }

        public static Report PetCount()
        {
            var client = new RestClient("https://petmd.us.janraincapture.com/entity.count?client_id=b6ggz2hnm8w7eg6pgpwqbs7apvqehdmr&client_secret=e8u4fwqdkfqcgyw3cbhg37dbqzm6axx4");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("type_name", "user");
            request.AddParameter("filter", "created>'2016-05-04' and Pets.Name!='null'");
            //request.AddHeader("header", "value");
            var response = client.Execute(request);
            var report = JsonConvert.DeserializeObject<Report>(response.Content);
            var count = report.Count.ToString();

            return report;
        }

    }
}
