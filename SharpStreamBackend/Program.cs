var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
       .AddEnvironmentVariables();

builder.Services.AddCors(options =>
                         {
                             options.AddDefaultPolicy(policy =>
                                                      {
                                                          policy.AllowAnyHeader()
                                                                .AllowAnyMethod()
                                                                .SetIsOriginAllowed(_ => true)
                                                                .AllowCredentials();
                                                      });
                         });
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
                               {
                                   c.SwaggerDoc("v1", new() { Title = "SharpStream Signaling API", Version = "v1" });
                               });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SharpStream API v1"));
}

app.UseCors();

app.MapGet("/", () => "SharpStream Signaling Server is running!");

app.Run();