using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Domain.Enums;

namespace LabPort.Backend.Application.Services.TestResult.Evaluator
{
    public class TestResultEvaluator : ITestResultEvaluator
    {
        public ResultStatus Evaluate(Domain.Entities.Test test, double? valueNumeric)
        {
            var testType = test.TestType;

            if (testType == null ||
                !testType.ReferenceMin.HasValue ||
                !testType.ReferenceMax.HasValue ||
                !valueNumeric.HasValue)
            {
                return ResultStatus.Pending;
            }

            var value = valueNumeric.Value;
            var min = testType.ReferenceMin.Value;
            var max = testType.ReferenceMax.Value;

            if (value < min || value > max)
            {
                return ResultStatus.Unexpected;
            }

            return ResultStatus.Expected;
        }
    }
}
