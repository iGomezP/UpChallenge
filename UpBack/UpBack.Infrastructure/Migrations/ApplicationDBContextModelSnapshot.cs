﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UpBack.Infrastructure;

#nullable disable

namespace UpBack.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UpBack.Domain.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("customer_id");

                    b.Property<string>("MovReference")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("mov_reference");

                    b.Property<string>("ObjectStatus")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("object_status");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_accounts");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_accounts_customer_id");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("UpBack.Domain.Customers.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateOnly>("BirthDay")
                        .HasColumnType("date")
                        .HasColumnName("birth_day");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("ObjectStatus")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("object_status");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RoleId");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_customers");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("UpBack.Domain.Permissions.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("ObjectStatus")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("object_status");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_id");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_permissions");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_permissions_role_id");

                    b.ToTable("Permissions", (string)null);
                });

            modelBuilder.Entity("UpBack.Domain.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("ObjectStatus")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("object_status");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("UpBack.Domain.Transactions.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("account_id");

                    b.Property<string>("MovReference")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("mov_reference");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("transaction_date");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_transactions");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("ix_transactions_account_id");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("UpBack.Domain.Accounts.Account", b =>
                {
                    b.HasOne("UpBack.Domain.Customers.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_accounts_customers_customer_id");

                    b.OwnsOne("UpBack.Domain.ObjectValues.AccountBalance", "Balance", b1 =>
                        {
                            b1.Property<Guid>("AccountId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18, 2)")
                                .HasColumnName("Balance");

                            b1.HasKey("AccountId");

                            b1.ToTable("Accounts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId")
                                .HasConstraintName("fk_accounts_accounts_id");
                        });

                    b.OwnsOne("UpBack.Domain.ObjectValues.AccountNumber", "Number", b1 =>
                        {
                            b1.Property<Guid>("AccountId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("AccountNumber");

                            b1.HasKey("AccountId");

                            b1.HasIndex("Value")
                                .IsUnique()
                                .HasDatabaseName("ix_accounts_account_number");

                            b1.ToTable("Accounts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId")
                                .HasConstraintName("fk_accounts_accounts_id");
                        });

                    b.Navigation("Balance")
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Number")
                        .IsRequired();
                });

            modelBuilder.Entity("UpBack.Domain.Customers.Customer", b =>
                {
                    b.OwnsOne("UpBack.Domain.ObjectValues.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Country");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("State");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Street");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)")
                                .HasColumnName("ZipCode");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId")
                                .HasConstraintName("fk_customers_customers_id");
                        });

                    b.OwnsOne("UpBack.Domain.ObjectValues.CustomerEmail", "Email", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Email");

                            b1.HasKey("CustomerId");

                            b1.HasIndex("Value")
                                .IsUnique()
                                .HasDatabaseName("ix_customers_email");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId")
                                .HasConstraintName("fk_customers_customers_id");
                        });

                    b.OwnsOne("UpBack.Domain.ObjectValues.LastName", "LastName", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("LastName");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId")
                                .HasConstraintName("fk_customers_customers_id");
                        });

                    b.OwnsOne("UpBack.Domain.ObjectValues.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId")
                                .HasConstraintName("fk_customers_customers_id");
                        });

                    b.OwnsOne("UpBack.Domain.ObjectValues.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("HashedPassword")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasColumnName("Password");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId")
                                .HasConstraintName("fk_customers_customers_id");
                        });

                    b.OwnsOne("UpBack.Domain.ObjectValues.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("nvarchar(15)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId")
                                .HasConstraintName("fk_customers_customers_id");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("LastName")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("UpBack.Domain.Permissions.Permission", b =>
                {
                    b.HasOne("UpBack.Domain.Roles.Role", null)
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_permissions_role_role_id");

                    b.OwnsOne("UpBack.Domain.ObjectValues.Scope", "Scope", b1 =>
                        {
                            b1.Property<Guid>("PermissionId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Scope");

                            b1.HasKey("PermissionId");

                            b1.ToTable("Permissions");

                            b1.WithOwner()
                                .HasForeignKey("PermissionId")
                                .HasConstraintName("fk_permissions_permissions_id");
                        });

                    b.OwnsOne("UpBack.Domain.ObjectValues.Title", "Title", b1 =>
                        {
                            b1.Property<Guid>("PermissionId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Title");

                            b1.HasKey("PermissionId");

                            b1.ToTable("Permissions");

                            b1.WithOwner()
                                .HasForeignKey("PermissionId")
                                .HasConstraintName("fk_permissions_permissions_id");
                        });

                    b.Navigation("Scope")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("UpBack.Domain.Roles.Role", b =>
                {
                    b.OwnsOne("UpBack.Domain.ObjectValues.Title", "Title", b1 =>
                        {
                            b1.Property<Guid>("RoleId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Title");

                            b1.HasKey("RoleId");

                            b1.ToTable("Roles");

                            b1.WithOwner()
                                .HasForeignKey("RoleId")
                                .HasConstraintName("fk_roles_roles_id");
                        });

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("UpBack.Domain.Transactions.Transaction", b =>
                {
                    b.HasOne("UpBack.Domain.Accounts.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_transactions_account_account_id");

                    b.OwnsOne("UpBack.Domain.ObjectValues.TransactionQuantity", "Quantity", b1 =>
                        {
                            b1.Property<Guid>("TransactionId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18, 2)")
                                .HasColumnName("Quantity");

                            b1.HasKey("TransactionId");

                            b1.ToTable("Transactions");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId")
                                .HasConstraintName("fk_transactions_transactions_id");
                        });

                    b.OwnsOne("UpBack.Domain.ObjectValues.TransactionType", "Type", b1 =>
                        {
                            b1.Property<Guid>("TransactionId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)")
                                .HasColumnName("TransactionType");

                            b1.HasKey("TransactionId");

                            b1.ToTable("Transactions");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId")
                                .HasConstraintName("fk_transactions_transactions_id");
                        });

                    b.Navigation("Account");

                    b.Navigation("Quantity")
                        .IsRequired();

                    b.Navigation("Type")
                        .IsRequired();
                });

            modelBuilder.Entity("UpBack.Domain.Roles.Role", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
