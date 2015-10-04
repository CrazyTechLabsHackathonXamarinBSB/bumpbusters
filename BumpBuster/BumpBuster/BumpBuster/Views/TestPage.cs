using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using BumpBuster.Models;

using XLabs.Platform.Services.Geolocation;

namespace BumpBuster
{
	public class TestPage : TabbedPage
	{
		ListView listView;

		public TestPage ()
		{
			this.listView = new ListView ();

			var list = new ContentPage {
				Title = "List",
				// Icon = "Icon-60.png",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					Children = {
						listView
					}
				}
			};


			var latitudeEntry = new Entry { Text = "" };
			var longitudeEntry = new Entry { Text = "" };
			var severityEntry = new Entry { Text = "" };
			var addButton = new Button { Text = "Add" };
			var deleteButton = new Button { Text = "Delete" };

			addButton.Clicked += async (object sender, EventArgs e) => {
				try {
					var service = new BumpService();
					var latitude = double.Parse(latitudeEntry.Text);
					var longitude = double.Parse(longitudeEntry.Text);
					var severity = int.Parse(severityEntry.Text);

					await service.AddAsync(latitude, longitude, (BumpSeverity)severity);
				} catch (Exception ex) {
					Debug.WriteLine(ex.Message);
					await DisplayAlert("Não foi possível executar a operação.", "Alerta", "Ok");
				}

			};

			deleteButton.Clicked += async (object sender, EventArgs e) => {
				try {
					var service = new BumpService();
					var latitude = double.Parse(latitudeEntry.Text);
					var longitude = double.Parse(longitudeEntry.Text);

					await service.DeleteAsync(latitude, longitude);

					await DisplayAlert("Operação concluída com sucesso!", "Alerta", "Ok");

				} catch (Exception ex) {
					Debug.WriteLine(ex.Message);
					await DisplayAlert("Não foi possível executar a operação.", "Alerta", "Ok");
				}
			};

			var edit = new ContentPage {
				Title = "Edit",
				// Icon = "Icon-60.png",
				Content = new StackLayout
				{
					Padding = new Thickness (15, 0, 15, 0),
					Orientation = StackOrientation.Vertical,
					VerticalOptions = LayoutOptions.Center,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Children = {
						new Label { Text = "Latitude" },
						latitudeEntry,
						new Label { Text = "Longitude" },
						longitudeEntry,
						new Label { Text = "Severity" },
						severityEntry,
						addButton,
						deleteButton
					}
				}
			};

			this.Children.Add (list);
			this.Children.Add (edit);
		}

		protected override async void OnAppearing ()
		{
			await LoadAsync();
		}

		private async Task LoadAsync() {
			var service = new BumpService();
			var result = await service.ListAsync();

			listView.ItemsSource = result;
		}
	}
}

