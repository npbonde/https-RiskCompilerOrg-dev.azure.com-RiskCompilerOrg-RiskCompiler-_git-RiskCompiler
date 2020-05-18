using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskCompiler.Shared.DTO
{
    [ProtoContract]
    public class RiskAssessmentDto : Dto
    {
        [ProtoMember(1)]
        public Guid RiskAssessmentGuid;

        [ProtoMember(2)]
        public string? Title { get; set; }

        [ProtoMember(3)]
        public string? Description { get; set; }

        [ProtoMember(4)]
        public List<Guid> ParentGuids { get; set; } = new List<Guid>();

        [ProtoMember(5)]
        public bool IsCoreRiskAssessment { get; set; }

    }
}
