using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BumpBuster.Views
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            this.Content = new StackLayout
            {
				VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
					new Map (MapSpan.FromCenterAndRadius (new Position (37, -122), Distance.FromMiles (10))) {
						VerticalOptions = LayoutOptions.FillAndExpand
					},
                    new Label {
                        XAlign = TextAlignment.Center,
                        Text = "Welcome to Xamarin Forms!"
                    }
                }
            };
        }
    }
}
