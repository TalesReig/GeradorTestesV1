using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using GeradorTestes.Infra.Orm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace GeradorTeste.ConsoleApp
{
    internal class Program
    {
        private static string connectionString;

        static void Main(string[] args)
        {
            //ConfigurarConnectionString();

            //LimparTabelas();

            //InserindoDisciplina(); //inserindo um registro sem relacionamento

            //InserindoMaterias(); //inserindo um registro com relacionamentos de dependencia

            //InserindoQuestoes(); //inserindo registros filhos 

            //AtualizandoQuestoes(); //atualização de registros com depedencias

            //InserindoTestes(); //inserindo registros N x N
        }

        /** Códigos de exemplo
        private static void ConfigurarConnectionString()
        {
            var configuracao = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("ConfiguracaoAplicacao.json")
                          .Build();

            connectionString = configuracao.GetConnectionString("SqlServer");
        }

        private static void InserindoTestes()
        {
            Console.Clear();

            var dbContext = new GeradorTesteDbContext(connectionString);

            for (int i = 1; i < 11; i++)
            {
                InserindoQuestoes($"Quanto é {i} + {i} ?");
            }

            var teste = new Teste();
            teste.Titulo = "Provão de Recuperação";

            var disciplina = dbContext.Disciplinas
                .Include(x => x.Materias)
                .ThenInclude(x => x.Questoes)
                .First(x => x.Nome == "Matemática");

            teste.Disciplina = disciplina;
            teste.Materia = disciplina.Materias[0];
            teste.Provao = true;
            teste.QuantidadeQuestoes = 5;

            teste.SortearQuestoes();

            dbContext.Testes.Add(teste);

            dbContext.SaveChanges();
        }

        private static void AtualizandoQuestoes()
        {
            Console.Clear();

            var dbContext = new GeradorTesteDbContext(connectionString);

            var questao = dbContext.Questoes
                .Include(x => x.Materia)
                .Include(x => x.Alternativas)
                .First();

            questao.Enunciado = "Quanto é 123 + 123?";

            questao.Alternativas.Remove(questao.Alternativas[0]);

            Alternativa alternativa = new Alternativa();
            alternativa.Letra = questao.GerarLetraAlternativa();
            alternativa.Resposta = 246.ToString();

            questao.AdicionarAlternativa(alternativa);

            dbContext.Questoes.Update(questao);

            dbContext.SaveChanges();
        }

        private static void InserindoQuestoes(string pergunta = "Quanto é 2 + 2 ?") //parâmetro opcional
        {
            Console.Clear();

            var dbContext = new GeradorTesteDbContext(connectionString);

            var questao = new Questao(pergunta);

            var materia = dbContext.Materias.First(x => x.Nome.Contains("Adição"));

            questao.ConfigurarMateria(materia);

            for (int i = 1; i < 5; i++)
            {
                Alternativa alternativa = new Alternativa();
                alternativa.Letra = questao.GerarLetraAlternativa();
                alternativa.Resposta = i.ToString();

                questao.AdicionarAlternativa(alternativa);
            }

            dbContext.Questoes.Add(questao);

            dbContext.SaveChanges();
        }

        private static void LimparTabelas()
        {
            var dbContext = new GeradorTesteDbContext(connectionString);

            dbContext.Testes.RemoveRange(dbContext.Testes);

            dbContext.Questoes.RemoveRange(dbContext.Questoes);

            dbContext.Materias.RemoveRange(dbContext.Materias);

            dbContext.Disciplinas.RemoveRange(dbContext.Disciplinas);

            dbContext.SaveChanges();
        }

        private static void InserindoDisciplina()
        {
            System.Console.Clear();

            GeradorTesteDbContext dbContext = new GeradorTesteDbContext(connectionString);

            var disciplina = new Disciplina();
            disciplina.Nome = "Matemática";

            dbContext.Disciplinas.Add(disciplina);

            dbContext.SaveChanges();
        }

        private static void InserindoMaterias()
        {
            System.Console.Clear();

            GeradorTesteDbContext dbContext = new GeradorTesteDbContext(connectionString);

            var materia = new Materia("Adição de Unidades", SerieMateriaEnum.PrimeiraSerie);

            var disciplina = dbContext.Disciplinas.First(x => x.Nome == "Matemática");

            materia.ConfigurarDisciplina(disciplina);

            dbContext.Materias.Add(materia);

            dbContext.SaveChanges();
        }
        */
    }
}