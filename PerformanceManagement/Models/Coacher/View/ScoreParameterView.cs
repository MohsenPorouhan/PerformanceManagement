using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.Coacher.View
{
    [NotMapped]
    public class ScoreParameterView
    {
        public int TaskId { get; set; }
        public int? Levell { get; set; }
        public int? AllocatorPersonId { get; set; }
        public string allocatorRoleName { get; set; }
        public int EvaluationId { get; set; }
        public int? hasParticipant { get; set; }
        public bool? participantConfirmation { get; set; }
        public int? AllocatorEvaluationHierarchyId { get; set; }
        public int? parent { get; set; }
        public int RecieverAllocationEvaluationHierarchyId { get; set; }
        public int RecieverAllocationPersonId { get; set; }
        public int index { get; set; }

    }
}
