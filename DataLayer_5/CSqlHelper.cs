using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility_5;

namespace DataLayer_5
{
    public class CSqlHelper
    {
        /// <summary>
        /// Tạo connection mới
        /// </summary>
        /// <param name="p_strConnStr">Connection string</param>
        /// <returns></returns>
        public static SqlConnection CreateConnection(string p_strConnStr)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = p_strConnStr;
            return conn;
        }

        /// <summary>
        /// Thực thi một store với các tham số truyền vào
        /// </summary>
        /// <param name="p_strSPname">Store Name</param>
        /// <param name="p_arrParameter">Danh sách các tham số truyền vào</param>
        public static int ExecuteNonquery(string p_strConnStr, string p_strSPname, params object[] p_arrValue)
        {
            if ((p_arrValue != null) && (p_arrValue.Length > 0))
            {
                // Tạo danh sách SqlParameter
                SqlParameter[] arrSQLParameter = CSqlHelperParameterCache.GetSpParameterSet(
                    p_strConnStr, p_strSPname);

                // Gán dữ liệu từ các mãng value vô mảng command parameter
                AssignParameterValues(arrSQLParameter, p_arrValue);

                // gọi hàm overload
                return ExecuteNonQuery(p_strConnStr, p_strSPname, arrSQLParameter);
            }

            else
            {
                return ExecuteNonQuery(p_strConnStr, p_strSPname, null);
            }
        }

        /// <summary>
        /// Thực thi một store với các tham số truyền vào
        /// </summary>
        /// <param name="p_strSPname">Store Name</param>
        /// <param name="p_arrParameter">Danh sách các tham số truyền vào</param>
        public static int ExecuteNonquery(SqlConnection p_objConn, SqlTransaction p_objTrans, string p_strConnString,
            string p_strSPname, params object[] p_arrValue)
        {
            if ((p_arrValue != null) && (p_arrValue.Length > 0))
            {
                // Tạo danh sách SqlParameter
                SqlParameter[] arrSQLParameter = CSqlHelperParameterCache.GetSpParameterSet(
                    p_strConnString, p_strSPname);

                // Gán dữ liệu từ các mãng value vô mảng command parameter
                AssignParameterValues(arrSQLParameter, p_arrValue);

                // gọi hàm overload
                return ExecuteNonQuery(p_objConn, p_objTrans, p_strSPname, arrSQLParameter);
            }

            else
            {
                return ExecuteNonQuery(p_strSPname, null);
            }
        }

        /// <summary>
        /// Thực thi 1 store dưới dạng Scalar với các tham số truyền vào
        /// </summary>
        /// <param name="p_strSPname">Store Name</param>
        /// <param name="p_arrParameter">Danh sách các tham số truyền vào</param>
        /// <returns>object</returns>
        public static object ExecuteScalar(string p_strConnStr, string p_strSPname, params object[] p_arrValue)
        {
            if ((p_arrValue != null) && (p_arrValue.Length > 0))
            {
                // Tạo danh sách SqlParameter
                SqlParameter[] arrSQLParameter = CSqlHelperParameterCache.GetSpParameterSet(
                    p_strConnStr, p_strSPname);

                // Gán dữ liệu từ các mãng value vô mảng command parameter
                AssignParameterValues(arrSQLParameter, p_arrValue);

                // gọi hàm overload
                return ExecuteScalar(p_strConnStr, p_strSPname, arrSQLParameter);
            }

            else
            {
                return ExecuteScalar(p_strConnStr, p_strSPname, null);
            }
        }

