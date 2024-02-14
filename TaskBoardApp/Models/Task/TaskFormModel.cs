using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Models.Board;
using static TaskBoardApp.Infrastructure.Constants.DataConstants.Task;
using static TaskBoardApp.Infrastructure.Constants.ErrorMessages;

namespace TaskBoardApp.Models.Task;

public class TaskFormModel
{
    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = ErrorMessage)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = ErrorMessage)]
    public string Description { get; set; } = string.Empty;

    public int BoardId { get; set; }

    public IEnumerable<TaskBoardModel> Boards { get; set; } = new List<TaskBoardModel>();
}