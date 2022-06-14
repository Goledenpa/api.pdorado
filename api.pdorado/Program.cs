using api.pdorado.Configuration;
using api.pdorado.Data;
using System.Net;
using System.Net.Sockets;
using System.Text.Json.Serialization;

string localIP = LocalIPAddress();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



Sesion.Instance.PublicKey = builder.Configuration.GetValue<string>("PublicKey");
Sesion.Instance.ConnectionString = builder.Configuration.GetConnectionString("ComicsDBConnection");

builder.Services.AddDbContext<DataContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.Urls.Add("https://" + localIP + ":7069");

app.UseAuthorization();

app.MapControllers();

app.Run();


static string LocalIPAddress()
{
    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
    {
        socket.Connect("8.8.8.8", 65530);
        IPEndPoint? endPoint = socket.LocalEndPoint as IPEndPoint;
        if (endPoint != null)
        {
            Console.WriteLine(endPoint.Address.ToString());
            return endPoint.Address.ToString();
        }
        else
        {
            return "127.0.0.1";
        }
    }
}