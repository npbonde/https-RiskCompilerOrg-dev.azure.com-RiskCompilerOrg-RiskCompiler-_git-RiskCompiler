using AutoMapper;
using AutoMapper.QueryableExtensions;
using RiskCompiler.DataAccessLayer.EfClasses;
using RiskCompiler.DataAccessLayer.EfCode;
using RiskCompiler.ServiceLayer.Misc;
using RiskCompiler.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskCompiler.ServiceLayer.Services.Core
{
    public class RiskAssessmentService : IRiskAssessmentService
    {
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _mapperCfg;

        private readonly RiskCompilerContext _context;

        public RiskAssessmentService(IMapper mapper, RiskCompilerContext context)
        {
            _mapper = mapper;
            _mapperCfg = mapper.ConfigurationProvider;

            _context = context;
        }

        public VoidDto DeleteRiskAssessment(RiskAssessmentDto riskAssessmentDto)
        {
            throw new BizLogicException(StatusCodeDto.NotImplemented);
        }

        public VoidDto DeriveRiskAssessment(RiskAssessmentDto riskAssessmentDto)
        {
            Dictionary<Guid, int> parentGuids = new Dictionary<Guid, int>();

            if (riskAssessmentDto == null ||
                riskAssessmentDto.ParentGuids == null ||
                riskAssessmentDto.Title == null)
            {
                throw new BizLogicException(StatusCodeDto.InvalidArgumentNull);
            }
            else if (riskAssessmentDto.Title.Length == 0)
            {
                throw new BizLogicException(StatusCodeDto.InvalidArgumentEmpty, "Title");
            }
            else if (riskAssessmentDto.Title.Length > 32)
            {
                throw new BizLogicException(StatusCodeDto.InvalidArgumentTooLong, "Title");
            }
            else
            {
                foreach (Guid guid in riskAssessmentDto.ParentGuids)
                {
                    if (parentGuids.ContainsKey(guid))
                    {
                        throw new BizLogicException(StatusCodeDto.InvalidArgument, "Dublicate Guid(s) detected");
                    }

                    if (_context.RiskAssessments
                        .Any(ra => ra.RiskAssessmentGuid == guid))
                    {
                        parentGuids.Add(guid, 1);
                    }
                    else
                    {
                        throw new BizLogicException(StatusCodeDto.InvalidArgument, "Provided Guid not found in database");
                    }
                }

                RiskAssessment newRiskAssessment = new RiskAssessment
                {
                    RiskAssessmentGuid = Guid.NewGuid(),
                    Title = riskAssessmentDto.Title,
                    Description = string.Empty,
                    IsCoreRiskAssessment = false,
                    Descendants = new List<RiskAssessmentRelation>()
                };

                foreach (Guid guid in parentGuids.Keys)
                {
                    RiskAssessment parent = _context.RiskAssessments
                                            .Where(ra => ra.RiskAssessmentGuid == guid)
                                            .First();

                    newRiskAssessment.Ancestors.Add(new RiskAssessmentRelation()
                    {
                        DirectRelation = true,
                        Descendant = newRiskAssessment,
                        Ancestor = parent
                    });

                    foreach (RiskAssessmentRelation rar in parent.Ancestors)
                    {
                        newRiskAssessment.Ancestors.Add(new RiskAssessmentRelation()
                        {
                            DirectRelation = false,
                            Descendant = newRiskAssessment,
                            Ancestor = rar.Ancestor
                        });
                    }
                }

                _context.RiskAssessments
                    .Add(newRiskAssessment);

                _context.SaveChanges();
            }

            return new VoidDto();
        }

        public RiskAssessmentsDto GetAllRiskAssessments()
        {
            List<RiskAssessmentDto> riskAssessments = _context.RiskAssessments
                .ProjectTo<RiskAssessmentDto>(_mapperCfg)
                .ToList();

            return new RiskAssessmentsDto { RiskAssessments = riskAssessments };
        }

        public VoidDto SelectRiskAssessment(RiskAssessmentDto riskAssessmentDto)
        {
            throw new BizLogicException(StatusCodeDto.NotImplemented);
        }
    }
}
