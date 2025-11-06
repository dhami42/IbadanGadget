using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbadanGadgetAPI.Entities
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, MaxLength(100)]
		public string FullName { get; set; } = string.Empty;

		[Required, MaxLength(100)]
		public string Email { get; set; } = string.Empty;

		// Store hashed password only
		[Required]
		public string PasswordHash { get; set; } = string.Empty;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }
	}
}
