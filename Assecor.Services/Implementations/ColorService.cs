using Assecor.Data.Core.IRepositories;
using Assecor.Data.Entities;
using Assecor.Services.Dtos;
using Assecor.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assecor.Services.Implementations
{

    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }
        public List<ColorDto> GetColors()
        {
            return _mapper.Map<List<ColorDto>>(_colorRepository.GetAll().ToList());
        }
    }
}
