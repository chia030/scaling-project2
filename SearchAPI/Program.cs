using Prometheus;
using RestSharp;


var restClient = new RestClient("http://load-balancer");
restClient.Post(
        new RestRequest(
            "configuration?url=http://" + Environment.MachineName, Method.Post)
        );
Console.WriteLine("Hostname: " + Environment.MachineName);

// var restClient = new RestClient("http://load-balancer");
// restClient.Post(
//     new RestRequest(
//         "configuration", Method.Post)
//     .AddJsonBody(new{
//         Url = "http://" + Environment.MachineName,
//     }));
// Console.WriteLine("Hostname: " + Environment.MachineName);
var  MyAllowAnyOrigins = "_MyAllowAnyOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAnyOrigins,
                      policy  =>
                      {
                          policy.AllowAnyOrigin();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpMetrics();


// app.UseHttpsRedirection();

//app.UseCors(config => config.AllowAnyOrigin());
app.UseCors(MyAllowAnyOrigins);
// app.UseAuthorization();

app.MapControllers();

app.UseEndpoints( endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapMetrics();

    // endpoints.MapGet("/", async context =>
    // {
    //     await context.Response.WriteAsync("Hello");
    // });
});

app.Run();
