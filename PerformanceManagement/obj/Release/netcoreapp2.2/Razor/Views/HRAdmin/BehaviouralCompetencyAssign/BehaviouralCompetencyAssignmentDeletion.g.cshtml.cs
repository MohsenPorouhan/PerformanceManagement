#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\BehaviouralCompetencyAssign\BehaviouralCompetencyAssignmentDeletion.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7b2a16c7af958f68e6fdd8623a2ccec59de71d3b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HRAdmin_BehaviouralCompetencyAssign_BehaviouralCompetencyAssignmentDeletion), @"mvc.1.0.view", @"/Views/HRAdmin/BehaviouralCompetencyAssign/BehaviouralCompetencyAssignmentDeletion.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/HRAdmin/BehaviouralCompetencyAssign/BehaviouralCompetencyAssignmentDeletion.cshtml", typeof(AspNetCore.Views_HRAdmin_BehaviouralCompetencyAssign_BehaviouralCompetencyAssignmentDeletion))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7b2a16c7af958f68e6fdd8623a2ccec59de71d3b", @"/Views/HRAdmin/BehaviouralCompetencyAssign/BehaviouralCompetencyAssignmentDeletion.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_HRAdmin_BehaviouralCompetencyAssign_BehaviouralCompetencyAssignmentDeletion : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PerformanceManagement.Models.HRAdmin.View.EvaluationBehaviouralDeletionView>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("deleteCompetencyAssignmentFrm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\BehaviouralCompetencyAssign\BehaviouralCompetencyAssignmentDeletion.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(111, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(113, 1940, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7b2a16c7af958f68e6fdd8623a2ccec59de71d3b6349", async() => {
                BeginContext(192, 26, true);
                WriteLiteral("\r\n    <input type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 218, "\"", 266, 1);
#line 7 "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\BehaviouralCompetencyAssign\BehaviouralCompetencyAssignmentDeletion.cshtml"
WriteAttributeValue("", 226, Model.EvaluationBehaviouralCompetencyId, 226, 40, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(267, 625, true);
                WriteLiteral(@" name=""evaluationBehaviouralCompetencyId"" id=""evaluationBehaviouralCompetencyId"" />
    <div class=""modal fade"" id=""deleteTCompetencyAssignmentModal"" tabindex="""" role="""" aria-hidden=""true"">
        <div class=""modal-dialog "">
            <div class=""modal-content"">
                <div class=""modal-header bg-blue"">
                    <button type=""button"" id=""modal-close"" class=""close""
                            data-dismiss=""modal"" aria-hidden=""true""></button>
                    <h4 class=""modal-title"">
                        <i class=""fa fa-file-o""></i>
                        <span>حذف شایستگی اختصاصی ");
                EndContext();
                BeginContext(893, 11, false);
#line 16 "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\BehaviouralCompetencyAssign\BehaviouralCompetencyAssignmentDeletion.cshtml"
                                             Write(Model.Title);

#line default
#line hidden
                EndContext();
                BeginContext(904, 1142, true);
                WriteLiteral(@"</span>
                    </h4>
                </div>
                <div class=""modal-body"">
                    <!-- BEGIN FORM-->
                    <div class=""form-body"">
                        <div id=""alert_danger"" class=""alert alert-danger display-hide"">
                            <button class=""close"" data-close=""alert""></button>
                            پر کردن فيلدهاي ستاره دار اجباري مي باشد.
                        </div>
                        <b>آیا از حذف شایستگی رفتاری اختصاصی مورد نظر اطمینان دارید؟</b>
                        <!--/row-->
                    </div>
                    <!-- END FORM-->
                </div>
                <div class=""modal-footer"">
                    <button type=""submit"" class=""btn green input-sm input-small"" id=""deleteCompetencyAssignmentBtn"">بله</button>
                    <button type=""button"" class=""btn red input-sm input-small"" id=""no"" data-dismiss=""modal""><i class=""fa fa-times""></i>خیر</button>
                </div>
 ");
                WriteLiteral("           </div>\r\n            <!-- /.modal-content -->\r\n        </div>\r\n        <!-- /.modal-dialog -->\r\n    </div>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2053, 1856, true);
            WriteLiteral(@"
<script>
    $(""#deleteTCompetencyAssignmentModal"").modal('show');
    $(""#deleteCompetencyAssignmentFrm"").submit(function (e) {
        $(""#deleteCompetencyAssignmentBtn"").attr(""disabled"", ""disabled"");
        var btn = $(""#deleteCompetencyAssignmentBtn"");
        btn.button('loading');
        var postdata = new FormData(this);
        $.ajax({
            //data:postdata,
            url: ""/BehaviouralCompetencyAssign/BehaviouralCompetencyAssignmentDeletion"",
            //url : formURL,
            //data: postdata2,
            data: postdata,
            //data: ""firstName="" + $(""fn"").val(),
            cache: false,
            contentType: false,
            processData: false,
            type: ""post"",
            success: function (data) {
                if (data == 0) {
                    toastr.options.timeOut = ""15000"";
                    toastr.options.positionClass = ""toast-top-center"";
                    toastr.info(""شایستگی رفتاری اختصاصی مورد نظر حذف نگردید"");
  ");
            WriteLiteral(@"              } else if (data > 0) {
                    toastr.options.timeOut = ""15000"";
                    toastr.options.positionClass = ""toast-top-center"";
                    toastr.success(""شایستگی رفتاری اختصاصی مورد نظر حذف گردید"");
                    dt.ajax.reload();
                } else {
                    toastr.options.timeOut = ""15000"";
                    toastr.options.positionClass = ""toast-top-center"";
                    toastr.info(data);
                }
                $(""#deleteTCompetencyAssignmentModal"").modal(""hide"");
            },
            error: function (status) {
                alert(""Error"");
            }
        }).always(function () {
            $(""#deleteCompetencyAssignmentBtn"").button('reset');
        });
        e.preventDefault(e);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PerformanceManagement.Models.HRAdmin.View.EvaluationBehaviouralDeletionView> Html { get; private set; }
    }
}
#pragma warning restore 1591
