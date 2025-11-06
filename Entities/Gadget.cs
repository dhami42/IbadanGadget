using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbadanGadgetAPI.Entities
{
	public class Gadget
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, MaxLength(100)]
		public string Name { get; set; } = string.Empty;

		[MaxLength(255)]
		public string? Description { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[Required]
		public int Quantity { get; set; }

		[MaxLength(100)]
		public string? Brand { get; set; }

		[MaxLength(100)]
		public string? Category { get; set; }

		[MaxLength(255)]
		public string? ImageUrl { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }

		// Optional: reference to the user who added this gadget
		public int? UserId { get; set; }
		public User? User { get; set; }
	}
}
