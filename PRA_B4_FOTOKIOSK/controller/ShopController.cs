using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {

        public static Home Window { get; set; }

        public void Start()
        {
            // Stel de prijslijst in aan de rechter kant.
            ShopManager.SetShopPriceList("Prijzen:\n");

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct("Foto 10x15", 2.55m, "Foto afdruk 10x15 cm"));
            ShopManager.Products.Add(new KioskProduct("Foto 20x30", 4.95m, "Foto afdruk 20x30 cm"));
            ShopManager.Products.Add(new KioskProduct("Mok met foto", 9.95m, "Mok bedrukt met foto"));
            ShopManager.Products.Add(new KioskProduct("Sleutelhanger met foto", 6.12m, "Sleutelhanger met foto"));
            ShopManager.Products.Add(new KioskProduct("T-shirt met foto", 11.99m, "T-shirt bedrukt met foto"));


            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();


            GeneratePriceList();
        }

        public void GeneratePriceList()
        {
            ShopManager.SetShopPriceList("Prijzen:\n");

            foreach (KioskProduct product in ShopManager.Products)
            {
                string priceListItem = $"{product.Name}: €{product.Price} - {product.Description}\n";
                ShopManager.AddShopPriceList(priceListItem);
            }
        }

        // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
        public void AddButtonClick()
        {
            
        }

        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {

        }

        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
        }

    }
}
