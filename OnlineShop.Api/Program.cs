var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string CorsOriginsName = "All";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsOriginsName,
                      builder =>
                      {
                          builder
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                      });
});

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

app.UseCors(CorsOriginsName);

app.UseAuthorization();

app.MapControllers();

app.Run();
