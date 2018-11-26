using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assecor.Services.Dtos
{
    public class PersonDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string City { get; set; }

        public string Color { get; set; }
    }
}
