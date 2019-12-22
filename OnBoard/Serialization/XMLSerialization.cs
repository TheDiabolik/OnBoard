﻿using System;
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



        //public string OBATO_to_WSATO_Sending_IPAddress { get; set; }
        //public string OBATP_to_OBATO_Sending_IPAddress { get; set; }
        //public string OBATO_to_ATS_Sending_IPAddress { get; set; }
        //public int ATS_to_OBATO_Port { get; set; }
        //public int OBATO_to_OBATP_Port { get; set; }
        //public int OBATP_to_OBATO_Port { get; set; }
        //public int OBATP_to_WSATP_Port { get; set; }
        //public int OBATO_to_ATS_Port { get; set; }
        //public int OBATO_to_WSATO_Port { get; set; }
        //public int TotalMilliseconds { get; set; }
        //public string ATS_to_OBATO_Receiving_IPAddress { get; set; }
        //public string WSATP_to_OBATP_Receiving_IPAddress { get; set; }
        //public string WSATO_to_OBATO_Receiving_IPAddress { get; set; }
        //public string OBATO_to_OBATP_Receiving_IPAddress { get; set; }
        //public string OBATP_to_OBATO_Receiving_IPAddress { get; set; }
        //public string OBATP_to_OBATP_Sending_IPAddress { get; set; }
        //public string OBATO_to_OBATP_Sending_IPAddress { get; set; }
        //public string OBATO_to_OBATO_Sending_IPAddress { get; set; }
        //public int WSATP_to_OBATP_Port { get; set; }
        //public int WSATO_to_OBATO_Port { get; set; }

        #region connection
        public string ATSToOBATPIPAddress { get; set; }
        public string ATSToOBATPPort { get; set; }

        public string OBATPToATSIPAddress { get; set; }
        public string OBATPToATSPort { get; set; }


        public string WSATCToOBATPIPAddress { get; set; }
        public string WSATCToOBATPPort { get; set; }

        public string OBATPToWSATCIPAddress { get; set; }
        public string OBATPToWSATCPort { get; set; }
        #endregion

        public int TrainLength { get; set; }
        public double MaxTrainDeceleration { get; set; }
        public double MaxTrainAcceleration { get; set; }
        public int TrainSpeedLimit { get; set; }



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
                    //this.ATS_to_OBATO_Port = 13400;
                    //this.OBATO_to_OBATP_Port = 14500;
                    //this.OBATP_to_OBATO_Port = 15400;
                    //this.OBATP_to_WSATP_Port = 15100;
                    //this.OBATO_to_ATS_Port = 14300;
                    //this.OBATO_to_WSATO_Port = 14200;
                    //this.TotalMilliseconds = 1000;
                    //this.WSATP_to_OBATP_Port = 11500;
                    //this.WSATO_to_OBATO_Port = 12400;

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
