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

        // Method called when the Search button is clicked
        public void SearchButtonClick()
        {
            // Get the search input from the UI
            string searchInput = SearchManager.GetSearchInput();

            // Perform search logic here (not shown in provided code)

            // For demonstration purposes, assuming you have a list of KioskPhoto objects as search result
            List<KioskPhoto> searchResult = GetSearchResult(searchInput);

            // Update the search image info with the information of the first photo in the search result
            if (searchResult.Count > 0)
            {
                KioskPhoto firstPhoto = searchResult[0];
                string imageInfo = $"ID: {firstPhoto.Id}, Date: {firstPhoto.Date.ToShortDateString()}, Time: {firstPhoto.Time.ToString(@"hh\:mm\:ss")}"; // Adjust date and time format as needed
                SearchManager.SetSearchImageInfo(imageInfo);
            }
            else
            {
                // If no search result is found, clear the search image info
                SearchManager.SetSearchImageInfo("No image found");
            }
        }

        // Method to simulate search result (replace with actual search logic)
        private List<KioskPhoto> GetSearchResult(string searchInput)
        {
            // Simulated search result
            List<KioskPhoto> searchResult = new List<KioskPhoto>();

            // Assuming you have a list of KioskPhoto objects containing search results
            // Populate searchResult based on searchInput

            return searchResult;
        }
    }
}
