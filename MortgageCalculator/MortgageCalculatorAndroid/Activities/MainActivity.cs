
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Support.V4.App;

using Java.Lang.Reflect;
using Android.Widget;
using AffiniteGames.MortgageCalculatorPortable.Managers;

namespace AffiniteGames.MortgageCalculatorAndroid
{
    [Activity (Label = "Mortgage Calculator", MainLauncher = true, Icon = "@drawable/icon")]			
	public class MainActivity : FragmentActivity
	{
        MainManager Manager { get; set; }

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

            // Set main layout
			SetContentView (Resource.Layout.Main);

            // Initialize main manager
            Manager = new MainManager();

            // Set mortgage amount edit text events
            EditText mortgageAmount = this.FindViewById<EditText>(Resource.Id.editTextMortgageAmount);
            mortgageAmount.TextChanged += DollarAmount_TextChanged;
            mortgageAmount.AfterTextChanged += AfterTextChanged;

            // Set interest rate edit text events
            EditText interestRate = this.FindViewById<EditText>(Resource.Id.editTextInterestRate);
            interestRate.TextChanged += Percent_TextChanged;
            interestRate.AfterTextChanged += AfterTextChanged;

            // Set years edit text events
            EditText years = this.FindViewById<EditText>(Resource.Id.editTextMortgagePeriod);
            years.AfterTextChanged += AfterTextChanged;

            // Set calculate button click
            Button calculateButton = this.FindViewById<Button>(Resource.Id.calculateButton);
            calculateButton.Click += CalculateButton_Click;
        }

        public override bool OnCreateOptionsMenu (IMenu menu)
        {
            // Inflate the menu; this adds items to the action bar if it is present.
            MenuInflater.Inflate (Resource.Menu.actionbar_main, menu);

            ViewConfiguration config = ViewConfiguration.Get(this); 
            Field menuKeyField = config.Class.GetDeclaredField("sHasPermanentMenuKey");

            if(menuKeyField != null) 
            { 
                menuKeyField.Accessible = true; 
                menuKeyField.SetBoolean(config, false); 
            } 

            return true;
        }

        public override bool OnOptionsItemSelected (IMenuItem item)
        {
            // Handle action buttons
            switch (item.ItemId) {
                case Resource.Id.action_settings:
                    // create intent to perform web search for this planet
                    Intent intent = new Intent(this, typeof(SettingsActivity)); 
                    StartActivity(intent);
                    return true;
                default:
                    return base.OnOptionsItemSelected (item);
            }
        }

        private void DollarAmount_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            EditText editText = sender as EditText;

            // If there is a number then format it to currency format
            if (!string.IsNullOrEmpty(editText.Text) && !editText.Text.Contains("$"))
            {
                editText.Text = "$" + editText.Text;
                editText.SetSelection(editText.Text.Length);
            }

            // If there is no number then clear the percent sign
            if (editText.Text.Equals("$"))
            {
                editText.Text = string.Empty;
            }
        }

        private void Percent_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            EditText editText = sender as EditText;

            // If there is a number then add a percent sign
            if (!string.IsNullOrEmpty(editText.Text) && !editText.Text.Contains("%"))
            {
                editText.Text = editText.Text + "%";
                editText.SetSelection(editText.Text.Length - 1);
            }

            // If there is no number then clear the percent sign
            if(editText.Text.Equals("%"))
            {
                editText.Text = string.Empty;
            }
        }

        /// <summary>
        /// Event that checks all inputs to enable the calculate button or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            // Get references to all UI inputs to check
            EditText mortgageAmount = this.FindViewById<EditText>(Resource.Id.editTextMortgageAmount);
            EditText interestRate = this.FindViewById<EditText>(Resource.Id.editTextInterestRate);
            EditText years = this.FindViewById<EditText>(Resource.Id.editTextMortgagePeriod);
            Button calculateButton = this.FindViewById<Button>(Resource.Id.calculateButton);

            // If inputs are all filled then enabled calculate button
            calculateButton.Enabled = !(string.IsNullOrEmpty(mortgageAmount.Text) || string.IsNullOrEmpty(interestRate.Text) || string.IsNullOrEmpty(years.Text));
        }

        /// <summary>
        /// Calculates monthly cost of mortgage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateButton_Click(object sender, System.EventArgs e)
        {
            // Grab data from ui
            string mortgageAmount = this.FindViewById<EditText>(Resource.Id.editTextMortgageAmount).Text;
            string interestRate = this.FindViewById<EditText>(Resource.Id.editTextInterestRate).Text;
            string mortgagePeriod = this.FindViewById<EditText>(Resource.Id.editTextMortgagePeriod).Text;

            // Get calculator output
            string result = Manager.GetMonthlyCostCalculations(mortgageAmount, interestRate, mortgagePeriod);

            // Update result text view
            TextView resultTextView = this.FindViewById<TextView>(Resource.Id.calculatorOutput);
            resultTextView.Text = result;
        }
	}
}

