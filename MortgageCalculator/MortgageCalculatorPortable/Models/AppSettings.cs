namespace AffiniteGames.MortgageCalculatorPortable.Models
{
    public class AppSettings
    {
        public const string KEY_APP = "Mortgage Calculator";
        public const string KEY_FIRST_TIME = "First time";
        public const string KEY_PAID = "Paid";
        public const string KEY_AD_TYPE = "Ad type";

        internal bool IsFirstTime { get; set; }
        internal bool IsPaid { get; set; }
        internal AdType AdType { get; set; }

        internal AppSettings()
        {
            IsFirstTime = true;
            IsPaid = false;
            AdType = AdType.PopUp;
        }
    }
}
