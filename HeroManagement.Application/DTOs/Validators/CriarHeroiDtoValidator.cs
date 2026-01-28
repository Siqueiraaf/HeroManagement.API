using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroManagement.Application.DTOs.Validators
{
    public class CriarHeroiDtoValidator : AbstractValidator<CriarHeroiDto>
    {
        public CriarHeroiDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(120).WithMessage("O nome deve ter no máximo 120 caracteres.");

            RuleFor(x => x.NomeHeroi)
                .NotEmpty().WithMessage("O nome do herói é obrigatório.")
                .MaximumLength(120).WithMessage("O nome do herói deve ter no máximo 120 caracteres.");

            When(x => x?.DataNascimento != null, () =>
            {
                RuleFor(x => x.DataNascimento)
                    .LessThan(DateTime.Today)
                    .WithMessage("A data de nascimento deve ser menor que a data atual.");
            });

            RuleFor(x => x.Altura)
                .GreaterThan(0).WithMessage("A altura deve ser maior que zero.");

            RuleFor(x => x.Peso)
                .GreaterThan(0).WithMessage("O peso deve ser maior que zero.");

            RuleFor(x => x.SuperpoderesIds)
                .NotNull().WithMessage("A lista de superpoderes não pode ser nula.")
                .Must(list => list.Count > 0).WithMessage("O herói deve ter pelo menos um superpoder.");
        }
    }
}
