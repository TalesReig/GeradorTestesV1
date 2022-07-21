using FluentResults;
using GeradorTestes.Dominio.ModuloTeste;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Aplicacao.ModuloTeste
{
    public class ServicoTeste
    {
        private IRepositorioTeste repositorioTeste;

        public ServicoTeste(IRepositorioTeste repositorioTeste)
        {
            this.repositorioTeste = repositorioTeste;
        }

        public Result<Teste> Inserir(Teste teste)
        {
            Log.Logger.Debug("Tentando inserir teste... {@t}", teste);

            Result resultado = ValidarTeste(teste);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioTeste.Inserir(teste);

                Log.Logger.Information("Teste {TesteId} inserido com sucesso", teste.Id);

                return Result.Ok(teste);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar inserir o Teste";

                Log.Logger.Error(ex, msgErro + " {TesteId}", teste.Id);

                return Result.Fail(msgErro);
            }
        }
      
        public Result Excluir(Teste teste)
        {
            Log.Logger.Debug("Tentando excluir teste... {@t}", teste);

            try
            {
                repositorioTeste.Excluir(teste);

                Log.Logger.Information("Teste {TesteId} editada com sucesso", teste.Id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar excluir o Teste";

                Log.Logger.Error(ex, msgErro + " {TesteId}", teste.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<Teste>> SelecionarTodos()
        {
            Log.Logger.Debug("Tentando selecionar testes...");

            try
            {
                return Result.Ok(repositorioTeste.SelecionarTodos());
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todos os testes";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }
        }

        public Result<Teste> SelecionarPorId(Guid id)
        {
            try
            {
                var teste = repositorioTeste.SelecionarPorId(id);

                if (teste == null)
                    return Result.Fail("Teste não encontrado");

                return Result.Ok(teste);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar o Teste";

                Log.Logger.Error(ex, msgErro + " {TesteId}", id);

                return Result.Fail(msgErro);
            }
        }

        private Result ValidarTeste(Teste teste)
        {
            var validador = new ValidadorTeste();

            var resultadoValidacao = validador.Validate(teste);

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
