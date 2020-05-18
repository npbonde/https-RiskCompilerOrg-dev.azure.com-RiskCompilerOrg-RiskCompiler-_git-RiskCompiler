using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RiskCompiler.DataAccessLayer.EfClasses
{
    public class RiskAssessment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RiskAssessmentId { get; set; }

        public Guid RiskAssessmentGuid { get; set; }

        public bool SoftDeleted { get; set; } = false;

        public ICollection<RiskAssessmentRelation> Ancestors { get; set; } = new List<RiskAssessmentRelation>();

        public ICollection<RiskAssessmentRelation> Descendants { get; set; } = new List<RiskAssessmentRelation>();

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsCoreRiskAssessment { get; set; } = false;

    }
}
