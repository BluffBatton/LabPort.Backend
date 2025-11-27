using FluentValidation;
using LabPort.Backend.Application.Common.Behavior;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Application.Services.TestResult.Evaluator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LabPort.Backend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

            services.AddScoped<ITestResultEvaluator, TestResultEvaluator>();

            return services;
        }

    }
}
