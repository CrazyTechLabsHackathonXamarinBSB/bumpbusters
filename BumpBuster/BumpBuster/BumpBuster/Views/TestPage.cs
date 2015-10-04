using System;
using Xamarin.Forms;
using BumpBuster.Models;

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

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			var service = new BumpService();

			var list = service.ListAsync();
			var result = list.Result;


			// var result = new string[] { "Item 1" };

			listView.ItemsSource = result;
		}
	}
}

