using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {
        public static Home Window { get; set; }
        // Een lijst met tuples (paren) die het product en de hoeveelheid bevatten die zijn geselecteerd door de gebruiker
        private List<(KioskProduct Product, int Amount)> selectedProducts = new List<(KioskProduct, int)>();
        // Het totale bedrag van de geselecteerde producten
        private decimal totalAmount = 0;

        public void Start()
        {
            // Stel de prijslijst van de winkel in met een koptekst "Prijzen:"
            ShopManager.SetShopPriceList("Prijzen:\n");


            // Stel het winkelbonnetje in met een koptekst "Eindbedrag" en een beginwaarde van €0.00

            ShopManager.SetShopReceipt("Eindbedrag\n€0.00");

            // Voeg verschillende producten toe aan de lijst van beschikbare producten in de winkel

            ShopManager.Products.Add(new KioskProduct("Foto 10x15", 2.55m, "Foto afdruk 10x15 cm"));
            ShopManager.Products.Add(new KioskProduct("Foto 20x30", 4.95m, "Foto afdruk 20x30 cm"));
            ShopManager.Products.Add(new KioskProduct("Mok met foto", 9.95m, "Mok bedrukt met foto"));
            ShopManager.Products.Add(new KioskProduct("Sleutelhanger met foto", 6.12m, "Sleutelhanger met foto"));
            ShopManager.Products.Add(new KioskProduct("T-shirt met foto", 11.99m, "T-shirt bedrukt met foto"));

            // Werk de drop-down lijst bij die de beschikbare producten toont
            ShopManager.UpdateDropDownProducts();

            // Genereer de prijslijst met alle beschikbare producten en hun prijzen
            GeneratePriceList();
        }

        public void GeneratePriceList()
        {
            // Stel de prijslijst van de winkel opnieuw in met een koptekst "Prijzen:"
            ShopManager.SetShopPriceList("Prijzen:\n");

            // Doorloop alle producten in de lijst van beschikbare producten
            foreach (KioskProduct product in ShopManager.Products)
            {
                // Maak een string voor elk product die de naam, prijs en beschrijving bevat
                string priceListItem = $"{product.Name}: €{product.Price:F2} - {product.Description}\n";
                // Voeg deze string toe aan de prijslijst in de winkelinterface
                ShopManager.AddShopPriceList(priceListItem);
            }
        }

        public void AddButtonClick()
        {
            try
            {
                // Verkrijg het geselecteerde product van de gebruiker
                KioskProduct selectedProduct = ShopManager.GetSelectedProduct();
                // Verkrijg de hoeveelheid van het geselecteerde product van de gebruiker
                int? amountNullable = ShopManager.GetAmount();



                // Controleer of zowel een product als een hoeveelheid zijn ingevoerd
                if (selectedProduct != null && amountNullable.HasValue)
                {
                    // Zet de nullable hoeveelheid om naar een int
                    int amount = amountNullable.Value;


                    // Voeg het geselecteerde product en de hoeveelheid toe aan de lijst van geselecteerde producten
                    selectedProducts.Add((selectedProduct, amount));

                    // Voeg de prijs van het geselecteerde product maal de hoeveelheid toe aan het totale bedrag
                    totalAmount += selectedProduct.Price * amount;

                    // Werk de kassabon bij om de nieuwe producten en het totale bedrag te tonen
                    UpdateReceipt();

                }
                else
                {
                    // Stel de kassabon in op een foutmelding als het product of de hoeveelheid niet is ingevoerd

                    ShopManager.SetShopReceipt("Product of hoeveelheid niet ingevoerd");
                }
            }
            catch (Exception ex)
            {
                // Stel de kassabon in op een foutmelding als er een uitzondering optreedt

                ShopManager.SetShopReceipt($"Fout: {ex.Message}");
            }
        }

        public void ResetButtonClick()
        {

            // Wis de lijst van geselecteerde producten
            selectedProducts.Clear();
            // Zet het totale bedrag terug naar 0
            totalAmount = 0;

            // Stel de kassabon in op de beginwaarde met een eindbedrag van €0.00
            ShopManager.SetShopReceipt("Eindbedrag\n€0.00");
        }

        private void UpdateReceipt()
        {
            // Maak een StringBuilder aan voor de kassabon
            StringBuilder receipt = new StringBuilder("Kassabon:\n");
            // Doorloop alle geselecteerde producten en voeg elk product met de hoeveelheid en totaalprijs toe aan de kassabon

            foreach (var (product, amount) in selectedProducts)
            {
                // Voeg het eindbedrag toe aan de kassabon
                receipt.AppendLine($"{amount} x {product.Name} - €{product.Price * amount:F2}");
            }

            receipt.AppendLine($"\nEindbedrag: €{totalAmount:F2}");
            // Stel de kassabon in op de gegenereerde string
            ShopManager.SetShopReceipt(receipt.ToString());
        }


        public void SaveButtonClick()
        {
            try
            {
                string receiptText = ShopManager.GetShopReceiptText();

                string path = "../../../bon.txt";
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine("GetShopReceipt");
                sw.Close();

                File.WriteAllText(path, receiptText);

                ShopManager.SetShopReceipt($"Bon succesvol opgeslagen");
            }
            catch (Exception ex)
            {
                ShopManager.SetShopReceipt($"Fout bij opslaan bon: {ex.Message}");
            }
        }
    }
}
