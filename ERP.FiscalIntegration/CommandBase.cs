using System.Collections.Generic;

namespace ERP.FiscalIntegration
{
    public abstract class CommandBase
    {
        protected List<CommandListItem> _commands;

        public CommandBase()
        {
            _commands = new List<CommandListItem>();
        }

        public virtual List<CommandListItem> ToCommands()
        {
            return _commands;
        }
    }
}
