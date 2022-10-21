using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Configuration
{
    public class PickTypeConfiguration : IEntityTypeConfiguration<PickType>
    {
        public void Configure(EntityTypeBuilder<PickType> builder)
        {
            builder.HasData(
                new PickType
                {
                    PickTypeID = 1,
                    PickTypeName = "Favorite"
                },
                new PickType
                {
                    PickTypeID = 2,
                    PickTypeName = "Underdog"
                },
                new PickType
                {
                    PickTypeID = 3,
                    PickTypeName = "Over"
                },
                new PickType
                {
                    PickTypeID = 4,
                    PickTypeName = "Under"
                },
                new PickType
                {
                    PickTypeID = 5,
                    PickTypeName = "SpreadPush"
                },
                new PickType
                {
                    PickTypeID = 1,
                    PickTypeName = "OverunderPush"
                }
            );;
        }
    }
}
