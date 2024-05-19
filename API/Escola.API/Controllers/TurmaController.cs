using Escola.API.Contract;
using Escola.Application.Contract.Aluno;
using Escola.Application.Contract.Turma;
using Escola.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Escola.API.Controllers
{
    [Route(ApiRoutes.Controller)]
    [ApiController]
    [Consumes(ContentTypes.Json)]
    public class TurmaController(ITurmaService turmaService) : ControllerBase
    {
        private readonly ITurmaService _turmaService = turmaService;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TurmaResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<TurmaResponse>>> Get()
            => Ok(await _turmaService.GetAsync());

        [HttpGet(ApiRoutes.Turma.Id)]
        [ProducesResponseType(typeof(IEnumerable<TurmaResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<TurmaResponse>>> GetById([FromRoute] int id)
            => Ok(await _turmaService.GetByIdAsync(id));

        [HttpGet(ApiRoutes.Turma.Aluno)]
        [ProducesResponseType(typeof(IEnumerable<AlunoResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<AlunoResponse>>> GetTurmaById([FromServices] IAlunoService alunoService, [FromRoute] int id)
            => Ok(await alunoService.GetByTurmaIdAsync(id));

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<TurmaResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TurmaResponse>> Add([FromBody] TurmaRequest turma)
        {
            var response = await _turmaService.AddAsync(turma);
            return CreatedAtAction(nameof(Add), response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<TurmaResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TurmaResponse>> Update([FromBody] TurmaRequest turma)
            => Ok(await _turmaService.UpdateAsync(turma));

        [HttpDelete(ApiRoutes.Turma.Id)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _turmaService.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}
