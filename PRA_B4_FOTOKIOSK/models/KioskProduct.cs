using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.models
{
    public class KioskProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public KioskProduct(string name, decimal price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }
    }

    public class OrderedProduct
    {
        public string Fotonummer { get; set; }
        public string ProductNaam { get; set; }
        public float Aantal { get; set; }
        public decimal Totaalprijs { get; set; }

        public OrderedProduct(string Fotonummer, string ProductNaam, float Aantal, decimal Totaalprijs)
        {
            Fotonummer = Fotonummer;
            ProductNaam = ProductNaam;
            Aantal = Aantal;
            Totaalprijs = Totaalprijs;
        }
    }
}