        /// <summary>
        /// Thực thi 1 store dưới dạng Scalar với các tham số truyền vào
        /// </summary>
        /// <param name="p_strSPname">Store Name</param>
        /// <param name="p_arrParameter">Danh sách các tham số truyền vào</param>
        /// <returns>object</returns>
        public static object ExecuteScalar(SqlConnection p_conn, SqlTransaction p_trans, string p_strConnStr, string p_strSPname,
            params object[] p_arrValue)
        {
            if ((p_arrValue != null) && (p_arrValue.Length > 0))
            {
                // Tạo danh sách SqlParameter
                SqlParameter[] arrSQLParameter = CSqlHelperParameterCache.GetSpParameterSet(
                    p_strConnStr, p_strSPname);

                // Gán dữ liệu từ các mãng value vô mảng command parameter
                AssignParameterValues(arrSQLParameter, p_arrValue);

                // gọi hàm overload
                return ExecuteScalar(p_conn, p_trans, p_strSPname, arrSQLParameter);
            }

            else
            {
                return ExecuteScalar(p_strConnStr, p_strSPname, null);
            }
        }

        /// <summary>
        /// Fill dữ liệu vào dataset dựa trên store name và danh sách parameter truyền vào
        /// </summary>
        /// <param name="p_dsData">DataSet cần truyền vào</param>
        /// <param name="p_strSPname">Store Name</param>
        /// <param name="p_arrParameter">Danh sách các tham số</param>
        public static void FillDataSet(string p_strConnStr, DataSet p_dsData, string p_strSPname,
            params object[] p_arrValue)
        {
            if ((p_arrValue != null) && (p_arrValue.Length > 0))
            {
                // Tạo danh sách SqlParameter
                SqlParameter[] arrSQLParameter = CSqlHelperParameterCache.GetSpParameterSet(
                    p_strConnStr, p_strSPname);

                // Gán dữ liệu từ các mãng value vô mảng command parameter
                AssignParameterValues(arrSQLParameter, p_arrValue);

                // gọi hàm overload
                FillDataSet(p_strConnStr, p_dsData, p_strSPname, arrSQLParameter);
            }

            else
            {
                FillDataSet(p_strConnStr, p_dsData, p_strSPname, null);
            }
        }

        /// <summary>
        /// Fill dữ liệu vô datatable dựa trên store name và danh sách parammeter truyền vào
        /// </summary>
        /// <param name="p_dtData">DataTable cần thêm dữ liệu vào</param>
        /// <param name="p_strSPname">Store Name</param>
        /// <param name="p_arrParameter">Danh sách các tham số</param>
        public static void FillDataTable(string p_strConnStr, DataTable p_dtData, string p_strSPname, params object[] p_arrValue)
        {
            if ((p_arrValue != null) && (p_arrValue.Length > 0))
            {
                // Tạo danh sách SqlParameter
                SqlParameter[] arrSQLParameter = CSqlHelperParameterCache.GetSpParameterSet(
                    p_strConnStr, p_strSPname);

                // Gán dữ liệu từ các mãng value vô mảng command parameter
                AssignParameterValues(arrSQLParameter, p_arrValue);

                // gọi hàm overload
                FillDataTable(p_strConnStr, p_dtData, p_strSPname, arrSQLParameter);
            }

            else
            {
                FillDataTable(p_strConnStr, p_dtData, p_strSPname, null);
            }
        }

        /// <summary>
        /// Fill dữ liệu vô datatable dựa trên store name và danh sách parammeter truyền vào
        /// </summary>
        /// <param name="p_dtData">DataTable cần thêm dữ liệu vào</param>
        /// <param name="p_strSPname">Store Name</param>
        /// <param name="p_arrParameter">Danh sách các tham số</param>
        public static void FillDataTable(SqlConnection p_conn, SqlTransaction p_trans, string p_strConnStr, DataTable p_dtData,
            string p_strSPname, params object[] p_arrValue)
        {
            if ((p_arrValue != null) && (p_arrValue.Length > 0))
            {
                // Tạo danh sách SqlParameter
                SqlParameter[] arrSQLParameter = CSqlHelperParameterCache.GetSpParameterSet(
                    p_strConnStr, p_strSPname);

                // Gán dữ liệu từ các mãng value vô mảng command parameter
                AssignParameterValues(arrSQLParameter, p_arrValue);

                // gọi hàm overload
                FillDataTable(p_conn, p_trans, p_dtData, p_strSPname, arrSQLParameter);
            }

            else
            {
                FillDataTable(p_strConnStr, p_dtData, p_strSPname, null);
            }
        }

