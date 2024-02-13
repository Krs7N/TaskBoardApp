using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static TaskBoardApp.Infrastructure.Constants.DataConstants.Task;

namespace TaskBoardApp.Data.Models;

[Comment("Represents a Task")]
public class Task
{
    [Key]
    [Comment("Task Identifier")]
    public int Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    [Comment("The title of the task")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    [Comment("The description of the task")]
    public string Description { get; set; } = string.Empty;

    [Comment("The date time when the task has been created")]
    public DateTime CreatedOn { get; set; }

    [Comment("Reference to the Board of which the Task is part of")]
    public int BoardId { get; set; }

    [ForeignKey(nameof(BoardId))]
    public Board? Board { get; set; }

    [Required]
    [Comment("The identifier of the user who has created the task")]
    public string OwnerId { get; set; } = null!;

    public IdentityUser Owner { get; set; } = null!;
}