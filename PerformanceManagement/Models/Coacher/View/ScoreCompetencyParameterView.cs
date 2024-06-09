using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class ScoreCompetencyParameterView
    {
        public int? Levell { get; set; }
        public int? AllocatorPersonId { get; set; }
        public string allocatorRoleName { get; set; }
        public int EvaluationBehaviouralCompetencyId { get; set; }
        public int? hasParticipant { get; set; }
        public bool? participantConfirmation { get; set; }
        public int? AllocatorEvaluationBehaviouralHierarchyId { get; set; }
        public int? parent { get; set; }
        public int RecieverAllocationEvaluationBehaviouralHierarchyId { get; set; }
        public int RecieverAllocationPersonId { get; set; }
        public int index { get; set; }

    }
}
