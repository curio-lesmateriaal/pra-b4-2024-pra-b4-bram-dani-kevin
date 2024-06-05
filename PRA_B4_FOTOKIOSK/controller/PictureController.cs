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
    public class PictureController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }

        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();
        public List<string> PhotoInList = new List<string>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;

            // Zet de dir op goede plek
            string dir = "";
            if (day == 0) dir = "../../../fotos/0_Zondag";
            else if (day == 1) dir = "../../../fotos/1_Maandag";
            else if (day == 2) dir = "../../../fotos/2_Dinsdag";
            else if (day == 3) dir = "../../../fotos/3_Woensdag";
            else if (day == 4) dir = "../../../fotos/4_Donderdag";
            else if (day == 5) dir = "../../../fotos/5_Vrijdag";
            else if (day == 6) dir = "../../../fotos/6_Zaterdag";

            // haalt photo op
            foreach (string file in Directory.GetFiles(dir))
            {
                int hour = now.Hour;
                int minute = now.Minute - 2;
                int second = now.Second;

                for (int i = 0; i < 1800; i++)
                {
                    second--;
                    if (second < 0)
                    {
                        second = 59;
                        minute--;
                    }

                    if (minute < 0)
                    {
                        minute = 59;
                        hour--;
                    }

                    string timeDateString = string.Format("{0:D2}_{1:D2}_{2:D2}_", hour, minute, second);

                    // Check if the file name contains the timeDateString
                    if (Path.GetFileName(file).Contains(timeDateString))
                    {
                        // Add the photo to the PicturesToDisplay list
                        PicturesToDisplay.Add(new KioskPhoto() { Id = PicturesToDisplay.Count, Source = file });
                        // Add the time string to the PhotoInList
                        PhotoInList.Add(timeDateString);
                    }
                }
            }

            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {

        }
    }
}
