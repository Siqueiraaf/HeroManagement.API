using HeroManagement.Application;
using Microsoft.AspNetCore.Mvc;

namespace HeroManagement.API;

[ApiController]
[Route("api/herois")]
public class HeroManagementController(IHeroiService service) : ControllerBase
{
    private readonly IHeroiService _service = service;

    [HttpPost("create")]
    public async Task<IActionResult> CriarHeroi([FromBody] CriarHeroiDto dto)
    {
        var id = await _service.CriarHeroiAsync(dto);
        return Ok(new { id });
    }

    [HttpGet("search/{id}")]
    public async Task<IActionResult> ObterHeroiPorId(int id)
    {
        var heroi = await _service.ObterHeroiPorIdAsync(id);
        if (heroi == null)
            return NotFound();
        return Ok(heroi);
    }

    [HttpGet("search")]
    public async Task<IActionResult> ObterTodosHerois()
    {
        var herois = await _service.ObterTodosHeroisAsync();
        return Ok(herois);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> AtualizarHeroi(int id, [FromBody] AtualizarHeroiDto dto)
    {
        await _service.AtualizarHeroiAsync(id, dto);
        return Ok(new { id });
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> RemoverHeroi(int id)
    {
        await _service.RemoverHeroiAsync(id);
        return Ok(new { id });
    }
}
