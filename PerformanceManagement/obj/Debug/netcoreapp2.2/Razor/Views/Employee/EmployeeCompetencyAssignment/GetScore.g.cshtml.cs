#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c91854e779f1097fbd6e716691310b0bf06366af"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employee_EmployeeCompetencyAssignment_GetScore), @"mvc.1.0.view", @"/Views/Employee/EmployeeCompetencyAssignment/GetScore.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Employee/EmployeeCompetencyAssignment/GetScore.cshtml", typeof(AspNetCore.Views_Employee_EmployeeCompetencyAssignment_GetScore))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c91854e779f1097fbd6e716691310b0bf06366af", @"/Views/Employee/EmployeeCompetencyAssignment/GetScore.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_Employee_EmployeeCompetencyAssignment_GetScore : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<ScoreView>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
  
    Layout = null;

#line default
#line hidden
#line 5 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
 if (ViewBag.type == 1)
{

#line default
#line hidden
            BeginContext(79, 389, true);
            WriteLiteral(@"    <table class=""table table-striped table-bordered table-hover"" id=""sample_dt"">
        <thead>
            <tr>

                <th id=""th1"">
                    نام شایستگی
                </th>

                <th id=""th2"">
                    نمره مربی سطح1
                </th>
                <th id=""th2"">
                    نمره مربی سطح2
                </th>
");
            EndContext();
#line 21 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                 if (ViewBag.hasParticipant > 0 && ViewBag.participantConfirmation ?? false)
                {

#line default
#line hidden
            BeginContext(581, 103, true);
            WriteLiteral("                    <th id=\"th4\">\r\n                        نمره سایرارزیاب\r\n                    </th>\r\n");
            EndContext();
#line 26 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                }

#line default
#line hidden
            BeginContext(703, 141, true);
            WriteLiteral("                <th id=\"th5\">\r\n                    خود ارزیابی\r\n                </th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
            EndContext();
#line 33 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(901, 48, true);
            WriteLiteral("                <tr>\r\n\r\n                    <td>");
            EndContext();
            BeginContext(950, 10, false);
#line 37 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                   Write(item.Title);

#line default
#line hidden
            EndContext();
            BeginContext(960, 33, true);
            WriteLiteral("</td>\r\n\r\n                    <td>");
            EndContext();
            BeginContext(994, 11, false);
#line 39 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                   Write(item.score1);

#line default
#line hidden
            EndContext();
            BeginContext(1005, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(1037, 11, false);
#line 40 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                   Write(item.score2);

#line default
#line hidden
            EndContext();
            BeginContext(1048, 11, true);
            WriteLiteral("</td>\r\n\r\n\r\n");
            EndContext();
#line 43 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                     if (ViewBag.hasParticipant > 0 && ViewBag.participantConfirmation ?? false)
                    {

#line default
#line hidden
            BeginContext(1180, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(1209, 21, false);
#line 45 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                       Write(item.participantScore);

#line default
#line hidden
            EndContext();
            BeginContext(1230, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 46 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                    }

#line default
#line hidden
            BeginContext(1260, 24, true);
            WriteLiteral("                    <td>");
            EndContext();
            BeginContext(1285, 14, false);
#line 47 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                   Write(item.selfScore);

#line default
#line hidden
            EndContext();
            BeginContext(1299, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 49 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
            }

#line default
#line hidden
            BeginContext(1344, 34, true);
            WriteLiteral("\r\n        </tbody>\r\n    </table>\r\n");
            EndContext();
#line 53 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
}
else if (ViewBag.type == 4)
{

#line default
#line hidden
            BeginContext(1413, 271, true);
            WriteLiteral(@"    <table class=""table table-striped table-bordered table-hover"" id=""sample_dt3"">
        <thead>
            <tr>
                <th id=""th1"">
                    نام شایستگی
                </th>
                <th id=""th2"">
                    نمره مربی سطح ");
            EndContext();
            BeginContext(1685, 20, false);
#line 63 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                             Write(ViewBag.coacherLevel);

#line default
#line hidden
            EndContext();
            BeginContext(1705, 25, true);
            WriteLiteral("\r\n                </th>\r\n");
            EndContext();
#line 65 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                 if (ViewBag.hasParticipant > 0 && ViewBag.participantConfirmation ?? false)
                {

#line default
#line hidden
            BeginContext(1843, 103, true);
            WriteLiteral("                    <th id=\"th4\">\r\n                        نمره سایرارزیاب\r\n                    </th>\r\n");
            EndContext();
#line 70 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                }

#line default
#line hidden
            BeginContext(1965, 141, true);
            WriteLiteral("                <th id=\"th5\">\r\n                    خود ارزیابی\r\n                </th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
            EndContext();
#line 77 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(2163, 46, true);
            WriteLiteral("                <tr>\r\n                    <td>");
            EndContext();
            BeginContext(2210, 10, false);
#line 80 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                   Write(item.Title);

#line default
#line hidden
            EndContext();
            BeginContext(2220, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 81 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                     switch (ViewBag.coacherLevel)
                    {
                        case 2:

#line default
#line hidden
            BeginContext(2335, 32, true);
            WriteLiteral("                            <td>");
            EndContext();
            BeginContext(2368, 11, false);
#line 84 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                           Write(item.score2);

#line default
#line hidden
            EndContext();
            BeginContext(2379, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 85 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                            break;
                        case 3:

#line default
#line hidden
            BeginContext(2455, 32, true);
            WriteLiteral("                            <td>");
            EndContext();
            BeginContext(2488, 11, false);
#line 87 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                           Write(item.score3);

#line default
#line hidden
            EndContext();
            BeginContext(2499, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 88 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                            break;
                        case 4:

#line default
#line hidden
            BeginContext(2575, 32, true);
            WriteLiteral("                            <td>");
            EndContext();
            BeginContext(2608, 11, false);
#line 90 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                           Write(item.score4);

#line default
#line hidden
            EndContext();
            BeginContext(2619, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 91 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                            break;
                        default:
                            break;
                    }

#line default
#line hidden
            BeginContext(2755, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 97 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                     if (ViewBag.hasParticipant > 0 && ViewBag.participantConfirmation ?? false)
                    {

#line default
#line hidden
            BeginContext(2880, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(2909, 21, false);
#line 99 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                       Write(item.participantScore);

#line default
#line hidden
            EndContext();
            BeginContext(2930, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 100 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                    }

#line default
#line hidden
            BeginContext(2960, 24, true);
            WriteLiteral("                    <td>");
            EndContext();
            BeginContext(2985, 14, false);
#line 101 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
                   Write(item.selfScore);

#line default
#line hidden
            EndContext();
            BeginContext(2999, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 103 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
            }

#line default
#line hidden
            BeginContext(3044, 34, true);
            WriteLiteral("\r\n        </tbody>\r\n    </table>\r\n");
            EndContext();
#line 107 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\GetScore.cshtml"
}

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<ScoreView>> Html { get; private set; }
    }
}
#pragma warning restore 1591
