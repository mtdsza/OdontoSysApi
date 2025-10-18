var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();
builder.Services.AddRouting();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapOpenApi();
app.UseOpenApi();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.MapControllers();

app.Run();
