using System.IO.Compression;
using System.Net.Http.Headers;
using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
public class ArquivoController : ControllerBase
{
    private readonly IArquivoService _arquivoService;
    public ArquivoController(IArquivoService arquivoService)
    {
        _arquivoService = arquivoService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload()
    {
        var files = Request.Form.Files;

        if (files.Any(x => x.Length == 0))
        {
            return BadRequest();
        }

        await _arquivoService.Adicionar(files);

        return Ok();
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Processar(int id)
    {
        await _arquivoService.Processar(id);

        return Ok();
    }
}