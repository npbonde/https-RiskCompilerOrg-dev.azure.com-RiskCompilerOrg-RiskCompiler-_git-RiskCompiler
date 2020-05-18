using RiskCompiler.ServiceLayer.Misc;
using RiskCompiler.ServiceLayer.Services.Core;
using RiskCompiler.Shared.DTO;
using RiskCompiler.Shared.GrpcServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskCompiler.Server.GrpcServices
{
    public class RiskCompilerGrpcService : IRiskCompilerGrpcService
    {
        private readonly IRiskAssessmentService? _riskAssessmentService = null;

        public RiskCompilerGrpcService(
            IRiskAssessmentService riskAssessmentService
            )
        {
            _riskAssessmentService = riskAssessmentService;
        }

        internal static ValueTask<ReturnValueDto> ServiceRunner(Func <Dto> func)
        {
            ReturnValueDto returnValue = new ReturnValueDto();

            try
            {
                returnValue.Dto = func();
            }
            catch (BizLogicException ex)
            {
                returnValue.StatusCode = ex.StatusCode;
            }
            catch (Exception)
            {
                returnValue.StatusCode = StatusCodeDto.UnknownError;
                // XXX TODO: Also do some logging to actually be able to detect this problem
            }

            return new ValueTask<ReturnValueDto>(returnValue);
        }

        public ValueTask<ReturnValueDto> DeleteRiskAssessmentAsync(RiskAssessmentDto riskAssessmentDto)
            => ServiceRunner(() => _riskAssessmentService!.DeleteRiskAssessment(riskAssessmentDto));

        public ValueTask<ReturnValueDto> DeriveRiskAssessmentAsync(RiskAssessmentDto riskAssessmentDto)
            => ServiceRunner(() => _riskAssessmentService!.DeriveRiskAssessment(riskAssessmentDto));
        
        public ValueTask<ReturnValueDto> GetAllRiskAssessmentsAsync()
            => ServiceRunner(() => _riskAssessmentService!.GetAllRiskAssessments());

        public ValueTask<ReturnValueDto> SelectRiskAssessmentAsync(RiskAssessmentDto riskAssessmentDto)
            => ServiceRunner(() => _riskAssessmentService!.SelectRiskAssessment(riskAssessmentDto));
    }
}
