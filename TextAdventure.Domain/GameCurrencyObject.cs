using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Domain
{
    public class GameCurrencyObject : GameObject
    {
        public GameCurrencyObject(string name, decimal financialValue)
        {
            Name = name;
            FinancialValue = financialValue;
        }

        public decimal FinancialValue { get; set; }
    }
}
