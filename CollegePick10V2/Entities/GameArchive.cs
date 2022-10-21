﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities
{
    public class GameArchive
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GameID { get; set; }
        public int FavoriteID { get; set; }
        public int UnderDogID { get; set; }
        public int HomeTeam { get; set; }
        [Column(TypeName = "decimal(3, 1)")]
        public Double PointSpread { get; set; }
        [Column(TypeName = "decimal(3, 1)")]
        public Double OverUnder { get; set; }
        public DateTime GameTime { get; set; }
        public int FavoriteScore { get; set; }
        public int UnderdogScore { get; set; }
        public int? WeekNumber { get; set; }
    }
}
