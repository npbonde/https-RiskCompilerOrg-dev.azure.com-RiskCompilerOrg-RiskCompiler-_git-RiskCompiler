using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskCompiler.Shared.DTO
{
    [ProtoContract]
    public class RiskAssessmentsDto : Dto
    {
        [ProtoMember(1)]
        public List<RiskAssessmentDto> RiskAssessments = new List<RiskAssessmentDto>();
    }
}
