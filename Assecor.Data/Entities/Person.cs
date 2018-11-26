using System;
using System.Collections.Generic;
using System.Text;

namespace Assecor.Data.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }

        public int? ColorId { get; set; }

        public Color Color { get; set; }
    }
}
