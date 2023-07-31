using api.BLL.ClientBLL;
using api.BLL.DocumentBLL;
using api.BLL.EmailBLL;
using api.DAL.ClientDAL;
using api.Data;
using api.Models;
using api.Repositories.ClientRepository;
using api.Repositories.DocumentRepository;
using api.Repositories.EmailRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// cors
services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .Build());
});

// ioc
services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "Test"));

services.AddScoped<DataSeeder>();
services.AddScoped<IClientRepository, ClientRepository>();
services.AddScoped<IEmailRepository, EmailRepository>();
services.AddScoped<IDocumentRepository, DocumentRepository>();

services.AddScoped<IClientBLL, ClientBLL>();
services.AddScoped<IEmailBLL, EmailBLL>();
services.AddScoped<IDocumentBLL, DocumentBLL>();
services.AddScoped<IClientDAL, ClientDAL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Rody Dulfo 07/28/2023 - Add maps for search, create and update
app.MapGet("/Clients/GetClients", async (IClientRepository clientRepository) 
=> await clientRepository.GetClients());

app.MapGet("/Clients/SearchClient", async (IClientRepository clientRepository, string searchString)
=> await clientRepository.SearchClients(searchString));

app.MapPost("/Clients/CreateClient", async (IClientRepository clientRepository, Client client)
=> await clientRepository.CreateClient(client));

app.MapPut("/Clients/UpdateClient/{ID}", async (IClientRepository clientRepository, string ID, Client client)
=> await clientRepository.UpdateClient(ID, client));

app.UseCors();

// seed data
using (var scope = app.Services.CreateScope())
{
    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();

    dataSeeder.Seed();
}

// run app
app.Run();