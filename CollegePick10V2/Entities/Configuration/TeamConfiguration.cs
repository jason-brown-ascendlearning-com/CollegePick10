using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Teams>
    {
        public void Configure(EntityTypeBuilder<Teams> builder)
        {
            builder.HasData(
                new Teams
                {
                    TeamID = 1,
                    FulleName="Abilene Christian Wildcats", 
                    EspnName="Abilene Christian",
                    Abbreviation="ABCHRST",
                    Rank=null                    
                },
                new Teams
                {
                    TeamID = 2,
                    FulleName = "Air Force Falcons",
                    EspnName = "Air Force",
                    Abbreviation = "AF",
                    Rank = null
                }
            );;
        }
    }
}
