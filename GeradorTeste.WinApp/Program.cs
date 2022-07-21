using eAgenda.Infra.Arquivos;
using eAgenda.Infra.Arquivos.ModuloDisciplina;
using eAgenda.Infra.Arquivos.ModuloMateria;
using eAgenda.Infra.Arquivos.ModuloQuestao;
using eAgenda.Infra.Arquivos.ModuloTeste;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace GeradorTeste.WinApp
{
    internal static class Program
    {
        static ISerializador serializador = new SerializadorDadosEmJsonDotnet();

        static GeradorTesteJsonContext contexto = new GeradorTesteJsonContext(serializador);

        static IRepositorioDisciplina repositorioDisciplina = new RepositorioDisciplinaEmArquivo(contexto);
        static IRepositorioMateria repositorioMateria = new RepositorioMateriaEmArquivo(contexto);
        static IRepositorioQuestao repositorioQuestao = new RepositorioQuestaoEmArquivo(contexto);
        static IRepositorioTeste repositorioTeste = new RepositorioTesteEmArquivo(contexto);


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (File.Exists(@"C:\temp\dados.json") == false)
                ConfigurarAplicacao();

            Application.Run(new TelaPrincipalForm(contexto));

            contexto.GravarDados();
        }
        
        private static void ConfigurarAplicacao()
        {
            ConfigurarTesteMatematica();
            
            ConfigurarTestePortugues();
        }

        private static void ConfigurarTestePortugues()
        {
            var portugues = new Disciplina
            {
                Nome = "Português"
            };

            var vogais = new Materia
            {
                Nome = "Vogais",
                Serie = SerieMateriaEnum.PrimeiraSerie
            };

            vogais.ConfigurarDisciplina(portugues);

            var consoantes = new Materia
            {
                Nome = "Consoantes",
                Serie = SerieMateriaEnum.PrimeiraSerie
            };

            consoantes.ConfigurarDisciplina(portugues);

            Questao q1 = NovaQuestaoPortugues(consoantes, Guid.NewGuid(), 'C', 'A');
            Questao q2 = NovaQuestaoPortugues(consoantes, Guid.NewGuid(), 'E', 'C');
            Questao q3 = NovaQuestaoPortugues(consoantes, Guid.NewGuid(), 'G', 'E');
            Questao q4 = NovaQuestaoPortugues(consoantes, Guid.NewGuid(), 'I', 'G');

            contexto.Disciplinas.Add(portugues);

            contexto.Materias.Add(vogais);
            contexto.Materias.Add(consoantes);

            contexto.Questoes.Add(q1);
            contexto.Questoes.Add(q2);
            contexto.Questoes.Add(q3);
            contexto.Questoes.Add(q4);           

            Teste novoTeste = new Teste();

            novoTeste.Titulo = "Revisão sobre Letras do Alfabeto";
            novoTeste.ConfigurarDisciplina(portugues);
            novoTeste.ConfigurarMateria(consoantes);
            novoTeste.Provao = false;
            novoTeste.QuantidadeQuestoes = 5;
            novoTeste.SortearQuestoes();

            //repositorioDisciplina.Inserir(portugues);

            //repositorioMateria.Inserir(vogais);
            //repositorioMateria.Inserir(consoantes);

            //repositorioQuestao.Inserir(q1);
            //repositorioQuestao.Inserir(q2);
            //repositorioQuestao.Inserir(q3);
            //repositorioQuestao.Inserir(q4);
            //repositorioTeste.Inserir(novoTeste);
        }

        private static Questao NovaQuestaoPortugues(Materia materia, Guid idQuestao, char letra, char resposta)
        {
            var questao = new Questao
            {
                Id = idQuestao,
                Enunciado = $"Depois da letra {letra} qual é a próxima letra do alfabeto?"
            };

            questao.ConfigurarMateria(materia);

            Alternativa[] alternativas = new Alternativa[4];

            for (int i = 0; i < 4; i++)
            {
                alternativas[i] = new Alternativa
                {
                    Resposta = ((char)(resposta + (i + 1))).ToString()
                };
            }

            foreach (var item in alternativas)
            {
                item.Letra = questao.GerarLetraAlternativa();
                questao.AdicionarAlternativa(item);
            }

            questao.AtualizarAlternativaCorreta(alternativas[2]);

            return questao;
        }

        private static void ConfigurarTesteMatematica()
        {
            var matematica = new Disciplina
            {
                Nome = "Matemática"
            };

            var adicaoUnidades = new Materia
            {
                Nome = "Adição de Unidades",
                Serie = SerieMateriaEnum.PrimeiraSerie
            };

            adicaoUnidades.ConfigurarDisciplina(matematica);

            var adicaoDezenas = new Materia
            {
                Nome = "Adição de Dezenas",
                Serie = SerieMateriaEnum.PrimeiraSerie
            };

            adicaoDezenas.ConfigurarDisciplina(matematica);

            var adicaoCentenas = new Materia
            {
                Nome = "Adição de Centenas",
                Serie = SerieMateriaEnum.SegundaSerie
            };

            adicaoCentenas.ConfigurarDisciplina(matematica);

            var adicaoMilhar = new Materia
            {
                Nome = "Adição de Milhar",
                Serie = SerieMateriaEnum.SegundaSerie
            };

            adicaoMilhar.ConfigurarDisciplina(matematica);

            var materias = new Materia[] { adicaoUnidades, adicaoDezenas, adicaoCentenas, adicaoMilhar };

            int contadorAlternativa = 1;
            int resposta = 0;

            var questoes = new List<Questao>();

            for (int i = 1; i < 40; i++)
            {
                if (i % 10 == 0)
                {
                    resposta = 1;
                    continue;
                }

                int fator, posicaoMateria;

                if (i <= 10)
                {
                    fator = 1; posicaoMateria = 0;
                }

                else if (i <= 20)
                {
                    fator = 10; posicaoMateria = 1;
                }

                else if (i <= 30)
                {
                    fator = 100; posicaoMateria = 2;
                }

                else
                {
                    fator = 1000; posicaoMateria = 3;
                }

                Questao q = NovaQuestaoMatematica(materias[posicaoMateria], Guid.NewGuid(), ++resposta, fator);

                contadorAlternativa += 4;

                questoes.Add(q);
            }

            contexto.Disciplinas.Add(matematica);

            contexto.Materias.Add(adicaoUnidades);
            contexto.Materias.Add(adicaoDezenas);
            contexto.Materias.Add(adicaoCentenas);
            contexto.Materias.Add(adicaoMilhar);

            foreach (var questao in questoes)
            {
                contexto.Questoes.Add(questao);
            }

            //foreach (var questao in questoes)
            //{
            //    repositorioQuestao.Inserir(questao);
            //}

            Teste novoTeste = new Teste();

            novoTeste.Titulo = "Revisão sobre Adição de Unidades";
            novoTeste.ConfigurarDisciplina(matematica);
            novoTeste.ConfigurarMateria(adicaoUnidades);
            novoTeste.Provao = false;
            novoTeste.QuantidadeQuestoes = 5;
            novoTeste.SortearQuestoes();

            //repositorioDisciplina.Inserir(matematica);

            //repositorioMateria.Inserir(adicaoUnidades);
            //repositorioMateria.Inserir(adicaoDezenas);
            //repositorioMateria.Inserir(adicaoCentenas);
            //repositorioMateria.Inserir(adicaoMilhar);
            //repositorioTeste.Inserir(novoTeste);
        }

        private static Questao NovaQuestaoMatematica(Materia materia, Guid idQuestao,  int resposta, int fator)
        {
            var questao = new Questao
            {
                Id = idQuestao,
                Enunciado = $"Quanto é {fator * resposta} + {fator * resposta } ?"
            };

            questao.ConfigurarMateria(materia);

            Alternativa[] alternativas = new Alternativa[4];

            for (int i = 0; i < 4; i++)
            {
                alternativas[i] = new Alternativa
                {
                    Resposta = (fator * resposta * i).ToString()
                };
            }

            foreach (var item in alternativas)
            {
                item.Letra = questao.GerarLetraAlternativa();
                questao.AdicionarAlternativa(item);
            }

            alternativas[2].Resposta = ((fator * resposta) + (fator * resposta)).ToString();
            questao.AtualizarAlternativaCorreta(alternativas[2]);

            return questao;
        }               
    }
}
