using System.ComponentModel.DataAnnotations;
using Demo.Core.Application.Students.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Students;

public class View : PageModel
{
    private readonly IMediator _mediator;

    public View(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var student = await _mediator.Send(new GetStudent.Query(id));
        Id = id;
        FirstName = student.FirstName;
        LastName = student.LastName;
        return Page();
    }
    
    public Guid Id { get; set; }
    
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }
}