// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Marketplace.Domain.Models.Geography;

namespace Marketplace.Data.Mappings.Geography
{
    internal sealed class ProvinceMap : EntityTypeConfiguration<Province>
    {
        public ProvinceMap()
        {
            ToTable("Provinces");

            #region Key

            HasKey(i => i.Id);

            Property(i => i.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(i => i.Active)
                .HasColumnName("Active");

            #endregion

            #region Fields

            Property(i => i.Name)
                .HasColumnName("Name");

            Property(i => i.Abbreviation)
                .HasColumnName("Abbreviation");

            Property(i => i.CountryId)
                .HasColumnName("CountryId");

            #endregion

            #region Relationships

            HasRequired(i => i.Country)
                .WithMany(m => m.Provinces)
                .HasForeignKey(c => c.CountryId);

            #endregion
        }
    }
}
