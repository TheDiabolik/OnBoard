using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    [Serializable]

    public class XMLSerialization
    {
        private static XMLSerialization m_xmlSerialization = new XMLSerialization();

        public XMLSerialization()
        {
        }

        #region singleton
        public static XMLSerialization Singleton()
        {
            return m_xmlSerialization;
        }
        #endregion

        #region properties 


 

        #region connection
        public string ATSToOBATPIPAddress { get; set; }
        public string ATSToOBATPPort { get; set; }
        public Enums.CommunicationType ATSToOBATPCommunicationType { get; set; }

        public string OBATPToATSIPAddress { get; set; }
        public string OBATPToATSPort { get; set; }
        public Enums.CommunicationType OBATPToATSCommunicationType { get; set; }

        public string WSATCToOBATPIPAddress { get; set; }
        public string WSATCToOBATPPort { get; set; }
        public Enums.CommunicationType WSATCToOBATPCommunicationType { get; set; }
        public string OBATPToWSATCIPAddress { get; set; }
        public string OBATPToWSATCPort { get; set; }
        public Enums.CommunicationType OBATPToWSATCCommunicationType { get; set; }
        #endregion

        public int TrainLength { get; set; }
        public double MaxTrainDeceleration { get; set; }
        public double MaxTrainAcceleration { get; set; }
        public int TrainSpeedLimit { get; set; }



        public int StartTrackID { get; set; }
        public int EndTrackID { get; set; }
        public Enums.TrackInput  TrackInput{ get; set; }

       
        public DataTable RouteTrack { get; set; } = new DataTable();



        public decimal TrainFrequency { get; set; }


        private HashSet<int> m_trains = new HashSet<int>();

        public HashSet<int> Trains
        {
            get { return m_trains; }
            set { m_trains = value; }
        }


        #endregion
        public void CheckSerializationFile()
        {
            try
            {
                //kayıt dosyası exe ile aynı yerde olması istendiği için comment yapıldı
                if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.Settings)))
                    Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.Settings));

                //xmlserilization dosyasını kontrol ediyoruz
                if (!File.Exists(SerializationPaths.Settings))
                {
                    this.OBATPToATSIPAddress = "127.0.0.1";
                    this.OBATPToATSPort = 10201.ToString();
                    OBATPToATSCommunicationType = Enums.CommunicationType.Client;

                    this.ATSToOBATPIPAddress = "127.0.0.1";
                    this.ATSToOBATPPort = 12101.ToString();
                    ATSToOBATPCommunicationType = Enums.CommunicationType.Client;

                    this.OBATPToWSATCIPAddress = "127.0.0.1";
                    this.OBATPToWSATCPort = 14001.ToString();
                    OBATPToWSATCCommunicationType = Enums.CommunicationType.Client;

                    this.WSATCToOBATPIPAddress = "127.0.0.1";
                    this.WSATCToOBATPPort = 11001.ToString();
                    WSATCToOBATPCommunicationType = Enums.CommunicationType.Client;


                    this.Serialize(XMLSerialization.Singleton());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.CheckSerilizationFileExceptionMessage, ex);
            }
        }


        public void Serialize(XMLSerialization serialization)
        {
            Serialization.Serialize(SerializationPaths.Settings, serialization);
        }

        public void SerializeBinary(XMLSerialization serialization)
        {
            Serialization.SerializeBinary(SerializationPaths.BinarySettings, serialization);
        }

        public XMLSerialization DeSerialize(XMLSerialization serialization)
        {
            CheckSerializationFile();
            return Serialization.DeSerialize(SerializationPaths.Settings, serialization);
        }

        public XMLSerialization DeSerializeBinary(XMLSerialization serialization)
        {
            CheckSerializationFile();
            return Serialization.DeSerializeBinary(SerializationPaths.BinarySettings, serialization);
        }

    }
}
