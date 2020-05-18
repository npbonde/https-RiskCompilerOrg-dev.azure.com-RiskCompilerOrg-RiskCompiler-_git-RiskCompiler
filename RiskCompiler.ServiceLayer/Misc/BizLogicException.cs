using RiskCompiler.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskCompiler.ServiceLayer.Misc
{
    public class BizLogicException : Exception
    {
        public StatusCodeDto StatusCode { get; set; }

        public string? StatusMessage { get; set; }

        public BizLogicException()
        {
        }

        public BizLogicException(StatusCodeDto statusCode)
        {
            StatusCode = statusCode;  
        }

        public BizLogicException(StatusCodeDto statusCode, string? statusMessage)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage;
        }
    }
}
