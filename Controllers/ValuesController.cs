using PolicyDetails_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PolicyDetails_WebApplication.Controllers
{
    public class ValuesController : ApiController
    {
        private bool isValid = false;
        private string headerToken = string.Empty;
        private readonly string _strToken = "c06fc4189a5645e4a4fd480e8b1556e7";
        private readonly string _strAppName = "PolicyDetails";
        private readonly SunnyDBEntities _dbContext;
        public ValuesController() : base()
        {
            if (_dbContext == null)
            {
                _dbContext = new SunnyDBEntities();
            }
        }

        // GET api/values
        [System.Web.Http.Route("api/values/getValues")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("savePolicyDetails")]
        public HttpResponseMessage savePolicyDetails([FromBody] PolicyData policyDetails)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            SuccessResponse sucessResponse = new SuccessResponse();
            try
            {
                if (policyDetails == null)
                {
                    errorResponse.Status = "0";
                    errorResponse.Message = "Request data not found";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
                }
                else
                {
                    _dbContext.PolicyDatas.Add(policyDetails);
                    _dbContext.SaveChanges();
                    sucessResponse.Status = "1";
                    sucessResponse.Message = "Policy data save successfuly";
                    return Request.CreateResponse(HttpStatusCode.OK, sucessResponse);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        [HttpPost]
        [Route("updatePolicyDetails")]
        public HttpResponseMessage updatePolicyDetails([FromBody] PolicyData paramPolicyData)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            SuccessResponse sucessResponse = new SuccessResponse();
            try
            {
                PolicyData _objPolicyData = _dbContext.PolicyDatas.FirstOrDefault(p => p.PolicyKey == paramPolicyData.PolicyKey);
                if (_objPolicyData == null)
                {
                    errorResponse.Status = "0";
                    errorResponse.Message = "Request data not found";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
                }
                else
                {
                    _objPolicyData.ContractNumber = paramPolicyData.ContractNumber;
                    _objPolicyData.CustomerCode = paramPolicyData.CustomerCode;
                    _objPolicyData.RiskCommencementDate = Convert.ToDateTime(paramPolicyData.RiskCommencementDate);
                    _objPolicyData.ProductName = paramPolicyData.ProductName;
                    _objPolicyData.MaturityDate = paramPolicyData.MaturityDate;
                    _objPolicyData.NextRenewalDue = Convert.ToDateTime(paramPolicyData.NextRenewalDue);
                    _objPolicyData.SumAssuredAmount = Convert.ToDecimal(paramPolicyData.SumAssuredAmount);
                    _objPolicyData.PremiumAmount = Convert.ToDecimal(paramPolicyData.PremiumAmount);
                    _objPolicyData.ContractStatusCode = paramPolicyData.ContractStatusCode;
                    _objPolicyData.PolicyStatus = paramPolicyData.PolicyStatus;
                    _objPolicyData.ETLDate = Convert.ToDateTime(paramPolicyData.ETLDate);
                    _dbContext.SaveChanges();
                    sucessResponse.Status = "1";
                    sucessResponse.Message = "Policy data save successfuly";
                    return Request.CreateResponse(HttpStatusCode.OK, sucessResponse);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        [HttpDelete]
        [Route("dltPolicy")]
        public HttpResponseMessage deletePolicyDetail([FromBody] PolicyData paramPolicy)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            SuccessResponse successResponse = new SuccessResponse();
            try
            {
                PolicyData _objPolicyData = _dbContext.PolicyDatas.FirstOrDefault(p => p.PolicyKey == paramPolicy.PolicyKey);
                if (_objPolicyData == null)
                {
                    errorResponse.Status = Enums.Enums.statusCode.Success.ToString();
                    errorResponse.Message = "Policy details was not found";
                    return Request.CreateResponse(HttpStatusCode.OK, errorResponse);
                }
                else
                {
                    _dbContext.PolicyDatas.Remove(_objPolicyData);
                    _dbContext.SaveChanges();
                    successResponse.Status = "1";
                    successResponse.Message = "Record deleted successfuly";
                    return Request.CreateResponse(HttpStatusCode.OK, successResponse);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        [HttpGet]
        [Route("getPolicyDetails")]
        public HttpResponseMessage Index(string Customer_Code)
        {
            ErrorResponse response = new ErrorResponse();
            SuccessResponse successResponse = new SuccessResponse();
            try
            {
                headerToken = Request.Headers.GetValues("headerToken").First();
                string APPName = Request.Headers.GetValues("APPName").First();
                isValid = (headerToken == _strToken);
                if (isValid && APPName == _strAppName)
                {
                    successResponse.PolicyDetails = this.getPolicyDataList(Customer_Code);
                    successResponse.Status = "1";
                    if (successResponse.PolicyDetails != null && successResponse.PolicyDetails.Count > 0)
                        return Request.CreateResponse(HttpStatusCode.OK, successResponse);
                    else
                    {
                        response.Status = "0";
                        response.Message = "Invalid customer code";
                        return Request.CreateResponse(HttpStatusCode.NoContent, response);
                    }
                }
                else
                {
                    response.Status = "0";
                    response.Message = "Invalid API Key";
                    return Request.CreateResponse(HttpStatusCode.NoContent, response);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, ex); ;
            }
        }

        public List<PolicyData_1> getPolicyDataList(string paramPolicyCode)
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
            List<PolicyData_1> _objPolicyDataList = ConvertDataTable<PolicyData_1>(dt);

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
