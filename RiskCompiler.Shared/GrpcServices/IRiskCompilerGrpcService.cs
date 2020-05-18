using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using RiskCompiler.Shared.DTO;

namespace RiskCompiler.Shared.GrpcServices
{
    [ServiceContract]
    public interface IRiskCompilerGrpcService
    {
        //
        // RiskAssessment
        //

        public ValueTask<ReturnValueDto> DeleteRiskAssessmentAsync(RiskAssessmentDto riskAssessmentDto);
        
        public ValueTask<ReturnValueDto> DeriveRiskAssessmentAsync(RiskAssessmentDto riskAssessmentDto); 
        
        public ValueTask<ReturnValueDto> GetAllRiskAssessmentsAsync();

        public ValueTask<ReturnValueDto> SelectRiskAssessmentAsync(RiskAssessmentDto riskAssessmentDto);
    }
}
