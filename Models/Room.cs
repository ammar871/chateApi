using System.ComponentModel.DataAnnotations;

using System;
using Microsoft.AspNetCore.Identity;
namespace chatApi.Models{


    public class Room{


         [Key]
        public int Id { get; set; }

        public string Message { get; set; }

         public string SenderId { get; set; }
          public string UserId { get; set; }
          public string Type { get; set; }
         public int Status { get; set; }

            public DateTime CreatedAt { get; set; }
          public Room() {
           CreatedAt = DateTime.Now;
        }

    }
}