using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Shared.Model;

namespace Integration.Tendering.Model
{
    public class Money : ValueObject
    {
        public double Amount { get; private set; }
        public Currency Currency { get; private set; }
        private Money(){}
        public Money(double amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
            Validate();
        }

        private void Validate()
        {
            if (Amount < 0) throw new ArgumentException("Invalid Amount, should not be negative!");
        }

        public void Add(int amount, Currency currency)
        {
            if (!CheckCurrency(currency)) return;
            Amount += amount;
            Validate();
        }

        public void Remove(int amount, Currency currency)
        {
            if (!CheckCurrency(currency)) return;
            Amount -= amount;
            Validate();
        }

        private bool CheckCurrency(Currency currency)
        {
            return currency == Currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency;
            yield return Amount;
        }
    }
}
