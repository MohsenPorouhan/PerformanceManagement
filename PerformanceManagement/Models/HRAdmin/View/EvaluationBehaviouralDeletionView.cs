﻿using PerformanceManagement.Models.Coacher;
using PerformanceManagement.Models.HRAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.View
{
    public class EvaluationBehaviouralDeletionView
    {
        public int EvaluationBehaviouralCompetencyId { get; set; }
        public string Title { get; set; }
    }
}
