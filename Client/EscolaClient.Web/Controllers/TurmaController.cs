using EscolaClient.Application.Interface.Service;
using EscolaClient.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace EscolaClient.Web.Controllers
{
    public class TurmaController(ITurmaService TurmaService) : Controller
    {
        private readonly ITurmaService _turmaService = TurmaService;

        public async Task<ActionResult> Index()
        {
            var turmas = await _turmaService.Get();

            if (turmas.Erros?.Any() == true) turmas.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));

            turmas.Response ??= [];

            return View(turmas.Response);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Turma turma)
        {
            try
            {
                var model = await _turmaService.Add(turma);

                if (model.Erros?.Any() == true) model.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));

                if (!ModelState.IsValid) return View(turma);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var turma = await _turmaService.GetById(id);

            if (turma.Erros?.Any() == true) turma.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

            return View(turma.Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Turma turma)
        {
            try
            {
                var model = await _turmaService.Update(turma);

                if (model.Erros?.Any() == true) model.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
                if (!ModelState.IsValid) return View(turma);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var turma = await _turmaService.GetById(id);

            if (turma.Erros?.Any() == true) turma.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

            return View(turma.Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Turma turma)
        {
            try
            {
                var model = await _turmaService.Delete(turma.Id ?? 0);

                if (model.Erros?.Any() == true) model.Erros.ToList().ForEach(x => ModelState.AddModelError(x.Codigo, x.Descricao));
                if (!ModelState.IsValid) return View(turma);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
