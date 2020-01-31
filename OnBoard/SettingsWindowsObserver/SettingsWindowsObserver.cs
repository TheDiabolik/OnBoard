using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnBoard 
{
    public class SettingsWindowsObserver : ISettingsWindowsObserver
    {
          private ThreadSafeList<ISettingsWindowsWatcher> m_settingsWindowWatcher = new ThreadSafeList<ISettingsWindowsWatcher>();
        private Enums.SettingsWindowStatus m_settingsWindowStatus;
        private Enums.SettingsWindow m_settingsWindow;

        public void InformSettingsWindowWatcher()
        {
            foreach (ISettingsWindowsWatcher watcher in m_settingsWindowWatcher)
            {
                watcher.SettingsWindowStatus(m_settingsWindowStatus, m_settingsWindow);
            }
        }


        public void AddSettingsWindowWatcher(ISettingsWindowsWatcher watcher)
        {
            m_settingsWindowWatcher.Add(watcher);
        }

        public void SettingsWindowStatus(Enums.SettingsWindowStatus settingsWindowStatus, Enums.SettingsWindow settingsWindow)
        {
            m_settingsWindowStatus = settingsWindowStatus;
            m_settingsWindow = settingsWindow;





            if (settingsWindowStatus == Enums.SettingsWindowStatus.Open)
            {
                DialogResult dr = MessageBox.Show("If you open the settings, the program will stop working.\nDo you really want to open the settings ? ", "OnBoard", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    InformSettingsWindowWatcher();
                }
            }
            else if (settingsWindowStatus == Enums.SettingsWindowStatus.Close)
            {
                DialogResult dr = MessageBox.Show("OnBoard will start working with the new settings.", "OnBoard", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    InformSettingsWindowWatcher();
                }
            }
        }
    }
}
