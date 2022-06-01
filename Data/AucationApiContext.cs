using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

using aucationApi.Models;
namespace aucationApi.Data
{



    public class AucationApiContext : IdentityDbContext<User>
    {

        public AucationApiContext(DbContextOptions<AucationApiContext> otp) : base(otp)
                     {}

        







        public DbSet<Category> Categories { get; set; }

        // public DbSet<Product> Products { get; set; }

        //    public DbSet<Slider> Sliders { get; set; }

        //   public DbSet<Field> Fields { get; set; }

        //     public DbSet<Cart> Carts { get; set; }

        //   public DbSet<Order> Orders { get; set; }

        // //     public DbSet<Post> Posts { get; set; }

        // //     public DbSet<Comment> Comments { get; set; }

        //     public DbSet<Favorite> Favorites { get; set; }

        //  public DbSet<Address> Addresses { get; set; }


        // public DbSet<Driver> Drivers { get; set; }
        // public DbSet<Driver_Field> Driver_Fields { get; set; }
        // public DbSet<Driver_Order> Driver_Orders { get; set; }

        // internal object Where(Func<object, object> value)
        // {
        //     throw new NotImplementedException();
        // }

    }
}