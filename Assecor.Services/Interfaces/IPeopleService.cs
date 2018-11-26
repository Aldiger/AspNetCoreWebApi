using Assecor.Services.Dtos;
using System.Collections.Generic;

namespace Assecor.Services.Interfaces
{
    public interface IPersonsService
    {
        /// <summary>
        /// Returns all persons rows
        /// </summary>
        /// <returns>List<PersonDto></returns>
        List<PersonDto> GetPersons();

        /// <summary>
        /// Filter persons using color
        /// </summary>
        /// <param name="color">Person color</param>
        /// <returns></returns>
        List<PersonDto> GetPersonsByColor(string color);

        /// <summary>
        /// Return person that match with the parameter id
        /// </summary>
        /// <param name="id">Person Object Id</param>
        /// <returns>PersonDto object</returns>
        PersonDto GetPerson(int id);

        /// <summary>
        /// Adds a new elemet
        /// </summary>
        /// <param name="person"></param>
        void AddPerson(PersonDto person);

        /// <summary>
        /// update element
        /// </summary>
        /// <param name="person"></param>
        void UpdatePerson(PersonDto person);

        /// <summary>
        /// Delete element
        /// </summary>
        /// <param name="id"></param>
        void DeletePerson(int id);
    }
}
