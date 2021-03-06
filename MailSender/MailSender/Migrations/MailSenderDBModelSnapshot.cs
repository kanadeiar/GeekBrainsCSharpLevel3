﻿// <auto-generated />
using System;
using MailSender.Data.Stores.InDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MailSender.Migrations
{
    [DbContext(typeof(MailSenderDb))]
    partial class MailSenderDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MailSender.lib.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Subject = "Заголовок1",
                            Text = "Текст сообщения 1"
                        },
                        new
                        {
                            Id = 2,
                            Subject = "Заголовок2",
                            Text = "Текст сообщения 2"
                        },
                        new
                        {
                            Id = 3,
                            Subject = "Заголовок3",
                            Text = "Текст сообщения 3"
                        },
                        new
                        {
                            Id = 4,
                            Subject = "Заголовок4",
                            Text = "Текст сообщения 4"
                        },
                        new
                        {
                            Id = 5,
                            Subject = "Заголовок5",
                            Text = "Текст сообщения 5"
                        },
                        new
                        {
                            Id = 6,
                            Subject = "Заголовок6",
                            Text = "Текст сообщения 6"
                        },
                        new
                        {
                            Id = 7,
                            Subject = "Заголовок7",
                            Text = "Текст сообщения 7"
                        },
                        new
                        {
                            Id = 8,
                            Subject = "Заголовок8",
                            Text = "Текст сообщения 8"
                        },
                        new
                        {
                            Id = 9,
                            Subject = "Заголовок9",
                            Text = "Текст сообщения 9"
                        },
                        new
                        {
                            Id = 10,
                            Subject = "Заголовок10",
                            Text = "Текст сообщения 10"
                        });
                });

            modelBuilder.Entity("MailSender.lib.Models.Recipient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("SchedulerId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("SchedulerId");

                    b.ToTable("Recipients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "recipient1@server1.com",
                            Description = "Тестовый получатель1",
                            Name = "Получатель1"
                        },
                        new
                        {
                            Id = 2,
                            Address = "recipient2@server2.com",
                            Description = "Тестовый получатель2",
                            Name = "Получатель2"
                        },
                        new
                        {
                            Id = 3,
                            Address = "recipient3@server3.com",
                            Description = "Тестовый получатель3",
                            Name = "Получатель3"
                        },
                        new
                        {
                            Id = 4,
                            Address = "recipient4@server4.com",
                            Description = "Тестовый получатель4",
                            Name = "Получатель4"
                        },
                        new
                        {
                            Id = 5,
                            Address = "recipient5@server5.com",
                            Description = "Тестовый получатель5",
                            Name = "Получатель5"
                        },
                        new
                        {
                            Id = 6,
                            Address = "recipient6@server6.com",
                            Description = "Тестовый получатель6",
                            Name = "Получатель6"
                        },
                        new
                        {
                            Id = 7,
                            Address = "recipient7@server7.com",
                            Description = "Тестовый получатель7",
                            Name = "Получатель7"
                        },
                        new
                        {
                            Id = 8,
                            Address = "recipient8@server8.com",
                            Description = "Тестовый получатель8",
                            Name = "Получатель8"
                        },
                        new
                        {
                            Id = 9,
                            Address = "recipient9@server9.com",
                            Description = "Тестовый получатель9",
                            Name = "Получатель9"
                        },
                        new
                        {
                            Id = 10,
                            Address = "recipient10@server10.com",
                            Description = "Тестовый получатель10",
                            Name = "Получатель10"
                        });
                });

            modelBuilder.Entity("MailSender.lib.Models.Scheduler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeSend")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MessageId")
                        .HasColumnType("int");

                    b.Property<int?>("SenderId")
                        .HasColumnType("int");

                    b.Property<int?>("ServerId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("SenderId");

                    b.HasIndex("ServerId");

                    b.ToTable("Scheduler");
                });

            modelBuilder.Entity("MailSender.lib.Models.Sender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Senders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "sender1@server1.com",
                            Description = "Тестовый отправитель1",
                            Name = "Отправитель1"
                        },
                        new
                        {
                            Id = 2,
                            Address = "sender2@server2.com",
                            Description = "Тестовый отправитель2",
                            Name = "Отправитель2"
                        },
                        new
                        {
                            Id = 3,
                            Address = "sender3@server3.com",
                            Description = "Тестовый отправитель3",
                            Name = "Отправитель3"
                        },
                        new
                        {
                            Id = 4,
                            Address = "sender4@server4.com",
                            Description = "Тестовый отправитель4",
                            Name = "Отправитель4"
                        },
                        new
                        {
                            Id = 5,
                            Address = "sender5@server5.com",
                            Description = "Тестовый отправитель5",
                            Name = "Отправитель5"
                        },
                        new
                        {
                            Id = 6,
                            Address = "sender6@server6.com",
                            Description = "Тестовый отправитель6",
                            Name = "Отправитель6"
                        },
                        new
                        {
                            Id = 7,
                            Address = "sender7@server7.com",
                            Description = "Тестовый отправитель7",
                            Name = "Отправитель7"
                        },
                        new
                        {
                            Id = 8,
                            Address = "sender8@server8.com",
                            Description = "Тестовый отправитель8",
                            Name = "Отправитель8"
                        },
                        new
                        {
                            Id = 9,
                            Address = "sender9@server9.com",
                            Description = "Тестовый отправитель9",
                            Name = "Отправитель9"
                        },
                        new
                        {
                            Id = 10,
                            Address = "sender10@server10.com",
                            Description = "Тестовый отправитель10",
                            Name = "Отправитель10"
                        });
                });

            modelBuilder.Entity("MailSender.lib.Models.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<bool>("UseSsl")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Servers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "smtp.server1.com",
                            Description = "Тестовый сервер 1",
                            Login = "Login1",
                            Name = "Сервер-1",
                            Password = "2sRzNen0UIjofQT7zHYx1A==",
                            Port = 25,
                            UseSsl = true
                        },
                        new
                        {
                            Id = 2,
                            Address = "smtp.server2.com",
                            Description = "Тестовый сервер 2",
                            Login = "Login2",
                            Name = "Сервер-2",
                            Password = "oWvSSy99/xkgH3zScQINig==",
                            Port = 25,
                            UseSsl = false
                        },
                        new
                        {
                            Id = 3,
                            Address = "smtp.server3.com",
                            Description = "Тестовый сервер 3",
                            Login = "Login3",
                            Name = "Сервер-3",
                            Password = "vCWHXdunbi7VWn2qiJ/YJQ==",
                            Port = 25,
                            UseSsl = true
                        },
                        new
                        {
                            Id = 4,
                            Address = "smtp.server4.com",
                            Description = "Тестовый сервер 4",
                            Login = "Login4",
                            Name = "Сервер-4",
                            Password = "RPk560IEUKYLNcSmY/uX3A==",
                            Port = 25,
                            UseSsl = false
                        });
                });

            modelBuilder.Entity("MailSender.lib.Models.Recipient", b =>
                {
                    b.HasOne("MailSender.lib.Models.Scheduler", null)
                        .WithMany("Recipients")
                        .HasForeignKey("SchedulerId");
                });

            modelBuilder.Entity("MailSender.lib.Models.Scheduler", b =>
                {
                    b.HasOne("MailSender.lib.Models.Message", "Message")
                        .WithMany()
                        .HasForeignKey("MessageId");

                    b.HasOne("MailSender.lib.Models.Sender", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");

                    b.HasOne("MailSender.lib.Models.Server", "Server")
                        .WithMany()
                        .HasForeignKey("ServerId");

                    b.Navigation("Message");

                    b.Navigation("Sender");

                    b.Navigation("Server");
                });

            modelBuilder.Entity("MailSender.lib.Models.Scheduler", b =>
                {
                    b.Navigation("Recipients");
                });
#pragma warning restore 612, 618
        }
    }
}
