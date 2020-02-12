using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mine.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage
	{
		public GamePage ()
		{
			InitializeComponent ();
		}

        public void GameButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("SU", "Go RedHawks", "OK");
        }
    }
}