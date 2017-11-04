using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffiniteGames.MortgageCalculatorPortable.Models
{
    public class MortgageCalculator
    {
        public double Amortization(double loanAmount, double rate, int year)
        {
            double r = rate / 12 / 100;
            int n = year * 12;
            return r * Math.Pow(1 + r, n) / (Math.Pow(1 + r, n) - 1) * loanAmount;
        }

        public double TotalCostOfMortgage(double monthlyPayment, int year)
        {
            int n = year * 12;
            return monthlyPayment * n;
        }
    }
}
