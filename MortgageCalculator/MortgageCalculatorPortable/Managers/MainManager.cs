using AffiniteGames.MortgageCalculatorPortable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffiniteGames.MortgageCalculatorPortable.Managers
{
    public class MainManager
    {
        const double EPSILON = 0.1;

        AppSettings Settings { get; set; }
        MortgageCalculator Calculator { get; set; }

        public MainManager()
        {
            Settings = DataManager.Instance.Settings;
            Calculator = DataManager.Instance.Calculator;
        }

        public string GetMonthlyCostCalculations(string mortgageAmount, string interestRate, string mortgagePeriod)
        {
            // Convert to proper values
            string cleanMortgageAmount = mortgageAmount.Replace("$", "").Replace(",", "");
            double mortgageAmountValue = double.Parse(cleanMortgageAmount);
            string cleanInterestRate = interestRate.Replace("%", "");
            double interestRateValue = double.Parse(cleanInterestRate);
            int mortgagePeriodValue = int.Parse(mortgagePeriod);

            // Calculate monthly cost of mortgage
            double monthlyCost = Calculator.Amortization(mortgageAmountValue, interestRateValue, mortgagePeriodValue);

            // Calculate total cost of mortgage
            double totalCostOfMortgage = Calculator.TotalCostOfMortgage(monthlyCost, mortgagePeriodValue);

            // Return formatted string
            return $"Total cost of mortgage: \n{totalCostOfMortgage:c} \n\nMonthly payments: \n{monthlyCost:c}";
        }

        public bool IsFirstTime
        {
            get
            {
                return Settings.IsFirstTime;
            }
            set
            {
                Settings.IsFirstTime = value;
            }
        }

        public AdType AdType
        {
            get
            {
                return Settings.AdType;
            }
        }
    }
}
