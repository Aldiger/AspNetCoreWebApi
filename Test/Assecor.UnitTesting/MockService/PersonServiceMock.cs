using Assecor.Data.Core.IRepositories;
using Assecor.Data.Entities;
using Assecor.Services.CsvHelper;
using Assecor.Services.Dtos;
using Assecor.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Assecor.UnitTesting.Test
{
    public class PersonsServiceMock : IPersonsService
    {
        private readonly IMapper _mapper;
        private readonly IColorRepository _colorRep;
        private readonly ICsvService _csvService;

        private readonly string fileLocationPath = Path.Combine(Environment.CurrentDirectory, "Files", "DataExample.csv");
        //In memory Csv data
        public List<PersonDto> _persons { get; set; } = new List<PersonDto>();

        public PersonsServiceMock(IMapper mapper, IColorRepository colorRep, ICsvService csvService)
        {
            _mapper = mapper;
            _colorRep = colorRep;
            _csvService = csvService;
        }

        public List<PersonDto> GetPersons()
        {
            if (_persons?.Count == 0)
            {
                _persons = _mapper.Map<List<PersonDto>>(GetPersonsMapped());
            }
            return _persons;
        }

        public PersonDto GetPerson(int id)
        {
            if (_persons.Count == 0)
            {
                _persons = _mapper.Map<List<PersonDto>>(GetPersonsMapped());
            }
            return _persons.FirstOrDefault(x => x.Id == id);
        }

        public List<PersonDto> GetPersonsByColor(string color)
        {
            var colorId = _colorRep.FirstOrDefault(x => x.Name.ToLower() == color.ToLower())?.Id;
            if (colorId.HasValue)
            {
                if (_persons?.Count == 0)
                {
                    _persons = _mapper.Map<List<PersonDto>>(GetPersonsMapped());
                }
                return _mapper.Map<List<PersonDto>>(_persons.Where(x => x.Color.ToLower() == color.ToLower()));
            }
            return new List<PersonDto>(); ;
        }

        public void AddPerson(PersonDto person)
        {
            //Control if color exist
            var colorId = 0;
            if (!String.IsNullOrEmpty(person.Color))
            {
                var exitColor = _colorRep.FirstOrDefault(x => x.Name.ToLower() == person.Color.ToLower());
                if (exitColor != null)
                {
                    colorId = exitColor.Id;
                }
                else
                {
                    var newColor = _colorRep.AddWithReturn(_mapper.Map<Color>(new ColorDto { Name = person.Color }));
                    colorId = newColor.Id;
                }
            }
            //Write to Csv File

            _csvService.InsertElementToCsvFile(
                new Person
                {
                    City = person.City,
                    ColorId = colorId,
                    Lastname = person.Lastname,
                    Name = person.Name,
                    Zipcode = person.Zipcode
                }
                , fileLocationPath);
            _persons = _mapper.Map<List<PersonDto>>(GetPersonsMapped());
        }

        public void DeletePerson(int id)
        {
            _csvService.DeleteElementToCsvFile(id, fileLocationPath);
            _persons = _mapper.Map<List<PersonDto>>(GetPersonsMapped());
        }

        public void UpdatePerson(PersonDto person)
        {
            //Control if color exist
            var colorId = 0;
            if (!String.IsNullOrEmpty(person.Color))
            {
                var exitColor = _colorRep.FirstOrDefault(x => x.Name.ToLower() == person.Color.ToLower());
                if (exitColor != null)
                {
                    colorId = exitColor.Id;
                }
                else
                {
                    var newColor = _colorRep.AddWithReturn(_mapper.Map<Color>(new ColorDto { Name = person.Color }));
                    colorId = newColor.Id;
                }
            }
            //Write to Csv File

            _csvService.UpdateElementToCsvFile(
                new Person
                {
                    Id = person.Id ?? 0,
                    City = person.City,
                    ColorId = colorId,
                    Lastname = person.Lastname,
                    Name = person.Name,
                    Zipcode = person.Zipcode
                }
                , fileLocationPath);
            _persons = _mapper.Map<List<PersonDto>>(GetPersonsMapped());
        }


        #region Persons Service Private Methods

        private List<Person> GetPersonsMapped()
        {
            _csvService.VerifyAndFormatCsvFile(fileLocationPath);

            var data = _csvService.GetCsvData(fileLocationPath);

            var colors = _colorRep.GetAll().ToList();

            data.ForEach(x => x.Color = colors.FirstOrDefault(a => a.Id == x.ColorId));

            return data;
        }

        #endregion
    }
}
