using Microsoft.EntityFrameworkCore;
using System;

namespace IdentityServer.Data
{
    public class DbInitializer
    {
        public static void Initialize(AuthDbContext context, IServiceProvider service)
        {
            context.Database.Migrate();
        }
    }
}