using Microsoft.Win32.SafeHandles;
using PolicyDetails_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace PolicyDetails_WebApplication.Controllers
{
    public class PolicyController : Controller
    {
        // GET: Policy
        [HttpGet]
        [HttpHead]
        public ActionResult Index(string headerToken)
        {
            ErrorResponse response = new ErrorResponse();
            SuccessResponse successResponse = new SuccessResponse();
            try
            {
                string CustomerCode = Request.QueryString["Customer_Code"].ToString();
                string APPName = Request.Headers["APPName"].ToString();
                bool isValid = (headerToken == "c06fc4189a5645e4a4fd480e8b1556e7");
                if (isValid && APPName == "PolicyDetails")
                {
                    successResponse.PolicyDetails = getPolicyDataList(CustomerCode);
                    //var PolicyDetail = _dbContext.PolicyData.Where(x => x.CustomerCode == CustomerCode).ToList();
                    successResponse.Status = "1";
                    if (successResponse.PolicyDetails != null && successResponse.PolicyDetails.Count > 0)
                        return Json(successResponse);
                    else
                    {
                        response.Status = "0";
                        response.Message = "Invalid customer code";
                        return Json(response);
                    }
                }
                else
                {
                    response.Status = "0";
                    response.Message = "Invalid API Key";
                    return Json(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = "0";
                response.Message = ex.Message.ToString();
                return Json(response);
            }
        }
        public List<Models.PolicyData_1> getPolicyDataList(string paramPolicyCode)
        {
            DataTable dt = new DataTable();
            string cnnString = "Server= (local)\\SQLEXPRESS;DataBase=SunnyDB;trusted_connection=True;Encrypt=False;MultipleActiveResultSets=True;";

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CustoemerCode", paramPolicyCode));
            cmd.CommandText = "SP_GetPolicyDetails";
            cnn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            //object o = cmd.ExecuteScalar();
            cnn.Close();
            List<Models.PolicyData_1> _objPolicyDataList = ConvertDataTable<Models.PolicyData_1>(dt);

            return _objPolicyDataList;
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