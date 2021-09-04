using System;
using System.Collections.Generic;

namespace ERP.FiscalIntegration
{
    public class SetClockCommand : CommandBase
    {
        public SetClockCommand(DateTime datetime) : base()
        {
            _commands.Add(new CommandListItem(Commands.Clock.SetDateTime, datetime.ToString("dd-MM-yy HH:mm:ss")));
        }
    }
}
