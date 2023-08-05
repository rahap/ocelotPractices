using OcelotCore.Model;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;
//https://stackoverflow.com/questions/71264496/api-gateway-ocelot-net-core-6-1-setup

var authenticationProviderKey = "MyOcelot";
var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("ocelot.json")
.Build();


// Add services to the container.
builder.Services.AddOcelot(configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = authenticationProviderKey;
    option.DefaultChallengeScheme = authenticationProviderKey;
}).AddJwtBearer(authenticationProviderKey, options =>
{
    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTModel.Key)),
        ValidateIssuer = true,
        ValidIssuer = JWTModel.Issuer,
        ValidateAudience = true,
        ValidAudience = JWTModel.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero, //Verilen s�rede token sonlan�r. e�er Zero olarak verilmez ise default da 5 dakikad�r. Jetonunuzun s�resinin tam zaman�nda bitmesini istiyorsan�z; ClockSkew'i a�a��daki gibi s�f�ra ayarlaman�z gerekir,
        RequireExpirationTime = true
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
 
}

app.UseHttpsRedirection();
app.UseOcelot().Wait(); // 404  hatalar� i�in
app.UseAuthorization();

app.MapControllers();

app.Run();
