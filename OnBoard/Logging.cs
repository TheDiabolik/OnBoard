using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{ 

    class Logging
    {
        internal static string Path = PreparePath();
        public static string PreparePath()
        {
            StringBuilder loggingPath = new StringBuilder();
            loggingPath.Append(DateTime.Now.Year.ToString("0000")).Append("-").Append(DateTime.Now.Month.ToString("00")).Append("-").Append(DateTime.Now.Day.ToString("00"))
                .Append("-").Append(DateTime.Now.Hour.ToString("00")).Append("-").Append(DateTime.Now.Minute.ToString("00")).Append("-").Append(DateTime.Now.Second.ToString("00"));

            return loggingPath.ToString();
        }


        public static void WriteLog(string message, string stackTrace, string targetSite, string comment)
        {
            try
            {
                if (!Directory.Exists("Logs"))
                    Directory.CreateDirectory("Logs");

                if (!Directory.Exists("Logs\\" + DateTime.Now.Year.ToString()))
                    Directory.CreateDirectory("Logs\\" + DateTime.Now.Year.ToString());

                if (!Directory.Exists("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString()))
                    Directory.CreateDirectory("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString());


                string mainPath = "Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString();

                StringBuilder loggingPath = new StringBuilder();
                loggingPath.Append(mainPath).Append("\\").Append(Path).Append(".txt");

               


                //string path = "Logs\\" + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" +
                //    DateTime.Now.Date.ToShortDateString() + ".txt";

                StreamWriter sw;

                if (!File.Exists(loggingPath.ToString()))
                    sw = new StreamWriter(loggingPath.ToString());
                else
                    sw = File.AppendText(loggingPath.ToString());

                sw.WriteLine("-------------------------------------");
                sw.WriteLine("Hata Zamanı : ");
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine();
                sw.WriteLine("Hata Mesajı :");
                sw.WriteLine(message);
                sw.WriteLine();
                sw.WriteLine("Hata Oluşan Kod Parçacığı :");
                sw.WriteLine(stackTrace);
                sw.WriteLine();
                sw.WriteLine("Hata Oluşan Metot :");
                sw.WriteLine(targetSite);
                sw.WriteLine();
                sw.WriteLine("Yorum :");
                sw.WriteLine(comment);
                sw.WriteLine();
                sw.WriteLine("-------------------------------------");

                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.LoggingExceptionMessage, ex);
            }
        }
    }
}
