using System.ComponentModel.DataAnnotations;
using Demo.Core.Application.Students.Commands;
using Demo.Core.Application.Students.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Users;

public class Edit : PageModel
{
    private readonly IMediator _mediator;

    public Edit(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var student = await _mediator.Send(new GetStudent.Query(id));
        Id = student.Id;
        FirstName = student.FirstName;
        LastName = student.LastName;
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _mediator.Send(new UpdateStudentName.Command(Id, FirstName, LastName));
        
        return RedirectToPage(nameof(Index));
    }

    [Required]
    [BindProperty]
    public Guid Id { get; set; }
    
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