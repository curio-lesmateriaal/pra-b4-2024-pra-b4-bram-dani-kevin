﻿using PRA_B4_FOTOKIOSK.magie;
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
            // Haal de huidige datum en tijd op
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;

            // Zet de map op de juiste plek gebaseerd op de huidige dag van de week
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
            // Een tuple is een datastructuur die een vast aantal elementen bevat, in dit geval een DateTime en een string.
            // Het wordt gebruikt om de tijdstempel van de foto en het pad naar het fotobestand samen te groeperen.

            // Lees foto's en parse tijdstempels
            foreach (string file in Directory.GetFiles(dir))
            {
                string fileName = Path.GetFileName(file);
                DateTime timestamp = ParseTimestampFromFileName(fileName);

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

                // Controleer of de volgende foto 60 seconden na de huidige foto is genomen
                if ((allPhotos[i + 1].Item1 - currentTimestamp).TotalSeconds == 60)
                {
                    string pairedPhoto = allPhotos[i + 1].Item2;

                    // Voeg beide foto's toe aan de weergavelijst
                    PicturesToDisplay.Add(new KioskPhoto() { Id = PicturesToDisplay.Count, Source = currentPhoto });
                    PicturesToDisplay.Add(new KioskPhoto() { Id = PicturesToDisplay.Count, Source = pairedPhoto });

                    // Voeg hun tijdstempels toe aan de lijst van fototijden
                    PhotoInList.Add(currentTimestamp.ToString("HH_mm_ss"));
                    PhotoInList.Add(allPhotos[i + 1].Item1.ToString("HH_mm_ss"));

                    // Sla de volgende foto over omdat deze al is gekoppeld
                    i++;
                }
            }

            // Update de foto's in de PictureManager
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Methode om de tijdstempel uit de bestandsnaam te parseren
        private DateTime ParseTimestampFromFileName(string fileName)
        {
            // Neem aan dat het bestandsnaamformaat "HH_mm_ss_<other_info>.jpg" is
            try
            {
                string[] parts = fileName.Split('_');
                int hour = int.Parse(parts[0]);
                int minute = int.Parse(parts[1]);
                int second = int.Parse(parts[2]);

                // Retourneer de geparste tijdstempel
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, second);
            }
            catch
            {
                // Verwerk parse fouten of onverwachte bestandsnaamformaten
                return DateTime.MinValue;
            }
        }

        // Methode die wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            // Logica voor de refresh knop
        }
    }
}
