using Microsoft.OpenApi.Models;
using Supabase;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Read Supabase URL and Key from configuration
String supabaseUrl = builder.Configuration["Supabase:Url"]!;
String supabaseKey = builder.Configuration["Supabase:Key"]!;
SupabaseOptions supabaseOptions = new SupabaseOptions
{
    AutoConnectRealtime = true,
    AutoRefreshToken = true
};

// Create & initialize the Supabase client 
Client supabase = new Supabase.Client(supabaseUrl, supabaseKey, supabaseOptions);
await supabase.InitializeAsync(); 

builder.Services.AddSingleton(supabase);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Swagger configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
