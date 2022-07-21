using FluentValidation;
using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloMateria;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Dominio.ModuloQuestao
{
    public class ValidadorQuestao : AbstractValidator<Questao>
    {
        public ValidadorQuestao()
        {
            RuleFor(x => x.Materia)
               .Custom(DisciplinaEMateriaNaoPodemSerNulo);

            RuleFor(x => x.Enunciado)
               .NotNull()
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.Alternativas)
                .Must(x => x.Count >= 3)
                .WithMessage("No mínimo 3 alternativas precisa ser informada");

            RuleFor(x => x.Alternativas)
                .Must(x => x.Count <= 5)
                .WithMessage("No máximo 5 alternativas pode ser informada");

            RuleFor(x => x.Alternativas).Custom(ApenasUmaAlternativaCorreta);

            RuleFor(x => x.Alternativas).Custom(NenhumaAlternativaCorreta);

            RuleFor(x => x.Alternativas).Custom(AlternativasComValoresDuplicados);
        }

        private void DisciplinaEMateriaNaoPodemSerNulo(Materia materia, ValidationContext<Questao> ctx)
        {
            if (materia == null)
                ctx.AddFailure(new ValidationFailure("Disciplina", "Disciplina e Matéria não podem estar em branco"));
        }

        private void AlternativasComValoresDuplicados(ICollection<Alternativa> alternativas, ValidationContext<Questao> ctx)
        {
            var array = alternativas.Select(a => a.Resposta);

            var dict = new Dictionary<string, int>();

            foreach (var value in array)
            {
                dict.TryGetValue(value, out int count);
                dict[value] = count + 1;
            }

            if (dict.Values.Any(x => x > 1))
                ctx.AddFailure(new ValidationFailure("Alternativas", "Respostas iguais foram informadas nas alternativas"));
        }

        private void NenhumaAlternativaCorreta(ICollection<Alternativa> alternativas, ValidationContext<Questao> ctx)
        {
            if (alternativas.Count(x => x.Correta) == 0)
                ctx.AddFailure(new ValidationFailure("Alternativas", "Nenhuma alternativa correta foi informada"));
        }

        private void ApenasUmaAlternativaCorreta(ICollection<Alternativa> alternativas, ValidationContext<Questao> ctx)
        {
            if (alternativas.Count(x => x.Correta) > 1)
                ctx.AddFailure(new ValidationFailure("Alternativas", "Apenas uma alternativa pode ser correta"));
        }
    }
}