using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static TaskBoardApp.Data.DataConstants.Board;

namespace TaskBoardApp.Data.Models;

public class Board
{
    [Key]
    [Comment("Board identifier")]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment("The name of the board")]
    public string Name { get; set; } = string.Empty;

    public virtual IEnumerable<Task> Tasks { get; set; } = new List<Task>();
}