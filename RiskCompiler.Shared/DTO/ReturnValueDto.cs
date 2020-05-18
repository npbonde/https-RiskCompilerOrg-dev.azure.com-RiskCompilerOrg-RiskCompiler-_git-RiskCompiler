using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskCompiler.Shared.DTO
{
    [ProtoContract]
    public class ReturnValueDto
    {
        [ProtoMember(1)]
        public StatusCodeDto StatusCode { get; set; } = StatusCodeDto.OK;

        [ProtoMember(2)]
        public string? StatusMessage { get; set; } = null;

        [ProtoMember(3)]
        public Dto? Dto { get; set; } = null;
    }
}
