using AutoMapper;
using RiskCompiler.DataAccessLayer.EfClasses;
using RiskCompiler.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskCompiler.ServiceLayer.AutoMapper
{
    public class RiskAssessmentProfile : Profile
    {
        public RiskAssessmentProfile()
        {
            CreateMap<RiskAssessment, RiskAssessmentDto>()
                .ForMember(dto => dto.ParentGuids, opt => opt.MapFrom(x => x.Ancestors.Select(y => y.Ancestor.RiskAssessmentGuid).ToList()));
        }
    }
}
