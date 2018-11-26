using Assecor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assecor.Services.CsvHelper
{
    public interface ICsvService
    {
        /// <summary>
        /// Read Csv File data and returns a list of persons
        /// </summary>
        /// <param name="location">file path location</param>
        /// <returns>List<Person></returns>
        List<Person> GetCsvData(string location);

        /// <summary>
        /// Delete an element from Csv File
        /// </summary>
        /// <param name="id">element id</param>
        /// <param name="location">file path location</param>
        void DeleteElementToCsvFile(int id, string location);

        /// <summary>
        /// Insert a new element to the CsvFile
        /// </summary>
        /// <param name="person">Person object</param>
        /// <param name="location">file path location</param>
        void InsertElementToCsvFile(Person person, string location);

        /// <summary>
        /// Update element from csv file
        /// </summary>
        /// <param name="person">Person Object that with be modified</param>
        /// <param name="location">file path loaction</param>
        void UpdateElementToCsvFile(Person person, string location);

        /// <summary>
        /// Read the file line by line and try to format it correctly
        /// </summary>
        /// <param name="location"></param>
        void VerifyAndFormatCsvFile(string location);
    }
}
