using FluentValidation;
using LabPort.Backend.Application.Services.Container.Commands;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;

namespace LabPort.Backend.Application.Common.Validator.Container
{
    public class UpdateContainerCommandValidator : AbstractValidator<UpdateContainerCommand>
    {
        public UpdateContainerCommandValidator()
        {
            RuleFor(x => x.Container.TemperatureMin)
                .InclusiveBetween(-20, 30)
                .When(x => x.Container.TemperatureMin.HasValue)
                .WithMessage("Container temperature must be between -20 and 30 in Celsius");

            RuleFor(x => x.Container.TemperatureMax)
                .InclusiveBetween(-20, 30)
                .When(x => x.Container.TemperatureMax.HasValue)
                .WithMessage("Container temperature must be between -20 and 30 in Celsius");

            RuleFor(x => x.Container.HumidityMin)
                .InclusiveBetween(0, 100)
                .When(x => x.Container.HumidityMin.HasValue)
                .WithMessage("Container humidity must be between 0 and 100");

            RuleFor(x => x.Container.HumidityMax)
                .InclusiveBetween(0, 100)
                .When(x => x.Container.HumidityMax.HasValue)
                .WithMessage("Container humidity must be between 0 and 100");

           
            RuleFor(x => x.Container)
                .Must(c =>
                {
                    if (c.TemperatureMin.HasValue && c.TemperatureMax.HasValue)
                        return c.TemperatureMin.Value < c.TemperatureMax.Value;

                    return true;
                })
                .WithMessage("Minimal temperature must be less than maximal temperature");

            RuleFor(x => x.Container)
                .Must(c =>
                {
                    if (c.HumidityMin.HasValue && c.HumidityMax.HasValue)
                        return c.HumidityMin.Value < c.HumidityMax.Value;

                    return true;
                })
                .WithMessage("Minimal humidity must be less than maximal humidity");
        }
    }
}
