using LibraryAPI.Data;
using LibraryAPI.Data.Repositories;
using LibraryAPI.Middleware;
using LibraryAPI.Profiles;
using LibraryAPI.Services;
using LibraryAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Slugify;

var builder = WebApplication.CreateBuilder(args);

// Подключение EF Core
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper и SlugHelper
builder.Services.AddAutoMapper(
        cfg => cfg.AddMaps(
            typeof(BookMappingProfile).Assembly,
            typeof(AuthorMappingProfile).Assembly,
            typeof(GenreMappingProfile).Assembly
        )
    );
builder.Services.AddSingleton<SlugHelper>();

// Репозитории и UnitOfWork
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Сервисы
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IGenreService, GenreService>();

// Контроллеры + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS
app.UseHttpsRedirection();

// Поддержка статических файлов (для картинок)
app.UseStaticFiles();

// Авторизация (пока не используем, но пусть стоит)
app.UseAuthorization();

// Глобальная обработка ошибок
app.UseErrorHandling();

// Подключение контроллеров
app.MapControllers();

app.Run();
