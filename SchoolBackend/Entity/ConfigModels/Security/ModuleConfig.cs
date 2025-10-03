﻿using Entity.ConfigModels.global;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.ConfigModels.Security
{
    public class ModuleConfig : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("module" , schema: "security");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .IsRequired();
            builder.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Order)
                .HasColumnName("order")
                .IsRequired();
            builder.Property(p => p.Path)
                .HasColumnName("path")
                .HasColumnType("text");

            builder.Property(p => p.Icon)
                .HasColumnName("icon")
                .HasColumnType("text");

            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .IsRequired();

            builder.MapBaseModel();

            builder.HasData(
                 new Module
                 {
                     Id = 1,
                     Name = "Inicio",
                     Description = "Pantalla principal del sistema",
                     Path = "/dashboard",
                     Icon = "home", // Usa el nombre del icono que maneje tu UI
                     Order = 1,
                     Status = 1
                 },
                 new Module
                 {
                     Id = 2,
                     Name = "Administración",
                     Description = "Opciones administrativas del sistema",
                     Path = "",
                     Icon = "person",
                     Order = 2,
                     Status = 1
                 },
                 new Module
                 {
                     Id = 3,
                     Name = "Académico",
                     Description = "Módulo para gestión académica",
                     Path = "",
                     Icon = "school",
                     Order = 3,
                     Status = 1
                 },
                 new Module
                 {
                     Id = 4,
                     Name = "Agenda",
                     Description = "Gestión de eventos y agendas",
                     Path = "",
                     Icon = "book",
                     Order = 4,
                     Status = 1
                 },
                 new Module
                 {
                     Id = 5,
                     Name = "Configuración",
                     Description = "Parámetros y ajustes del sistema",
                     Path = "",
                     Icon = "settings",
                     Order = 5,
                     Status = 1
                 },
                 new Module
                 {
                     Id = 6,
                     Name = "Seguridad",
                     Description = "Todo el tema de permisos del sistema",
                     Path = "",
                     Icon = "calendar",
                     Order = 6,
                     Status = 1
                 }
             );

        }
    }
}
