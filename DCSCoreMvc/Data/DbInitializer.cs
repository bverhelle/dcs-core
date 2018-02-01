using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCSCoreMvc.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            //Verify if Db is seeded an do so if necessary.
        }

    }
}
