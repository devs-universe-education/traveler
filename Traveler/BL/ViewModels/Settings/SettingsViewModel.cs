using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
