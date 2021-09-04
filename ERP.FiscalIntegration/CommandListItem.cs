namespace ERP.FiscalIntegration
{
    public class CommandListItem
    {
        public CommandListItem() { }

        public CommandListItem(byte command, string commandBody)
        {
            Command = command;
            CommandBody = commandBody;
        }

        public byte Command { get; set; }
        public string CommandBody { get; set; }
    }
}
