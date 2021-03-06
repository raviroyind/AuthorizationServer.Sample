﻿// <auto-generated />
using AuthorizationServer.Core.MyCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AuthorizationServer.Data.Migrations.My
{
    [DbContext(typeof(MyContext))]
    [Migration("20180213132820_InitialMyUserXClient")]
    partial class InitialMyUserXClient
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AuthorizationServer.Core.Domain.UserXClient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("UserXClient");
                });
#pragma warning restore 612, 618
        }
    }
}
