using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Models
{
    public class ScheduleModel : CurrentWeek
    {
        public string Favorite { get; set; }
        public string UnderDog { get; set; }
        public string FavoriteAbbreviation { get; set; }
        public string UnderdogAbbreviation { get; set; }
        public int? FavortieRank { get; set; }
        public int? UnderdogRank { get; set; }
        public string FavoriteCssClass { get; set; }
        public string UnderdogCssClass { get; set; }
        public string OverCssClass { get; set; }
        public string UnderCssClass { get; set; }
        public string SpreadPushCssClass { get; set; }
        public string OverUnderPushCssClass { get; set; }
        public int? SpreadPickType { get; set; }
        public int? OverUnderPickType { get; set; }
    }
}
