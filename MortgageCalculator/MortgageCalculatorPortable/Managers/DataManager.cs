using AffiniteGames.MortgageCalculatorPortable.Models;
using System.Collections.Generic;

namespace AffiniteGames.MortgageCalculatorPortable.Managers
{
    public class DataManager
    {
        #region Singleton pattern
        static DataManager mInstance;
        internal static DataManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new DataManager();
                }
                return mInstance;
            }
        }
        #endregion

        internal List<string> ErrorLog { get; private set; }

        internal AppSettings Settings { get; private set; }

        internal MortgageCalculator Calculator { get; private set; }

        DataManager()
        {
            // Must be initialized first to log errors
            ErrorLog = new List<string>();

            Settings = new AppSettings();
            Calculator = new MortgageCalculator();
        }

        internal void SetPrefs(bool isFirstTime, bool isPaid, bool isMapEnabled, bool isHighlighted, int adType)
        {
            Settings.IsFirstTime = isFirstTime;
            Settings.IsPaid = isPaid;
            Settings.AdType = (AdType)adType;
        }
    }
}
