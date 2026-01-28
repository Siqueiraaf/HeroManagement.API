using HeroManagement.Application;
using HeroManagement.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HeroManagement.API;

[ApiController]
[Route("api/heroi")]
public class HeroManagementController(IHeroiService service) : ControllerBase
{
    private readonly IHeroiService _service = service;

    [HttpPost("criar-heroi")]
    public async Task<IActionResult> CriarHeroi([FromBody]CriarHeroiDto dto)
    {
        var id = await _service.CriarHeroiAsync(dto);
        return CreatedAtAction(nameof(ObterHeroiPorId), new { id }, null);
    }

    [HttpGet("buscar-heroi-por-{id}")]
    public async Task<IActionResult> ObterHeroiPorId(int id)
    {
        var heroi = await _service.ObterHeroiPorIdAsync(id);
        if (heroi == null)
            return NotFound();
        return Ok(heroi);
    }

    [HttpGet("buscar-todos-herois")]
    public async Task<IActionResult> ObterTodosHerois()
    {
        var herois = await _service.ObterTodosHeroisAsync();
        return Ok(herois);
    }

    [HttpPut("atualizar-heroi-{id}")]
    public async Task<Heroi> AtualizarHeroi(int id, [FromBody] AtualizarHeroiDto dto)
    {
        await _service.AtualizarHeroiAsync(id, dto);
        var heroiAtualizado = await _service.ObterHeroiPorIdAsync(id);
        return heroiAtualizado!;
    }

    [HttpDelete("deletar-heroi-{id}")]
    public async Task<IActionResult> RemoverHeroi(int id)
    {
        await _service.RemoverHeroiAsync(id);
        return Ok();
    }
}
