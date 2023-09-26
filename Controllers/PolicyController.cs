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
        private SqlParameter[] _objSqlParameters = null;
        // GET: Policy
        [HttpGet]
        [HttpHead]
        public ActionResult Index(string headerToken)
        {
            ErrorResponse response = new ErrorResponse();
            GetSuccessResponse successResponse = new GetSuccessResponse();
            try
            {
                DataTable _dtPolicyDetails = new DataTable();
                string CustomerCode = Request.QueryString["Customer_Code"].ToString();
                string APPName = Request.Headers["APPName"].ToString();
                bool isValid = (headerToken == "c06fc4189a5645e4a4fd480e8b1556e7");
                if (isValid && APPName == "PolicyDetails")
                {
                    _dtPolicyDetails = Common.getPolicyDataList(_objSqlParameters, "SP_GetPolicyDetails");
                    successResponse.PolicyDetails = Common.ConvertDataTable<PolicyData>(_dtPolicyDetails);
                    successResponse.Status = 1;
                    if (successResponse.PolicyDetails != null && successResponse.PolicyDetails.Count > 0)
                        return Json(successResponse);
                    else
                    {
                        response.Status = 0;
                        response.Message = "Invalid customer code";
                        return Json(response);
                    }
                }
                else
                {
                    response.Status = 0;
                    response.Message = "Invalid API Key";
                    return Json(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = ex.Message.ToString();
                return Json(response);
            }
        }
    }
}