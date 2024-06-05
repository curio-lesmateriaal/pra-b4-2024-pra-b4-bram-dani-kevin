using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        // Het venster dat we op het scherm laten zien
        public static Home Window { get; set; }

        // De lijst met foto's die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();
        public List<string> PhotoInList = new List<string>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;

            // Zet de map op de juiste plek
            string dir = "";
            if (day == 0) dir = "../../../fotos/0_Zondag";
            else if (day == 1) dir = "../../../fotos/1_Maandag";
            else if (day == 2) dir = "../../../fotos/2_Dinsdag";
            else if (day == 3) dir = "../../../fotos/3_Woensdag";
            else if (day == 4) dir = "../../../fotos/4_Donderdag";
            else if (day == 5) dir = "../../../fotos/5_Vrijdag";
            else if (day == 6) dir = "../../../fotos/6_Zaterdag";

            // Lijst om alle foto's met hun tijdstempels te bewaren
            List<Tuple<DateTime, string>> allPhotos = new List<Tuple<DateTime, string>>();

            // Lees foto's en parse tijdstempels
            foreach (string file in Directory.GetFiles(dir))
            {
                string fileName = Path.GetFileName(file);
                DateTime timestamp = ParseTimestampFromFileName(fileName);

                int hour = now.Hour;
                int minute = now.Minute - 2;
                int second = now.Second;

                if (timestamp != DateTime.MinValue)
                {
                    allPhotos.Add(new Tuple<DateTime, string>(timestamp, file));
                }
            }


            // Koppel foto's die 60 seconden uit elkaar zijn genomen
            for (int i = 0; i < allPhotos.Count - 1; i++)
            {
                DateTime currentTimestamp = allPhotos[i].Item1;
                string currentPhoto = allPhotos[i].Item2;

                if ((allPhotos[i + 1].Item1 - currentTimestamp).TotalSeconds == 60)
                {
                    string pairedPhoto = allPhotos[i + 1].Item2;

                    PicturesToDisplay.Add(new KioskPhoto() { Id = PicturesToDisplay.Count, Source = currentPhoto });
                    PicturesToDisplay.Add(new KioskPhoto() { Id = PicturesToDisplay.Count, Source = pairedPhoto });

                    PhotoInList.Add(currentTimestamp.ToString("HH_mm_ss"));
                    PhotoInList.Add(allPhotos[i + 1].Item1.ToString("HH_mm_ss"));

                    i++; // Sla de volgende foto over omdat deze al is gekoppeld
                }
            }

            // Update de foto's
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        private DateTime ParseTimestampFromFileName(string fileName)
        {
            // Neem aan dat het bestandsnaamformaat "HH_mm_ss_<other_info>.jpg" is
            try
            {
                string[] parts = fileName.Split('_');
                int hour = int.Parse(parts[0]);
                int minute = int.Parse(parts[1]);
                int second = int.Parse(parts[2]);

                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, second);
            }
            catch
            {
                // Verwerk parse fouten of onverwachte bestandsnaamformaten
                return DateTime.MinValue;
            }
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            // Logica voor de refresh knop
        }
    }
}
