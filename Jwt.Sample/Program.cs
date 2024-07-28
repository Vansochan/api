using Jwt.Sample;
using Jwt.Sample.Domain.Database;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurationServices(builder.Configuration);

builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var run=app.Services.GetRequiredService<IDatabaseInitializer>();
await run.InitializeAsync();

app.Run();

