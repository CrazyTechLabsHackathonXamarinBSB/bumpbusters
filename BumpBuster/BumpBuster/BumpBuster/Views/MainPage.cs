using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Diagnostics;
using XLabs.Platform.Services.Geolocation;
using XLabs.Platform.Device;
using XLabs.Ioc;

namespace BumpBuster.Views
{
	public class MainPage : ContentPage
	{


		Map map;

		public MainPage()
		{
			map = new Map { 
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};


			// add the slider
			var slider = new Slider (1, 18, 1);
			slider.ValueChanged += (sender, e) => {
				var zoomLevel = e.NewValue; // between 1 and 18
				var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));

				Debug.WriteLine(zoomLevel + " -> " + latlongdegrees);
				if (map.VisibleRegion != null)
					map.MoveToRegion(new MapSpan (map.VisibleRegion.Center, latlongdegrees, latlongdegrees)); 
			};


			// create map style buttons
			var street = new Button { Text = "Street" };
			var hybrid = new Button { Text = "Hybrid" };
			var satellite = new Button { Text = "Satellite" };
			street.Clicked += HandleClicked;
			hybrid.Clicked += HandleClicked;
			satellite.Clicked += HandleClicked;
			var segments = new StackLayout { Spacing = 30,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Orientation = StackOrientation.Horizontal, 
				Children = {street, hybrid, satellite},
			};


			//			if (geoLocator.IsGeolocationAvailable == true) {
			//				geoLocator.PositionChanged
			//			}

			this.Content = new StackLayout
			{
				Children = {segments, map, slider}

			};

		}

		void HandleClicked (object sender, EventArgs e)
		{
			var b = sender as Button;
			switch (b.Text) {
			case "Street":
				map.MapType = MapType.Street;
				break;
			case "Hybrid":
				map.MapType = MapType.Hybrid;
				break;
			case "Satellite":
				map.MapType = MapType.Satellite;
				break;
			}
		}

		protected override async void OnAppearing ()
		{
			await GetPosition ();
		}

		async Task GetPosition ()
		{
			// Inicia o Acelelometro
			var device = Resolver.Resolve<IDevice>();
			Debug.WriteLine (device.FirmwareVersion);


			var geolocator = DependencyService.Get<IGeolocator> ();
			//geolocator.PositionError += OnPositionError;
			//geolocator.PositionChanged += OnPositionChanged;

			if (!geolocator.IsListening)
				geolocator.StartListening(minTime: 1000, minDistance: 0);

			var position = await geolocator.GetPositionAsync (timeout: 10000);

			string message = string.Format ("Latitude: {0} | Longitude: {1}", position.Latitude, position.Longitude);

			Debug.WriteLine (message);
		}

		void OnPositionError (object sender, PositionErrorEventArgs e) {
			string message = string.Format ("Error: {0}", e.Error);

			Debug.WriteLine (message);
		}

		void OnPositionChanged (object sender, PositionEventArgs e) {
			var position = e.Position;
			string message = string.Format ("Latitude: {0} | Longitude: {1}", position.Latitude, position.Longitude);

			Debug.WriteLine (message);
		}
	}
}