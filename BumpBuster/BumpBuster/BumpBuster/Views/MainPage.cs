using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BumpBuster.Views
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    new Label {
                        XAlign = TextAlignment.Center,
                        Text = "Welcome to Xamarin Forms!"
                    }
                }
            };
        }
    }
}
