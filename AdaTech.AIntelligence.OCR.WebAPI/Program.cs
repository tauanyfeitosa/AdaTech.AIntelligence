using AdaTech.AIntelligence.OCR.Services.ChatGPT;
using AdaTech.AIntelligence.OCR.Services.Image;
using AdaTech.AIntelligence.OCR.WebAPI.ConvertService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ScriptGPTService, ScriptGPTService>();
builder.Services.AddScoped<InputService, InputService>();
builder.Services.AddScoped<GPTResponseService, GPTResponseService>();
builder.Services.AddScoped<ImageConvertService, ImageConvertService>();

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
