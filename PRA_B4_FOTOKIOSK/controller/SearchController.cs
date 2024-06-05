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
    public class SearchController
    {
        // The window displayed on the screen
        public static Home Window { get; set; }

        // Start method called when the search page opens
        public void Start()
        {
            ShopManager.Instance = Window;
        }
        public void SearchButtonClick()
        {
            string searchInput = SearchManager.GetSearchInput();


            List<KioskPhoto> searchResult = GetSearchResult(searchInput);

            if (searchResult.Count > 0)
            {
                KioskPhoto firstPhoto = searchResult[0];
                string imageInfo = $"ID: {firstPhoto.Id}, Date: {firstPhoto.Date.ToShortDateString()}, Time: {firstPhoto.Time.ToString(@"hh\:mm\:ss")}";
                SearchManager.SetSearchImageInfo(imageInfo);
            }
            else
            {
                SearchManager.SetSearchImageInfo("No image found");
            }
        }
        private List<KioskPhoto> GetSearchResult(string searchInput)
        {
            List<KioskPhoto> searchResult = new List<KioskPhoto>();

            return searchResult;
        }
    }
}
