using System.ComponentModel.DataAnnotations;
using Demo.Core.Application.Courses.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Courses;

public class Create : PageModel
{
    private readonly IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _mediator.Send(new CreateCourse.Command(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Title, Description));

        return RedirectToPage(nameof(Students.Index));
    }

    [Display(Name = "Title")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    public string Title { get; set; }

    [Display(Name = "Description")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    public string Description { get; set; }
}