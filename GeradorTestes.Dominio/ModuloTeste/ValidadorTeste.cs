using FluentValidation;
using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloMateria;

namespace GeradorTestes.Dominio.ModuloTeste
{
    public class ValidadorTeste : AbstractValidator<Teste>
    {
        public ValidadorTeste()
        {
            RuleFor(x => x.Titulo)
                .NotNull().NotEmpty();

            RuleFor(x => x.Disciplina)
                .NotNull().NotEmpty();

            RuleFor(x => x.DataGeracao)
                .NotNull().NotEmpty();

            RuleFor(x => x.Titulo)
                .NotNull().NotEmpty();

            When(x => x.Provao == true, () =>
            {
                RuleFor(x => x.Materia).Null();

            }).Otherwise(() =>
            {
                RuleFor(x => x.Materia).NotNull().Custom(MateriaNaoPodeSerNulo).Custom(MateriaDeveTerQuestoes);
            });

            RuleFor(x => x.QuantidadeQuestoes)
                .GreaterThanOrEqualTo(1);
        }

        private void MateriaDeveTerQuestoes(Materia materia, ValidationContext<Teste> ctx)
        {
            if (materia != null && materia.Questoes.Count < 1)
                ctx.AddFailure(new ValidationFailure("Materia", "Matéria deve ter no mínimo uma questão"));
        }

        private void MateriaNaoPodeSerNulo(Materia materia, ValidationContext<Teste> ctx)
        {
            if (materia == null)
                ctx.AddFailure(new ValidationFailure("Materia", "Matéria não podem estar em branco"));
        }
    }
}
