using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using RestSharp;
using RestSharp.Authenticators;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using WebApplication1.Models;


namespace WebApplication1.DataAccess
{
    public class PubSubRepository
    {
        TopicName tn ;
        SubscriptionName sn;
        public PubSubRepository()
        {
            tn  = new TopicName("cloudprogrammingyf", "DemoTopic");  //A Queue/Topic will be created to hold the emails to be sent.  It will always have the same name DemoTopic, which you can change
            sn = new SubscriptionName("cloudprogrammingyf", "DemoSubscription");  //A Subscription will be created to hold which messages were read or not.  It will always have the same name DemoSubscription, which you can change
        }
        private Topic CreateGetTopic()
        {
            PublisherServiceApiClient client = PublisherServiceApiClient.Create();   //We check if Topic exists, if no we create it and return it
            TopicName tn = new TopicName("cloudprogrammingyf", "DemoTopic");
            try
            {
                 return client.GetTopic(tn);
            }
            catch
            {
                return client.CreateTopic(tn);
            }
          

        }

        /// <summary>
        /// Publish method: uploads a message to the queue
        /// </summary>
        /// <param name="p"></param>
        public void AddToEmailQueue(File p) 
        {
            PublisherServiceApiClient client = PublisherServiceApiClient.Create();
            var t = CreateGetTopic();

            string serialized = JsonSerializer.Serialize(p, typeof(File));

            List<PubsubMessage> messagesToAddToQueue = new List<PubsubMessage>(); // the method takes a list, so you can upload more than 1 message/item/file at a time
            PubsubMessage msg = new PubsubMessage();
            msg.Data = ByteString.CopyFromUtf8(serialized);

            messagesToAddToQueue.Add(msg);


            client.Publish(t.TopicName, messagesToAddToQueue); //committing to queue
        }


        private Subscription CreateGetSubscription()
        {
            SubscriberServiceApiClient client = SubscriberServiceApiClient.Create();  //We check if Subscription exists, if no we create it and return it
 
            try
            {
               return client.GetSubscription(sn);
            }
            catch
            {
                return  client.CreateSubscription(sn, tn, null, 30);
            }

        }

        public void DownloadEmailFromQueueAndSend()
        {
            SubscriberServiceApiClient client = SubscriberServiceApiClient.Create();

            var s = CreateGetSubscription(); //This must be called before being able to read messages from Topic/Queue
            var pullResponse = client.Pull(s.SubscriptionName, true, 1); //Reading the message on top; You can read more than just 1 at a time
            if (pullResponse != null)
            {
                for (int l = 0; l < pullResponse.ReceivedMessages.Count; l++)
                {
                    string toDeserialize = pullResponse.ReceivedMessages[0].Message.Data.ToStringUtf8(); //extracting the first message since in the previous line it was specified to read one at a time. if you decide to read more then a loop is needed
                    File deserialized = JsonSerializer.Deserialize<File>(toDeserialize); //Deserializing since when we published it we serialized it

                    MailMessage mm = new MailMessage();  //Message on queue/topic will consist of a ready made email with the desired content, you can upload anything which is serializable

                    UsersRepository ur = new UsersRepository();

                    try
                    {
                        string reci =KeyRepository.Decrypt(deserialized.Recipient);
                        string downloadLink = KeyRepository.Decrypt(deserialized.Link);

                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress("programmingforthecloud1234@gmail.com");
                        message.To.Add(new MailAddress(reci)); // ur.GetEmail(deserialized.Owner)
                        message.Subject = "New File In Inventory - ShareFiles";
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = $"File Name:{deserialized.Name}; <p style='text-decoration:none' > <b>Download Link:</b>{downloadLink} </p>"; //{deserialized.Link}
                        smtp.Port = 587;
                        smtp.Host = "smtp.gmail.com"; //for gmail host  
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("cloudprogramming4321@gmail.com", "EASFI23fr");

                        //go on google while you are logged in in this account > search for lesssecureapps > turn it to on

                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);


                        /*
                        RestClient c = new RestClient();
                        c.BaseUrl = new Uri("https://api.mailgun.net/v3");


                        c.Authenticator = new HttpBasicAuthenticator("api", "81508fb5c901d48f904b06340a169d34-0afbfc6c-e2c30710");
                        RestRequest request = new RestRequest();
                        request.AddParameter("domain", "sandbox4b6e064558e3424fabb0b7ae002fd66d.mailgun.org", ParameterType.UrlSegment);
                        request.Resource = "{domain}/messages";
                        request.AddParameter("from", "Excited User <sharefiles@sandbox4b6e064558e3424fabb0b7ae002fd66d.mailgun.org>");
                        request.AddParameter("to", deserialized.Recipient);
                        request.AddParameter("to", "cloudprogramming@ShareFiles");
                        request.AddParameter("subject", "New File In Inventory - ShareFiles");
                        request.AddParameter("text", $"File Name:{deserialized.Name}; <p style='text-decoration:none' > <b>Download Link:</b></p>");
                        request.Method = Method.POST;

                        c.Execute(request);
                        */
                    }
                    catch (Exception e)
                    {
                        new LogsRepository().LogError(e);
                    }

                    List<string> acksIds = new List<string>();
                    acksIds.Add(pullResponse.ReceivedMessages[0].AckId); //after the email is sent successfully you acknolwedge the message so it is confirmed that it was processed

                    client.Acknowledge(s.SubscriptionName, acksIds.AsEnumerable());
                }

            }
        }
    }
}