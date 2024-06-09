#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\Coacher\TaskAssignment\SubSetViewScore.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e5f92d73192a7137cf78525d1c3964eddcc69b9b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Coacher_TaskAssignment_SubSetViewScore), @"mvc.1.0.view", @"/Views/Coacher/TaskAssignment/SubSetViewScore.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Coacher/TaskAssignment/SubSetViewScore.cshtml", typeof(AspNetCore.Views_Coacher_TaskAssignment_SubSetViewScore))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\_ViewImports.cshtml"
using PerformanceManagement;

#line default
#line hidden
#line 2 "D:\PerformanceManagement\PerformanceManagement\Views\_ViewImports.cshtml"
using PerformanceManagement.Models;

#line default
#line hidden
#line 3 "D:\PerformanceManagement\PerformanceManagement\Views\_ViewImports.cshtml"
using PerformanceManagement.Models.HRAdmin;

#line default
#line hidden
#line 4 "D:\PerformanceManagement\PerformanceManagement\Views\_ViewImports.cshtml"
using PerformanceManagement.Models.HRAdmin.View;

#line default
#line hidden
#line 5 "D:\PerformanceManagement\PerformanceManagement\Views\_ViewImports.cshtml"
using PerformanceManagement.ViewModels;

#line default
#line hidden
#line 6 "D:\PerformanceManagement\PerformanceManagement\Views\_ViewImports.cshtml"
using PerformanceManagement.Models.Coacher.View;

#line default
#line hidden
#line 7 "D:\PerformanceManagement\PerformanceManagement\Views\_ViewImports.cshtml"
using PerformanceManagement.Models.PlanningAdmin;

#line default
#line hidden
#line 8 "D:\PerformanceManagement\PerformanceManagement\Views\_ViewImports.cshtml"
using PerformanceManagement.Models.PlanningAdmin.View;

