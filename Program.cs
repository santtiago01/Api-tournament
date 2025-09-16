using Microsoft.EntityFrameworkCore;
using TournamentApi.Data;
using TournamentApi.Public.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Configuración JWT
var key = config["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key not configured");
var issuer = config["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer not configured");
var audience = config["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience not configured");
var expireMinutes = int.TryParse(config["Jwt:ExpireMinutes"], out var minutes) ? minutes : 120;

// Servicios
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

// DbContext
builder.Services.AddDbContext<AdminDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.MapHub<PublicHub>("/hubs/public");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Endpoint Login
app.MapPost("/api/auth/login", async (LoginRequest req, AdminDbContext db) =>
{
    // Buscar usuario en DB
    var user = await db.users
        .FirstOrDefaultAsync(u => u.Username == req.Username && u.Password == req.Password);

    if (user is null)
        return Results.BadRequest(new { message = "Usuario o contraseña incorrectos" });

    var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
    var keyBytes = Encoding.UTF8.GetBytes(key);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim("sub", user.Id.ToString()),
            new Claim("username", user.Username),
            new Claim("role", user.Role)
        }),
        Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
        Issuer = issuer,
        Audience = audience,
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    var jwt = tokenHandler.WriteToken(token);

    return Results.Ok(new
    {
        token = jwt,
        expiresIn = expireMinutes,
        role = user.Role
    });
});

app.Run();

record LoginRequest(string Username, string Password);
