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
    public partial class CommunicationSettingsModal : Form
    {
        private MainForm m_mf;
        XMLSerialization m_settings;

        public CommunicationSettingsModal()
        {
            InitializeComponent();

            //ayarları okuma
            m_settings = XMLSerialization.Singleton();
            m_settings = m_settings.DeSerialize(m_settings);


            //ayarları atama
            m_ıpAddressControlATSToOBATP.Text = m_settings.ATSToOBATPIPAddress;
            m_textBoxATSToOBATPPort.Text = m_settings.ATSToOBATPPort;

            m_ıpAddressControlOBATPToATS.Text = m_settings.OBATPToATSIPAddress;
            m_textBoxOBATPToATSPort.Text = m_settings.OBATPToATSPort;

            m_ıpAddressControlWSATCToOBATP.Text = m_settings.WSATCToOBATPIPAddress;
            m_textBoxWSATCToOBATPPort.Text = m_settings.WSATCToOBATPPort;

            m_ıpAddressControlOBATPToWSATC.Text = m_settings.OBATPToWSATCIPAddress;
            m_textBoxOBATPToWSATCPort.Text = m_settings.OBATPToWSATCPort;

            if (m_settings.OBATPToWSATCCommunicationType == Enums.CommunicationType.Server)
                m_radioButtonOBATPToWSATCServer.Checked = true;
            else
                m_radioButtonOBATPToWSATCClient.Checked = true;

            if (m_settings.OBATPToATSCommunicationType == Enums.CommunicationType.Server)
                m_radioButtonOBATPToATSServer.Checked = true;
            else
                m_radioButtonOBATPToATSClient.Checked = true;

            if (m_settings.ATSToOBATPCommunicationType == Enums.CommunicationType.Server)
                m_radioButtonATSToOBATPServer.Checked = true;
            else
                m_radioButtonATSToOBATPClient.Checked = true;

            if (m_settings.WSATCToOBATPCommunicationType == Enums.CommunicationType.Server)
                m_radioButtonWSATCToOBATPServer.Checked = true;
            else
                m_radioButtonWSATCToOBATPClient.Checked = true;
        }


        public CommunicationSettingsModal(MainForm mf) : this()
        {
            m_mf = mf;
        }

        private void m_buttonSave_Click(object sender, EventArgs e)
        { 
            m_settings.ATSToOBATPIPAddress = m_ıpAddressControlATSToOBATP.Text;
            m_settings.ATSToOBATPPort = m_textBoxATSToOBATPPort.Text;

            m_settings.OBATPToATSIPAddress = m_ıpAddressControlOBATPToATS.Text;
            m_settings.OBATPToATSPort = m_textBoxOBATPToATSPort.Text ;

            m_settings.WSATCToOBATPIPAddress = m_ıpAddressControlWSATCToOBATP.Text;
            m_settings.WSATCToOBATPPort = m_textBoxWSATCToOBATPPort.Text;

            m_settings.OBATPToWSATCIPAddress = m_ıpAddressControlOBATPToWSATC.Text;
            m_settings.OBATPToWSATCPort = m_textBoxOBATPToWSATCPort.Text;


            if (m_radioButtonOBATPToWSATCServer.Checked)
                m_settings.OBATPToWSATCCommunicationType = Enums.CommunicationType.Server;
            else
                m_settings.OBATPToWSATCCommunicationType = Enums.CommunicationType.Client;

            if (m_radioButtonOBATPToATSServer.Checked)
                m_settings.OBATPToATSCommunicationType = Enums.CommunicationType.Server;
            else
                m_settings.OBATPToATSCommunicationType = Enums.CommunicationType.Client; 

            if (m_radioButtonATSToOBATPServer.Checked)
                m_settings.ATSToOBATPCommunicationType = Enums.CommunicationType.Server;
            else
                m_settings.ATSToOBATPCommunicationType = Enums.CommunicationType.Client;


            if (m_radioButtonWSATCToOBATPServer.Checked)
                m_settings.WSATCToOBATPCommunicationType = Enums.CommunicationType.Server;
            else
                m_settings.WSATCToOBATPCommunicationType = Enums.CommunicationType.Client;  


            m_settings.Serialize(m_settings);
            m_settings = m_settings.DeSerialize(m_settings);

            if ((Button)sender == m_buttonApply)
                this.Close();

        }

        private void CommunicationSettingsModal_Load(object sender, EventArgs e)
        {

        }
    }
}
