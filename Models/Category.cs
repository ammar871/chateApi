using System.ComponentModel.DataAnnotations;

namespace aucationApi.Models{


    public class Category{


         [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }
    }
}