using Serilog;
using System;
using System.Configuration;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GetDocsExpediente
{
    class Program
    {
        public static Serilog.Core.Logger logFile = new LoggerConfiguration().WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "LOGS\\GetDocsExpediente_LOG_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".log").CreateLogger();
        public static Serilog.Core.Logger logConsole = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        static void Main(string[] args)
        {
            string User = ConfigurationManager.AppSettings["user"];
            string Pass = ConfigurationManager.AppSettings["password"];
            string Url = ConfigurationManager.AppSettings["SiteURL"];


            SharepointController spcontroller = new SharepointController(Url);

            //LOGIN
            try
            {
                spcontroller.Login(User, Pass);
                //UtilsLogs.AddingLog(logFile, logConsole, "Inicio de sesión con éxito", "INF");
                logConsole.Information("Inicio de sesión con éxito");
            }
            catch(Exception ex)
            {
                UtilsLogs.AddingLog(logFile, logConsole, "Fallo en el inicio de sesión", "ERR");
            }

            //GET ID REGISTRO
            int idExpediente = spcontroller.GetIdExpediente($"Lista registros del archivo EXPEDIENTE", "MAD-114/1995-0");
            
            //GET LISTA DE DOCUMENTOS / LISTA DE PUNTOS DOCUMENTALES DEL EXPEDIENTE
            var listaDocumentos = spcontroller.GetListaDocumentos(idExpediente, "Lista de documentos para todos los registros del archivo EXPEDIENTE", "UnCtype");

            listaDocumentos.ForEach(documento => logConsole.Information(documento));
            //POR CADA PUNTO DOCUMENTAL HACER LLAMADA A GETALLDOCSEXPEDIENTE (SOLUCIÓN YA EXISTENTE)

            Console.Read();
            

        }

        private static string CheckFilename(string name)
        {
            string formatedName = name;
            formatedName = formatedName.Replace("/", " ");
            formatedName = formatedName.Replace(":", " ");
            formatedName = formatedName.Replace("*", " ");
            formatedName = formatedName.Replace("?", " ");
            formatedName = formatedName.Replace("\"", " ");
            formatedName = formatedName.Replace("<", " ");
            formatedName = formatedName.Replace(">", " ");
            formatedName = formatedName.Replace("|", " ");
            formatedName = formatedName.Replace("!", " ");
            formatedName = formatedName.Replace("@", " ");
            formatedName = formatedName.Replace("#", " ");
            formatedName = formatedName.Replace("$", " ");
            formatedName = formatedName.Replace("%", " ");
            formatedName = formatedName.Replace("&", " ");

            return formatedName;
        }

        private static void CopyFile(string origin, string rename, int tipoDocumento, bool hasBatch)
        {
            List<string> renameFormated = origin.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
            string oldname = renameFormated[4];
            string ext = oldname.Substring(oldname.Length - 4, 4);
            string destinationUrl = ConfigurationManager.AppSettings["Target"];

            if (hasBatch)
            {
                string batch = oldname.Split('.')[0];
                int id = Convert.ToInt32(batch);
                destinationUrl += GetUrlForBatch(id);
            }

            if (!Directory.Exists(destinationUrl))
            {
                Directory.CreateDirectory(destinationUrl);
            }

            destinationUrl += rename + " - " + tipoDocumento + ext;

            if (!File.Exists(destinationUrl))
            {
                if (File.Exists(origin))
                {
                    File.Copy(origin, destinationUrl);
                }
                else
                {
                    Console.WriteLine($"El fichero {origin} no se ha encontrado");
                }
            }
            else
            {
                int count = 1;
                while (File.Exists(destinationUrl) && count < 10)
                {
                    destinationUrl = destinationUrl.Substring(0, destinationUrl.Length - 4);
                    destinationUrl += "(" + count + ")" + ".pdf";
                    count++;
                }
                File.Copy(origin, destinationUrl);
            }
        }

        private static string GetUrlForBatch(int batch)
        {
            string staticPath = "";
            switch (batch)
            {
                case 2:
                    staticPath += @"SGC-28 2021-100\";
                    break;
                case 3:
                    staticPath += @"SGC-28 2021-101\";
                    break;
                case 4:
                    staticPath += @"SGC-28 2021-102\";
                    break;
                case 5:
                    staticPath += @"SGC-28 2021-103\";
                    break;
                case 6:
                    staticPath += @"SGC-28 2021-104\";
                    break;
                case 7:
                    staticPath += @"SGC-28 2021-105\";
                    break;
                case 8:
                    staticPath += @"SGC-28 2021-106\";
                    break;
                case 9:
                    staticPath += @"SGC-28 2021-107\";
                    break;
                case 10:
                    staticPath += @"SGC-28 2021-108\";
                    break;
                case 11:
                    staticPath += @"SGC-28 2021-109\";
                    break;
                case 12:
                    staticPath += @"SGC-28 2021-110\";
                    break;
                case 13:
                    staticPath += @"SGC-28 2021-111\";
                    break;
                case 14:
                    staticPath += @"SGC-28 2021-112\";
                    break;
                case 15:
                    staticPath += @"SGC-28 2021-113\";
                    break;
                case 16:
                    staticPath += @"SGC-28 2021-114\";
                    break;
                case 17:
                    staticPath += @"SGC-28 2021-115\";
                    break;
                case 18:
                    staticPath += @"SGC-28 2021-116\";
                    break;
                case 19:
                    staticPath += @"SGC-28 2021-117\";
                    break;
                case 20:
                    staticPath += @"SGC-28 2021-118\";
                    break;
                case 21:
                    staticPath += @"SGC-28 2021-119\";
                    break;
                case 22:
                    staticPath += @"SGC-28 2021-120\";
                    break;
                case 23:
                    staticPath += @"SGC-28 2021-121\";
                    break;
                case 24:
                    staticPath += @"SGC-28 2021-122\";
                    break;
                case 25:
                    staticPath += @"SGC-28 2021-123\";
                    break;
                case 26:
                    staticPath += @"SGC-28 2021-124\";
                    break;
                case 27:
                    staticPath += @"SGC-28 2021-125\";
                    break;
                case 28:
                    staticPath += @"SGC-28 2021-126\";
                    break;
                case 29:
                    staticPath += @"SGC-28 2021-127\";
                    break;
                case 30:
                    staticPath += @"SGC-28 2021-128\";
                    break;
                case 31:
                    staticPath += @"SGC-28 2021-129\";
                    break;
                case 32:
                    staticPath += @"SGC-28 2021-130\";
                    break;
                case 33:
                    staticPath += @"SGC-28 2021-131\";
                    break;
                case 34:
                    staticPath += @"SGC-28 2021-132\";
                    break;
                case 35:
                    staticPath += @"SGC-28 2021-133\";
                    break;
                case 36:
                    staticPath += @"SGC-28 2021-134\";
                    break;
                case 37:
                    staticPath += @"SGC-28 2021-135\";
                    break;
                case 38:
                    staticPath += @"SGC-28 2021-136\";
                    break;
                case 39:
                    staticPath += @"SGC-28 2021-137\";
                    break;
                case 40:
                    staticPath += @"SGC-28 2021-138\";
                    break;
                case 41:
                    staticPath += @"SGC-28 2021-139\";
                    break;
                case 42:
                    staticPath += @"SGC-28 2021-140\";
                    break;
                case 43:
                    staticPath += @"SGC-28 2021-141\";
                    break;
                case 44:
                    staticPath += @"SGC-90 2021-100\";
                    break;
                case 45:
                    staticPath += @"SGC-90 2021-101\";
                    break;
                case 46:
                    staticPath += @"SGC-90 2021-102\";
                    break;
                case 47:
                    staticPath += @"SGC-90 2021-103\";
                    break;
                case 48:
                    staticPath += @"SGC-90 2021-104\";
                    break;
                case 49:
                    staticPath += @"SGC-90 2021-105\";
                    break;
                case 50:
                    staticPath += @"SGC-90 2021-106\";
                    break;
                case 51:
                    staticPath += @"SGC-90 2021-107\";
                    break;
                case 52:
                    staticPath += @"SGC-90 2021-108\";
                    break;
                case 53:
                    staticPath += @"SGC-90 2021-109\";
                    break;
                case 54:
                    staticPath += @"SGC-90 2021-110\";
                    break;
                case 55:
                    staticPath += @"SGC-90 2021-111\";
                    break;
                case 56:
                    staticPath += @"SGC-90 2021-112\";
                    break;
                case 57:
                    staticPath += @"SGC-90 2021-113\";
                    break;
                case 58:
                    staticPath += @"SGC-90 2021-114\";
                    break;
                case 59:
                    staticPath += @"SGC-90 2021-115\";
                    break;
                case 60:
                    staticPath += @"SGC-90 2021-116\";
                    break;
                case 61:
                    staticPath += @"SGC-90 2021-117\";
                    break;
                case 62:
                    staticPath += @"SGC-90 2021-118\";
                    break;
                case 68:
                    staticPath += @"DTC-112 2021-100\";
                    break;
                case 69:
                    staticPath += @"DTC-112 2021-101\";
                    break;
                case 70:
                    staticPath += @"DTC-112 2021-102\";
                    break;
                case 71:
                    staticPath += @"DTC-112 2021-103\";
                    break;
                case 72:
                    staticPath += @"DTC-112 2021-104\";
                    break;
                case 73:
                    staticPath += @"DTC-112 2021-105\";
                    break;
                case 74:
                    staticPath += @"DTC-112 2021-106\";
                    break;
                case 75:
                    staticPath += @"DTC-112 2021-107\";
                    break;
                case 76:
                    staticPath += @"DTC-112 2021-108\";
                    break;
                case 77:
                    staticPath += @"DTC-112 2021-109\";
                    break;
                case 78:
                    staticPath += @"DTC-112 2021-110\";
                    break;
                case 146:
                    staticPath += @"MAD-229 2021-0\";
                    break;
                case 147:
                    staticPath += @"MAD-229 2021-1\";
                    break;
                case 148:
                    staticPath += @"MAD-229 2021-2\";
                    break;
                case 465:
                    staticPath += @"DTC-677 2021-2\";
                    break;
                case 466:
                    staticPath += @"DTC-677 2021-0\";
                    break;
                case 467:
                    staticPath += @"DTC-677 2021-1\";
                    break;
                case 34050:
                    staticPath += @"DIN-506 2020-0";
                    break;
                case 34095:
                    staticPath += @"DIN-506 2020-1";
                    break;
                case 34096:
                    staticPath += @"DIN-506 2020-2";
                    break;
                case 34097:
                    staticPath += @"DIN-506 2020-3";
                    break;
            }

            return staticPath;
        }
    }
}

