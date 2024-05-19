using EscolaClient.Application.Interface.Service;
using EscolaClient.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace EscolaClient.Web.Controllers
{
    public class AlunoController(IAlunoService alunoService) : Controller
    {
        private readonly IAlunoService _alunoService = alunoService;

        public async Task<ActionResult> Index()
        {
            var alunos = await _alunoService.Get();

            if (alunos.Erros?.Any() == true) alunos.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));

            alunos.Response ??= [];

            return View(alunos.Response);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                var model = await _alunoService.Add(aluno);

                if (model.Erros?.Any() == true) model.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));

                if (!ModelState.IsValid) return View(aluno);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var aluno = await _alunoService.GetById(id);

            if (aluno.Erros?.Any() == true) aluno.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

            return View(aluno.Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Aluno aluno)
        {
            try
            {
                var model = await _alunoService.Update(aluno);

                if (model.Erros?.Any() == true) model.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
                if (!ModelState.IsValid) return View(aluno);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var aluno = await _alunoService.GetById(id);

            if (aluno.Erros?.Any() == true) aluno.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

            return View(aluno.Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Aluno aluno)
        {
            try
            {
                var model = await _alunoService.Delete(aluno.Id ?? 0);

                if (model.Erros?.Any() == true) model.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
                if (!ModelState.IsValid) return View(aluno);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
