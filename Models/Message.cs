using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Identity;
namespace chatApi.Models
{
  
    public class Message
    {


       [Key]
       public int Id { get; set; }

         public int RoomId { get; set; }

         public string UserId { get; set; }
         public string SenderId { get; set; }

         public string MessageText { get; set; }

          public string Type { get; set; }
          public string TypeSender { get; set; }
          public int Status { get; set; }

          public DateTime CreatedAt { get; set; }
          public Message() {
           CreatedAt = DateTime.Now;
        }
    }
}