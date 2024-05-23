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

        // De lijst met foto's die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            // Verkrijg het huidige dagnummer (0 = Zondag, 6 = Zaterdag)
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;

            // Initializeer de lijst met foto's
            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            // foreach is een for-loop die door een array loopt
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                /**
                 * dir string is de map waar de foto's in staan. Bijvoorbeeld:
                 * \fotos\0_Zondag
                 */

                // Haal de naam van de map op
                var dirName = Path.GetFileName(dir);
                if (dirName.StartsWith(day.ToString() + "_"))
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        /**
                         * file string is het bestand van de foto. Bijvoorbeeld:
                         * \fotos\0_Zondag\10_05_30_id8824.jpg
                         */
                        // Haal de tijd uit de bestandsnaam
                        var fileName = Path.GetFileNameWithoutExtension(file);
                        var timeParts = fileName.Split('_');
                        if (timeParts.Length >= 3)
                        {
                            int hour = int.Parse(timeParts[0]);
                            int minute = int.Parse(timeParts[1]);
                            int second = int.Parse(timeParts[2]);

                            var fileTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);
                            var timeDifference = now - fileTime;

                            if (timeDifference.TotalMinutes >= 2 && timeDifference.TotalMinutes <= 30)
                            {
                                PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
                            }
                        }
                    }
                }
            }

            // Update de foto's
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            // Hier kan je de logica toevoegen voor wat er moet gebeuren wanneer de refresh knop wordt ingedrukt
        }
    }
}
