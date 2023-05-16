using Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var corsPolicyName = "_corsPolicy-Browser-Game";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:3000");
    });
});

var dbConfiguration = builder.Configuration.GetSection("DatabaseSettings");

builder.Services.Configure<DatabaseSettings>(dbConfiguration);
builder.Services.AddDbContext<BrowserGameContext>(options =>
{
    options.UseNpgsql($"Host={dbConfiguration["Server"]};UserId={dbConfiguration["UserId"]};Password={dbConfiguration["Password"]};Database={dbConfiguration["Database"]}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
