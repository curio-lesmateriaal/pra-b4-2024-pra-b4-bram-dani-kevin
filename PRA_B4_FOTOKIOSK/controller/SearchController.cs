using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            SearchManager.Instance = Window;
        }

        // Method called when the Search button is clicked
        public void SearchButtonClick()
        {
            string foundsearch = "";
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;

            string dir = "";
            if (day == 0) dir = "../../../fotos/0_Zondag";
            else if (day == 1) dir = "../../../fotos/1_Maandag";
            else if (day == 2) dir = "../../../fotos/2_Dinsdag";
            else if (day == 3) dir = "../../../fotos/3_Woensdag";
            else if (day == 4) dir = "../../../fotos/4_Donderdag";
            else if (day == 5) dir = "../../../fotos/5_Vrijdag";
            else if (day == 6) dir = "../../../fotos/6_Zaterdag";


            foreach (string file in Directory.GetFiles(dir))
            {
                string[] hankiepankie3000 = SearchManager.GetSearchInput().Split("_");
                int hour = int.Parse(hankiepankie3000[0]);
                int minute = int.Parse(hankiepankie3000[1]);
                int second = int.Parse(hankiepankie3000[2]);

                string timeDateString = string.Format("{0:D2}_{1:D2}_{2:D2}_", hour, minute, second);

                if (file.Contains(timeDateString))
                {
                    foundsearch = file;
                    SearchManager.SetPicture(foundsearch);

                    string[] fileInfo = Path.GetFileNameWithoutExtension(file).Split('_');

                    string id = fileInfo[3].Split("id")[1];

                    string imageInfo = $"ID: {id}\n";
                    imageInfo += $"Tijd: {fileInfo[0]}:{fileInfo[1]}:{fileInfo[2]}\n";
                    imageInfo += $"Datum: {now:dd/MM/yyyy}";
                    SearchManager.SetSearchImageInfo(imageInfo);
                }

            }
        }
    }
}













