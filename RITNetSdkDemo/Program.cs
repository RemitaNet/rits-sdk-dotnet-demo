using Newtonsoft.Json;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsAccountEnquiry;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsAddAccount;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPayment;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;
using System;
using System.Collections.Generic;
using System.Text;

namespace RITNetSdkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("##############################");
            Console.WriteLine("## INITIALIZING CREDENTIALS ##");
            Console.WriteLine("##############################");
            Console.WriteLine(" ");
            Credentials credentials = new Credentials();
            credentials.TimeoutInMilliSec = 10000;
            credentials.ReadWriteTimeoutMilliSec = 150000;
            credentials.MerchantId = "DEMOMDA1234";
            credentials.ApiToken = "bmR1ZFFFWEx5R2c2NmhnMEk5a25WenJaZWZwbHFFYldKOGY0bHlGZnBZQ1N5WEpXU2Y1dGt3PT0=";
            credentials.ApiKey = "REVNT01EQTEyMzR8REVNT01EQQ==";
            credentials.EncKey = "nbzjfdiehurgsxct";
            credentials.EncVector = "sngtmqpfurxdbkwj";
            credentials.Environment = "TEST";

            Console.WriteLine("#########################################");
            Console.WriteLine("## INITIALIZING REMITA PAYMENT GATEWAY ##");
            Console.WriteLine("#########################################");
            Console.WriteLine(" ");
            RemitaRITs remitaRITs = new RemitaRITs(credentials);

            Console.WriteLine(" ");
            Console.WriteLine("#############################");
            Console.WriteLine("###### ACCOUNT ENQUIRY ######");
            Console.WriteLine("#############################");
            Console.WriteLine(" ");
            AccountEnquiryPayload payload = new AccountEnquiryPayload();
            payload.AccountNo = "0581234567890";
            payload.BankCode = "058";
            payload.RequestId = "48934799767878902";
            AccountEnquiryResponseData accountEnquiryResponseData = remitaRITs.accountEnquiry(payload);
            Console.WriteLine("++++ RESPONSE: " + JsonConvert.SerializeObject(accountEnquiryResponseData));

            Console.WriteLine(" ");
            Console.WriteLine("#########################");
            Console.WriteLine("###### ADD ACCOUNT ######");
            Console.WriteLine("#########################");
            Console.WriteLine(" ");
            credentials.MerchantId = "DEMOMDA1234";
            credentials.ApiToken = "bmR1ZFFFWEx5R2c2NmhnMEk5a25WenJaZWZwbHFFYldKOGY0bHlGZnBZQ1N5WEpXU2Y1dGt3PT0=";
            credentials.ApiKey = "REVNT01EQTEyMzR8REVNT01EQQ==";
            credentials.EncKey = "nbzjfdiehurgsxct";
            credentials.EncVector = "sngtmqpfurxdbkwj";
            remitaRITs = new RemitaRITs(credentials);
            AddAccountPayload addAccountPayload = new AddAccountPayload();
            addAccountPayload.AccountNo = "0581234567890";
            addAccountPayload.BankCode = "058";
            addAccountPayload.RequestId = "489347";
            addAccountPayload.TransRef = "4893478";
            AddAccountResponseData addAccountResponseData = remitaRITs.addAccount(addAccountPayload);
            Console.WriteLine("++++ RESPONSE: " + JsonConvert.SerializeObject(addAccountResponseData));

            Console.WriteLine(" ");
            Console.WriteLine("#############################");
            Console.WriteLine("####### BULK PAYMENT #######");
            Console.WriteLine("#############################");
            Console.WriteLine(" ");
            BulkPaymentPayload bulkPaymentPayload = new BulkPaymentPayload();
            BulkPaymentInfo bulkPaymentInfo = new BulkPaymentInfo();
            bulkPaymentInfo.bankCode = "044";
            bulkPaymentInfo.batchRef = "123456";
            bulkPaymentInfo.debitAccount = "1234565678";
            bulkPaymentInfo.narration = "Regular payment";
            bulkPaymentInfo.requestId = "865489635";
            bulkPaymentInfo.totalAmount = 1000;
            PaymentDetails paymentDetail = new PaymentDetails();
            paymentDetail.transRef = "5354335";
            paymentDetail.narration= "Regular Payment";
            paymentDetail.benficiaryEmail= "qa@test.com";
            paymentDetail.benficiaryBankCode= "058";
            paymentDetail.benficiaryAccountNumber= "05829152080517";
            paymentDetail.amount= 5000;                     
            List<PaymentDetails> paymentDetails = new List<PaymentDetails>();
            paymentDetails.Add(paymentDetail);
            bulkPaymentPayload.paymentDetails = paymentDetails;
            bulkPaymentPayload.bulkPaymentInfo = bulkPaymentInfo;
            BulkPaymentResponseData bulkPaymentResponseData = remitaRITs.bulkPayment(bulkPaymentPayload);
            Console.WriteLine("++++ RESPONSE: " + JsonConvert.SerializeObject(bulkPaymentResponseData));

            Console.ReadLine();
        }
    }
}
