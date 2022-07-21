uusing FluentValidation.Results;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GeradorTestes.Infra.Sql.ModuloDisciplina
{
    public class RepositorioDisciplinaEmBancoDados : IRepositorioDisciplina
    {
        private const string enderecoBanco =
             "Data Source=(LocalDB)\\MSSqlLocalDB;" +
             "Initial Catalog=GeradorTeste;" +
             "Integrated Security=True;" +
             "Pooling=False";

        #region Sql Queries

        private const string sqlInserir =
            @"INSERT INTO [TBDISCIPLINA]
                (
                    [NOME]
                )    
                 VALUES
                (
                    @NOME
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBDISCIPLINA]	
		        SET
			        [NOME] = @NOME
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBDISCIPLINA]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [NUMERO], 
		            [NOME] 
	            FROM 
		            [TBDISCIPLINA]";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [NUMERO], 
		            [NOME]
	            FROM 
		            [TBDISCIPLINA]
		        WHERE
                    [NUMERO] = @NUMERO";

        private const string sqlSelecionarMateriasDaDisciplina =
            @"SELECT 
		            [NUMERO] MATERIA_NUMERO, 
		            [NOME] MATERIA_NOME, 
                    [SERIE] MATERIA_SERIE
	            FROM 
		            [TBMATERIA]
		        WHERE
                    [DISCIPLINA_NUMERO] = @DISCIPLINA_NUMERO";

        private const string sqlSelecionarQuestoesDaMateria =
            @"SELECT 
		            [NUMERO] QUESTAO_NUMERO, 
		            [ENUNCIADO] QUESTAO_ENUNCIADO
	            FROM 
		            [TBQUESTAO]
		        WHERE
                    [MATERIA_NUMERO] = @MATERIA_NUMERO";

        #endregion

        public void Inserir(Disciplina novaDisciplina)
        {
            SqlConnection conexao = new SqlConnection(enderecoBanco);
            SqlCommand cmdInserir = new SqlCommand(sqlInserir, conexao);

            ConfigurarParametrosDisciplina(novaDisciplina, cmdInserir);
            conexao.Open();

            var numero = cmdInserir.ExecuteScalar();

            novaDisciplina.Id = Convert.ToInt32(numero);
            conexao.Close();
        }

        public void Editar(Disciplina disciplina)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosDisciplina(disciplina, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        public void Excluir(Disciplina disciplina)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", disciplina.Id);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        public List<Disciplina> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorDisciplina = comandoSelecao.ExecuteReader();

            List<Disciplina> disciplinas = new List<Disciplina>();

            while (leitorDisciplina.Read())
            {
                Disciplina disciplina = ConverterParaDisciplina(leitorDisciplina);

                CarregarMaterias(disciplina);

                disciplinas.Add(disciplina);
            }

            conexaoComBanco.Close();

            return disciplinas;
        }

        private void CarregarMaterias(Disciplina disciplina)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarMateriasDaDisciplina, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("DISCIPLINA_NUMERO", disciplina.Id);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            while (leitorMateria.Read())
            {
                Materia materia = ConverterParaMateria(leitorMateria);

                CarregarQuestoes(materia);

                disciplina.AdicionarMateria(materia);
            }

            conexaoComBanco.Close();
        }

        private void CarregarQuestoes(Materia materia)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarQuestoesDaMateria, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("MATERIA_NUMERO", materia.Id);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestoes = comandoSelecao.ExecuteReader();

            while (leitorQuestoes.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestoes);

                materia.AdicionaQuestao(questao);
            }

            conexaoComBanco.Close();
        }

        private Questao ConverterParaQuestao(SqlDataReader leitorQuestoes)
        {
            var numero = Guid.Parse(leitorQuestoes["QUESTAO_NUMERO"]);
            string enunciado = Convert.ToString(leitorQuestoes["QUESTAO_ENUNCIADO"]);

            return new Questao
            {
                Id = numero,
                Enunciado = enunciado
            };
        }

        private Materia ConverterParaMateria(SqlDataReader leitorMateria)
        {
            var numero = Guid.Parse(leitorMateria["MATERIA_NUMERO"]);
            string nome = Convert.ToString(leitorMateria["MATERIA_NOME"]);
            var serie = (SerieMateriaEnum)leitorMateria["MATERIA_SERIE"];

            var materia = new Materia
            {
                Id = numero,
                Nome = nome,
                Serie = serie
            };

            return materia;
        }

        public Disciplina SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorDisciplina = comandoSelecao.ExecuteReader();

            Disciplina disciplina = null;
            if (leitorDisciplina.Read())
                disciplina = ConverterParaDisciplina(leitorDisciplina);

            conexaoComBanco.Close();

            CarregarMaterias(disciplina);

            return disciplina;
        }

        private Disciplina ConverterParaDisciplina(SqlDataReader leitorDisciplina)
        {
            var numero = Guid.Parse(leitorDisciplina["NUMERO"].ToString());
            string nome = Convert.ToString(leitorDisciplina["NOME"]);

            var disciplina = new Disciplina
            {
                Id = numero,
                Nome = nome,
            };

            return disciplina;
        }

        private static void ConfigurarParametrosDisciplina(Disciplina novaDisciplina, SqlCommand cmdInserir)
        {
            cmdInserir.Parameters.AddWithValue("NUMERO", novaDisciplina.Id);
            cmdInserir.Parameters.AddWithValue("NOME", novaDisciplina.Nome);
        }
    }
}
