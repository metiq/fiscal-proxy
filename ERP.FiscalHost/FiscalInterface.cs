using Accent.Ecr;
using ERP.FiscalIntegration;
using NLog;
using System.Collections.Generic;

namespace ERP.FiscalHost
{
    public class FiscalInterface
    {
        public static (bool success, string result) RunCommands(string serialPort, int baudRate, List<CommandListItem> commands, ILogger logger)
        {
            var ecr = new Ecr(serialPort, baudRate);
            ecr.OpenPort();

            logger.Info("Port open, writing commands...");
            string result = null;
            foreach (var command in commands)
            {
                logger.Info($"Writing {command.Command} {command.CommandBody}");
                var ecrResult = ecr.WriteCommand(command.Command, command.CommandBody);

                result = EcrTranslator.ConvertByteArrayToCp1251String(ecrResult.Data);

                logger.Info($"Result {ecrResult.ResultStatus.ToString()} {result}");
                if (ecrResult.ResultStatus != EcrResultStatus.OK)
                {
                    result = ecrResult.ResultStatus.ToString();

                    ecr.ClosePort();
                    return (false, result);
                }
            }

            ecr.ClosePort();
            return (true, result);
        }
    }
}
