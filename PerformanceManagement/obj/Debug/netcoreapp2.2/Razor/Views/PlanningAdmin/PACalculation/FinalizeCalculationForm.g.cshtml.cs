#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\PlanningAdmin\PACalculation\FinalizeCalculationForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca3b6571579a726ed46f3665def150c9a419b6e7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PlanningAdmin_PACalculation_FinalizeCalculationForm), @"mvc.1.0.view", @"/Views/PlanningAdmin/PACalculation/FinalizeCalculationForm.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/PlanningAdmin/PACalculation/FinalizeCalculationForm.cshtml", typeof(AspNetCore.Views_PlanningAdmin_PACalculation_FinalizeCalculationForm))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca3b6571579a726ed46f3665def150c9a419b6e7", @"/Views/PlanningAdmin/PACalculation/FinalizeCalculationForm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_PlanningAdmin_PACalculation_FinalizeCalculationForm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("finalizeCalculationFrm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\PlanningAdmin\PACalculation\FinalizeCalculationForm.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(27, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(29, 1801, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ca3b6571579a726ed46f3665def150c9a419b6e76080", async() => {
                BeginContext(101, 1722, true);
                WriteLiteral(@"
    <div class=""modal fade department modalClass"" id=""finalizeCalculationModal"" tabindex="""" aria-hidden=""true"" data-backdrop=""static"">
        <div class=""modal-dialog "">
            <div class=""modal-content"">
                <div class=""modal-header bg-blue"">
                    <button type=""button"" id=""modal-close"" class=""close modalclose2""
                            data-dismiss=""modal"" aria-hidden=""true""></button>
                    <h4 id=""sabt_hazine_personnel"" class=""modal-title"">
                        <i class=""fa fa-file-o""></i>
                        عقبگرد محاسبات
                    </h4>
                </div>

                <div class=""modal-body"">
                    <div class=""form-body"">

                        <div id=""alert_danger"" class=""alert alert-danger display-hide"">
                            <button class=""close"" data-close=""alert""></button>
                            پر کردن فيلدهاي ستاره دار اجباري مي باشد.
                        </div>

        ");
                WriteLiteral(@"                <b>در صورت نهایی کردن ، محاسبات مجدد امکان پذیر نمی باشد. آیا از نهایی کردن محاسبات انجام شده اطمینان دارید؟</b>


                    </div>
                    <!-- END FORM-->
                </div>

                <div class=""modal-footer"">
                    <button type=""submit"" class=""btn green input-sm input-small"" id=""finalizeCalculationSubmit"">ذخيره</button>
                    <button type=""button"" class=""btn red input-sm input-small"" id=""cancel2"" data-dismiss=""modal""><i class=""fa fa-times""></i>انصراف</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
");
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
            BeginContext(1830, 3951, true);
            WriteLiteral(@"
<script>
    $('#finalizeCalculationModal').modal('show');
    $(""#finalizeCalculationFrm"").submit(function (e) {
        //alert('dpCreationSubmit');
        //var postdata2 = $(this).serializeArray();
        //debugger;

        var form = $('#finalizeCalculationFrm');

        //$(""#articleSubmit"").addClass('disabled');
        $(""#finalizeCalculationSubmit"").attr(""disabled"", ""disabled"");
        $(""#cancel2"").attr(""disabled"", ""disabled"");
        $("".modalclose2"").attr(""disabled"", ""disabled"");

        var btn = $(""#finalizeCalculationSubmit"");
        btn.button('loading');
        //var postdata = new FormData(this);
        //listOfSubTask = JSON.stringify(listOfSubTask);
        //var postdata = $(this).serializeArray();
        //postdata.append(JSON.stringify(listOfSubTask));
        //console.log(postdata);
        $.ajax(
            {
                url: '/PACalculation/FinalizeCalculation',
                //url : formURL,
                //data: postdata,
         ");
            WriteLiteral(@"       //data: JSON.stringify(CoveredEmployee),
                //data: ""firstName="" + $(""fn"").val(),
                //cache: false,
                contentType: false,
                processData: false,
                //datatype: 'json',
                //contentType: 'application/json; charset=utf-8',
                type: 'POST',

                success: function (data, textStatus, jqXHR) {
                    var message = """";
                    if (data > 0) {
                        message += ""<span>محاسبات مورد نظر با موفقیت نهایی گردید</span><br><br>"";
                        toastr.options.timeOut = ""15000"";
                        toastr.options.positionClass = ""toast-top-center"";
                        toastr.success(message);
                        dt.ajax.url(""/PACalculation/GetCalculationScoreList"");
                        dt.ajax.reload();
                    }
                    else if (data == 0) {
                        message += ""<span>نهایی کردن محاسبات موف");
            WriteLiteral(@"قیت آمیز نبود.</span><br><br>"";
                        toastr.options.timeOut = ""15000"";
                        toastr.options.positionClass = ""toast-top-center"";
                        toastr.info(message);
                    } else if (data == -1) {
                        message += ""<span>در دوره جاری محاسبات قبلا نهایی شده است.</span><br><br>"";
                        toastr.options.timeOut = ""15000"";
                        toastr.options.positionClass = ""toast-top-center"";
                        toastr.info(message);
                    } else if (data == -2) {
                        message += ""<span>محاسباتی در سیستم موجود نمی باشد لازم است ابتدا محاسبات را اجرا نمایید</span><br><br>"";
                        toastr.options.timeOut = ""15000"";
                        toastr.options.positionClass = ""toast-top-center"";
                        toastr.info(message);
                    }

                    //resetFormValidation();
                    $(""#finalizeCalculationFrm"")[0");
            WriteLiteral(@"].reset();
                    $(""#finalizeCalculationModal"").modal(""hide"");
                    //$(""#periodDefinitionTable"").DataTable().destroy();
                    //show_dataTable();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(""erorr00000"");
                    alert(jqXHR);
                    alert(textStatus);
                }
            }).always(function () {
                $(""#finalizeCalculationSubmit"").button('reset');
                $(""#cancel2"").removeAttr(""disabled"");
                $("".modalclose2"").removeAttr(""disabled"");
            });
        //$.getScript('/assets/javascript/articleForm.js', function () {
        //    ArticleForm.init(postdata);
        //    $(""#articleForm"")[0].reset();
        //});
        e.preventDefault(e);

    });
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
