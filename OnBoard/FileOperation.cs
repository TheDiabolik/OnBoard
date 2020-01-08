using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
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

        public static DataTable ReadSimulationRouteTableInExcel()
        {
            string dosya = "simülasyon rota çalışması.xlsx";
            string connString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=YES;IMEX=1""", dosya);

            OleDbConnection conn = new OleDbConnection(connString);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sayfa1$]", conn);
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            //dt.Rows[0].Delete();

            DataColumnCollection dtweqrewr = dt.Columns;
            DataRow sd33f = dt.Rows[0];
            DataRow sd33333f = dt.Rows[1];
            DataRow sd3333sdf3f = dt.Rows[2];
            DataRow sdf = dt.Rows[4];


            DataRow yedi = dt.Rows[7];

            var seeedf = sd33333f.ItemArray[2].ToString(); 


            return dt;
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

    }
}
