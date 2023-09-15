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
        private readonly string _strAppName = "TransactionDetails";
        private readonly TransactionDBEntities _dbContext;
        #endregion

        #region "Constructor"
        public ValuesController() : base()
        {
            if (_dbContext == null)
            {
                _dbContext = new TransactionDBEntities();
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

        #region "Get Account Transaction"
        [HttpPost]
        [Route("GetAccountTransaction")]
        public HttpResponseMessage GetAccountTransaction([FromBody] AccountTransactionRequest paramRequestData)
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
                        tbl_AccountDetails _objAccountDetails = _dbContext.tbl_AccountDetails.FirstOrDefault(x => x.AccountNo == paramRequestData.AccountNo);
                        List<Proc_GetAccountTransactionDetails_Result> _objProc_GTD = _dbContext.Proc_GetAccountTransactionDetails(paramRequestData.AccountNo, paramRequestData.StartDate, paramRequestData.EndDate).ToList<Proc_GetAccountTransactionDetails_Result>();
                        if (_objAccountDetails != null)
                        {
                            successResponse.customerDetails = new AccountDetail();
                            AccountDetail customerDetails = new AccountDetail();
                            successResponse.customerDetails.AccountNo = _objAccountDetails.AccountNo;
                            successResponse.customerDetails.AccountName = _objAccountDetails.AccountName;
                            successResponse.customerDetails.Address = _objAccountDetails.Address;
                            successResponse.customerDetails.AccountDescription = _objAccountDetails.AccountDescription;
                            successResponse.customerDetails.OpeningBalance = _objProc_GTD != null && _objProc_GTD.Count > 0 ? (_objProc_GTD.FirstOrDefault().Debit > 0 ?
                                        _objProc_GTD.FirstOrDefault().Balance + _objProc_GTD.FirstOrDefault().Debit : _objProc_GTD.FirstOrDefault().Balance - _objProc_GTD.FirstOrDefault().Credit) : 0;
                            successResponse.customerDetails.ClosingBalance = _objProc_GTD != null && _objProc_GTD.Count > 0 ? _objProc_GTD.OrderByDescending(s => s.TransDate).FirstOrDefault().Balance : 0;
                            successResponse.customerDetails.Branch = _objAccountDetails.Branch;
                            successResponse.customerDetails.IFSCode = _objAccountDetails.IFSCode;
                            successResponse.customerDetails.Transactions =  new List<Transaction>();
                            if (_objProc_GTD != null && _objProc_GTD.Count > 0)
                            {
                                foreach (var Item_Trans in _objProc_GTD)
                                {
                                    Transaction _objTransaction = new Transaction();
                                    _objTransaction.TransactionId = Item_Trans.TransactionId;
                                    _objTransaction.TransDate = Item_Trans.TransDate;
                                    _objTransaction.Details = Item_Trans.Details;
                                    _objTransaction.Debit = Item_Trans.Debit;
                                    _objTransaction.Credit = Item_Trans.Credit;
                                    _objTransaction.Balance = Item_Trans.Balance;
                                    successResponse.customerDetails.Transactions.Add(_objTransaction);
                                }
                            }
                            successResponse.Status = (int)Enums.Enums.statusCode.Success;
                            return Request.CreateResponse(HttpStatusCode.OK, successResponse);
                        }
                        else
                        {
                            errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                            errorResponse.Message = "Customers account details not found";
                            return Request.CreateResponse(HttpStatusCode.NotFound, errorResponse);
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
