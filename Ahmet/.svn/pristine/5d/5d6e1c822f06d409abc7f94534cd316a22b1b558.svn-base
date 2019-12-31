using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    class FileOperation
    { 
        public static DataTable ReadTrackTableInExcel()
        {
            string dosya = "HatHaritası.xlsx";
            string connString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0", dosya);

            OleDbConnection conn = new OleDbConnection(connString);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [TrackDatabase$]", conn);
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataColumnCollection dtweqrewr = dt.Columns;
            DataRow sdf = dt.Rows[4];

            return dt;
        }

        public static DataTable ReadRouteTableInExcel()
        {
            string dosya = "RotaTablosu.xlsx";
            //string connString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0", dosya);
            string connString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=NO;""", dosya);

            OleDbConnection conn = new OleDbConnection(connString);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Rota Tablosu$]", conn);
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataColumnCollection dtweqrewr = dt.Columns;
            DataRow sdf = dt.Rows[2];

            return dt;
        }
}
}
