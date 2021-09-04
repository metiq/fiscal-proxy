namespace ERP.FiscalIntegration
{
    public static class Commands
    {
        public const byte DaySettlement = 69;
        public const byte CashIO = 70;
        public const byte Diagnostics = 71;

        public const byte CutPaper = 45;

        public class Reports
        {
            public const byte ShortPeriod = 79;
            public const byte DetailedPeriod = 94;
        }

        public static class Clock
        {
            public const byte SetDateTime = 61;
            public const byte GetDateTime = 62;
        }

        public static class Bill
        {
            public const byte OpenBill = 48;
            public const byte CloseBill = 56;

            public const byte OpenVoidBill = 85;
            public const byte CloseVoidBill = 86;

            public const byte AddItem = 49;
            public const byte CalculateSum = 53;
        }

    }
}
