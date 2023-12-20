using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.EntityFramework;

public class EfDbInitializer
    : IDbInitializer
{
    private readonly DatabaseContext _databaseContext;

    public EfDbInitializer(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public void InitializeDb()
    {
        try
        {
            var deleted = _databaseContext.Database.EnsureDeleted();
            var exist = _databaseContext.Database.EnsureCreated();
            // Этот метод по сути создает базу по такой же логике
            // что и EnsureCreated, после создания применяет миграции

            // Если уже существует БД, но без миграций, то тут разово раскоментировать
            // строчку EnsureDeleted(). Т.е. нет истории миграций.

            _databaseContext.Database.Migrate();
            

            // После успешной миграции в таблице __EFMigrationsHistory добавится запись с названием миграции
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
}