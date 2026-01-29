using HeroManagement.Domain;
using HeroManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/superpoderes")]
public class SuperpoderesController(HeroManagementDbContext context) : ControllerBase
{
    private readonly HeroManagementDbContext _context = context;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var superpoderes = await _context.Superpoderes.ToListAsync();
        return Ok(superpoderes ?? []);
    }
}
