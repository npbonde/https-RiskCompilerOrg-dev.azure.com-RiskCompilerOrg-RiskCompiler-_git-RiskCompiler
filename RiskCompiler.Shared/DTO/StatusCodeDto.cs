using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace RiskCompiler.Shared.DTO
{
    public enum StatusCodeDto
    {
        OK,
        InvalidArgument,
        InvalidArgumentNull,
        InvalidArgumentEmpty,
        InvalidArgumentTooLong,
        InvalidArgumentTooShort,
        NotImplemented,
        UnknownError
    }

}
