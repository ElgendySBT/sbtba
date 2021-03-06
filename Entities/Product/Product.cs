using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Entities.Product
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        public SubProduct subProduct { get; set; }
    }
}
