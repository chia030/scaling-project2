using LoadBalancer.LoadBalancer;
using LoadBalancer.Strategies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore;
using Prometheus;

var  MyAllowAnyOrigins = "_MyAllowAnyOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAnyOrigins,
                      policy  =>
                      {
                          policy.AllowAnyOrigin();
                      });
});

builder.Services.AddSingleton<ILoadBalancerStrategy, RoundRobinStrategy>();
builder.Services.AddSingleton<ILoadBalancer, LoadBalancer.LoadBalancer.LoadBalancer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(options => options.AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseCors(MyAllowAnyOrigins);
app.UseAuthorization();

app.MapControllers();


app.Run();
