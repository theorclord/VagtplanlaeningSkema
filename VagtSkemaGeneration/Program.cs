using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagtSkemaGeneration
{
  class Program
  {
    static void Main(string[] args)
    {
      DateTime startDate = new DateTime(2017, 4,24);
      DateTime endDate = new DateTime(2017,6,24);

      DateTime currentDate = startDate;
      string header = @"Herunder følger datoer i blok 4.
!!! Der må KUN bruges ét, og kun ét, af følgende fire ord i hvert felt (af hensyn til algoritme lavning) kan, ikke, åbne, lukke. Da vi har automatiseret skemalægningen er det essentielt, at man ikke skriver udtryk som " + "\"kan ikke\" eller \"helst ikke\"!" +@"

!!!Hverdagsvagter
Hvis du  tage en fast hverdagsvagt, så skriv det evt.her!Husk dog stadigvæk at udfylde alle dage.

!!!Kommentarer
Hvis du har kommentarer, så skriv dem her, så er der større chance for at vi opdager dem.

'''Åbne- og lukketider'''

* *Rengøringsdage: Åbner 12 - 16, lukker fra 16 -??
**Mandag: Åbner 12 - 17, lukker fra 17 - 22
* *Tirsdag / onsdag: Åbner 12 - 16, lukker fra 16 - 19.
* *Torsdag: Åbner 12 - 17, lukker fra 17 - 22.
* *Fredag: Åbner 12 - 20, lukker fra 20 - 03 + oprydning.

|| border = 1
|| !Dato || !Dag || !Uge || !Bemærkning || !Preference

";
      StringBuilder finalStr = new StringBuilder();
      finalStr.Append(header);
      while (currentDate < endDate)
      {
        string calenderLine = "";
        string year = currentDate.Year +"";
        string month = "";
        if(currentDate.Month < 10)
        {
          month = "0" +currentDate.Month;
        } else
        {
          month = currentDate.Month + "";
        }

        string day = "";
        if (currentDate.Day < 10)
        {
          day = "0" + currentDate.Day;
        }
        else
        {
          day = currentDate.Day + "";
        }
        string dayName = "";
        switch (currentDate.DayOfWeek)
        {
          case DayOfWeek.Monday:
            dayName = "Mandag";
            break;
          case DayOfWeek.Tuesday:
            dayName = "Tirsdag";
            break;
          case DayOfWeek.Wednesday:
            dayName = "Onsdag";
            break;
          case DayOfWeek.Thursday:
            dayName = "Torsdag";
            break;
          case DayOfWeek.Friday:
            dayName = "Fredag";
            break;
        }
        string weekNum = "" +CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek  != DayOfWeek.Sunday)
        {
          if( currentDate.DayOfWeek == DayOfWeek.Monday)
          {
            calenderLine = string.Format("||{2}{1}-{0}|| {3} || {4} || Brætspilsmandag ||  ||", new string[] { year, month, day, dayName, weekNum });
          } else
          {
            calenderLine = string.Format("||{2}{1}-{0}|| {3} || {4} ||  ||  ||", new string[] { year, month, day, dayName, weekNum });
          }
          finalStr.AppendLine(calenderLine);
        } else if(currentDate.DayOfWeek == DayOfWeek.Saturday)
        {
          finalStr.AppendLine("");
        }
        currentDate = currentDate.AddDays(1);
      }
      StreamWriter sw = new StreamWriter( File.Open(@"C:\VagtSkemaKalenderUdkast.txt", FileMode.OpenOrCreate));
      sw.Write(finalStr);
      sw.Close();
      Console.WriteLine(finalStr);
      Console.ReadKey();
    }
  }
}
