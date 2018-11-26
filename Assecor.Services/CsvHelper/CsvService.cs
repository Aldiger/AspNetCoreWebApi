using Assecor.Data.Entities;
using AutoMapper;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assecor.Services.CsvHelper
{
    public class CsvService : ICsvService
    {
        private readonly IMapper _mapper;

        public CsvService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<Person> GetCsvData(string location)
        {
            using (TextReader textReader = new StreamReader(location))
            {
                var csv = new CsvReader(textReader);
                csv.Configuration.HasHeaderRecord = false;
                var records = csv.GetRecords<dynamic>().ToList();
                var persons = new List<Person>();
                for (int i = 0; i < records.Count; i++)
                {
                    var record = (IDictionary<String, object>)records[i];
                    int colorId;
                    var success = Int32.TryParse(record["Field" + 4].ToString(), out colorId);
                    persons.Add(
                        new Person
                        {
                            Id = i + 1,
                            Lastname = record["Field" + 1].ToString(),
                            Name = record["Field" + 2].ToString(),
                            Zipcode = record["Field" + 3].ToString()?.TrimStart()?.TrimEnd()?.Split(" ")[0],
                            City = String.Join(' ', record["Field" + 3].ToString()?.TrimStart()?.TrimEnd()?.Split(" ")?.Skip(1)),
                            ColorId = success ? colorId : (int?)null
                        });
                }
                return persons;

            }
        }

        public void DeleteElementToCsvFile(int id, string location)
        {
            var csvdata = GetCsvData(location);
            var elementToDelete = csvdata.FirstOrDefault(x => x.Id == id);
            if (elementToDelete != null)
            {
                var updateElements = csvdata.Remove(elementToDelete);
                var updateCsvElements = _mapper.Map<List<CsvDataModel>>(updateElements);
                using (TextWriter textWriter = new StreamWriter(location))
                {
                    var csv = new CsvWriter(textWriter);
                    csv.Configuration.HasHeaderRecord = false;
                    csv.WriteRecords(updateCsvElements);
                }
            }
        }

        public void InsertElementToCsvFile(Person person, string location)
        {
            var existingValues = _mapper.Map<List<CsvDataModel>>(GetCsvData(location));
            using (TextWriter textWriter = new StreamWriter(location))
            {
                var csv = new CsvWriter(textWriter);
                csv.Configuration.HasHeaderRecord = false;
                existingValues.Add(_mapper.Map<CsvDataModel>(person));
                csv.WriteRecords(existingValues);
            }
        }

        public void UpdateElementToCsvFile(Person person, string location)
        {
            var csvdata = GetCsvData(location);
            var elementToUpdate = csvdata.FirstOrDefault(x => x.Id == person.Id);
            if (elementToUpdate != null)
            {
                for (int i = 0; i < csvdata.Count; i++)
                {
                    if (csvdata[i].Id == person.Id)
                    {
                        csvdata[i].Name = person.Name;
                        csvdata[i].Lastname = person.Lastname;
                        csvdata[i].Zipcode = person.Zipcode;
                        csvdata[i].City = person.City;
                        csvdata[i].ColorId = person.ColorId;
                    }
                }
                var updateCsvElements = _mapper.Map<List<CsvDataModel>>(csvdata);
                using (TextWriter textWriter = new StreamWriter(location))
                {
                    var csv = new CsvWriter(textWriter);
                    csv.Configuration.HasHeaderRecord = false;
                    csv.WriteRecords(updateCsvElements);
                }
            }
        }

        public void VerifyAndFormatCsvFile(string location)
        {
            var allLines = new List<string>();
            string line;
            using (StreamReader file = new StreamReader(location))
            {
                while ((line = file.ReadLine()) != null)
                {
                    allLines.Add(line);
                }
            }
            var modifiedLines = new List<string>();
            for (int i = 0; i < allLines.Count; i++)
            {
                var lineElements = allLines[i].Split(',');
                if (lineElements.Length != 4)
                {
                    if ((lineElements.Length - 1) % 3 == 0)
                    {
                        var newLineNumber = lineElements.Length / 3;
                        ///1 4, 2 7, 3 10
                        string newLine = "";
                        for (int j = 0; j < lineElements.Length-1; j++)
                        {
                            if (string.IsNullOrEmpty(newLine))
                            {
                                newLine = lineElements[j];
                            }
                            else
                            {
                                newLine += "," + lineElements[j];
                            }

                            if ((j + 1) % 3 == 0)
                            {
                                newLine += "," + lineElements[j + 1].TrimStart().TrimEnd().Split(" ")[0];
                                modifiedLines.Add(newLine);
                                if (lineElements[j + 1].TrimStart().TrimEnd().Split(" ").Length==2)
                                    lineElements[j + 1] = lineElements[j + 1]?.TrimStart()?.TrimEnd()?.Split(" ")[1];
                                newLine = "";
                            }
                        }
                    }
                    //just skip line, it wrong formated

                }
                else
                    modifiedLines.Add(allLines[i]);


            }
            if (!allLines.SequenceEqual(modifiedLines))
            {
                using (StreamWriter sw = new StreamWriter(location))
                {

                    foreach (string s in modifiedLines)
                    {
                        sw.WriteLine(s);
                    }
                }
            }

        }
    }
}
