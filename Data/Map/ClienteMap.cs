using DesafioWebApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioWebApi.Data.Map
{
    public class ClienteMap : IEntityTypeConfiguration<ClienteModel>
    {
        public void Configure(EntityTypeBuilder<ClienteModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Multiplicador).IsRequired();
        }
    }
}
