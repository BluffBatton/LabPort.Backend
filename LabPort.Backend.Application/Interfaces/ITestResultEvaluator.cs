using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Domain.Enums;

namespace LabPort.Backend.Application.Interfaces
{
    public interface ITestResultEvaluator
    {
        ResultStatus Evaluate(Test test, double? valueNumeric);
    }
}
