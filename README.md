# Library API

Простое CRUD-приложение для управления библиотекой (книги, авторы, жанры), реализованное на **ASP.NET Core Web API**. Построено с использованием принципов **Best Practices**: слоистая архитектура, DTO, AutoMapper, репозитории, сервисный слой и глобальная обработка ошибок.

---

## 🚀 Функционал
- Управление **книгами, авторами и жанрами** (CRUD)
- Поиск книг по названию, автору и жанру
- Генерация **slug** для SEO-friendly URL
- Загрузка и хранение **изображений** (обложки книг, фото авторов)
- **Глобальная обработка ошибок** через кастомный Middleware
- Документация API через **Swagger UI**

---

## 🛠 Технологии
- **ASP.NET Core 8**
- **Entity Framework Core**
- **SQL Server**
- **AutoMapper**
- **Slugify**
- **Swagger / OpenAPI**

---

## 🏗 Архитектура
- **Controllers** — REST API endpoints  
- **Services** — бизнес-логика приложения  
- **Repositories** — доступ к данным через EF Core  
- **DTOs** — передача данных между слоями  
- **Middleware** — глобальная обработка ошибок  

---

## 📦 Установка и запуск

```bash
# 1. Клонировать репозиторий
git clone https://github.com/KseniyaOz45/LibraryAPI.git

# 2. Перейти в папку проекта
cd LibraryAPI

# 3. Настроить строку подключения в appsettings.json
# "DefaultConnection": "Server=...;Database=LibraryDb;Trusted_Connection=True;"

# 4. Применить миграции
dotnet ef database update

# 5. Запустить проект
dotnet run
```

После запуска API будет доступен по адресу:  
`https://localhost:7222/swagger` (Swagger UI)

---

## 📚 API Endpoints

### Книги
- `GET /api/books` — список книг  
- `GET /api/books/{id}` — книга по ID  
- `GET /api/books/{slug}` — книга по slug  
- `POST /api/books` — создание книги  
- `PUT /api/books/{id}` — обновление книги  
- `DELETE /api/books/{id}` — удаление книги  
- `GET /api/books/search?query=string` — поиск книг  

### Авторы
- `GET /api/authors` — список авторов  
- `GET /api/authors/{id}` — автор по ID  
- `GET /api/authors/by-slug/{slug}` — автор по slug  
- `POST /api/authors` — создание автора  
- `PUT /api/authors/{id}` — обновление автора  
- `DELETE /api/authors/{id}` — удаление автора  
- `GET /api/authors/search?query=string` — поиск авторов  

### Жанры
- `GET /api/genres` — список жанров  
- `GET /api/genres/{id}` — жанр по ID  
- `GET /api/genres/by-slug/{slug}` — жанр по slug  
- `POST /api/genres` — создание жанра  
- `PUT /api/genres/{id}` — обновление жанра  
- `DELETE /api/genres/{id}` — удаление жанра  
- `GET /api/genres/search?name=string` — поиск жанров  

---

## 🔮 Будущие улучшения
- Авторизация и аутентификация через **JWT**
- **Роли** (администратор/пользователь)
- Пагинация и фильтрация списков
- Кэширование запросов
- Тесты (Unit и Integration)

---

## 👩‍💻 Автор
[**KseniyaOz45**](https://github.com/KseniyaOz45)  
Манжула Ксения, .NET Developer
