using LibraryAPI.Data;
using LibraryAPI.Data.Repositories;
using LibraryAPI.Middleware;
using LibraryAPI.Profiles;
using LibraryAPI.Services;
using LibraryAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Slugify;

var builder = WebApplication.CreateBuilder(args);

// ����������� EF Core
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper � SlugHelper
builder.Services.AddAutoMapper(
        cfg => cfg.AddMaps(
            typeof(BookMappingProfile).Assembly,
            typeof(AuthorMappingProfile).Assembly,
            typeof(GenreMappingProfile).Assembly
        )
    );
builder.Services.AddSingleton<SlugHelper>();

// ����������� � UnitOfWork
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// �������
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IGenreService, GenreService>();

// ����������� + Swagger
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

// ��������� ����������� ������ (��� ��������)
app.UseStaticFiles();

// ����������� (���� �� ����������, �� ����� �����)
app.UseAuthorization();

// ���������� ��������� ������
app.UseErrorHandling();

// ����������� ������������
app.MapControllers();

app.Run();
