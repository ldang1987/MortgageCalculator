using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Content.PM;

namespace AffiniteGames.MortgageCalculatorAndroid
{
    [Activity(Label = "Settings", ScreenOrientation = ScreenOrientation.Portrait)]			
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Settings);

            this.ActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    // NavUtils.NavigateUpFromSameTask(this);
                    Intent activityIntent = new Intent(this, typeof(MainActivity));
                    StartActivity(activityIntent);
                    this.Finish();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

