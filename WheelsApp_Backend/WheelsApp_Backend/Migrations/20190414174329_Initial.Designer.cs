﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WheelsApp_Backend.Models;

namespace WheelsApp_Backend.Migrations
{
    [DbContext(typeof(WheelsContext))]
    [Migration("20190414174329_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WheelsApp_Backend.Models.Address", b =>
                {
                    b.Property<long>("Address_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Building_name");

                    b.Property<string>("City");

                    b.Property<long?>("ClientUser_Id");

                    b.Property<string>("Country");

                    b.Property<string>("Postal_code");

                    b.Property<string>("Street");

                    b.HasKey("Address_Id");

                    b.HasIndex("ClientUser_Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("WheelsApp_Backend.Models.NextOfKin", b =>
                {
                    b.Property<long>("OfKin_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ClientUser_Id");

                    b.Property<string>("Telephone");

                    b.Property<string>("Work_telephone");

                    b.HasKey("OfKin_ID");

                    b.HasIndex("ClientUser_Id");

                    b.ToTable("NextOfs");
                });

            modelBuilder.Entity("WheelsApp_Backend.Models.User", b =>
                {
                    b.Property<long>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Account_status");

                    b.Property<string>("Date_created");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("First_Name");

                    b.Property<long>("Id_number");

                    b.Property<string>("Last_Name");

                    b.Property<string>("Password");

                    b.Property<int?>("Role");

                    b.Property<string>("Sex");

                    b.Property<string>("Telephone");

                    b.Property<string>("Telephone_2");

                    b.Property<string>("Username");

                    b.HasKey("User_Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("WheelsApp_Backend.Models.Client", b =>
                {
                    b.HasBaseType("WheelsApp_Backend.Models.User");

                    b.Property<string>("Avatar");

                    b.Property<int>("Token");

                    b.Property<string>("Work_telephone");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("WheelsApp_Backend.Models.Address", b =>
                {
                    b.HasOne("WheelsApp_Backend.Models.Client")
                        .WithMany("Addresses")
                        .HasForeignKey("ClientUser_Id");
                });

            modelBuilder.Entity("WheelsApp_Backend.Models.NextOfKin", b =>
                {
                    b.HasOne("WheelsApp_Backend.Models.Client")
                        .WithMany("OfKins")
                        .HasForeignKey("ClientUser_Id");
                });
#pragma warning restore 612, 618
        }
    }
}
