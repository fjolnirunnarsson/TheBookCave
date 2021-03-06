﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TheBookCave.Data;

namespace TheBookCave.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180508144426_DeleteAccounts")]
    partial class DeleteAccounts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheBookCave.Data.EntityModels.Account", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BillingAddressCity");

                    b.Property<string>("BillingAddressCountry");

                    b.Property<string>("BillingAddressHouseNumber");

                    b.Property<string>("BillingAddressLine2");

                    b.Property<string>("BillingAddressStreet");

                    b.Property<string>("BillingAddressZipCode");

                    b.Property<string>("DeliveryAddressCity");

                    b.Property<string>("DeliveryAddressCountry");

                    b.Property<string>("DeliveryAddressHouseNumber");

                    b.Property<string>("DeliveryAddressLine2");

                    b.Property<string>("DeliveryAddressStreet");

                    b.Property<string>("DeliveryAddressZipCode");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Email");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int>("AuthorId");

                    b.Property<int>("BoughtCopies");

                    b.Property<string>("Description");

                    b.Property<int>("Discount");

                    b.Property<string>("Genre");

                    b.Property<string>("Image");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<double>("Rating");

                    b.Property<string>("Title");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.Cart", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<string>("CartId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("Quantity");

                    b.HasKey("RecordId");

                    b.HasIndex("BookId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<string>("Comment");

                    b.Property<int>("Rating");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("TheBookCave.Data.EntityModels.Cart", b =>
                {
                    b.HasOne("TheBookCave.Data.EntityModels.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
