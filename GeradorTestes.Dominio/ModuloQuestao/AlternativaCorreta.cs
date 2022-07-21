using System;

namespace GeradorTestes.Dominio.ModuloQuestao
{
    public class AlternativaCorreta
    {
        public AlternativaCorreta(Guid n, char l)
        {
            numero = n;
            letra = l;
        }

        public Guid numero;
        public char letra;

        public override string ToString()
        {
            return string.Format("Letra: {0} Número: {1}", numero, letra);
        }
    }
}