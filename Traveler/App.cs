using Traveler.DAL.DataServices;
using Traveler.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Traveler
{
	public class App : Application
	{
		public App ()
		{
			DialogService.Init(this);
			NavigationService.InitTabbed(AppPages.Display, AppPages.Calendar, AppPages.Settings);
			DataServices.Init(true);
		}

        protected override void OnStart()
        {

        }
	}
}

