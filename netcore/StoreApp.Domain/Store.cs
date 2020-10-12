

using System.ComponentModel.DataAnnotations;

namespace StoreApp.Domain
{
    public partial class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