        /// <summary>
        /// Gán dữ liệu từ các object qua các sqlparameter
        /// </summary>
        /// <param name="commandParameters"></param>
        /// <param name="parameterValues"></param>
        private static void AssignParameterValues(SqlParameter[] p_arrSQLParameter, object[] p_arrValue)
        {
            if ((p_arrSQLParameter == null) || (p_arrValue == null))
            {
                return;
            }

            if (p_arrSQLParameter.Length != p_arrValue.Length)
            {
                throw new Exception("Parameter count does not match Parameter Value count.");
            }

            for (int i = 0, j = p_arrSQLParameter.Length; i < j; i++)
            {
                p_arrSQLParameter[i].Value = p_arrValue[i];
            }
        }

        /// <summary>
        /// Attach các parameter vô SqlCommand
        /// </summary>
        /// <param name="p_cmd">SqlCommand</param>
        /// <param name="p_arrSQLParameter">Danh sách tham số</param>
        private static void AttachParameters(SqlCommand p_cmd, SqlParameter[] p_arrSQLParameter)
        {
            foreach (SqlParameter p in p_arrSQLParameter)
            {
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }

                p_cmd.Parameters.Add(p);
            }
        }

        /// <summary>
        /// Thực thi 1 store
        /// </summary>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách tham số</param>
        /// <returns></returns>
        private static int ExecuteNonQuery(string p_strConnStr, string p_strStoreName,
            params SqlParameter[] p_arrSQLParameter)
        {
            DateTime v_dtStart = DateTime.Now;

            SqlConnection conn = new SqlConnection(p_strConnStr);
            SqlCommand cmd = new SqlCommand();
            int result = -5;

            try
            {
                PrepareCommand(cmd, conn, (SqlTransaction)null, p_strStoreName, p_arrSQLParameter);

                // Execute Sql Command
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                cmd.Dispose();
            }

            DateTime v_dtEnd = DateTime.Now;
            TimeSpan v_ts = v_dtEnd - v_dtStart;
            if (v_ts.TotalMilliseconds >= 100)
                CLogger.Trace("CSqlHelper", "ExecuteNonQuery", "Store: " + p_strStoreName + " execute " + v_ts.TotalSeconds.ToString("###,###0.######"));

            return result;
        }

        /// <summary>
        /// Thực thi 1 store
        /// </summary>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách tham số</param>
        /// <returns></returns>
        private static int ExecuteNonQuery(SqlConnection p_objConn, SqlTransaction p_objTrans,
            string p_strStoreName, params SqlParameter[] p_arrSQLParameter)
        {
            DateTime v_dtStart = DateTime.Now;

            SqlCommand cmd = new SqlCommand();
            int result = -5;

            try
            {
                PrepareCommand(cmd, p_objConn, p_objTrans, p_strStoreName, p_arrSQLParameter);

                // Execute Sql Command
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                cmd.Dispose();
            }

            DateTime v_dtEnd = DateTime.Now;
            TimeSpan v_ts = v_dtEnd - v_dtStart;
            if (v_ts.TotalMilliseconds >= 100)
                CLogger.Trace("CSqlHelper", "ExecuteNonQuery", "Store: " + p_strStoreName + " execute " + v_ts.TotalSeconds.ToString("###,###0.######"));

            return result;
        }

        /// <summary>
        /// Thực thi 1 store ở dạng scalar
        /// </summary>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách tham số</param>
        /// <returns></returns>
        private static object ExecuteScalar(string p_strConnStr, string p_strStoreName, params SqlParameter[] p_arrSQLParameter)
        {
            DateTime v_dtStart = DateTime.Now;

            SqlConnection conn = new SqlConnection(p_strConnStr);
            SqlCommand cmd = new SqlCommand();
            object result = null;

            try
            {
                PrepareCommand(cmd, conn, (SqlTransaction)null, p_strStoreName, p_arrSQLParameter);
                // Execute Sql Command
                result = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                cmd.Dispose();
            }

            DateTime v_dtEnd = DateTime.Now;
            TimeSpan v_ts = v_dtEnd - v_dtStart;
            if (v_ts.TotalMilliseconds >= 100)
                CLogger.Trace("CSqlHelper", "ExecuteScalar", "Store: " + p_strStoreName + " execute " + v_ts.TotalSeconds.ToString("###,###0.######"));

            return result;
        }

        /// <summary>
        /// Thực thi 1 store ở dạng scalar
        /// </summary>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách tham số</param>
        /// <returns></returns>
        private static object ExecuteScalar(SqlConnection p_conn, SqlTransaction p_trans, string p_strStoreName,
            params SqlParameter[] p_arrSQLParameter)
        {
            DateTime v_dtStart = DateTime.Now;

            SqlCommand cmd = new SqlCommand();
            object result = null;

            try
            {
                PrepareCommand(cmd, p_conn, p_trans, p_strStoreName, p_arrSQLParameter);
                // Execute Sql Command
                result = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                cmd.Dispose();
            }

            DateTime v_dtEnd = DateTime.Now;
            TimeSpan v_ts = v_dtEnd - v_dtStart;
            if (v_ts.TotalMilliseconds >= 100)
                CLogger.Trace("CSqlHelper", "ExecuteScalar", "Store: " + p_strStoreName + " execute " + v_ts.TotalSeconds.ToString("###,###0.######"));

            return result;
        }

        /// <summary>
        /// Đưa dữ liệu vô dataset
        /// </summary>
        /// <param name="p_dsData">DataSet cần đưa dữ liệu vô</param>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách parameyet</param>
        private static void FillDataSet(string p_strConnStr, DataSet p_dsData, string p_strStoreName, params SqlParameter[] p_arrSQLParameter)
        {
            DateTime v_dtStart = DateTime.Now;

            SqlConnection conn = new SqlConnection(p_strConnStr);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                PrepareCommand(cmd, conn, (SqlTransaction)null, p_strStoreName, p_arrSQLParameter);
                da.Fill(p_dsData);
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                cmd.Dispose();
                da.Dispose();
            }

            DateTime v_dtEnd = DateTime.Now;
            TimeSpan v_ts = v_dtEnd - v_dtStart;
            if (v_ts.TotalMilliseconds >= 100)
                CLogger.Trace("CSqlHelper", "FillDataSet", "Store: " + p_strStoreName + " execute " + v_ts.TotalSeconds.ToString("###,###0.######"));
        }

        /// <summary>
        /// Đưa dữ liệu vô DataTable
        /// </summary>
        /// <param name="p_dtData">DataTable cần đưa dữ liệu vô</param>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách parameyet</param>
        private static void FillDataTable(string p_strConnStr, DataTable p_dtData, string p_strStoreName, params SqlParameter[] p_arrSQLParameter)
        {
            DateTime v_dtStart = DateTime.Now;

            SqlConnection conn = new SqlConnection(p_strConnStr);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                PrepareCommand(cmd, conn, (SqlTransaction)null, p_strStoreName, p_arrSQLParameter);
                da.Fill(p_dtData);
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                cmd.Dispose();
                da.Dispose();
            }

            DateTime v_dtEnd = DateTime.Now;
            TimeSpan v_ts = v_dtEnd - v_dtStart;
            if (v_ts.TotalMilliseconds >= 100)
                CLogger.Trace("CSqlHelper", "FillDataTable", "Store: " + p_strStoreName + " execute " + v_ts.TotalSeconds.ToString("###,###0.######"));
        }

        /// <summary>
        /// Đưa dữ liệu vô DataTable
        /// </summary>
        /// <param name="p_dtData">DataTable cần đưa dữ liệu vô</param>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách parameyet</param>
        private static void FillDataTable(SqlConnection p_conn, SqlTransaction p_trans,
            DataTable p_dtData, string p_strStoreName, params SqlParameter[] p_arrSQLParameter)
        {
            DateTime v_dtStart = DateTime.Now;

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                PrepareCommand(cmd, p_conn, p_trans, p_strStoreName, p_arrSQLParameter);
                da.Fill(p_dtData);
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                cmd.Dispose();
                da.Dispose();
            }

            DateTime v_dtEnd = DateTime.Now;
            TimeSpan v_ts = v_dtEnd - v_dtStart;
            if (v_ts.TotalMilliseconds >= 100)
                CLogger.Trace("CSqlHelper", "FillDataTable", "Store: " + p_strStoreName + " execute " + v_ts.TotalSeconds.ToString(CConfig.Number_Format_String));
        }

        /// <summary>
        /// Xác nhận một command
        /// </summary>
        /// <param name="p_cmd">SqlCommand</param>
        /// <param name="p_conn">Connection</param>
        /// <param name="p_trans">Sql Transaction</param>
        /// <param name="p_strSPName">Store Name</param>
        /// <param name="p_arrSQLParameter">Sql Parameter</param>
        private static void PrepareCommand(SqlCommand p_cmd, SqlConnection p_conn,
            SqlTransaction p_trans, string p_strSPName, SqlParameter[] p_arrSQLParameter)
        {
            //if the provided connection is not open, we will open it
            if (p_conn.State != ConnectionState.Open)
            {
                p_conn.Open();
            }

            //associate the connection with the command
            p_cmd.Connection = p_conn;

            //set the command text (stored procedure name or SQL statement)
            p_cmd.CommandText = p_strSPName;

            //if we were provided a transaction, assign it.
            if (p_trans != null)
            {
                p_cmd.Transaction = p_trans;
            }

            //set the command type
            p_cmd.CommandType = CommandType.StoredProcedure;

            //attach the command parameters if they are provided
            if (p_arrSQLParameter != null)
            {
                AttachParameters(p_cmd, p_arrSQLParameter);
            }
        }

        /// <summary>
        /// Đưa dữ liệu vô DataTable
        /// </summary>
        /// <param name="p_dtData">DataTable cần đưa dữ liệu vô</param>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách parameyet</param>
        public static void FillDataTable_Cmd(string p_strConnStr, DataTable p_dtData, string p_strCmd)
        {
            SqlConnection conn = new SqlConnection(p_strConnStr);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                //associate the connection with the command
                cmd.Connection = conn;

                //set the command text (stored procedure name or SQL statement)
                cmd.CommandText = p_strCmd;

                //set the command type
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;
                da.Fill(p_dtData);
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                cmd.Dispose();
                da.Dispose();
            }
        }

        /// <summary>
        /// Đưa dữ liệu vô DataTable
        /// </summary>
        /// <param name="p_dtData">DataTable cần đưa dữ liệu vô</param>
        /// <param name="p_strStoreName">Store Name</param>
        /// <param name="p_arrSQLParameter">Danh sách parameyet</param>
        public static void ExecuteNonquery_Cmd(string p_strConnStr, string p_strCmd)
        {
            SqlConnection conn = new SqlConnection(p_strConnStr);
            SqlCommand cmd = new SqlCommand();

            try
            {
                conn.Open();

                //associate the connection with the command
                cmd.Connection = conn;

                //set the command text (stored procedure name or SQL statement)
                cmd.CommandText = p_strCmd;

                //set the command type
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;

                cmd.ExecuteNonQuery();
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                cmd.Dispose();
            }
        }
    }
}
