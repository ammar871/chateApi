
using System.Security.Claims;
using System.Threading.Tasks;
using chatApi.Models;
using chatApi.Data;
using Microsoft.AspNetCore.Http;
using chatApi.Notification;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;


namespace chatApi.Helpers{



    class Functions {

 public static Functions slt = new Functions();


    public async Task<bool> SendNotificationAsync(string userId, string title, string body,bool save, DBContext context,string senderId)
    {

          User user =  context.Users.FirstOrDefault(x => x.Id == userId);

           string token = user.DeviceToken;
        using (var client = new HttpClient())
        {
            var firebaseOptionsServerId = "AAAARWFZ2zQ:APA91bEJ7wT0KO9MdhfYDkkC4RI_kPsy4SKduQr0N_a_LURCeoEOTuRDybZs6cnG-0rgr5_3XS1vSPseDdZW8CeOqJuMhYFEQTlOKgxbLCtvIAUf5Q8wTaqH6s7nC-eX8EBiUwpAHg-L";
            var firebaseOptionsSenderId = "297986022196";

            client.BaseAddress = new Uri("https://fcm.googleapis.com");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                $"key={firebaseOptionsServerId}");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={firebaseOptionsSenderId}");
            var data = new
            {
                to = token,
                notification = new
                {
                    body = body,
                    title = title,
                },
                data=new  {
                    senderId=senderId
                },
                priority = "high"
            };

            var json = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("/fcm/send", httpContent);

            // if (save) {

            //     userIds.ForEach((id) => {
            //         Alert alert = new Alert()
            //         {
            //             Body = body,
            //             UserId = id
            //         };
            //         context.Alerts.AddAsync(alert);

            //     });

            //     await context.SaveChangesAsync();
            // }
            return result.StatusCode.Equals(HttpStatusCode.OK);
        }
    }




           public static async Task<User> getCurrentUser(IHttpContextAccessor _httpContextAccessor, DBContext _context)
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = await _context.Users.FindAsync(userId);
        return user;
    }

       public static NotificationData SendNotificationFromFirebaseCloud([FromForm] NotificationData data)
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAA835VwYI:APA91bFBUJcvyacL4hB3jUyI2UqwD4zFjpwp_13rK9VbI6iUX-myo1T7Q6UP1a6bONzViRS0VSLTQtdXKkRPGJR5OF54Vq_lFEUk-jyYiWGEYQ2d1spu83RPBolahOrXn3iHEPUGAd3p"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "1045796602242"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = "/topics/admin",
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = data.Body,
                    title = data.Title,
                    badge = 1
                },
                data = new
                {
                    subject = data.Subject,
                    imageUrl = data.ImageUrl,
                    desc = data.Desc,
                    data = data.createAt
                }

            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }

            return data;
        }


    }
    }
