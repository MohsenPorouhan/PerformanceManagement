#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "07fc1c2ebb7e5e9c5a8621093a4feecea5f978f9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employee_EmployeeTaskAssignment_GetScore), @"mvc.1.0.view", @"/Views/Employee/EmployeeTaskAssignment/GetScore.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Employee/EmployeeTaskAssignment/GetScore.cshtml", typeof(AspNetCore.Views_Employee_EmployeeTaskAssignment_GetScore))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"07fc1c2ebb7e5e9c5a8621093a4feecea5f978f9", @"/Views/Employee/EmployeeTaskAssignment/GetScore.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_Employee_EmployeeTaskAssignment_GetScore : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<ScoreView>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
  
    Layout = null;

#line default
#line hidden
#line 5 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
 if (ViewBag.type == 1)
{

#line default
#line hidden
            BeginContext(79, 118, true);
            WriteLiteral("    <table class=\"table table-striped table-bordered table-hover\" id=\"sample_dt\">\r\n        <thead>\r\n            <tr>\r\n");
            EndContext();
#line 10 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                 if (ViewBag.hasCriteria > 0)
                {

#line default
#line hidden
            BeginContext(263, 96, true);
            WriteLiteral("                    <th id=\"th1\">\r\n                        نام شاخص\r\n                    </th>\r\n");
            EndContext();
#line 15 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(419, 97, true);
            WriteLiteral("                    <th id=\"th1\">\r\n                        نام وظیفه\r\n                    </th>\r\n");
            EndContext();
#line 21 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                }

#line default
#line hidden
            BeginContext(535, 180, true);
            WriteLiteral("                <th id=\"th2\">\r\n                    نمره مربی سطح1\r\n                </th>\r\n                <th id=\"th2\">\r\n                    نمره مربی سطح2\r\n                </th>\r\n");
            EndContext();
#line 28 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                 if (ViewBag.hasParticipant > 0 && ViewBag.participantConfirmation ?? false)
                {

#line default
#line hidden
            BeginContext(828, 103, true);
            WriteLiteral("                    <th id=\"th4\">\r\n                        نمره سایرارزیاب\r\n                    </th>\r\n");
            EndContext();
#line 33 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                }

#line default
#line hidden
            BeginContext(950, 141, true);
            WriteLiteral("                <th id=\"th5\">\r\n                    خود ارزیابی\r\n                </th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
            EndContext();
#line 40 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(1148, 22, true);
            WriteLiteral("                <tr>\r\n");
            EndContext();
#line 43 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                     if (ViewBag.hasCriteria > 0)
                    {

#line default
#line hidden
            BeginContext(1244, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(1273, 18, false);
#line 45 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                       Write(item.CriteriaTitle);

#line default
#line hidden
            EndContext();
            BeginContext(1291, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 46 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                    }
                    else
                    {

#line default
#line hidden
            BeginContext(1370, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(1399, 10, false);
#line 49 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                       Write(item.Title);

#line default
#line hidden
            EndContext();
            BeginContext(1409, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 50 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                    }

#line default
#line hidden
            BeginContext(1439, 24, true);
            WriteLiteral("                    <td>");
            EndContext();
            BeginContext(1464, 11, false);
#line 51 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                   Write(item.score1);

#line default
#line hidden
            EndContext();
            BeginContext(1475, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(1507, 11, false);
#line 52 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                   Write(item.score2);

#line default
#line hidden
            EndContext();
            BeginContext(1518, 9, true);
            WriteLiteral("</td>\r\n\r\n");
            EndContext();
#line 54 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                     if (ViewBag.hasParticipant > 0 && ViewBag.participantConfirmation ?? false)
                    {

#line default
#line hidden
            BeginContext(1648, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(1677, 21, false);
#line 56 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                       Write(item.participantScore);

#line default
#line hidden
            EndContext();
            BeginContext(1698, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 57 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                    }

#line default
#line hidden
            BeginContext(1728, 24, true);
            WriteLiteral("                    <td>");
            EndContext();
            BeginContext(1753, 14, false);
#line 58 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                   Write(item.selfScore);

#line default
#line hidden
            EndContext();
            BeginContext(1767, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 60 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
            }

#line default
#line hidden
            BeginContext(1812, 34, true);
            WriteLiteral("\r\n        </tbody>\r\n    </table>\r\n");
            EndContext();
#line 64 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
}
else if (ViewBag.type == 3)
{

#line default
#line hidden
            BeginContext(1881, 119, true);
            WriteLiteral("    <table class=\"table table-striped table-bordered table-hover\" id=\"sample_dt3\">\r\n        <thead>\r\n            <tr>\r\n");
            EndContext();
#line 70 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                 if (ViewBag.hasCriteria > 0)
                {

#line default
#line hidden
            BeginContext(2066, 96, true);
            WriteLiteral("                    <th id=\"th1\">\r\n                        نام شاخص\r\n                    </th>\r\n");
            EndContext();
#line 75 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(2222, 97, true);
            WriteLiteral("                    <th id=\"th1\">\r\n                        نام وظیفه\r\n                    </th>\r\n");
            EndContext();
#line 81 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                }

#line default
#line hidden
            BeginContext(2338, 98, true);
            WriteLiteral("                <th id=\"th2\">\r\n                    نمره ادمین برنامه ریزی\r\n                </th>\r\n");
            EndContext();
#line 85 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                 if (ViewBag.hasParticipant > 0 && ViewBag.participantConfirmation ?? false)
                {

#line default
#line hidden
            BeginContext(2549, 103, true);
            WriteLiteral("                    <th id=\"th4\">\r\n                        نمره سایرارزیاب\r\n                    </th>\r\n");
            EndContext();
#line 90 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                }

#line default
#line hidden
            BeginContext(2671, 141, true);
            WriteLiteral("                <th id=\"th5\">\r\n                    خود ارزیابی\r\n                </th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
            EndContext();
#line 97 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(2869, 22, true);
            WriteLiteral("                <tr>\r\n");
            EndContext();
#line 100 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                     if (ViewBag.hasCriteria > 0)
                    {

#line default
#line hidden
            BeginContext(2965, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(2994, 18, false);
#line 102 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                       Write(item.CriteriaTitle);

#line default
#line hidden
            EndContext();
            BeginContext(3012, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 103 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                    }
                    else
                    {

#line default
#line hidden
            BeginContext(3091, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(3120, 10, false);
#line 106 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                       Write(item.Title);

#line default
#line hidden
            EndContext();
            BeginContext(3130, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 107 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                    }

#line default
#line hidden
            BeginContext(3160, 24, true);
            WriteLiteral("                    <td>");
            EndContext();
            BeginContext(3185, 23, false);
#line 108 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                   Write(item.planningAdminScore);

#line default
#line hidden
            EndContext();
            BeginContext(3208, 9, true);
            WriteLiteral("</td>\r\n\r\n");
            EndContext();
#line 110 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                     if (ViewBag.hasParticipant > 0 && ViewBag.participantConfirmation ?? false)
                    {

#line default
#line hidden
            BeginContext(3338, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(3367, 21, false);
#line 112 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                       Write(item.participantScore);

#line default
#line hidden
            EndContext();
            BeginContext(3388, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 113 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                    }

#line default
#line hidden
            BeginContext(3418, 24, true);
            WriteLiteral("                    <td>");
            EndContext();
            BeginContext(3443, 14, false);
#line 114 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                   Write(item.selfScore);

#line default
#line hidden
            EndContext();
            BeginContext(3457, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 116 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
            }

#line default
#line hidden
            BeginContext(3502, 34, true);
            WriteLiteral("\r\n        </tbody>\r\n    </table>\r\n");
            EndContext();
#line 120 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
}
else if (ViewBag.type == 4)
{

#line default
#line hidden
            BeginContext(3571, 119, true);
            WriteLiteral("    <table class=\"table table-striped table-bordered table-hover\" id=\"sample_dt3\">\r\n        <thead>\r\n            <tr>\r\n");
            EndContext();
#line 126 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                 if (ViewBag.hasCriteria > 0)
                {

#line default
#line hidden
            BeginContext(3756, 96, true);
            WriteLiteral("                    <th id=\"th1\">\r\n                        نام شاخص\r\n                    </th>\r\n");
            EndContext();
#line 131 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(3912, 97, true);
            WriteLiteral("                    <th id=\"th1\">\r\n                        نام وظیفه\r\n                    </th>\r\n");
            EndContext();
#line 137 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                }

#line default
#line hidden
            BeginContext(4028, 65, true);
            WriteLiteral("                <th id=\"th2\">\r\n                    نمره مربی سطح ");
            EndContext();
            BeginContext(4094, 20, false);
#line 139 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                             Write(ViewBag.coacherLevel);

#line default
#line hidden
            EndContext();
            BeginContext(4114, 25, true);
            WriteLiteral("\r\n                </th>\r\n");
            EndContext();
#line 141 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                 if (ViewBag.hasParticipant > 0 && ViewBag.participantConfirmation ?? false)
                {

#line default
#line hidden
            BeginContext(4252, 103, true);
            WriteLiteral("                    <th id=\"th4\">\r\n                        نمره سایرارزیاب\r\n                    </th>\r\n");
            EndContext();
#line 146 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                }

#line default
#line hidden
            BeginContext(4374, 141, true);
            WriteLiteral("                <th id=\"th5\">\r\n                    خود ارزیابی\r\n                </th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
            EndContext();
#line 153 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(4572, 22, true);
            WriteLiteral("                <tr>\r\n");
            EndContext();
#line 156 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                     if (ViewBag.hasCriteria > 0)
                    {

#line default
#line hidden
            BeginContext(4668, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(4697, 18, false);
#line 158 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                       Write(item.CriteriaTitle);

#line default
#line hidden
            EndContext();
            BeginContext(4715, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 159 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                    }
                    else
                    {

#line default
#line hidden
            BeginContext(4794, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(4823, 10, false);
#line 162 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                       Write(item.Title);

#line default
#line hidden
            EndContext();
            BeginContext(4833, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 163 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                    }

#line default
#line hidden
            BeginContext(4863, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 165 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                     switch (ViewBag.coacherLevel)
                    {
                        case 2:

#line default
#line hidden
            BeginContext(4973, 32, true);
            WriteLiteral("                            <td>");
            EndContext();
            BeginContext(5006, 11, false);
#line 168 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                           Write(item.score2);

#line default
#line hidden
            EndContext();
            BeginContext(5017, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 169 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                            break;
                        case 3:

#line default
#line hidden
            BeginContext(5093, 32, true);
            WriteLiteral("                            <td>");
            EndContext();
            BeginContext(5126, 11, false);
#line 171 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                           Write(item.score3);

#line default
#line hidden
            EndContext();
            BeginContext(5137, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 172 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                            break;
                        case 4:

#line default
#line hidden
            BeginContext(5213, 32, true);
            WriteLiteral("                            <td>");
            EndContext();
            BeginContext(5246, 11, false);
#line 174 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                           Write(item.score4);

#line default
#line hidden
            EndContext();
            BeginContext(5257, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 175 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                            break;
                        default:
                            break;
                    }

#line default
#line hidden
            BeginContext(5393, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 181 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                     if (ViewBag.hasParticipant > 0 && ViewBag.participantConfirmation ?? false)
                    {

#line default
#line hidden
            BeginContext(5518, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(5547, 21, false);
#line 183 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                       Write(item.participantScore);

#line default
#line hidden
            EndContext();
            BeginContext(5568, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 184 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                    }

#line default
#line hidden
            BeginContext(5598, 24, true);
            WriteLiteral("                    <td>");
            EndContext();
            BeginContext(5623, 14, false);
#line 185 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
                   Write(item.selfScore);

#line default
#line hidden
            EndContext();
            BeginContext(5637, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 187 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
            }

#line default
#line hidden
            BeginContext(5682, 34, true);
            WriteLiteral("\r\n        </tbody>\r\n    </table>\r\n");
            EndContext();
#line 191 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeTaskAssignment\GetScore.cshtml"
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
