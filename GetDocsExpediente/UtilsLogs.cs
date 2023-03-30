namespace GetDocsExpediente
{
    class UtilsLogs
    {
        /// <summary>
        /// Muestra y guarda logs
        /// </summary>
        /// <param name="logFile">Fichero de logs</param>
        /// <param name="logConsole">VMostrar consola</param>
        /// <param name="mensaje">Mensaje para guardar y mostrar</param>
        public static void AddingLog(Serilog.Core.Logger logFile, Serilog.Core.Logger logConsole, string mensaje, string tipo)
        {
            switch (tipo) { 
                case "INF":
                    {
                        logFile.Information(mensaje);
                        logConsole.Information(mensaje);
                        break;
                     }
                case "ERR":
                    {
                        logFile.Error(mensaje);
                        logConsole.Error(mensaje);
                        break;
                    }
                case "WNG":
                    {
                        logFile.Warning(mensaje);
                        logConsole.Warning(mensaje);
                        break;
                    }
            }

        
        }
    }
}
