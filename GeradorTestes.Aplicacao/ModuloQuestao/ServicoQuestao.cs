using FluentResults;
using GeradorTestes.Dominio;
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
        private IContextoPersistencia contextoDados;

        public ServicoQuestao(IRepositorioQuestao repositorioQuestao, IContextoPersistencia contextoDados)
        {
            this.repositorioQuestao = repositorioQuestao;
            this.contextoDados = contextoDados;
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

                contextoDados.GravarDados();

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

                contextoDados.GravarDados();

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

                contextoDados.GravarDados();

                Log.Logger.Information("Questao {QuestaoId} excluída com sucesso", questao.Id);

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
                var questoes = repositorioQuestao.SelecionarTodos();

                Log.Logger.Information("Questões selecionadas com sucesso");

                return Result.Ok(questoes);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todas as Questões";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }
        }

        /// <summary>
        /// Retorna uma questão a partir do seu identificador
        /// </summary>
        /// <returns>Questão com matéria e alternativas carregadas</returns>
        public Result<Questao> SelecionarPorId(Guid id)
        {
            try
            {
                var questao = repositorioQuestao.SelecionarPorId(id, incluirMateria: true, incluirAlternativas: true);

                if (questao == null)
                {
                    Log.Logger.Warning("Questão {QuestaoId} não encontrada", id);

                    return Result.Fail("Questão não encontrada");
                }

                Log.Logger.Information("Questão {QuestaoId} selecionada com sucesso", id);

                return Result.Ok(questao);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar a Questão";

                Log.Logger.Error(ex, msgErro + " {QuestaoId}", id);

                return Result.Fail(msgErro);
            }
        }

        #region Métodos Privados
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

        #endregion
    }
}
