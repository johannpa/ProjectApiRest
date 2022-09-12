using System.ComponentModel.DataAnnotations;

namespace Projet.Api.Rest.Models
{
    public class ItemModel
    {
        [Key]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
