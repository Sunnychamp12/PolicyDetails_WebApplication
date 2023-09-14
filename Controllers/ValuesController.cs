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
using System.Data.Entity.Core.Objects;

namespace PolicyDetails_WebApplication.Controllers
{
    public class ValuesController : ApiController
    {
        #region "Declare Common Parameters"
        private bool isValid = false;
        private string headerToken = string.Empty;
        private readonly string _strToken = "c06fc4189a5645e4a4fd480e8b1556e7";
        private readonly string _strAppName = "PolicyDetails";
        private readonly SunnyDBEntities _dbContext;
        #endregion

        #region "Constructor"
        public ValuesController() : base()
        {
            if (_dbContext == null)
            {
                _dbContext = new SunnyDBEntities();
            }
        }
        #endregion

        #region "PolicyDetails Crud Opr"
        [HttpPost]
        [Route("savePolicyDetails")]
        public HttpResponseMessage savePolicyDetails([FromBody] PolicyData policyDetails)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            SuccessResponse sucessResponse = new SuccessResponse();
            try
            {
                headerToken = Request.Headers.GetValues(Enums.Enums.headerParams.headerToken.ToString()).First();
                string APPName = Request.Headers.GetValues(Enums.Enums.headerParams.APPName.ToString()).First();
                isValid = (headerToken == _strToken);
                if (isValid && APPName == _strAppName)
                {
                    if (policyDetails == null)
                    {
                        errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                        errorResponse.Message = "Request data not found";
                        return Request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
                    }
                    else
                    {
                        _dbContext.PolicyDatas.Add(policyDetails);
                        _dbContext.SaveChanges();
                        sucessResponse.Status = (int)Enums.Enums.statusCode.Success;
                        sucessResponse.Message = "Policy data save successfuly";
                        return Request.CreateResponse(HttpStatusCode.OK, sucessResponse);
                    }
                }
                else
                {
                    errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                    errorResponse.Message = "Invalid API Key";
                    return Request.CreateResponse(HttpStatusCode.NoContent, errorResponse);
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
                headerToken = Request.Headers.GetValues(Enums.Enums.headerParams.headerToken.ToString()).First();
                string APPName = Request.Headers.GetValues(Enums.Enums.headerParams.APPName.ToString()).First();
                isValid = (headerToken == _strToken);
                if (isValid && APPName == _strAppName)
                {
                    PolicyData _objPolicyData = _dbContext.PolicyDatas.FirstOrDefault(p => p.PolicyKey == paramPolicyData.PolicyKey);
                    if (_objPolicyData == null)
                    {
                        errorResponse.Status = (int)Enums.Enums.statusCode.Error;
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
                        sucessResponse.Status = (int)Enums.Enums.statusCode.Success;
                        sucessResponse.Message = "Policy data save successfuly";
                        return Request.CreateResponse(HttpStatusCode.OK, sucessResponse);
                    }
                }
                else
                {
                    errorResponse.Status = (int)Enums.Enums.statusCode.Success;
                    errorResponse.Message = "Invalid API Key";
                    return Request.CreateResponse(HttpStatusCode.NoContent, errorResponse);
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
                headerToken = Request.Headers.GetValues(Enums.Enums.headerParams.headerToken.ToString()).First();
                string APPName = Request.Headers.GetValues(Enums.Enums.headerParams.APPName.ToString()).First();
                isValid = (headerToken == _strToken);
                if (isValid && APPName == _strAppName)
                {
                    PolicyData _objPolicyData = _dbContext.PolicyDatas.FirstOrDefault(p => p.PolicyKey == paramPolicy.PolicyKey);
                    if (_objPolicyData == null)
                    {
                        errorResponse.Status = (int)Enums.Enums.statusCode.Success;
                        errorResponse.Message = "Policy details was not found";
                        return Request.CreateResponse(HttpStatusCode.OK, errorResponse);
                    }
                    else
                    {
                        _dbContext.PolicyDatas.Remove(_objPolicyData);
                        _dbContext.SaveChanges();
                        successResponse.Status = (int)Enums.Enums.statusCode.Success;
                        successResponse.Message = "Record deleted successfuly";
                        return Request.CreateResponse(HttpStatusCode.OK, successResponse);
                    }
                }
                else
                {
                    errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                    errorResponse.Message = "Invalid API Key";
                    return Request.CreateResponse(HttpStatusCode.NoContent, errorResponse);
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
            GetSuccessResponse successResponse = new GetSuccessResponse();
            try
            {
                DataTable _dtPolicyDetails = new DataTable();
                headerToken = Request.Headers.GetValues(Enums.Enums.headerParams.headerToken.ToString()).First();
                string APPName = Request.Headers.GetValues(Enums.Enums.headerParams.APPName.ToString()).First();
                isValid = (headerToken == _strToken);
                if (isValid && APPName == _strAppName)
                {
                    _dtPolicyDetails = Common.getPolicyDataList(Customer_Code);
                    successResponse.PolicyDetails = Common.ConvertDataTable<PolicyData>(_dtPolicyDetails);
                    successResponse.Status = (int)Enums.Enums.statusCode.Success;
                    if (successResponse.PolicyDetails != null && successResponse.PolicyDetails.Count > 0)
                        return Request.CreateResponse(HttpStatusCode.OK, successResponse);
                    else
                    {
                        response.Status = (int)Enums.Enums.statusCode.Error;
                        response.Message = "Invalid customer code";
                        return Request.CreateResponse(HttpStatusCode.NoContent, response);
                    }
                }
                else
                {
                    response.Status = (int)Enums.Enums.statusCode.Error;
                    response.Message = "Invalid API Key";
                    return Request.CreateResponse(HttpStatusCode.NoContent, response);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, ex);
            }
        }
        #endregion

        #region "Get Policy Transaction"
        [HttpPost]
        [Route("GetPolicyTransaction")]
        public HttpResponseMessage GetPolicyTransaction([FromBody] PolicyTransactionRequest paramPolicyData)
        {
            GetPolicyTransactionResponse successResponse = new GetPolicyTransactionResponse();
            ErrorResponse errorResponse = new ErrorResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    headerToken = Request.Headers.GetValues(Enums.Enums.headerParams.headerToken.ToString()).First();
                    string APPName = Request.Headers.GetValues(Enums.Enums.headerParams.APPName.ToString()).First();
                    isValid = (headerToken == _strToken);
                    if (isValid && APPName == _strAppName)
                    {
                        List<SP_GetPolicyTransaction_Result> _objPolicyTransation = _dbContext.SP_GetPolicyTransaction(paramPolicyData.PolicyNo, paramPolicyData.StartDate, paramPolicyData.EndDate).ToList<SP_GetPolicyTransaction_Result>();
                        if (_objPolicyTransation != null)
                        {
                            successResponse.customerDetails = new List<CustomerDetails>();
                            foreach (var getCustomerDetails in _objPolicyTransation.Distinct())
                            {
                                if (successResponse.customerDetails != null && !successResponse.customerDetails.Any(C => C.CustomerId == getCustomerDetails.CustomerId))
                                {
                                    CustomerDetails customerDetails = new CustomerDetails();
                                    customerDetails.CustomerId = getCustomerDetails.CustomerId;
                                    customerDetails.PremiumDate = getCustomerDetails.PremiumDate;
                                    customerDetails.PolicyNo = getCustomerDetails.PolicyNo;
                                    customerDetails.PremiumAmount = getCustomerDetails.PremiumAmount;
                                    customerDetails.PolicyStatus = getCustomerDetails.PolicyStatus;
                                    customerDetails.policyDetails = new List<PolicyData>();
                                    successResponse.customerDetails.Add(customerDetails);
                                }
                            }
                            foreach (var item in successResponse.customerDetails)
                            {
                                foreach (var getPolicyDetails in _objPolicyTransation.Where(C => C.CustomerId == item.CustomerId).ToList())
                                {
                                    PolicyData policyDetails = new PolicyData();
                                    policyDetails.ContractNumber = getPolicyDetails.ContractNumber;
                                    policyDetails.CustomerCode = getPolicyDetails.CustomerCode;
                                    policyDetails.RiskCommencementDate = getPolicyDetails.RiskCommencementDate;
                                    policyDetails.ProductName = getPolicyDetails.ProductName;
                                    policyDetails.MaturityDate = getPolicyDetails.MaturityDate;
                                    policyDetails.NextRenewalDue = getPolicyDetails.NextRenewalDue;
                                    policyDetails.SumAssuredAmount = getPolicyDetails.SumAssuredAmount;
                                    policyDetails.PremiumAmount = getPolicyDetails.PremiumAmount;
                                    policyDetails.ContractStatusCode = getPolicyDetails.ContractStatusCode;
                                    policyDetails.PolicyStatus = getPolicyDetails.PolicyStatus;
                                    policyDetails.ETLDate = getPolicyDetails.ETLDate;
                                    item.policyDetails.Add(policyDetails);
                                }
                            }
                            successResponse.Status = (int)Enums.Enums.statusCode.Success;
                            return Request.CreateResponse(HttpStatusCode.OK, successResponse);
                        }
                        else
                        {
                            errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                            errorResponse.Message = "Customers policy transaction details not found";
                            return Request.CreateResponse(HttpStatusCode.OK, errorResponse);
                        }
                    }
                    else
                    {
                        errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                        errorResponse.Message = "This request has failed authentication";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, errorResponse);
                    }
                }
                else
                {
                    errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                    errorResponse.Message = "Please enter valid data";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, ex);
            }
        }
        #endregion
    }
}