#line default
#line hidden
#line 9 "D:\PerformanceManagement\PerformanceManagement\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 10 "D:\PerformanceManagement\PerformanceManagement\Views\_ViewImports.cshtml"
using PerformanceManagement.Util;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e5f92d73192a7137cf78525d1c3964eddcc69b9b", @"/Views/Coacher/TaskAssignment/SubSetViewScore.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_Coacher_TaskAssignment_SubSetViewScore : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\Coacher\TaskAssignment\SubSetViewScore.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(27, 11070, true);
            WriteLiteral(@"<div class=""modal fade department modalClass"" id=""scoreAssignmentModal2"" tabindex="""" aria-hidden=""true"">
    <div class=""modal-dialog "">
        <div class=""modal-content"">
            <div class=""modal-header bg-blue"">
                <button type=""button"" id=""modal-close"" class=""close""
                        data-dismiss=""modal"" aria-hidden=""true""></button>
                <h4 class=""modal-title"">
                    <i class=""fa fa-file-o""></i>
                    مشاهده نمره
                </h4>
            </div>

            <div class=""modal-body"">
                <div class=""form-body"" id=""scoreTbl2"">

                </div>
                <!-- END FORM-->
            </div>

            <div class=""modal-footer"">
                <button type=""button"" class=""btn green input-sm input-small"" id=""cancel112"" data-dismiss=""modal""><i class=""fa fa-times""></i>بستن</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</d");
            WriteLiteral(@"iv>
<script>
    //  $(document).ready(function () {

    var isTrue2 = false;
    var finalResult2 = false;
    var row2;
    var coacherId2;
    var allocatorLevel2;
    var coacherLevell2
    $.ajax({
        type: 'get',
        url: '/Share/GetCurrentUserInfo',
        async: false,
        success: function (data) {
            coacherId2 = data;
        },
        error: function (status) {
            alert(""Error"");
        }
    })

    var type2;
    var length2 = dt2.rows('.selected').data().length;
    var selector2;
    if (length2 > 0) {
        selector2 = ""#subSetScoreTable >tbody>tr.selected"";
    } else {
        selector2 = ""#subSetScoreTable >tbody>tr"";
    }
    var criteriaCount;
    $(selector2).each(function (i, tr) {
        row2 = dt2.row(tr);

        $.ajax({
            type: 'get',
            url: '/Share/CriteiaCount',
            data: { taskId: row2.data().TaskId },
            async: false,
            success: function (data) {
     ");
            WriteLiteral(@"           criteriaCount = data;
            },
            error: function (status) {
                alert(""Error"");
            }
        })

        if (row2.data().Levell == 2 && ((row2.data().AllocatorPersonId == coacherId2 && row2.data().allocatorRoleName != ""PlanningAdmin"") || row2.data().allocatorRoleName == ""HRAdmin"")) {
            $.ajax({
                url: '/TaskAssignment/GetScore',
                data: {
                    evaluationId: row2.data().EvaluationId
                    , hasCriteria: criteriaCount
                    , hasParticipant: row2.data().hasParticipant
                    , participantConfirmation: row2.data().participantConfirmation
                    , type: 1
                },
                //contentType: 'aplication/json;charset=utf-8',
                type: ""POST"",
                datatype: 'html',
                async: false,
                success: function (data) {
                    $('#scoreTbl2').html(data);
                },
");
            WriteLiteral(@"                error: function (jqXHR, textStatus, errorThrown) {
                    alert(""Error"");
                }
            })
            finalResult2 = true;
        }
        else if (row2.data().Levell >= 3 && (row2.data().allocatorRoleName == ""HRAdmin"")) {
            $.ajax({
                url: '/TaskAssignment/GetScore',
                data: {
                    evaluationId: row2.data().EvaluationId
                    , hasCriteria: criteriaCount
                    , hasParticipant: row2.data().hasParticipant
                    , participantConfirmation: row2.data().participantConfirmation
                    , type: 2
                },
                //contentType: 'aplication/json;charset=utf-8',
                type: ""POST"",
                datatype: 'html',
                async: false,
                success: function (data) {
                    $('#scoreTbl2').html(data);
                },
                error: function (jqXHR, textStatus, errorThrown");
            WriteLiteral(@") {
                    alert(""Error"");
                }
            })
            finalResult2 = true;
        }
        else if (row2.data().Levell >= 2 && row2.data().allocatorRoleName == ""PlanningAdmin"") {
            $.ajax({
                url: '/TaskAssignment/GetScore',
                data: {
                    evaluationId: row2.data().EvaluationId
                    , hasCriteria: criteriaCount
                    , hasParticipant: row2.data().hasParticipant
                    , participantConfirmation: row2.data().participantConfirmation
                    , type: 3
                },
                //contentType: 'aplication/json;charset=utf-8',
                type: ""POST"",
                datatype: 'html',
                async: false,
                success: function (data) {
                    $('#scoreTbl2').html(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(""Error"");
                }");
            WriteLiteral(@"
            })
            finalResult2 = true;
        }
        else if (row2.data().Levell >= 3 && row2.data().AllocatorEvaluationHierarchyId == row2.data().parent && row2.data().allocatorRoleName != ""HRAdmin"" && row2.data().allocatorRoleName != ""PlanningAdmin"") {
            $.ajax({
                url: '/TaskAssignment/GetScore',
                data: {
                    evaluationId: row2.data().EvaluationId
                    , hasCriteria: criteriaCount
                    , hasParticipant: row2.data().hasParticipant
                    , participantConfirmation: row2.data().participantConfirmation
                    , type: 2
                },
                //contentType: 'aplication/json;charset=utf-8',
                type: ""POST"",
                datatype: 'html',
                async: false,
                success: function (data) {
                    $('#scoreTbl2').html(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
            WriteLiteral(@"
                    alert(""Error"");
                }
            })
            finalResult2 = true;
        }
        else if (row2.data().Levell >= 3 && (row2.data().AllocatorPersonId == coacherId2 && row2.data().allocatorRoleName != ""PlanningAdmin"" && row2.data().allocatorRoleName != ""HRAdmin"")) {
            $.ajax({
                type: 'get',
                url: '/Share/AllocatorLevel',
                async: false,
                data: {
                    coacherId: row2.data().AllocatorPersonId
                    , coacherDepartmentId: row2.data().AllocatorEvaluationHierarchyId
                    , departmentId: row2.data().RecieverAllocationEvaluationHierarchyId
                    , personId: row2.data().RecieverAllocationPersonId
                },
                success: function (data) {
                    allocatorLevel2 = data[0].Levell;
                },
                error: function (status) {
                    alert(""Error"");
                }
          ");
            WriteLiteral(@"  })
            $.ajax({
                url: '/TaskAssignment/GetScore',
                data: {
                    evaluationId: row2.data().EvaluationId
                    , hasCriteria: criteriaCount
                    , hasParticipant: row2.data().hasParticipant
                    , participantConfirmation: row2.data().participantConfirmation
                    , type: 4
                    , coacherLevel: (4-allocatorLevel2)
                },
                //contentType: 'aplication/json;charset=utf-8',
                type: ""POST"",
                datatype: 'html',
                async: false,
                success: function (data) {
                    $('#scoreTbl2').html(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(""Error"");
                }
            })
            finalResult2 = true;
        }
        else {
            $.ajax({
                type: 'get',
                url: '/Sh");
            WriteLiteral(@"are/AllocatorLevel',
                async: false,
                data: {
                    coacherId: row2.data().AllocatorPersonId
                    , coacherDepartmentId: row2.data().AllocatorEvaluationHierarchyId
                    , departmentId: row2.data().RecieverAllocationEvaluationHierarchyId
                    , personId: row2.data().RecieverAllocationPersonId
                },
                success: function (data) {
                    allocatorLevel2 = data[0].Levell;
                },
                error: function (status) {
                    alert(""Error"");
                }
            })
            $.ajax({
                type: 'get',
                url: '/Share/AllocatorLevel',
                async: false,
                data: {
                    coacherId: coacherId2
                    , coacherDepartmentId: $('#departmentId2').children('option:selected').val()
                    , departmentId: row2.data().RecieverAllocationEvaluationHierarchy");
            WriteLiteral(@"Id
                    , personId: row2.data().RecieverAllocationPersonId
                },
                success: function (data) {
                    coacherLevell2 = data[0].Levell;
                },
                error: function (status) {
                    alert(""Error"");
                }
            })
            if (row2.data().Levell >= 3 && (row2.data().AllocatorPersonId != coacherId2 && row2.data().allocatorRoleName != ""PlanningAdmin"" && row2.data().allocatorRoleName != ""HRAdmin"" && row2.data().AllocatorEvaluationHierarchyId != row2.data().parent) && coacherLevell2 <= allocatorLevel2) {
                finalResult2 = true;
                $.ajax({
                    url: '/TaskAssignment/GetScore',
                    data: {
                        evaluationId: row2.data().EvaluationId
                        , hasCriteria: criteriaCount
                        , hasParticipant: row2.data().hasParticipant
                        , participantConfirmation: row2.data().");
            WriteLiteral(@"participantConfirmation
                        , type: 5
                        , coacherLevel: (4-allocatorLevel2)
                    },
                    //contentType: 'aplication/json;charset=utf-8',
                    type: ""POST"",
                    datatype: 'html',
                    async: false,
                    success: function (data) {
                        $('#scoreTbl2').html(data);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert(""Error"");
                    }
                })
            }
        }
    })
    if (!finalResult2) {
        alert(""مجاز به مشاهده نمرات وظیفه مورد نظر نمی باشید."");
    } else if (finalResult2) {
        $('#scoreAssignmentModal2').modal('show');
    }
</script>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591