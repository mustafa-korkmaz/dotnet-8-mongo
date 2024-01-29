using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Presentation.Middlewares;
using Presentation;
using Microsoft.OpenApi.Models;
using Infrastructure.Persistence.MongoDb;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var defaultCorsPolicy = "default_cors_policy";

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        //prevent automatic 400-404 response
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
    });

builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Type = SecuritySchemeType.Http
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddConfigSections(builder.Configuration);

builder.Services.AddMappings();

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration["MongoDbConfig:ConnectionString"])
);

builder.Services.AddScoped<IMongoContext, MongoContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddApplicationServices();

builder.Services.AddCors(config =>
{
    var policy = new CorsPolicy();
    policy.Headers.Add("*");
    policy.Methods.Add("*");
    policy.Origins.Add("*");
    config.AddPolicy(defaultCorsPolicy, policy);
});

builder.Services.ConfigureJwtAuthentication();
builder.Services.ConfigureJwtAuthorization();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

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


