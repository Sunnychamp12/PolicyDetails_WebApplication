using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PolicyDetails_WebApplication.Models
{
    public class Common
    {
        public static string cnnString = "Server= (local)\\SQLEXPRESS;DataBase=SunnyDB;trusted_connection=True;Encrypt=False;MultipleActiveResultSets=True;";
        public DataTable _dt = new DataTable();
        public static DataTable getPolicyDataList(SqlParameter[] paramSqlParam, string paramProcedure)
        {
            DataTable _dt = new DataTable();
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            if (paramSqlParam != null)
                foreach (var item in paramSqlParam)
                    cmd.Parameters.Add(new SqlParameter(item.ParameterName, item.Value));
            cmd.CommandText = paramProcedure;
            cnn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(_dt);
            cnn.Close();
            return _dt;
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
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