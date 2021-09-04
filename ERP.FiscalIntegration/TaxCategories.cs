using System.Collections.Generic;

namespace ERP.FiscalIntegration
{
    public static class TaxCategories
    {
        private static Dictionary<decimal, char> _taxDictionary;
        static TaxCategories()
        {
            _taxDictionary = new Dictionary<decimal, char>
            {
                { 18m, Ddv18 }, { 5m, Ddv5 }, { 0m, Ddv0 }
            };
        }

        public static char GetTaxCategory(decimal value)
        {
            return _taxDictionary[value];
        }

        public const char Ddv18 = 'А';
        public const char Ddv5 = 'Б';
        public const char Ddv0 = 'В';
        //public const char DDV? = 'Г';
    }
}
