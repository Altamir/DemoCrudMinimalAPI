using DemoCrud.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCrud.Data.EntitysConfigurations;

public class AlunoCF : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("aluno");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Matricula)
            .IsUnicode()
            .HasMaxLength(30)
            .IsRequired();
        builder.Property(a => a.Nome)
            .HasMaxLength(180)
            .IsRequired();
        builder.Property(a => a.Nascimento).IsRequired();
    }
}

