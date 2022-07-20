using Demo.Core.Application.Courses.Queries;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Courses;

public class Index : PageModel
{
    private readonly IMediator _mediator;

    public Index(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task OnGetAsync()
    {
        Data = await _mediator.Send(new ListCourses.Query());
    }

    public ListCourses.Result Data { get; private set; }
}