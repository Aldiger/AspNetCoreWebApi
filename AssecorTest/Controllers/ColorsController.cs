using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assecor.Data;
using Assecor.Data.Entities;
using Assecor.Services.Interfaces;

namespace Assecor.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        // GET: api/Color
        [HttpGet]
        public IActionResult GetColors()
        {
            return Ok(_colorService.GetColors());
        }
    }
}