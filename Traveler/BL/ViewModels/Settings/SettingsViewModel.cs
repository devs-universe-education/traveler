using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public override Task OnPageAppearing()
        {
            PushesEnabled = CrossSettings.Current.GetValueOrDefault(nameof(PushesEnabled), true);
            return base.OnPageAppearing();
        }
    }
}
