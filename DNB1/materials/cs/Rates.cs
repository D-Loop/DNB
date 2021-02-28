using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NbrbAPI.Models
{
    public class Rate : DefaultContractResolver
    {
        public static readonly Rate Instance =
         new Rate();

        [JsonProperty("Cur_ID")]
        public int Cur_ID { get; set; }
        [JsonProperty("Date")]
        public DateTime Date { get; set; }
        [JsonProperty("Cur_Abbreviation")]
        public string Cur_Abbreviation { get; set; }
        [JsonProperty("Cur_Scale")]
        public int Cur_Scale { get; set; }
        [JsonProperty("Cur_Name")]
        public string Cur_Name { get; set; }
        [JsonProperty("Cur_OfficialRate")]
        public decimal? Cur_OfficialRate { get; set; }
    }


    public class RateShort : DefaultContractResolver
    {
        public static readonly RateShort  Instance =
         new RateShort();
        [JsonProperty("Cur_ID")]
        public int Cur_ID { get; set; }
        [JsonProperty("Date")]
        public System.DateTime Date { get; set; }
        [JsonProperty("Cur_OfficialRate")]
        public decimal? Cur_OfficialRate { get; set; }
    }

}