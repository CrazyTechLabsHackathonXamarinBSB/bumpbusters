using System;
using Xamarin.Forms;
using BumpBuster.Models;

using XLabs.Platform.Services.Geolocation;

namespace BumpBuster
{
	public class TestPage : ContentPage
	{
		ListView listView;

		public TestPage ()
		{
			this.listView = new ListView ();

			this.Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					listView
				}
			};
		}

		protected override async void OnAppearing ()
		{
			var service = new BumpService();

			var result = await service.ListAsync();

			listView.ItemsSource = result;
		}
	}
}

