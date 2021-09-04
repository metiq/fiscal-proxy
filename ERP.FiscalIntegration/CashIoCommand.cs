using System.Collections.Generic;
using System.Globalization;

namespace ERP.FiscalIntegration
{
    public class CashIoCommand : CommandBase
    {
        public CashIoCommand(decimal cashAmount) : base()
        {
            string amount = string.Format(CultureInfo.InvariantCulture, "{0:0.00}", cashAmount);

            _commands.Add(new CommandListItem(Commands.CashIO, amount));
        }
    }
}
