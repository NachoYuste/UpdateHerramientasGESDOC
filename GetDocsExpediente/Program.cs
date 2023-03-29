using Serilog;
using System;
using System.Configuration;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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


            //LOGIN

            //GET ID REGISTRO

            //GET LISTA DE DOCUMENTOS / LISTA DE PUNTOS DOCUMENTALES DEL EXPEDIENTE

            //POR CADA PUNTO DOCUMENTAL HACER LLAMADA A GETALLDOCSEXPEDIENTE (SOLUCIÓN YA EXISTENTE)
        }
    }
}
