#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\Coacher\TaskDivision\DeleteCriteria.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b9673e77d925e19cf3373d0b5d0dfedd67a063de"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Coacher_TaskDivision_DeleteCriteria), @"mvc.1.0.view", @"/Views/Coacher/TaskDivision/DeleteCriteria.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Coacher/TaskDivision/DeleteCriteria.cshtml", typeof(AspNetCore.Views_Coacher_TaskDivision_DeleteCriteria))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b9673e77d925e19cf3373d0b5d0dfedd67a063de", @"/Views/Coacher/TaskDivision/DeleteCriteria.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_Coacher_TaskDivision_DeleteCriteria : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\Coacher\TaskDivision\DeleteCriteria.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(27, 3063, true);
            WriteLiteral(@"
<div class=""modal fade"" id=""deleteConfirmationModal"" tabindex="""" role="""" aria-hidden=""true"">
    <div class=""modal-dialog "">
        <div class=""modal-content"">
            <div class=""modal-header bg-blue"">
                <button type=""button"" id=""modal-close"" class=""close""
                        data-dismiss=""modal"" aria-hidden=""true""></button>
                <h4 class=""modal-title"">
                    <i class=""fa fa-file-o""></i>
                    <span id=""deleteConfirmationModalTitle""></span>
                </h4>
            </div>
            <div class=""modal-body"">
                <!-- BEGIN FORM-->
                <div class=""form-body"">
                    <div id=""alert_danger"" class=""alert alert-danger display-hide"">
                        <button class=""close"" data-close=""alert""></button>
                        پر کردن فيلدهاي ستاره دار اجباري مي باشد.
                    </div>
                    <b>آیا از حذف شاخص مورد نظر اطمینان دارید؟</b>
                    <!");
            WriteLiteral(@"--/row-->
                </div>
                <!-- END FORM-->
            </div>
            <div class=""modal-footer"">
                <button type=""submit"" class=""btn green input-sm input-small"" id=""yesDeleteCriteria"">بله</button>
                <button type=""button"" class=""btn red input-sm input-small"" id=""no"" data-dismiss=""modal""><i class=""fa fa-times""></i>خیر</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
<script>
    $(""#deleteConfirmationModalTitle"").text(""تایید حذف شاخص "" + criteriaTitlee);
    $(""#deleteConfirmationModal"").modal('show');
    $(""#yesDeleteCriteria"").click(function () {
        $(""#yesDeleteCriteria"").attr(""disabled"", ""disabled"");
        var btn = $(""#yesDeleteCriteria"");
        btn.button('loading');
        $.ajax({
            type: 'post',
            url: '/TaskDivision/DeleteCriteria',
            data: { criteriaId: criteriaIdd, periodDefinitionId: periodDefinitionIdd, depart");
            WriteLiteral(@"mentId: departmentIdd },
            success: function (data) {
                if (data == 0) {
                    toastr.options.timeOut = ""15000"";
                    toastr.options.positionClass = ""toast-top-center"";
                    toastr.info(""شاخص مورد نظر حذف نگردید"");
                } else if (data > 0) {
                    toastr.options.timeOut = ""15000"";
                    toastr.options.positionClass = ""toast-top-center"";
                    toastr.success(""شاخص مورد نظر حذف گردید"");
                } else {
                    toastr.options.timeOut = ""15000"";
                    toastr.options.positionClass = ""toast-top-center"";
                    toastr.info(data);
                }
                $(""#deleteConfirmationModal"").modal(""hide"");
            },
            error: function (status) {
                alert(""Error"");
            }
        }).always(function () {
            $(""#yesDeleteCriteria"").button('reset');
        });
    })
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
