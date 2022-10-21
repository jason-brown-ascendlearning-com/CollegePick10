using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Configuration
{
    public class CurrentWeekConfiguration : IEntityTypeConfiguration<CurrentWeek>
    {
        public void Configure(EntityTypeBuilder<CurrentWeek> builder)
        {
            builder.HasData(
                new CurrentWeek
                {
                   GameID = 1,
                   FavoriteID = 1,
                   UnderDogID = 2,
                   PointSpread = 21.5,
                   OverUnder = 45.5,
                   GameTime = DateTime.Now,
                   HomeTeam = 1,
                   Locked = false
                },
                new CurrentWeek
                {
                    GameID = 2,
                    FavoriteID = 2,
                    UnderDogID = 1,
                    PointSpread = 23,
                    OverUnder = 52,
                    GameTime = DateTime.Now,
                    HomeTeam = 2,
                    Locked = false
                }
            );
        }
    }
}
