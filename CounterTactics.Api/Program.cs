using CounterTactics.Api.Api;
using CounterTactics.Api.Authentication;
using CounterTactics.Api.Authorization;
using CounterTactics.Api.Extensions;

using JasperFx.CodeGeneration;

using Marten;

using Serilog;

using Swashbuckle.AspNetCore.SwaggerGen;

using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);


// Add Serilog
builder.AddSerilog();

// Configure auth
builder.Services.AddAuthentication(builder.Configuration);

// Configure the database
builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection(builder.Configuration.GetConnectionString("Marten")!);
    options.UseDefaultSerialization(collectionStorage: CollectionStorage.AsArray);

    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
}).OptimizeArtifactWorkflow(TypeLoadMode.Static);

// State that represents the current user from the database *and* the request
builder.Services.AddCurrentUser();

// Configure Open API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);

// Configure rate limiting
builder.Services.AddRateLimiting();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", policyBuilder =>
{
    policyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

var app = builder.Build();


// Add Serilog requests logging
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseRateLimiter();
app.UseCors("MyPolicy");

// Configure the APIs
app.RegisterApiEndpoints();

app.Run();