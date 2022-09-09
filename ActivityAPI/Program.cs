using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidAudience = "www.gauss.com",
            ValidIssuer = "www.gauss.com",
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("2t82u5b2t82u5b2t82u5b2t82u5b2t82u5b"))
        };
    });

var app = builder.Build();

app.UseAuthentication();

app.MapControllers();

app.UseAuthorization();

app.Run();
