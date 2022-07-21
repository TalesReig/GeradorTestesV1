using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using GeradorTestes.Dominio.ModuloDisciplina;
using Serilog;

namespace GeradorTestes.Aplicacao.ModuloDisciplina
{
    public class ServicoDisciplina
    {
        private IRepositorioDisciplina repositorioDisciplina;

        public ServicoDisciplina(IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioDisciplina = repositorioDisciplina;
        }

        public Result<Disciplina> Inserir(Disciplina disciplina)
        {
            Log.Logger.Debug("Tentando inserir disciplina... {@d}", disciplina);

            Result resultado = ValidarDisciplina(disciplina);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioDisciplina.Inserir(disciplina);

                Log.Logger.Information("Disciplina {DisciplinaId} inserida com sucesso", disciplina.Id);

                return Result.Ok(disciplina);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar inserir a Disciplina";

                Log.Logger.Error(ex, msgErro + " {DisciplinaId}", disciplina.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<Disciplina> Editar(Disciplina disciplina)
        {
            Log.Logger.Debug("Tentando editar disciplina... {@d}", disciplina);

            var resultado = ValidarDisciplina(disciplina);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioDisciplina.Editar(disciplina);

                Log.Logger.Information("Disciplina {DisciplinaId} editada com sucesso", disciplina.Id);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar editar a Disciplina";

                Log.Logger.Error(ex, msgErro + " {DisciplinaId}", disciplina.Id);

                return Result.Fail(msgErro);
            }

            return Result.Ok(disciplina);
        }

        public Result Excluir(Disciplina disciplina)
        {
            Log.Logger.Debug("Tentando excluir disciplina... {@d}", disciplina);

            try
            {
                repositorioDisciplina.Excluir(disciplina);

                Log.Logger.Information("Disciplina {DisciplinaId} editada com sucesso", disciplina.Id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar excluir a Disciplina";

                Log.Logger.Error(ex, msgErro + " {DisciplinaId}", disciplina.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<Disciplina>> SelecionarTodos()
        {
            Log.Logger.Debug("Tentando selecionar disciplinas...");

            try
            {
                return Result.Ok(repositorioDisciplina.SelecionarTodos());
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todas as Disciplinas";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }
        }

        public Result<Disciplina> SelecionarPorId(Guid id)
        {
            try
            {
                var disciplina = repositorioDisciplina.SelecionarPorId(id);

                if (disciplina == null)
                    return Result.Fail("Disciplina não encontrado");

                return Result.Ok(disciplina);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar o Disciplina";

                Log.Logger.Error(ex, msgErro + " {DisciplinaId}", id);

                return Result.Fail(msgErro);
            }
        }

        private Result ValidarDisciplina(Disciplina disciplina)
        {
            var validador = new ValidadorDisciplina();

            var resultadoValidacao = validador.Validate(disciplina);

            var erros = new List<Error>();

            foreach (var validationFailure in resultadoValidacao.Errors)
            {
                Log.Logger.Warning(validationFailure.ErrorMessage);

                erros.Add(new Error(validationFailure.ErrorMessage));
            }

            if (NomeDuplicado(disciplina))
                erros.Add(new Error($"Este nome \"{disciplina.Nome}\" já está cadastrado no sistema"));
            
            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }

        private bool NomeDuplicado(Disciplina disciplina)
        {
            var disciplinaEncontrado = repositorioDisciplina.SelecionarDisciplinaPorNome(disciplina.Nome);

            return disciplinaEncontrado != null &&
                   disciplinaEncontrado.Nome == disciplina.Nome &&
                   disciplinaEncontrado.Id != disciplina.Id;
        }       
    }
}
