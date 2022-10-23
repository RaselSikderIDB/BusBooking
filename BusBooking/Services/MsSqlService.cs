using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking.Services
{
    public class MsSqlService
    {
        public DataTable Get(string connectionString, string query)
        {
            string connStr = connectionString;
            var conn = new SqlConnection(connStr);
            var cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 30;
            var da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            conn.Open();
            var ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            return ds.Tables[0];
        }

        public async Task<DataTable> Get(string connectionString, string query, bool async)
        {
            string connStr = connectionString;
            var conn = new SqlConnection(connStr);
            var cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 30;
            var da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            await conn.OpenAsync();
            var ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            return ds.Tables[0];
        }

        public bool Set(string connectionString, string nonQuery)
        {
            bool status = false;
            string connStr = connectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction tran;


                tran = conn.BeginTransaction("oneTran");


                cmd.Connection = conn;
                cmd.Transaction = tran;

                try
                {

                    cmd.CommandText = nonQuery;
                    cmd.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    tran.Commit();
                    //Console.WriteLine("Both records are written to database.");
                    status = true;
                }
                catch //(Exception ex)
                {

                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception ex2)
                    {


                        throw ex2;
                    }
                }
            }
            return status;
        }

        public int Set(string connectionString, string[] nonQueries)
        {
            //bool status = false;
            int nEffect = 0;
            string connStr = connectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction tran;


                tran = conn.BeginTransaction("oneTran");


                cmd.Connection = conn;
                cmd.Transaction = tran;

                try
                {
                    if (nonQueries != null && nonQueries.Length > 0)
                    {
                        for (int i = 0; i < nonQueries.Length; i++)
                        {
                            cmd.CommandText = nonQueries[i];
                            nEffect += cmd.ExecuteNonQuery();
                        }
                    }


                    tran.Commit();

                }
                catch //(Exception ex)
                {

                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception ex2)
                    {


                        throw ex2;
                    }
                }
            }
            return nEffect;
        }

    }
}
