using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Models;

public abstract class BaseEntity
{
  [Key, Column("guid")] public Guid Guid { get; set; }
  [Column("created_date")] public DateTime CreatedDate { get; set; }
  [Column("created_date")] public DateTime ModifiedDate { get; set; }
}