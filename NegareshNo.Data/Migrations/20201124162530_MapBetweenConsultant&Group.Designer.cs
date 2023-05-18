﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NegareshNo.Data.Context;

namespace NegareshNo.Data.Migrations
{
    [DbContext(typeof(NegareshNoContext))]
    [Migration("20201124162530_MapBetweenConsultant&Group")]
    partial class MapBetweenConsultantGroup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NegareshNo.Data.Model.Access.Permmision", b =>
                {
                    b.Property<int>("PermmisionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParentId");

                    b.Property<string>("PermmisionTitle")
                        .IsRequired();

                    b.HasKey("PermmisionId");

                    b.ToTable("Permmisions");
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Access.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete");

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Access.Role_Consultant", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("ConsultantId");

                    b.HasKey("RoleId", "ConsultantId");

                    b.HasIndex("ConsultantId");

                    b.ToTable("Role_Consultants");
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Access.Role_Permmision", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("PermmisionId");

                    b.HasKey("RoleId", "PermmisionId");

                    b.HasIndex("PermmisionId");

                    b.ToTable("Role_Permmisions");
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Consulting.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConsultingId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsPublished");

                    b.Property<DateTime>("PublishedDate");

                    b.Property<string>("Summery")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("ArticleId");

                    b.HasIndex("ConsultingId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Consulting.Consultant", b =>
                {
                    b.Property<int>("ConsultantId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("CareerRecord")
                        .IsRequired();

                    b.Property<string>("CareerSummary")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("City");

                    b.Property<string>("ConsultantFullName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("EducationalRecord");

                    b.Property<string>("Email");

                    b.Property<string>("ImageName");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Job")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("LiceneRecord");

                    b.Property<string>("MobilePhone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Skills");

                    b.Property<bool>("Status");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Website");

                    b.Property<string>("WorkRecord");

                    b.HasKey("ConsultantId");

                    b.ToTable("Consultants");
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Consulting.Consultant_Group", b =>
                {
                    b.Property<int>("ConsultantId");

                    b.Property<int>("GroupId");

                    b.HasKey("ConsultantId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("Consultant_Groups");
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Consulting.ConsultingGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("ImageName");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Summery")
                        .HasMaxLength(150);

                    b.HasKey("GroupId");

                    b.ToTable("ConsultingGroups");
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Users.UserRequest", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConsultantId");

                    b.Property<string>("Description");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("GivenTime");

                    b.Property<int>("GroupId");

                    b.Property<bool>("HasTime");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDone");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<DateTime>("RegistrationConsultingTime");

                    b.Property<string>("TellPhoneNumber");

                    b.Property<string>("Title");

                    b.HasKey("RequestId");

                    b.HasIndex("ConsultantId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserRequests");
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Access.Role_Consultant", b =>
                {
                    b.HasOne("NegareshNo.Data.Model.Consulting.Consultant", "Consultant")
                        .WithMany("Role_Consultants")
                        .HasForeignKey("ConsultantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NegareshNo.Data.Model.Access.Role", "Role")
                        .WithMany("Role_Consultants")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Access.Role_Permmision", b =>
                {
                    b.HasOne("NegareshNo.Data.Model.Access.Permmision", "Permmision")
                        .WithMany("Role_Permmisions")
                        .HasForeignKey("PermmisionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NegareshNo.Data.Model.Access.Role", "Role")
                        .WithMany("Role_Permmisions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Consulting.Article", b =>
                {
                    b.HasOne("NegareshNo.Data.Model.Consulting.Consultant", "Consultant")
                        .WithMany("Articles")
                        .HasForeignKey("ConsultingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Consulting.Consultant_Group", b =>
                {
                    b.HasOne("NegareshNo.Data.Model.Consulting.Consultant", "Consultant")
                        .WithMany("Consultant_Groups")
                        .HasForeignKey("ConsultantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NegareshNo.Data.Model.Consulting.ConsultingGroup", "Group")
                        .WithMany("Consultant_Groups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NegareshNo.Data.Model.Users.UserRequest", b =>
                {
                    b.HasOne("NegareshNo.Data.Model.Consulting.Consultant", "Consultant")
                        .WithMany("UserRequests")
                        .HasForeignKey("ConsultantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NegareshNo.Data.Model.Consulting.ConsultingGroup", "ConsultingGroup")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
