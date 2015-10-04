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
using BumpBuster.Models;

namespace BumpBuster.Views
{
	public class MainPage : ContentPage
	{


		Map map;

		Double gForce;

		IGeolocator geolocator;

		public MainPage()
		{
			var pos = new Xamarin.Forms.Maps.Position (-15.8046057, -47.896674);
			var Span = MapSpan.FromCenterAndRadius (pos, Distance.FromMiles (0.3));
			map = new Map (Span){ 
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
				Children = {map, segments, slider}

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


			this.geolocator = DependencyService.Get<IGeolocator> ();
			//geolocator.PositionError += OnPositionError;
			//geolocator.PositionChanged += OnPositionChanged;

			if (!geolocator.IsListening)
				geolocator.StartListening(minTime: 1000, minDistance: 0);

			var position = await geolocator.GetPositionAsync (timeout: 10000);

			string message = string.Format ("Latitude: {0} | Longitude: {1}", position.Latitude, position.Longitude);

			Debug.WriteLine (message);

			device.Accelerometer.ReadingAvailable += getEixos;


		}

		async private void getEixos(object sender, XLabs.EventArgs<XLabs.Vector3> e){
			var x = e.Value.X;
			var y = e.Value.Y;
			var z = e.Value.Z;

			gForce = Math.Sqrt ((x * x) + (y * y) + (z * z));

			if (gForce > 2 && gForce <= 5) {

				var service = new BumpService ();

				var position = await this.geolocator.GetPositionAsync (timeout: 10000);

				await service.AddAsync(position.Latitude, position.Longitude, (int)gForce);

				var list = await service.ListAsync ();

				foreach (var pino in list) {
					SetPinOnMap (pino.Latitude, pino.Longitude, pino.Severity);
				} 


			}

			Debug.WriteLine ("Forca G: " + gForce);

		}


		void SetPinOnMap(Double Latitude, Double Longitude, int severidade){


			var position = new Xamarin.Forms.Maps.Position(Latitude, Longitude); // Latitude, Longitude

			PinType pinType = new PinType();
			if (severidade < 2) {
				pinType = PinType.Place;
			}else{
				pinType = PinType.SavedPin;
			}

			var pin = new Pin {
				Type = pinType,
				Position = position,
				Label = "Bump",
				Address = "Endereco do defeito"
			};



			map.Pins.Add(pin);



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