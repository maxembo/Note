using NoteWeb.Core.Abstractions;
using NoteWeb.Core.Services;
using NoteWeb.Persistence;
using NoteWeb.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Configure the database
builder.Services.AddScoped<NotesDbContext>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddScoped<INotesRepository, NotesRepository>();
builder.Services.AddScoped<INotesService, NotesService>();


var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var context = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
await context.Database.EnsureCreatedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.MapControllers();

app.Run();