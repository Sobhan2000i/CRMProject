using CRMProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMProject.DataBase.Configurations
{
    public class ExpertConfiguration : IEntityTypeConfiguration<ExpertNote>
    {
        public void Configure(EntityTypeBuilder<ExpertNote> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Customer>() 
                   .WithMany(x => x.ExpertNotes!)
                   .HasForeignKey(x => x.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
