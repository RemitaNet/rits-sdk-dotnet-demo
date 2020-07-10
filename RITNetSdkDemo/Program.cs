using System;
using System.Collections.Generic;
using System.Text;

namespace RITNetSdkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize credentials 
            Credentials credentials = new Credentials();
            credentials.TimeoutInMilliSec = 10000;
            credentials.ReadWriteTimeoutMilliSec = 150000;

            //Assign values to credentials 
            credentials.MerchantId = "DEMOMDA1234";
            credentials.ApiToken = "bmR1ZFFFWEx5R2c2NmhnMEk5a25WenJaZWZwbHFFYldKOGY0bHlGZnBZQ1N5WEpXU2Y1dGt3PT0=";
            credentials.ApiKey = "REVNT01EQTEyMzR8REVNT01EQQ==";
            credentials.EncKey = "nbzjfdiehurgsxct";
            credentials.EncVector = "sngtmqpfurxdbkwj";
            credentials.Environment = "TEST";

            RemitaRITs remitaRITs = new RemitaRITs(credentials);

            //Account Enquiry Payload
            /**
            AccountEnquiryPayload payload = new AccountEnquiryPayload();
            payload.AccountNo = "0581234567890";
            payload.BankCode = "058";
            payload.RequestId = "48934799767878902";
            var responseData = remitaRITs.accountEnquiry(payload);**/

            BulkPaymentPayload payload = new BulkPaymentPayload();
            BulkPaymentInfo bulkPaymentInfo = new BulkPaymentInfo();

            bulkPaymentInfo.bankCode = "044";
            bulkPaymentInfo.batchRef = "123456";
            bulkPaymentInfo.debitAccount = "1234565678";
            bulkPaymentInfo.narration = "Regular payment";
            bulkPaymentInfo.requestId = "865489635";
            bulkPaymentInfo.totalAmount = 1000;

            PaymentDetails paymentDetail = new PaymentDetails();
            paymentDetail.transRef = "5354335";
            paymentDetail.narration = "Regular Payment";
            paymentDetail.benficiaryEmail = "qa@test.com";
            paymentDetail.benficiaryBankCode = "058";
            paymentDetail.benficiaryAccountNumber = "05829152080517";
            paymentDetail.amount = 5000;

            List<PaymentDetails> paymentDetails = new List<PaymentDetails>();
            paymentDetails.Add(paymentDetail);

            payload.paymentDetails = paymentDetails;
            payload.bulkPaymentInfo = bulkPaymentInfo;

            var responseData = remitaRITs.bulkPayment(payload);

            Console.WriteLine("++++ RESPONSE: " + JsonConvert.SerializeObject(responseData));
            Console.ReadLine();

        }
    }
}
