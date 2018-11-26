using Assecor.Data.Entities;
using Assecor.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assecor.Services.Interfaces
{
    public interface IColorService
    {
        /// <summary>
        /// Returns list of Color Dto
        /// </summary>
        /// <returns>Color Dto</returns>
        List<ColorDto> GetColors();
    }
}
