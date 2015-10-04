using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BumpBuster.Droid
{
    [Activity(Label = "BumpBuster", Icon = "@drawable/icon", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			Xamarin.Forms.DependencyService.Register<XLabs.Platform.Services.Geolocation.Geolocator> ();
			//Xamarin.Forms.DependencyService.Register<XLabs.Platform.Device.AndroidDevice> ();

			var container = new XLabs.Ioc.SimpleContainer ();
			container.Register<XLabs.Platform.Device.IDevice, XLabs.Platform.Device.AndroidDevice> ();

			XLabs.Ioc.Resolver.SetResolver (container.GetResolver ());

            global::Xamarin.Forms.Forms.Init(this, bundle);
			global::Xamarin.FormsMaps.Init(this, bundle);
			global::Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init ();

            LoadApplication(new App());
        }
    }
}

