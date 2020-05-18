using RiskCompiler.Shared.DTO;

namespace RiskCompiler.ServiceLayer.Services.Core
{
    public interface IRiskAssessmentService
    {
        VoidDto DeleteRiskAssessment(RiskAssessmentDto riskAssessmentDto);
        VoidDto DeriveRiskAssessment(RiskAssessmentDto riskAssessmentDto);
        RiskAssessmentsDto GetAllRiskAssessments();
        VoidDto SelectRiskAssessment(RiskAssessmentDto riskAssessmentDto);
    }
}