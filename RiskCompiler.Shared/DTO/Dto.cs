using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
namespace RiskCompiler.Shared.DTO
{
    [ProtoContract]
    [ProtoInclude(1, typeof(VoidDto))]
    [ProtoInclude(2, typeof(RiskAssessmentsDto))]
    [ProtoInclude(3, typeof(RiskAssessmentDto))]
    public abstract class Dto
    {
    }
}
