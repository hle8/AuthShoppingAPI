﻿// <auto-generated />
using System;
using AuthShopping.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthShopping.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240223235345_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("AuthShopping.Models.CategoryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AuthShopping.Models.ProductModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ShoppingCartModelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ShoppingCartModelId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AuthShopping.Models.ShoppingCartModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("User")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("AuthShopping.Models.ProductModel", b =>
                {
                    b.HasOne("AuthShopping.Models.CategoryModel", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("AuthShopping.Models.ShoppingCartModel", null)
                        .WithMany("Products")
                        .HasForeignKey("ShoppingCartModelId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("AuthShopping.Models.ShoppingCartModel", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
