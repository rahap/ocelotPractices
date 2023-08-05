using AuthenticationApi.JwtService;
using OcelotCore.Model;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var authenticationProviderKey = "MyOcelot";
// Add services to the container.

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
//sss
builder.Services.AddSingleton<IJWTManager>(new JwtTokenService());
var app = builder.Build();
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
