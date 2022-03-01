var builder = WebApplication.CreateBuilder(args);

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
