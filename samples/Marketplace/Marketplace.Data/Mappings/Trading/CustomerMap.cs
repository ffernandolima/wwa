// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Marketplace.Domain.Models.Trading;

namespace Marketplace.Data.Mappings.Trading
{
    internal sealed class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("Customers");

            #region Main

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

            Property(i => i.UserId)
                .HasColumnName("UserId");

            #endregion

            #region Relationships

            HasRequired(i => i.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            #endregion
        }
    }
}
