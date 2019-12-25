using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnBoard
{
    public partial class TrainSettingsModal : Form
    {
        private MainForm m_mf;
        XMLSerialization m_settings;

        public TrainSettingsModal()
        {
            InitializeComponent();

            //ayarları okuma
            m_settings = XMLSerialization.Singleton();
            m_settings = m_settings.DeSerialize(m_settings);

            //ayarları atama
            m_textBoxTrainLength.Text = m_settings.TrainLength.ToString();
            m_textBoxTrainDeceleration.Text = m_settings.MaxTrainDeceleration.ToString();
            m_textBoxTrainAcceleration.Text = m_settings.MaxTrainAcceleration.ToString();
            m_textBoxTrainSpeedLimit.Text = m_settings.TrainSpeedLimit.ToString();


          
        }

        public TrainSettingsModal(MainForm mf) : this()
        {
            m_mf = mf;
        }

        private void TrainSettings_Load(object sender, EventArgs e)
        {
            
        }

        private void m_buttonSave_Click(object sender, EventArgs e)
        {
            m_settings.TrainLength = Convert.ToInt32(m_textBoxTrainLength.Text);
            m_settings.MaxTrainDeceleration = Convert.ToDouble(m_textBoxTrainDeceleration.Text);
            m_settings.MaxTrainAcceleration = Convert.ToDouble(m_textBoxTrainAcceleration.Text);
            m_settings.TrainSpeedLimit = Convert.ToInt32(m_textBoxTrainSpeedLimit.Text);

            m_settings.Serialize(m_settings);

            m_settings = m_settings.DeSerialize(m_settings);

            if ((Button)sender == m_buttonApply)
                this.Close();
        }
    }
}
