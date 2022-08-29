using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Identity;
namespace chatApi.Models{


    public class Group{


         [Key]
        public int Id { get; set; }


         public string Name { get; set; }

         public string Image { get; set; }

         public string Desc { get; set; }
   
         public string Users { get; set; }

         public string Admins { get; set; }

         public string Message { get; set; }

       

         public string Type { get; set; }

         public int Status { get; set; }

            public DateTime CreatedAt { get; set; }
          public Group() {
           CreatedAt = DateTime.Now;
        }

    }
}