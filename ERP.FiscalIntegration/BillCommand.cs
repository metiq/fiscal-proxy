using System;
using System.Collections.Generic;
using System.Globalization;

namespace ERP.FiscalIntegration
{
    public class BillCommand : CommandBase
    {
        private const char CashAmount = 'P';
        private const char CardAmount = 'D';

        private decimal _sum;
        private bool _isVoid;
        private BillPaymentType _paymentType;
        private decimal _cashAmount;
        private decimal _cardAmount;

        public BillCommand(int operatorCode, int operatorNumber, string operatorPassword,
            BillPaymentType paymentType, decimal cashAmount = 0, decimal cardAmount = 0) : base()
        {
            _isVoid = false;
            _cashAmount = cashAmount;
            _cardAmount = cardAmount;
            _paymentType = paymentType;

            Create(operatorCode, operatorNumber, operatorPassword, Commands.Bill.OpenBill);
        }

        public BillCommand(int operatorCode, int operatorNumber, string operatorPassword) : base()
        {
            _isVoid = true;
            Create(operatorCode, operatorNumber, operatorPassword, Commands.Bill.OpenVoidBill);
        }

        private void Create(int operatorCode, int operatorNumber, string operatorPassword, byte openCommand)
        {
            _sum = 0;
            _commands.Add(new CommandListItem(openCommand, $"{operatorCode},{operatorPassword},{operatorNumber}"));
        }

        public BillCommand AddItem(string name, decimal quantity, decimal price, decimal taxValue)
        {
            string priceText = string.Format(CultureInfo.InvariantCulture, "{0:0.00}*{1:0.000}", price, quantity);

            _sum += quantity * price;
            char taxCategory = TaxCategories.GetTaxCategory(taxValue);

            _commands.Add(new CommandListItem(Commands.Bill.AddItem, $"{name}{'\t'}{taxCategory}{priceText}"));

            return this;
        }

        public override List<CommandListItem> ToCommands()
        {
            if (!_isVoid)
            {
                switch (_paymentType)
                {
                    case BillPaymentType.Cash:
                        AddSumCommand(_sum, CashAmount);
                        break;
                    case BillPaymentType.Card:
                        AddSumCommand(_sum, CardAmount);
                        break;
                    case BillPaymentType.Mixed:
                        if (_cashAmount + _cardAmount != _sum)
                        {
                            throw new Exception("The sum of cashAmount and cardAmount does not equal the sum of the sale.");
                        }

                        AddSumCommand(_cashAmount, CashAmount);
                        AddSumCommand(_cardAmount, CardAmount);
                        break;
                }
            }
            else
            {
                _commands.Add(new CommandListItem(Commands.Bill.CalculateSum, string.Empty));
            }

            byte closeCommand = _isVoid ? Commands.Bill.CloseVoidBill : Commands.Bill.CloseBill;
            _commands.Add(new CommandListItem(closeCommand, string.Empty));

            return base.ToCommands();
        }

        private void AddSumCommand(decimal amount, char paymentType)
        {
            string sumText = string.Format(CultureInfo.InvariantCulture, "{0:0.00}", amount);

            _commands.Add(new CommandListItem(Commands.Bill.CalculateSum, $"{'\t'}{paymentType}{sumText}"));
        }
    }
}
