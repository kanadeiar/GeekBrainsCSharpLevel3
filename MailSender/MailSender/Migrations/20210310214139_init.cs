using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailSender.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Senders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    UseSsl = table.Column<bool>(type: "bit", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Subject", "Text" },
                values: new object[,]
                {
                    { 1, "Заголовок1", "Текст сообщения 1" },
                    { 2, "Заголовок2", "Текст сообщения 2" },
                    { 3, "Заголовок3", "Текст сообщения 3" },
                    { 4, "Заголовок4", "Текст сообщения 4" },
                    { 5, "Заголовок5", "Текст сообщения 5" },
                    { 6, "Заголовок6", "Текст сообщения 6" },
                    { 7, "Заголовок7", "Текст сообщения 7" },
                    { 8, "Заголовок8", "Текст сообщения 8" },
                    { 9, "Заголовок9", "Текст сообщения 9" },
                    { 10, "Заголовок10", "Текст сообщения 10" }
                });

            migrationBuilder.InsertData(
                table: "Recipients",
                columns: new[] { "Id", "Address", "Description", "Name" },
                values: new object[,]
                {
                    { 10, "recipient10@server10.com", "Тестовый получатель10", "Получатель10" },
                    { 9, "recipient9@server9.com", "Тестовый получатель9", "Получатель9" },
                    { 8, "recipient8@server8.com", "Тестовый получатель8", "Получатель8" },
                    { 6, "recipient6@server6.com", "Тестовый получатель6", "Получатель6" },
                    { 7, "recipient7@server7.com", "Тестовый получатель7", "Получатель7" },
                    { 4, "recipient4@server4.com", "Тестовый получатель4", "Получатель4" },
                    { 3, "recipient3@server3.com", "Тестовый получатель3", "Получатель3" },
                    { 2, "recipient2@server2.com", "Тестовый получатель2", "Получатель2" },
                    { 1, "recipient1@server1.com", "Тестовый получатель1", "Получатель1" },
                    { 5, "recipient5@server5.com", "Тестовый получатель5", "Получатель5" }
                });

            migrationBuilder.InsertData(
                table: "Senders",
                columns: new[] { "Id", "Address", "Description", "Name" },
                values: new object[,]
                {
                    { 10, "sender10@server10.com", "Тестовый отправитель10", "Отправитель10" },
                    { 9, "sender9@server9.com", "Тестовый отправитель9", "Отправитель9" },
                    { 8, "sender8@server8.com", "Тестовый отправитель8", "Отправитель8" },
                    { 7, "sender7@server7.com", "Тестовый отправитель7", "Отправитель7" },
                    { 6, "sender6@server6.com", "Тестовый отправитель6", "Отправитель6" },
                    { 5, "sender5@server5.com", "Тестовый отправитель5", "Отправитель5" },
                    { 3, "sender3@server3.com", "Тестовый отправитель3", "Отправитель3" },
                    { 2, "sender2@server2.com", "Тестовый отправитель2", "Отправитель2" },
                    { 1, "sender1@server1.com", "Тестовый отправитель1", "Отправитель1" },
                    { 4, "sender4@server4.com", "Тестовый отправитель4", "Отправитель4" }
                });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "Address", "Description", "Login", "Name", "Password", "Port", "UseSsl" },
                values: new object[,]
                {
                    { 3, "smtp.server3.com", "Тестовый сервер 3", "Login3", "Сервер-3", "vCWHXdunbi7VWn2qiJ/YJQ==", 25, true },
                    { 1, "smtp.server1.com", "Тестовый сервер 1", "Login1", "Сервер-1", "2sRzNen0UIjofQT7zHYx1A==", 25, true },
                    { 2, "smtp.server2.com", "Тестовый сервер 2", "Login2", "Сервер-2", "oWvSSy99/xkgH3zScQINig==", 25, false },
                    { 4, "smtp.server4.com", "Тестовый сервер 4", "Login4", "Сервер-4", "RPk560IEUKYLNcSmY/uX3A==", 25, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "Senders");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}
