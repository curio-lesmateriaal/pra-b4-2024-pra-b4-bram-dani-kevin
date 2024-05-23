using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
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
            ShopManager.Products.Add(new KioskProduct("Foto 20x30", 5.00m, "Foto afdruk 20x30 cm"));
            ShopManager.Products.Add(new KioskProduct("Mok met foto", 9.99m, "Mok bedrukt met foto"));
            ShopManager.Products.Add(new KioskProduct("Sleutelhanger met foto", 4.99m, "Sleutelhanger met foto"));
            ShopManager.Products.Add(new KioskProduct("T-shirt met foto", 14.99m, "T-shirt bedrukt met foto"));

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
            try
            {
                // Haal geselecteerde product en aantal op
                KioskProduct selectedProduct = ShopManager.GetSelectedProduct();
                int? amountNullable = ShopManager.GetAmount();

                // Controleer of er een product is geselecteerd
                if (selectedProduct != null && amountNullable.HasValue)
                {
                    int amount = amountNullable.Value;

                    // Bereken het totaalbedrag en toon het op de bon
                    decimal totalAmount = selectedProduct.Price * amount;
                    ShopManager.SetShopReceipt($"Totaalbedrag\n€{totalAmount}");
                }
                else
                {
                    // Product niet gevonden of geen hoeveelheid ingevoerd
                    ShopManager.SetShopReceipt("Product of hoeveelheid niet ingevoerd");
                }
            }
            catch (Exception ex)
            {
                // Handel andere fouten af
                ShopManager.SetShopReceipt($"Fout: {ex.Message}");
            }
        }

        public void ResetButtonClick()
        {
            // Reset de bon
            ShopManager.SetShopReceipt("Eindbedrag\n€");
        }

        public void SaveButtonClick()
        {
            // Hier kun je code toevoegen om de transactie op te slaan, indien nodig
        }
    }
}
