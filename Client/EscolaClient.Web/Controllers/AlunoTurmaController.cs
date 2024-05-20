using EscolaClient.Application.Interface.Service;
using EscolaClient.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EscolaClient.Web.Controllers
{
    public class AlunoTurmaController(IAlunoService alunoService, IAlunoTurmaService alunoTurmaService, ITurmaService turmaService) : Controller
    {
        private readonly IAlunoService _alunoService = alunoService;
        private readonly IAlunoTurmaService _alunoTurmaService = alunoTurmaService;
        private readonly ITurmaService _turmaService = turmaService;

        public async Task<ActionResult> Index()
        {
            var turmas = await _turmaService.Get();

            if (turmas.Erros?.Any() == true) turmas.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));

            turmas.Response ??= [];

            return View(turmas.Response);
        }

        public async Task<ActionResult> Turma(int id)
        {
            var turma = await _turmaService.GetById(id);
            if (turma.Erros?.Any() == true) turma.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));

            var alunos = await _alunoService.GetByTurmaId(id);
            if (alunos.Erros?.Any() == true) alunos.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
            alunos.Response ??= [];

            var model = new AlunoTurmaListViewModel()
            {
                Turma = turma.Response,
                Alunos = alunos.Response
            };

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var alunos = await _alunoService.Get();
            var turmas = await _turmaService.Get();

            if (alunos.Erros?.Any() == true) alunos.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
            if (turmas.Erros?.Any() == true) turmas.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));

            var model = new AlunoTurmaCreateViewModel()
            {
                Alunos = alunos.Response,
                Turmas = turmas.Response
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AlunoTurmaCreateViewModel alunoTurma)
        {
            try
            {
                var model = await _alunoTurmaService.Add(alunoTurma);

                if (model.Erros?.Any() == true) model.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));

                if (!ModelState.IsValid)
                {
                    var alunos = await _alunoService.Get();
                    var turmas = await _turmaService.Get();

                    if (alunos.Erros?.Any() == true) alunos.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
                    if (turmas.Erros?.Any() == true) turmas.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));

                    alunoTurma = new AlunoTurmaCreateViewModel()
                    {
                        Alunos = alunos.Response,
                        Turmas = turmas.Response
                    };

                    return View(alunoTurma);
                }

                return RedirectToAction(nameof(Turma), new { id = alunoTurma.TurmaId });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int alunoId, int turmaId)
        {
            var aluno = await _alunoService.GetById(alunoId);
            var turma = await _turmaService.GetById(turmaId);

            if (aluno.Erros?.Any() == true) aluno.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
            if (turma.Erros?.Any() == true) turma.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));

            var model = new AlunoTurmaDeleteViewModel()
            {
                Aluno = aluno.Response,
                Turma = turma.Response,
                AlunoId = aluno.Response?.Id,
                TurmaId = turma.Response?.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(AlunoTurmaDeleteViewModel model)
        {
            try
            {
                var response = await _alunoTurmaService.Delete(model);

                if (response.Erros?.Any() == true) response.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));

                if (!ModelState.IsValid)
                {
                    var aluno = await _alunoService.GetById(model.AlunoId ?? 0);
                    if (aluno.Erros?.Any() == true) aluno.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
                    model.Aluno = aluno.Response;

                    var turma = await _turmaService.GetById(model.TurmaId ?? 0);
                    if (turma.Erros?.Any() == true) turma.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
                    model.Turma = turma.Response;

                    return View(model);
                }

                return RedirectToAction(nameof(Turma), new { id = model.TurmaId });
            }
            catch
            {
                return View();
            }
        }
    }
}
