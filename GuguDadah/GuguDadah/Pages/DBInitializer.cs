using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GuguDadah.Data;

namespace GuguDadah.Pages {

    public static class DbInitializer {

        public static void Initialize(AppDbContext context) {

            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Clients.Any()) {
                return;   // DB has been seeded
            }

        }
    }
}