using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard 
{
    class veri
    {
        public async Task<List<Track>> AsycSelectYNK1_KIR2()
        { 
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
            {
                List<Track> exitDates = new List<Track>();

                try
                {
                    await conn.OpenAsync();

                    string selectsyncFileNames = "SELECT *  FROM YNK1_KIR2"; 

                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = conn;
                    command.CommandText = selectsyncFileNames;
                    //command.Parameters.AddWithValue("@Plate", plate);


                    DbDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Track track = new Track();

                        track.Station_Name = reader["StationName"].ToString();
                        track.Track_ID = int.Parse(reader["Track"].ToString());
                        track.Track_Length = int.Parse(reader["Length"].ToString());
                        track.StartPositionInRoute = int.Parse(reader["StartPositionInRoute"].ToString());
                        track.StopPositionInRoute = int.Parse(reader["StopPositionInRoute"].ToString());

                        exitDates.Add(track);
                    }

                    reader.Dispose();
                    command.Dispose(); 
                


                    return exitDates;
                }
                catch (Exception ex)
                {
                    Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "AsycSelectYNK1_KIR2()");
                    return exitDates;
                }
            }
        }

        public async Task<List<Track>> AsycSelectKIR2_YNK1()
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
            {
                List<Track> exitDates = new List<Track>();

                try
                {
                    await conn.OpenAsync();

                    string selectsyncFileNames = "SELECT *  FROM KIR2_YNK1";

                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = conn;
                    command.CommandText = selectsyncFileNames;
                    //command.Parameters.AddWithValue("@Plate", plate);


                    DbDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Track track = new Track();

                        track.Station_Name = reader["StationName"].ToString();
                        track.Track_ID = int.Parse(reader["Track"].ToString());
                        track.Track_Length = int.Parse(reader["Length"].ToString());
                        track.StartPositionInRoute = int.Parse(reader["StartPositionInRoute"].ToString());
                        track.StopPositionInRoute = int.Parse(reader["StopPositionInRoute"].ToString());

                        exitDates.Add(track);
                    }

                    reader.Dispose();
                    command.Dispose();



                    return exitDates;
                }
                catch (Exception ex)
                {
                    Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "AsycSelectKIR2_YNK1()");
                    return exitDates;
                }
            }
        }

        public async Task<List<Track>> AsycSelectYNK1_KIR2_YNK1()
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
            {
                List<Track> exitDates = new List<Track>();

                try
                {
                    await conn.OpenAsync();

                    string selectsyncFileNames = "SELECT *  FROM YNK1_KIR2_YNK1";

                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = conn;
                    command.CommandText = selectsyncFileNames;
                    //command.Parameters.AddWithValue("@Plate", plate);


                    DbDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Track track = new Track();

                        track.Station_Name = reader["StationName"].ToString();
                        track.Track_ID = int.Parse(reader["Track"].ToString());
                        track.Track_Length = int.Parse(reader["Length"].ToString());
                        track.StartPositionInRoute = int.Parse(reader["StartPositionInRoute"].ToString());
                        track.StopPositionInRoute = int.Parse(reader["StopPositionInRoute"].ToString());

                        exitDates.Add(track);
                    }

                    reader.Dispose();
                    command.Dispose();



                    return exitDates;
                }
                catch (Exception ex)
                {
                    Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "AsycSelectYNK1_KIR2()");
                    return exitDates;
                }
            }
        }

        public DataTable FillDataSet()
        {
            DataTable dt = new DataTable("HAV");

            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
            {
                try
                {
                    string selectFixedParking = "SELECT * FROM HAV";
                    SQLiteDataAdapter sqliteDataAdapterFixedParking = new SQLiteDataAdapter(selectFixedParking, conn);
                    sqliteDataAdapterFixedParking.Fill(dt);
                    //long autoIncrementSeedFixedParking = GetNextAutoincrementValue(conn, "FixedParking");

                    //dt.Columns["ID"].AutoIncrement = true;
                    //dt.Columns["ID"].AutoIncrementSeed = autoIncrementSeedFixedParking;
                    //dt.Columns["ID"].AutoIncrementStep = 1;

                    //DataColumn[] keysFixedParking = new DataColumn[1];
                    //keysFixedParking[0] = dt.Columns["ID"];
                    //dt.PrimaryKey = keysFixedParking;

                    sqliteDataAdapterFixedParking.Dispose();

                    return dt;
                }
                catch(Exception ex)
                {
                    //MessageBox.Show("Bilgiler Veritabanından Okunamadı!", "Uyarı");
                    return dt;
                }
            }

        }


        public async Task<int> AsyncInsert(List<string> value)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
            {
                int result = 0;
                try
                {
                    await conn.OpenAsync();

                    SQLiteCommand command = new SQLiteCommand("insert into HAV2_YNK2 (StationName, Track, Length, StartPositionInRoute, StopPositionInRoute) values (@StationName, @Track, @Length, @StartPositionInRoute, @StopPositionInRoute)",
                       conn);

                    //SQLiteCommand command = new SQLiteCommand("insert into YNK2_HAV2 (StationName, Track, Length) values (@StationName, @Track, @Length)",
                    //  conn);
                    command.Parameters.AddWithValue("@StationName", value[0]);
                    command.Parameters.AddWithValue("@Track", value[1]);
                    command.Parameters.AddWithValue("@Length", value[2]);
                    command.Parameters.AddWithValue("@StartPositionInRoute", value[3]);
                    command.Parameters.AddWithValue("@StopPositionInRoute", value[4]);

                    result = await command.ExecuteNonQueryAsync();

                    command.Dispose();

                    return result;
                }
                catch (Exception ex)
                {
                    Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "AsyncInsert(List<string> value)");
                    return result;
                }
            }
        }

        public async Task<int> AsyncInsertCircle(List<string> value)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString.CnnString))
            {
                int result = 0;
                try
                {
                    await conn.OpenAsync();

                    SQLiteCommand command = new SQLiteCommand("update YNK1_KIR2_YNK1 set StartPositionInRoute=@StartPositionInRoute, StopPositionInRoute=@StopPositionInRoute  where ID=@ID",
                       conn);

                    //SQLiteCommand command = new SQLiteCommand("insert into YNK2_HAV2 (StationName, Track, Length) values (@StationName, @Track, @Length)",
                    //  conn);
                    //command.Parameters.AddWithValue("@StationName", value[0]);
                    //command.Parameters.AddWithValue("@Track", value[1]);
                    //command.Parameters.AddWithValue("@Length", value[2]);
                    command.Parameters.AddWithValue("@StartPositionInRoute", value[0]);
                    command.Parameters.AddWithValue("@StopPositionInRoute", value[1]);
                    command.Parameters.AddWithValue("@ID", value[2]);
                    result = await command.ExecuteNonQueryAsync();

                    command.Dispose();

                    return result;
                }
                catch (Exception ex)
                {
                    Logging.WriteLog(ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "AsyncInsert(List<string> value)");
                    return result;
                }
            }
        }
    }
}
