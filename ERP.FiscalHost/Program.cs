using ERP.FiscalIntegration;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;

namespace ERP.FiscalHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                var clientData = ConsoleDataIO.Read();

                string serialPort = clientData["serialPort"].Value<string>();
                int baudRate = clientData["baudRate"].Value<int>();
                var commands = clientData["message"].ToObject<List<CommandListItem>>();

                var data = FiscalInterface.RunCommands(serialPort, baudRate, commands, logger);

                if (data.success)
                {
                    logger.Info($"Commands successfully ran. Returning result {data.result}");
                    ConsoleDataIO.Write(data.result);
                }
                else
                {
                    ConsoleDataIO.Write(null, data.result);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                ConsoleDataIO.Write(null, ex.Message);
            }
        }
    }
}
