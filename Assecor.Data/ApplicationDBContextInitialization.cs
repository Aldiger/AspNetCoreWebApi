using Assecor.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assecor.Data
{

    public static class DbInitializer
    {
        private static ApplicationDBContext context;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            context = (ApplicationDBContext)serviceProvider.GetService(typeof(ApplicationDBContext));
            InitializeDbWithSomeData();
        }
        private static void InitializeDbWithSomeData()
        {
            #region Colors
            if (!context.Colors.Any())
            {

                context.Colors.AddRange(new Color[]
                {
                    new Color(){ Id=1, Name="Blue" },
                    new Color(){ Id=2, Name="Green" },
                    new Color(){ Id=3, Name="Purple" },
                    new Color(){ Id=4, Name="Red" },
                    new Color(){ Id=5, Name="Yellow" },
                    new Color(){ Id=6, Name="Turquois" },
                    new Color(){ Id=7, Name="White" }
                });

                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Colors ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Colors OFF");
                }
                finally
                {
                    context.Database.CloseConnection();
                }
            }
            #endregion
            #region Persons
            if (!context.Persons.Any())
            {

                context.Persons.AddRange(new Person[]
                {
                    new Person(){ Id=1,
                        Name ="Hans",
                        Lastname ="Müller",
                        Zipcode ="67742",
                        City ="Lauterecken",
                        ColorId =1 },
                    new Person(){ Id=2,
                        Name ="Peter",
                        Lastname ="Petersen",
                        Zipcode ="18439",
                        City ="Stralsund",
                        ColorId =2 },
                    new Person(){ Id=3,
                        Name ="Johnny",
                        Lastname ="Johnson",
                        Zipcode ="67742",
                        City ="made up too",
                        ColorId =3 },
                    new Person(){ Id=4,
                        Name ="Milly",
                        Lastname ="Millenium",
                        Zipcode ="77777",
                        City ="made up",
                        ColorId =4 },
                    new Person(){ Id=5,
                        Name ="Jonas",
                        Lastname ="Müller",
                        Zipcode ="32323",
                        City ="Hansstadt",
                        ColorId =5 },
                    new Person(){ Id=6,
                        Name ="Tastatur",
                        Lastname ="Fujitsu",
                        Zipcode ="42342",
                        City ="Japan",
                        ColorId =6 },
                    new Person(){ Id=7,
                        Name ="Anders",
                        Lastname ="Andersson",
                        Zipcode ="32132",
                        City ="Schweden - Bonus",
                        ColorId =2 },
                });

                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Persons ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Persons OFF");
                }
                finally
                {
                    context.Database.CloseConnection();
                }
            }
            #endregion
        }
    }
}
