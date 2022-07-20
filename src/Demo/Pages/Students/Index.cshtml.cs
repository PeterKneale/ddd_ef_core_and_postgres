using Demo.Core.Application.Students.Queries;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Students;

public class Index : PageModel
{
    private readonly IMediator _mediator;

    public Index(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task OnGetAsync()
    {
        Data = await _mediator.Send(new ListStudents.Query());
    }

    public ListStudents.Result Data { get; private set; }
}