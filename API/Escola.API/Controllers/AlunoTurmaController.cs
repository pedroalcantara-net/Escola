using Escola.API.Contract;
using Escola.Application.Contract.AlunoTurma;
using Escola.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Escola.Api.Controllers
{
    [Route(ApiRoutes.Controller)]
    [ApiController]
    [Consumes(ContentTypes.Json)]
    public class AlunoTurmaController(IAlunoTurmaService alunoTurmaService) : ControllerBase
    {

        private readonly IAlunoTurmaService _alunoTurmaService = alunoTurmaService;

        [HttpPost]
        [ProducesResponseType(typeof(AlunoTurmaResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErroResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErroResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AlunoTurmaResponse>> Add([FromBody] AlunoTurmaRequest alunoTurma)
        {
            var response = await _alunoTurmaService.AddAsync(alunoTurma);
            return CreatedAtAction(nameof(Add), response);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErroResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromQuery] int alunoId, [FromQuery] int turmaId)
        {
            await _alunoTurmaService.DeleteAsync(alunoId, turmaId);
            return NoContent();
        }
    }
}
