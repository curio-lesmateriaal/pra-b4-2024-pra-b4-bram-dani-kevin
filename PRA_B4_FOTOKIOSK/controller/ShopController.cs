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

        private List<(KioskProduct Product, int Amount)> selectedProducts = new List<(KioskProduct, int)>();
        private decimal totalAmount = 0;

        public void Start()
        {
            ShopManager.SetShopPriceList("Prijzen:\n");



            ShopManager.SetShopReceipt("Eindbedrag\n€0.00");


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
                string priceListItem = $"{product.Name}: €{product.Price:F2} - {product.Description}\n";
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



                    selectedProducts.Add((selectedProduct, amount));

   
                    totalAmount += selectedProduct.Price * amount;


                    UpdateReceipt();

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


            selectedProducts.Clear();
            totalAmount = 0;


            ShopManager.SetShopReceipt("Eindbedrag\n€0.00");
        }

        private void UpdateReceipt()
        {
            StringBuilder receipt = new StringBuilder("Kassabon:\n");

            foreach (var (product, amount) in selectedProducts)
            {
                receipt.AppendLine($"{amount} x {product.Name} - €{product.Price * amount:F2}");
            }

            receipt.AppendLine($"\nEindbedrag: €{totalAmount:F2}");
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
