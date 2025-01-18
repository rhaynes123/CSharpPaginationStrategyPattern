using System.ComponentModel.DataAnnotations;

namespace Physicists.Models;

public class Physicist
{
    [Key]
    public int Id { get; init; }
    [StringLength(150)]
    public required string Name { get; init; }
    public DateOnly DateOfBirth { get; init; }
    
}