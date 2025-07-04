﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

public partial class TrackerDbContext : DbContext
{
    public TrackerDbContext()
    {
    }

    public TrackerDbContext(DbContextOptions<TrackerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<InventoryTransaction> InventoryTransactions { get; set; }

    public virtual DbSet<JournalEntriesArchive> JournalEntriesArchives { get; set; }

    public virtual DbSet<JournalEntry> JournalEntries { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ReorderAlert> ReorderAlerts { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShopProduct> ShopProducts { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CartItems_1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.ReceiptNumber).HasMaxLength(500);
            entity.Property(e => e.ReversalDate).HasColumnType("datetime");
            entity.Property(e => e.ReversedBy).HasMaxLength(50);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BCEF5D177");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            entity.Property(e => e.DeleteApprover).HasMaxLength(50);
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.CountryName).HasMaxLength(50);
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasIndex(e => e.CountryId, "IX_Districts_CountryId");

            entity.Property(e => e.DistrictName).HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.Districts)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Districts_Districts");
        });

        modelBuilder.Entity<InventoryTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Inventor__55433A4B7619278F");

            entity.HasIndex(e => e.ShopProductId, "IX_InventoryTransactions_ShopProductID");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.BarCode).HasMaxLength(200);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            entity.Property(e => e.DeleteApprover).HasMaxLength(50);
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.OrderPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.ReceivingShop).HasMaxLength(50);
            entity.Property(e => e.RetailPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SendingShop).HasMaxLength(50);
            entity.Property(e => e.ShopProductId).HasColumnName("ShopProductID");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionType).HasMaxLength(50);
            entity.Property(e => e.UnitPriceOfPreviousStock).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.ShopProduct).WithMany(p => p.InventoryTransactions)
                .HasForeignKey(d => d.ShopProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__Produ__59FA5E80");
        });

        modelBuilder.Entity<JournalEntriesArchive>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("JournalEntriesArchive");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AmountReceivedFromCustomer).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CashBack).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChequeNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.DrCr)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.PayMode).HasMaxLength(50);
            entity.Property(e => e.ProcessDateTime).HasColumnType("datetime");
            entity.Property(e => e.ProcessedStatus)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ReceiptNo).HasMaxLength(50);
            entity.Property(e => e.Rev)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Revreq)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<JournalEntry>(entity =>
        {
            entity.HasKey(e => e.JournalEntryTransId).HasName("PK_ReceiptNo");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AmountReceivedFromCustomer).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CashBack).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChequeNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.DrCr)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.PayMode).HasMaxLength(50);
            entity.Property(e => e.ProcessDateTime).HasColumnType("datetime");
            entity.Property(e => e.ProcessedStatus)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ReceiptNo).HasMaxLength(50);
            entity.Property(e => e.Rev)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Revreq)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF4A276831");

            entity.HasIndex(e => e.SupplierId, "IX_Orders_SupplierID");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            entity.Property(e => e.DeletedApprover).HasMaxLength(50);
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Orders__Supplier__5165187F");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30C7A122BFF");

            entity.HasIndex(e => e.OrderId, "IX_OrderDetails_OrderID");

            entity.HasIndex(e => e.ProductId, "IX_OrderDetails_ProductID");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            entity.Property(e => e.DeleteApprover).HasMaxLength(50);
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__5441852A");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderDeta__Produ__5535A963");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED4D9A0596");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            entity.Property(e => e.DeletedApprover).HasMaxLength(50);
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ReorderAlert>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            entity.Property(e => e.DeletedApprover).HasMaxLength(50);
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ShopManagerContact).HasMaxLength(50);
            entity.Property(e => e.ShopManagerName).HasMaxLength(50);
            entity.Property(e => e.ShopName).HasMaxLength(50);
        });

        modelBuilder.Entity<ShopProduct>(entity =>
        {
            entity.HasKey(e => e.ShopProductId).HasName("PK__ShopProducts__B40CC6ED4D9A0596");

            entity.Property(e => e.ShopProductId).HasColumnName("ShopProductID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            entity.Property(e => e.DeletedApprover).HasMaxLength(50);
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.LastOrderDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE66694FC62D27A");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            entity.Property(e => e.DeletedApprover).HasMaxLength(50);
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("PK__Transact__20266D0B7AFB765E");

            entity.Property(e => e.TransactionTypeId).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            entity.Property(e => e.DeletedApprover).HasMaxLength(50);
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionTypeName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.RoleDescription).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
