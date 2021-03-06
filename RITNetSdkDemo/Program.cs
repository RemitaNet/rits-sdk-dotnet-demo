﻿using Newtonsoft.Json;
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
            Console.WriteLine("#########################################################");
            Console.WriteLine("## INITIALIZING REMITA PAYMENT CREDENTIALS AND GATEWAY ##");
            Console.WriteLine("#########################################################");
            Console.WriteLine(" ");
            Credentials credentials = new Credentials();
            credentials.TimeoutInMilliSec = 10000;
            credentials.ReadWriteTimeoutMilliSec = 150000;
            credentials.Environment = "TEST";
            RemitaRITs remitaRITs = new RemitaRITs(credentials);

            Console.WriteLine(" ");
            Console.WriteLine("#############################");
            Console.WriteLine("###### ACCOUNT ENQUIRY ######");
            Console.WriteLine("#############################");
            Console.WriteLine(" ");
            AccountEnquiry(credentials, remitaRITs);

            Console.WriteLine(" ");
            Console.WriteLine("############################");
            Console.WriteLine("####### BULK PAYMENT #######");
            Console.WriteLine("############################");
            Console.WriteLine(" ");
            BulkPayment(credentials, remitaRITs);


            Console.WriteLine(" ");
            Console.WriteLine("#########################");
            Console.WriteLine("###### ADD ACCOUNT ######");
            Console.WriteLine("#########################");
            Console.WriteLine(" ");
            AddAccount(credentials, remitaRITs);

            Console.ReadLine();
        }

        static void AccountEnquiry(Credentials credentials, RemitaRITs remitaRITs)
        {
            credentials.MerchantId = "DEMOMDA1234";
            credentials.ApiToken = "bmR1ZFFFWEx5R2c2NmhnMEk5a25WenJaZWZwbHFFYldKOGY0bHlGZnBZQ1N5WEpXU2Y1dGt3PT0=";
            credentials.ApiKey = "REVNT01EQTEyMzR8REVNT01EQQ==";
            credentials.EncKey = "nbzjfdiehurgsxct";
            credentials.EncVector = "sngtmqpfurxdbkwj";
            AccountEnquiryPayload payload = new AccountEnquiryPayload();
            payload.AccountNo = "0581234567890";
            payload.BankCode = "058";
            payload.RequestId = generateRequestID();
            AccountEnquiryResponseData accountEnquiryResponseData = remitaRITs.accountEnquiry(payload);
            Console.WriteLine("++++ RESPONSE: " + JsonConvert.SerializeObject(accountEnquiryResponseData));
        }
       
        static void AddAccount(Credentials credentials, RemitaRITs remitaRITs)
        {
            credentials.MerchantId = "42192033";
            credentials.ApiToken = "TmNGYlc4RHl6ajdCWUtxNTFmTnR1MG1IRzFjcVByQ1htbmJJL2V1ZVQ5eXl1dmRyN0xvL29nPT0=";
            credentials.ApiKey = "REVNT1RFQ0gxMjM0fERFTU9URUNI";
            credentials.EncKey = "wiavbnktudcprxjf";
            credentials.EncVector = "finvwsegqzbtuykj";
            remitaRITs = new RemitaRITs(credentials);
            AddAccountPayload addAccountPayload = new AddAccountPayload();
            addAccountPayload.AccountNo = "0581234567890";
            addAccountPayload.BankCode = "058";
            addAccountPayload.RequestId = generateRequestID();
            addAccountPayload.TransRef = "4893478";
            AddAccountResponseData addAccountResponseData = remitaRITs.addAccount(addAccountPayload);
            Console.WriteLine("++++ RESPONSE: " + JsonConvert.SerializeObject(addAccountResponseData));
        }

        static void BulkPayment(Credentials credentials, RemitaRITs remitaRITs)
        {
            credentials.MerchantId = "DEMOMDA1234";
            credentials.ApiToken = "bmR1ZFFFWEx5R2c2NmhnMEk5a25WenJaZWZwbHFFYldKOGY0bHlGZnBZQ1N5WEpXU2Y1dGt3PT0=";
            credentials.ApiKey = "REVNT01EQTEyMzR8REVNT01EQQ==";
            credentials.EncKey = "nbzjfdiehurgsxct";
            credentials.EncVector = "sngtmqpfurxdbkwj";
            BulkPaymentPayload bulkPaymentPayload = new BulkPaymentPayload();
            BulkPaymentInfo bulkPaymentInfo = new BulkPaymentInfo();
            bulkPaymentInfo.bankCode = "044";
            bulkPaymentInfo.batchRef = generateRequestID();
            bulkPaymentInfo.debitAccount = "1234565678";
            bulkPaymentInfo.narration = "Regular payment";
            bulkPaymentInfo.requestId = generateRequestID();
            bulkPaymentInfo.totalAmount = 1000;
            PaymentDetails paymentDetail = new PaymentDetails();
            paymentDetail.transRef = generateRequestID();
            paymentDetail.narration = "Regular Payment";
            paymentDetail.benficiaryEmail = "qa@test.com";
            paymentDetail.benficiaryBankCode = "058";
            paymentDetail.benficiaryAccountNumber = "05829152080517";
            paymentDetail.amount = 1000;
            List<PaymentDetails> paymentDetails = new List<PaymentDetails>();
            paymentDetails.Add(paymentDetail);
            bulkPaymentPayload.paymentDetails = paymentDetails;
            bulkPaymentPayload.bulkPaymentInfo = bulkPaymentInfo;
            BulkPaymentResponseData bulkPaymentResponseData = remitaRITs.bulkPayment(bulkPaymentPayload);
            Console.WriteLine("++++ RESPONSE: " + JsonConvert.SerializeObject(bulkPaymentResponseData));
        }

        public static string generateRequestID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
