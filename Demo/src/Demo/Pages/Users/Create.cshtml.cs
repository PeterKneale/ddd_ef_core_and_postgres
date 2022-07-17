using System.ComponentModel.DataAnnotations;
using Demo.Core.Application.Students.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Users;

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

        await _mediator.Send(new CreateStudent.Command(Guid.NewGuid(), FirstName, LastName));
        
        return RedirectToPage(nameof(Index));
    }

    [Display(Name = "First Name")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    public string LastName { get; set; }
}