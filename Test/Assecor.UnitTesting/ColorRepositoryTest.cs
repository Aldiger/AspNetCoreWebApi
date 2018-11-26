using Assecor.Data;
using Assecor.Data.Core.IRepositories;
using Assecor.Data.Entities;
using Assecor.Data.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Assecor.UnitTesting
{
    public class ColorRepositoryTest
    {
        [Fact]
        public void Add_Color()
        {
            IColorRepository sut = GetInMemoryColorRepository();
            Color color = new Color()
            {
                Name = "Black"
            };

            Color savedColor = sut.AddWithReturn(color);

            Assert.Equal(1, sut.GetAll().Count());
            Assert.Equal("Black", savedColor.Name);
        }

        private IColorRepository GetInMemoryColorRepository()
        {
            DbContextOptions<ApplicationDBContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDBContext>();
            builder.UseInMemoryDatabase();
            options = builder.Options;
            ApplicationDBContext appDataContext = new ApplicationDBContext(options);
            appDataContext.Database.EnsureDeleted();
            appDataContext.Database.EnsureCreated();
            return new ColorRepository(appDataContext);
        }
    }
}
