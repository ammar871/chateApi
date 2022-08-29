using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

using chatApi.Models;
namespace chatApi.Data
{



    public class DBContext : IdentityDbContext<User>
    {

        public DBContext(DbContextOptions<DBContext> otp) : base(otp)
                     {}

          public DbSet<Room> Rooms { get; set; }

            public DbSet<Message> Messages { get; set; }

            public DbSet<Group> Groups { get; set; }

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