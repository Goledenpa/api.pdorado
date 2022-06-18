using api.pdorado.Auth;
using api.pdorado.Configuration;
using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios;
using api.pdorado.Servicios.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using pdorado.data.Models;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;

string localIP = LocalIPAddress();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Comics API",
        Description = "Api para la base de datos de Comics"
    });

    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Autorización JWT en la cabecera usando el esquema JWT"
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, new List<string>()
        }
    });
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Key")))
    };
});

Sesion.Instance.PublicKey = builder.Configuration.GetValue<string>("PublicKey");
Sesion.Instance.ConnectionString = builder.Configuration.GetConnectionString("ComicsDBConnection");

builder.Services.AddDbContext<DataContext>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IDataService<AutorDTO, Autor>, AutorService>();
builder.Services.AddTransient<IDataService<ColeccionDTO, Coleccion>, ColeccionService>();
builder.Services.AddTransient<IDataService<ComicDTO, Comic>, ComicService>();
builder.Services.AddTransient<IDataService<EditorDTO, Editor>, EditorService>();

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Comic Api");
        c.RoutePrefix = String.Empty;
    });
}

app.UseMiddleware<JWTMiddleware>();

app.UseDeveloperExceptionPage();

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