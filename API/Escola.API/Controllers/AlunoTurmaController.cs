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
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AlunoTurmaResponse>> Add([FromBody] AlunoTurmaRequest alunoTurma)
        {
            var response = await _alunoTurmaService.AddAsync(alunoTurma);
            return CreatedAtAction(nameof(Add), response);
        }

        [HttpDelete(ApiRoutes.AlunoTurma.Ids)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int alunoId, [FromRoute] int turmaId)
        {
            await _alunoTurmaService.DeleteAsync(alunoId, turmaId);
            return NoContent();
        }
    }
}
