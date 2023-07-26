using Cms.Data.Repository.Repositories;
using Cms.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// This used to be under Startup.cs > Configuration > ConfigureServices in the tutorial but the future is now
// See https://learn.microsoft.com/en-us/aspnet/core/fundamentals/startup?view=aspnetcore-7.0 for more info
builder.Services.AddSingleton<ICmsRepository, InMemoryCmsRepository>();
builder.Services.AddAutoMapper(typeof(CmsMapper));
builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);

    // we can change this name if we want, such as changing to v
    // setupAction.ApiVersionReader = new QueryStringApiVersionReader("api-version"); 

    // ../v2/courses
    // setupAction.ApiVersionReader = new UrlSegmentApiVersionReader();

    // header
    // setupAction.ApiVersionReader = new HeaderApiVersionReader("X-Version");

    // this can be problematic if the url and header have different versions!
    // in practice, url versioning is common
    setupAction.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("v"),
        new HeaderApiVersionReader("X-Version")
    );
});

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
