using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NbrbAPI.Models
{
    public class Currency : DefaultContractResolver
    {
        public static readonly Currency Instance =
         new Currency();

        [JsonProperty("Cur_Code")]
        public string Cur_Code { get; set; }
        [JsonProperty("Cur_ID")]
        public int Cur_ID { get; set; }
        [JsonProperty("Cur_Abbreviation")]
        public string Cur_Abbreviation { get; set; }
        [JsonProperty("Cur_Name")]
        public string Cur_Name { get; set; }
        [JsonProperty("Cur_Name_Eng")]
        public string Cur_Name_Eng { get; set; }
        [JsonProperty("Cur_Periodicity")]
        public int Cur_Periodicity { get; set; }
        public string Cur_PeriodicityString { get; set; }
        [JsonProperty("Cur_DateStart")]
        public DateTime Cur_DateStart { get; set; }
        [JsonProperty("Cur_DateEnd")]
        public DateTime Cur_DateEnd { get; set; }



    }
}