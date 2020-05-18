using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RiskCompiler.DataAccessLayer.EfClasses
{
    public class RiskAssessmentRelation
    {
        public int DescendantId { get; set; }

        public int AncestorId { get; set; }

        public bool SoftDeleted { get; set; } = false;

        public bool DirectRelation { get; set; } = false;

        // Descendant

        private RiskAssessment? _descendant = null;

        public RiskAssessment Descendant 
        { 
            get => _descendant ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Descendant));
            set => _descendant = value;
        }

        // Ancestor

        private RiskAssessment? _ancestor = null;

        public RiskAssessment Ancestor 
        { 
            get => _ancestor ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Ancestor));
            set => _ancestor = value;
        }
    }
}
