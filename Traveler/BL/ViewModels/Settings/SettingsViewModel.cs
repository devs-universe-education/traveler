using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Traveler.BL.ViewModels.Settings
{
    class SettingsViewModel : BaseViewModel
    {
        public List<string> ThemeList
        {
            get => Get<List<string>>();
            private set => Set(value);
        }

        public string ThemeItem
        {
            get => Get<string>();
            set => Set(value);
        }

        public SettingsViewModel()
        {
            ThemeList = new List<string>()
            {
                "Светлая тема",
                "Темная тема"
            };
        }

        public bool PushesEnabled
        {
            get => Get<bool>();
            set
            {
                Set(value);
                CrossSettings.Current.AddOrUpdateValue(nameof(PushesEnabled), value);
            }
        }

        public ICommand ShowAboutCommand
        {
            get
            {
                return new Command(
                    execute: () =>
                    {
                        string text = "Данный продукт разработан в рамках курса \"Разработка кросплатформенных приложений на Xamarin.Forms\" студентами группы бВм-41.\n\n" +
                            "Веркошанский М.В.\nПруткова С.А.\nЯгодницин А.С.\n\n" +
                            "Binwell University, ВГТУ, 2019.";

                        ShowAlert("", text, "OK");
                    });
            }
        }

        public override Task OnPageAppearing()
        {
            PushesEnabled = CrossSettings.Current.GetValueOrDefault(nameof(PushesEnabled), true);
            return base.OnPageAppearing();
        }
    }
}
