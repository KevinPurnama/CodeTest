using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string corsLocalDevPolicy = "AllowLocalDev";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsLocalDevPolicy,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

// Add services to the container.
var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new Code_Test_API.PremiumCalculatorMappingProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<Code_Test.Domain.IPremiumCalcDomain, Code_Test.Domain.PremiumCalcDomain>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Code_Test.Domain.FactorDBContext>(opt => opt.UseInMemoryDatabase("FactorDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsLocalDevPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
