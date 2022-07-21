using FluentResults;
using GeradorTestes.Dominio.ModuloQuestao;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Aplicacao.ModuloQuestao
{
    public class ServicoQuestao
    {
        private IRepositorioQuestao repositorioQuestao;

        public ServicoQuestao(IRepositorioQuestao repositorioQuestao)
        {
            this.repositorioQuestao = repositorioQuestao;
        }

        public Result<Questao> Inserir(Questao questao)
        {
            Log.Logger.Debug("Tentando inserir questao... {@q}", questao);

            Result resultado = ValidarQuestao(questao);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioQuestao.Inserir(questao);

                Log.Logger.Information("Questao {QuestaoId} inserida com sucesso", questao.Id);

                return Result.Ok(questao);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar inserir a Questao";

                Log.Logger.Error(ex, msgErro + " {QuestaoId}", questao.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<Questao> Editar(Questao questao)
        {
            Log.Logger.Debug("Tentando editar questao... {@q}", questao);

            var resultado = ValidarQuestao(questao);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioQuestao.Editar(questao);

                Log.Logger.Information("Questao {QuestaoId} editada com sucesso", questao.Id);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar editar a Questao";

                Log.Logger.Error(ex, msgErro + " {QuestaoId}", questao.Id);

                return Result.Fail(msgErro);
            }

            return Result.Ok(questao);
        }

        public Result Excluir(Questao questao)
        {
            Log.Logger.Debug("Tentando excluir questao... {@q}", questao);

            try
            {
                repositorioQuestao.Excluir(questao);

                Log.Logger.Information("Questao {QuestaoId} editada com sucesso", questao.Id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar excluir a Questao";

                Log.Logger.Error(ex, msgErro + " {QuestaoId}", questao.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<Questao>> SelecionarTodos()
        {
            Log.Logger.Debug("Tentando selecionar questões...");

            try
            {
                return Result.Ok(repositorioQuestao.SelecionarTodos());
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todas as Questões";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }
        }

        public Result<Questao> SelecionarPorId(Guid id)
        {
            try
            {
                var questao = repositorioQuestao.SelecionarPorId(id);

                if (questao == null)
                    return Result.Fail("Questao não encontrado");

                return Result.Ok(questao);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar a Questao";

                Log.Logger.Error(ex, msgErro + " {QuestaoId}", id);

                return Result.Fail(msgErro);
            }
        }

        private Result ValidarQuestao(Questao questao)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidacao = validador.Validate(questao);

            var erros = new List<Error>();

            foreach (var validationFailure in resultadoValidacao.Errors)
            {
                Log.Logger.Warning(validationFailure.ErrorMessage);

                erros.Add(new Error(validationFailure.ErrorMessage));
            }
          
            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }       
    }
}
