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
    public partial class GeneralSettingsModal : Form
    {
        private MainForm m_mf;
        XMLSerialization m_settings;


        public GeneralSettingsModal()
        {
            InitializeComponent();

            //ayarları okuma
            m_settings = XMLSerialization.Singleton();
            m_settings = m_settings.DeSerialize(m_settings);



            m_textBoxStartRangeTrackID.Text = m_settings.StartTrackID.ToString();
            m_textBoxEndRangeTrackID.Text = m_settings.EndTrackID.ToString();


            if (m_settings.TrackInput == Enums.TrackInput.Manuel)
                m_radioButtonManuelInputTracks.Checked = true;
            else
                m_radioButtonFromFileTracks.Checked = true;
             


            m_numericUpDownTrainFrequency.Value = m_settings.TrainFrequency;
        }



        public GeneralSettingsModal(MainForm mf) : this()
        {
            m_mf = mf;
        }
        private void GeneralSettingsModal_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 31; i++)
            {
                m_checkedListBoxTrains.Items.Add("Train" + i.ToString());
            }

            //checklist tablosunu ayarlama
            foreach (int index in m_settings.Trains)
                m_checkedListBoxTrains.SetItemChecked(index, true);
        }

        private void m_buttonSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_checkedListBoxTrains.Items.Count; i++)
            {
                CheckState cs = m_checkedListBoxTrains.GetItemCheckState(i);

                if (cs == CheckState.Checked)
                    m_settings.Trains.Add(i);
                else if (cs == CheckState.Unchecked)
                    m_settings.Trains.Remove(i);
            }


            m_settings.TrainFrequency = m_numericUpDownTrainFrequency.Value;




            m_settings.StartTrackID = Convert.ToInt32(m_textBoxStartRangeTrackID.Text);
            m_settings.EndTrackID = Convert.ToInt32(m_textBoxEndRangeTrackID.Text);

            if (m_radioButtonManuelInputTracks.Checked)
                m_settings.TrackInput = Enums.TrackInput.Manuel;

            if (m_radioButtonFromFileTracks.Checked)
                m_settings.TrackInput = Enums.TrackInput.FromFile;


            m_settings.Serialize(m_settings);

            m_settings = m_settings.DeSerialize(m_settings);

            if ((Button)sender == m_buttonApply)
                this.Close();
        }

        private void GeneralSettingsModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_settings = m_settings.DeSerialize(m_settings);


            MainForm.m_allOBATP.Clear();
            MainForm.m_mf.m_comboBoxTrain.Items.Clear();

            foreach (int index in m_settings.Trains)
            { 
                int trainIndex = index + 1;
                Enums.Train_ID train_ID = (Enums.Train_ID)trainIndex;

                OBATP OBATP = new OBATP((Enums.Train_ID)trainIndex, m_settings.MaxTrainAcceleration, m_settings.MaxTrainDeceleration, m_settings.TrainSpeedLimit, m_settings.TrainLength, MainForm.m_route);

                MainForm.m_allOBATP.TryAdd(trainIndex, OBATP);

                MainForm.m_mf.m_comboBoxTrain.Items.Add(train_ID.ToString());
            }

        }
    }
}
