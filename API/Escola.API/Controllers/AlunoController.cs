using Escola.API.Contract;
using Escola.Application.Contract.Aluno;
using Escola.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Escola.API.Controllers
{
    [Route(ApiRoutes.Controller)]
    [ApiController]
    [Consumes(ContentTypes.Json)]
    public class AlunoController(IAlunoService alunoService) : ControllerBase
    {
        private readonly IAlunoService _alunoService = alunoService;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlunoResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<AlunoResponse>>> Get()
            => Ok(await _alunoService.GetAsync());

        [HttpGet(ApiRoutes.Aluno.Id)]
        [ProducesResponseType(typeof(IEnumerable<AlunoResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<AlunoResponse>>> GetById([FromRoute] int id)
            => Ok(await _alunoService.GetByIdAsync(id));

        [HttpGet(ApiRoutes.Aluno.Turma)]
        [ProducesResponseType(typeof(IEnumerable<AlunoResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<AlunoResponse>>> GetTurmaById([FromServices] ITurmaService turmaService, [FromRoute] int id)
            => Ok(await turmaService.GetByAlunoIdAsync(id));

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<AlunoResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AlunoResponse>> Add([FromBody] AlunoRequest aluno)
        {
            var response = await _alunoService.AddAsync(aluno);
            return CreatedAtAction(nameof(Add), response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<AlunoResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AlunoResponse>> Update([FromBody] AlunoRequest aluno)
            => Ok(await _alunoService.UpdateAsync(aluno));

        [HttpDelete(ApiRoutes.Aluno.Id)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErroListResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _alunoService.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}
