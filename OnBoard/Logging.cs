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


        public static void WriteCommunicationLog(DateTime logTime, StringBuilder logToWrite)
        {
            try
            {
                if (!Directory.Exists("Logs"))
                    Directory.CreateDirectory("Logs");

                string[] logToWriteSplitArray = logToWrite.ToString().Split('\n'); 
                string sizeName = logToWriteSplitArray[1];
                string[] sizeNameSplitArray = sizeName.Split(' '); 
                string idPath = sizeNameSplitArray[2];

                if (!Directory.Exists("Logs\\" ))
                    Directory.CreateDirectory("Logs\\");

                if (!Directory.Exists("Logs\\" +   DateTime.Now.Year.ToString()))
                    Directory.CreateDirectory("Logs\\"  + DateTime.Now.Year.ToString());

                if (!Directory.Exists("Logs\\"  + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString("00")))
                    Directory.CreateDirectory("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString("00"));


                if (!Directory.Exists("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString("00") + "\\" + DateTime.Now.Day.ToString("00")))
                    Directory.CreateDirectory("Logs\\" +  DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString("00") + "\\" + DateTime.Now.Day.ToString("00"));


                if (!Directory.Exists("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString("00") + "\\" + DateTime.Now.Day.ToString("00") + "\\" + idPath))
                    Directory.CreateDirectory("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString("00") + "\\" + DateTime.Now.Day.ToString("00") + "\\" + idPath);


                string mainPath = "Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString("00") + "\\" + DateTime.Now.Day.ToString("00") + "\\" + idPath;

                string lastFileName;
                string newFileName;
                StringBuilder loggingPath = new StringBuilder();


                if (Directory.Exists(mainPath))
                {
                    DirectoryInfo di = new DirectoryInfo(mainPath);
                    FileInfo[] fi = di.GetFiles().OrderByDescending(p => p.LastWriteTime).ToArray();

                    //FileInfo[] fi = di.GetFiles().OrderBy(p => File.GetLastAccessTime(p.Name)).ToArray();

                    if (fi.Length > 0)
                    {
                        lastFileName = System.IO.Path.GetFileNameWithoutExtension(fi[0].Name); 

                        long len = fi[0].Length;

                        //10485760;

                        if (len > 5242880)
                        {
                            newFileName = PreparePath();

                            loggingPath.Append(mainPath).Append("\\").Append(newFileName).Append(".txt");
                        }
                        else
                        {
                            loggingPath.Append(mainPath).Append("\\").Append(lastFileName).Append(".txt");
                        } 
                    }
                    else
                    {
                        loggingPath.Append(mainPath).Append("\\").Append(Path).Append(".txt");
                    } 
                } 

                StreamWriter sw;

                if (!File.Exists(loggingPath.ToString()))
                    sw = new StreamWriter(loggingPath.ToString());
                else
                    sw = File.AppendText(loggingPath.ToString());

                sw.WriteLine("-------------------------------------");
                sw.Write("Haberleşme Zamanı : ");
                sw.WriteLine(logTime.ToString());
                sw.Write(logToWrite.ToString()); 
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
