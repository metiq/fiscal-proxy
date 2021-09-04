using System.Collections.Generic;

namespace ERP.FiscalIntegration
{
    public class SingleCommand : CommandBase
    {
        public SingleCommand(byte command, string data = null) : base()
        {
            _commands.Add(new CommandListItem(command, data ?? string.Empty));
        }
    }
}
