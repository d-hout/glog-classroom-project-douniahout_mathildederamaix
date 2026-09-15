using Scalar.AspNetCore;

// Builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers(); 
builder.WebHost.UseUrls("http://localhost:7044");
builder.Services.AddDbContext<DataContext>();

//Repositories
builder.Services.AddScoped<IAdherentsRepository, AdherentsRepository>();
builder.Services.AddScoped<IAuteursRepository, AuteursRepository>();
builder.Services.AddScoped<IOeuvresRepository, OeuvresRepository>();
builder.Services.AddScoped<IEmpruntsRepository, EmpruntsRepository>();
builder.Services.AddScoped<IReservationsRepository, ReservationsRepository>();
builder.Services.AddScoped<IExemplairesRepository, ExemplairesRepository>();

// Service
builder.Services.AddScoped<AdherentsService>();
builder.Services.AddScoped<AuteursService>();
builder.Services.AddScoped<EmpruntsService>();
builder.Services.AddScoped<ExemplairesService>();
builder.Services.AddScoped<OeuvresService>();
builder.Services.AddScoped<ReservationsService>();

// Application
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    SeedData.Initialize();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
