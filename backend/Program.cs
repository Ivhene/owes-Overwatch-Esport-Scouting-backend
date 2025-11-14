using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Supabase;
using backend.Repositories.Interfaces;
using backend.Repositories.Implementations;
using backend.Services.Interfaces;
using backend.Services.Implementations;
using System.Linq;    
using System.Text;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Supabase setup (unchanged)...
string supabaseUrl = builder.Configuration["Supabase:Url"]!;
string supabaseKey = builder.Configuration["Supabase:Key"]!;
var supabaseOptions = new SupabaseOptions
{
    AutoConnectRealtime = true,
    AutoRefreshToken = true
};
var supabase = new Client(supabaseUrl, supabaseKey, supabaseOptions);
await supabase.InitializeAsync();

builder.Services.AddSingleton(supabase);

builder.Services.AddSingleton(supabase);

builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        // use your snake_case policy
        opts.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy();
        // ignore any default-valued properties (null, 0, false, etc.)
        opts.JsonSerializerOptions.DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingDefault;
        // skip all read-only properties (so PrimaryKey & TableName never get serialized)
        opts.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
        // still serialize enums as strings, respecting the [EnumMember(Value=...)] tags
        opts.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<TeamRepository>();
builder.Services.AddScoped<TeamService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


/// <summary>
/// A simple snake_case converter for System.Text.Json.
/// </summary>
public class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        // early-exit
        if (string.IsNullOrEmpty(name))
            return name;

        // each branch returns a string now
        return string.Concat(name.Select((ch, i) =>
            i > 0 && char.IsUpper(ch)
                ? "_" + char.ToLowerInvariant(ch)            
                : char.ToLowerInvariant(ch).ToString()       
        ));
    }
}

