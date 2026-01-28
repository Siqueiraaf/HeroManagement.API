using FluentValidation;

namespace HeroManagement.Application;

public class AtualizarHeroiDtoValidator : AbstractValidator<AtualizarHeroiDto>
{
    public AtualizarHeroiDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(120).WithMessage("Nome não pode ter mais de 100 caracteres");

        RuleFor(x => x.NomeHeroi)
            .NotEmpty().WithMessage("Nome do herói é obrigatório")
            .MaximumLength(120).WithMessage("Nome do herói não pode ter mais de 50 caracteres");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("Data de nascimento é obrigatória")
            .LessThan(DateTime.Today).WithMessage("Data de nascimento deve ser anterior a hoje");

        RuleFor(x => x.Altura)
            .GreaterThan(0).WithMessage("Altura deve ser maior que 0");

        RuleFor(x => x.Peso)
            .GreaterThan(0).WithMessage("Peso deve ser maior que 0");

        RuleFor(x => x.SuperpoderesIds)
            .NotNull().WithMessage("Superpoderes são obrigatórios")
            .Must(list => list != null && list.Count > 0).WithMessage("Deve ter pelo menos um superpoder");
    }
}