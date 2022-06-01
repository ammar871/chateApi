// Products
// id sellerId name detail price(double) image categoryId(int) brandId(int) status(int)

using System;
using System.ComponentModel.DataAnnotations;

namespace aucationApi.Dto
{

    // id  userId price(double) status(int) sellerId createdAt(datetime)

    public class OrderCreateDto
    {

       
        public string userId { get; set; }

        public double Price { get; set; }

          public int AddressId { get; set; }
        public int Status { get; set; }

        public int SellerId { get; set; }




        
    }
}