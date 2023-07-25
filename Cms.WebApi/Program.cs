using Cms.Data.Repository.Repositories;
using Cms.WebApi.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// This used to be under Startup.cs > Configuration > ConfigureServices in the tutorial but the future is now
// See https://learn.microsoft.com/en-us/aspnet/core/fundamentals/startup?view=aspnetcore-7.0 for more info
builder.Services.AddSingleton<ICmsRepository, InMemoryCmsRepository>();
builder.Services.AddAutoMapper(typeof(CmsMapper));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
