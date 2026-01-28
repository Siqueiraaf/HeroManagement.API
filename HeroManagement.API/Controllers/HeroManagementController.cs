using HeroManagement.Application;
using Microsoft.AspNetCore.Mvc;

namespace HeroManagement.API;

[ApiController]
[Route("api/heroes")]
public class HeroManagementController(IHeroiService service) : ControllerBase
{
    private readonly IHeroiService _service = service;

    [HttpPost("create")]
    public async Task<IActionResult> CriarHeroi(CriarHeroiDto dto)
    {
        var id = await _service.CriarHeroiAsync(dto);
        return CreatedAtAction(nameof(ObterHeroiPorIdAsync), new { id }, null);
    }

    [HttpGet("buscar-por-{id}")]
    public async Task<IActionResult> ObterHeroiPorIdAsync(int id)
    {
        var heroi = await _service.ObterHeroiPorIdAsync(id);
        return heroi is null ? NotFound() : Ok(heroi);
    }

    [HttpGet("buscar-todos")]
    public async Task<IActionResult> ObterTodosHeroisAsync()
    {
        var herois = await _service.ObterTodosHeroisAsync();
        return Ok(herois);
    }

    [HttpPut("atualizar-{id}")]
    public async Task<IActionResult> AtualizarHeroiAsync(int id, AtualizarHeroiDto dto)
    {
        var atualizado = await _service.AtualizarHeroiAsync(id, dto);
        return atualizado ? NoContent() : NotFound();
    }
}
