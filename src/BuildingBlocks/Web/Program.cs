using Infrastructure;
using Web.Extensions;
using Web.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.MapMyEndpoints();
// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Abolfazl and hamed");
    options.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();



app.Run();

