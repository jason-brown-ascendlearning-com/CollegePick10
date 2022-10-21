using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Models
{
    public class OddsFeedResponse
    {
        public List<OddsFeed> events { get; set; }
    }

    public class OddsFeed
    {
        public string id { get; set; }
        public string sport_key { get; set; }
        public DateTime commence_time { get; set; }
        public string home_team { get; set; }
        public string away_team { get; set; }
        public List<Bookmaker> bookmakers { get; set; }
    }

    public class Bookmaker
    {
        public string key { get; set; }
        public string title { get; set; }
        public DateTime last_update { get; set; }
        public List<Market> markets { get; set; }
    }

    public class Market
    {
        public string key { get; set; }
        public List<Outcome> outcomes { get; set; }
    }

    public class Outcome
    {
        public string name { get; set; }
        public int price { get; set; }
        public double point { get; set; }
    }
}
