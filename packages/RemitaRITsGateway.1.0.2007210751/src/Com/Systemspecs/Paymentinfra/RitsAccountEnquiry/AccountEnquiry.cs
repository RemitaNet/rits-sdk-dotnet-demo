﻿  
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
  
using System;
using System.Collections.Generic;
  
using System.Text;
  
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsAccountEnquiry
{
    class AccountEnquiry
    {
        private AccountEnquiryResponseData result;

        public AccountEnquiryResponseData getAccountEnquiry(AccountEnquiryPayload accountEnquiryPayload, Credentials credentials)
        {
            AccountEnquiryResponseData altResult = new AccountEnquiryResponseData();
            if (!Config.isCredentialAvailable(credentials))
            {
                altResult.data = new AccountEnquiryDto();
                altResult.status = SdkResponseCode.CredentialStatus;
                altResult.data.responseDescription = Config.emptyCredentialResponse;
                altResult.data.responseCode = Config.emptyCredentialResponseCode;
                return altResult;
            }
            else
            {
                try
                {
                    EnvironmentConfig environmentConfig = new EnvironmentConfig();
                    Dictionary<string, string> getHashValues = environmentConfig.getRitsEnvironment(credentials);
                    Dictionary<string, int> getEnvTimeOut = environmentConfig.getTimeOut(credentials);
                    EncryptionUtil enUtil = new EncryptionUtil();

                    string url = getHashValues["ACCOUNT_ENQUIRY_URL"];
                    long milliseconds = DateTime.Now.Ticks;
                    string requestId = accountEnquiryPayload.RequestId;
                    string hash_string = getHashValues["API_KEY"] + requestId + getHashValues["API_TOKEN"];
                    string hashed = Config.SHA512(hash_string);
                    var accountNo = accountEnquiryPayload.AccountNo;
                    var bankCode = accountEnquiryPayload.BankCode;

                    //HEADERS
                    List<Header> headers = new List<Header>();
                    headers.Add(new Header { header = "Content-Type", value = "application/json" });
                    headers.Add(new Header { header = "API_DETAILS_HASH", value = hashed });
                    headers.Add(new Header { header = "REQUEST_TS", value = Config.getTimeStamp() });
                    headers.Add(new Header { header = "REQUEST_ID", value = requestId });
                    headers.Add(new Header { header = "API_KEY", value = getHashValues["API_KEY"] });
                    headers.Add(new Header { header = "MERCHANT_ID", value = getHashValues["MERCHANT_ID"] });

                    var body = new
                    {
                        accountNo = enUtil.Encrypt(accountNo, getHashValues["KEY"], getHashValues["IV"]),
                        bankCode = enUtil.Encrypt(bankCode, getHashValues["KEY"], getHashValues["IV"])
                    };
                
                    try
                    {
                        var response = WebClientUtil.PostResponse(url, JsonConvert.SerializeObject(body), headers);
                        result = JsonConvert.DeserializeObject<AccountEnquiryResponseData>(response);
                    }
                    catch (Exception e1)
                    {
                        altResult.data = new AccountEnquiryDto();
                        altResult.status = SdkResponseCode.CredentialStatus;
                        altResult.data.responseCode = SdkResponseCode.ERROR_WHILE_CONNECTING_CODE;
                        altResult.data.responseDescription = SdkResponseCode.ERROR_WHILE_CONNECTING;
                        Console.WriteLine("ERROR : {0} ", SdkResponseCode.ERROR_WHILE_CONNECTING);
                        return altResult;
                    }
                }
                catch (Exception e2)
                {
                    altResult.data = new AccountEnquiryDto();
                    altResult.status = SdkResponseCode.CredentialStatus;
                    altResult.data.responseCode = SdkResponseCode.ERROR_PROCESSING_REQUEST_CODE;
                    altResult.data.responseDescription = SdkResponseCode.ERROR_PROCESSING_REQUEST;
                    Console.WriteLine("ERROR : {0} ", SdkResponseCode.ERROR_PROCESSING_REQUEST);
                    return altResult;
                }
                return result;
            }

        }
    }
}
