using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace PickTheName_db
{
    public class Settings
    {
        public static IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public static void checkSettings()
        {
            if (!settings.Contains(DataStrings.setting_nameid))
            {
                settings.Add(DataStrings.setting_nameid, 1);
            }
        }
    }
}
