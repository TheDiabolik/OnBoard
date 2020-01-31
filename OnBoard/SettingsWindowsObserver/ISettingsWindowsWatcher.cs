using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard 
{
    public  interface ISettingsWindowsWatcher
    {
        void SettingsWindowStatus(Enums.SettingsWindowStatus settingsWindowStatus, Enums.SettingsWindow settingsWindow);
        //void SettingsWindowOpen();
        //void SettingsWindowClose();
    }
}
