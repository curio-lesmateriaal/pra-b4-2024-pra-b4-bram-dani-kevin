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

        public void Start()
        {
            ShopManager.SetShopPriceList("Prijzen:\n");

            ShopManager.SetShopReceipt("Eindbedrag\n€");

            ShopManager.Products.Add(new KioskProduct("Foto 10x15", 2.55m, "Foto afdruk 10x15 cm"));
            ShopManager.Products.Add(new KioskProduct("Foto 20x30", 4.95m, "Foto afdruk 20x30 cm"));
            ShopManager.Products.Add(new KioskProduct("Mok met foto", 9.95m, "Mok bedrukt met foto"));
            ShopManager.Products.Add(new KioskProduct("Sleutelhanger met foto", 6.12m, "Sleutelhanger met foto"));
            ShopManager.Products.Add(new KioskProduct("T-shirt met foto", 11.99m, "T-shirt bedrukt met foto"));

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

        public void AddButtonClick()
        {
            try
            {
                KioskProduct selectedProduct = ShopManager.GetSelectedProduct();
                int? amountNullable = ShopManager.GetAmount();

                if (selectedProduct != null && amountNullable.HasValue)
                {
                    int amount = amountNullable.Value;

                    decimal totalAmount = selectedProduct.Price * amount;
                    ShopManager.SetShopReceipt($"Totaalbedrag\n€{totalAmount}");
                }
                else
                {
                    ShopManager.SetShopReceipt("Product of hoeveelheid niet ingevoerd");
                }
            }
            catch (Exception ex)
            {
                ShopManager.SetShopReceipt($"Fout: {ex.Message}");
            }
        }

        public void ResetButtonClick()
        {
            ShopManager.SetShopReceipt("Eindbedrag\n€");
        }


        public string GetShopReceiptText()
        {
            return ShopManager.GetShopReceipt();
        }
        public void SaveButtonClick()
        {
            try
            {
                string bonInhoud = GetShopReceiptText();

                if (!string.IsNullOrEmpty(bonInhoud))
                {
                    string bestandsnaam = "bon.txt";
                    string mapPad = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string volledigPad = Path.Combine(mapPad, bestandsnaam);

                    File.WriteAllText(volledigPad, bonInhoud);
                    ShopManager.SetShopReceipt("Bon succesvol opgeslagen");
                }
                else
                {
                    ShopManager.SetShopReceipt("Kan geen lege bon opslaan");
                }
            }
            catch (Exception ex)
            {
                ShopManager.SetShopReceipt($"Fout bij opslaan bon: {ex.Message}");
            }
        }

    }
}
