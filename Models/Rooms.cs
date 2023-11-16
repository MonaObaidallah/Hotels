using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Rooms
    {
		internal int id;

		[Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(50)]
        public string Images { get; set; }
        [Required]
        public int RoomNo { get; set; }
        public int IdHotel { get; set; }

    }
}
