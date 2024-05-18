using Escola.Application.Contract.Turma;
using Escola.Application.Interface;
using Escola.Domain.Entity;
using Escola.Domain.Erros;
using Escola.Domain.Exception;
using Escola.Domain.Interface.Repository;

namespace Escola.Application.Service
{
    public class TurmaService(ITurmaRepository turmaRepository) : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository = turmaRepository;
        private readonly List<Erro> _erros = [];

        public async Task<TurmaResponse> AddAsync(TurmaRequest turmaRequest)
        {
            if (await _turmaRepository.NomeExistsAsync(turmaRequest.Nome)) _erros.Add(DomainErrors.Turma.NomeExists);

            if (turmaRequest.Ano < DateTime.Now.Year) _erros.Add(DomainErrors.Turma.InvalidAno);

            if (_erros.Count == 0) throw new BadRequestException(_erros);

            var turma = new Turma()
            {
                Nome = turmaRequest.Nome,
                CursoId = turmaRequest.CursoId,
                Ano = turmaRequest.Ano
            };

            turma = await _turmaRepository.AddAsync(turma);

            return new TurmaResponse(turma);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _ = await _turmaRepository.GetByIdAsync(id) ?? throw new NotFoundException(DomainErrors.Turma.NotFound);

            await _turmaRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<TurmaResponse>> GetAsync()
        {
            var turmas = await _turmaRepository.GetAsync();

            if (!turmas.Any()) throw new NotFoundException(DomainErrors.Turma.NoneFound);

            return turmas.Select(x => new TurmaResponse(x)).ToList();
        }

        public async Task<TurmaResponse> GetByIdAsync(int id)
        {
            var turma = await _turmaRepository.GetByIdAsync(id) ?? throw new NotFoundException(DomainErrors.Turma.NotFound);

            return new TurmaResponse(turma);
        }

        public async Task<TurmaResponse> UpdateAsync(TurmaRequest turmaRequest)
        {
            var turma = await _turmaRepository.GetByIdAsync(turmaRequest.Id) ?? throw new NotFoundException(DomainErrors.Turma.NotFound);

            await _turmaRepository.UpdateAsync(turma);

            return new TurmaResponse(turma);
        }
    }
}
