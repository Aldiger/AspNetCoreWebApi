using Assecor.Data;
using Assecor.Data.Core.IRepositories;
using Assecor.Data.Persistence.Repositories;
using Assecor.Services.CsvHelper;
using Assecor.Services.Implementations;
using Assecor.Services.Interfaces;
using Assecor.UnitTesting.Test;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Assecor.UnitTesting
{
    public class PersonControllerTest
    {
        public IPersonsService PersonService { get; set; }
        public PersonControllerTest()
        {
            CreateServices();
        }

        [Fact]
        public void GetPersonByColor()
        {
           var persons= PersonService.GetPersonsByColor("red");
           Assert.Single(persons);
        }
        [Fact]
        public void GetPersons()
        {
            var persons = PersonService.GetPersons();
            Assert.NotNull(persons);
        }

        private void CreateServices()
        {
            var services = new ServiceCollection();
            services.AddAutoMapper();
            services.AddDbContext<ApplicationDBContext>
    (options => options.UseSqlServer("Data Source=localhost\\SqlExpress;Initial Catalog=AssecorTestDB;UID=sa;PWD=Abc?123"));

            //Repositories Injection
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ICsvService, CsvService>();

            //Service Injection
            services.AddScoped<IPersonsService, PersonsServiceMock>();
            services.AddScoped<IColorService, ColorService>();

            var serviceProvider = services.BuildServiceProvider();
            PersonService = serviceProvider.GetService<IPersonsService>();
        }
    }
}
