#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\PerformCompetencyConfirmation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7992f404499a87b2003d85f3e5311e0f6bd1a4f9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employee_EmployeeCompetencyAssignment_PerformCompetencyConfirmation), @"mvc.1.0.view", @"/Views/Employee/EmployeeCompetencyAssignment/PerformCompetencyConfirmation.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Employee/EmployeeCompetencyAssignment/PerformCompetencyConfirmation.cshtml", typeof(AspNetCore.Views_Employee_EmployeeCompetencyAssignment_PerformCompetencyConfirmation))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7992f404499a87b2003d85f3e5311e0f6bd1a4f9", @"/Views/Employee/EmployeeCompetencyAssignment/PerformCompetencyConfirmation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_Employee_EmployeeCompetencyAssignment_PerformCompetencyConfirmation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "4", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("performConfirmationFrm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\Employee\EmployeeCompetencyAssignment\PerformCompetencyConfirmation.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(27, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(29, 3452, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7992f404499a87b2003d85f3e5311e0f6bd1a4f96922", async() => {
                BeginContext(101, 1015, true);
                WriteLiteral(@"
    <div class=""modal fade department modalClass"" id=""performConfirmationModal"" aria-hidden=""true"">
        <div class=""modal-dialog "">
            <div class=""modal-content"">
                <div class=""modal-header bg-blue"">
                    <button type=""button"" id=""modal-close"" class=""close""
                            data-dismiss=""modal"" aria-hidden=""true""></button>
                    <h4 id=""sabt_hazine_personnel"" class=""modal-title"">
                        <i class=""fa fa-file-o""></i>
                        تایید/عدم تایید شایستگی های رفتاری اختصاص داده شده توسط مربی
                    </h4>
                </div>

                <div class=""modal-body"">
                    <div class=""form-body"">

                        <div id=""alert_danger"" class=""alert alert-danger display-hide"">
                            <button class=""close"" data-close=""alert""></button>
                            پر کردن فيلدهاي ستاره دار اجباري مي باشد.
                        </div>

");
                EndContext();
                BeginContext(1484, 924, true);
                WriteLiteral(@"                        <div class=""row"">
                            <!--/span-->
                            <div class=""col-md-6"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-6"">
                                        تایید/عدم تایید
                                        <span class=""required"">
                                            *
                                        </span>
                                    </label>
                                    <div class=""col-md-6"">
                                        <div class=""input-icon right"">
                                            <i class=""fa""></i>
                                            <select id=""EvaluationAcceptanceStatusId"" name=""EvaluationAcceptanceStatusId"" class=""form-control"">
                                                ");
                EndContext();
                BeginContext(2408, 17, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7992f404499a87b2003d85f3e5311e0f6bd1a4f99420", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2425, 50, true);
                WriteLiteral("\r\n                                                ");
                EndContext();
                BeginContext(2475, 32, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7992f404499a87b2003d85f3e5311e0f6bd1a4f910594", async() => {
                    BeginContext(2493, 5, true);
                    WriteLiteral("تایید");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2507, 50, true);
                WriteLiteral("\r\n                                                ");
                EndContext();
                BeginContext(2557, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7992f404499a87b2003d85f3e5311e0f6bd1a4f912107", async() => {
                    BeginContext(2575, 9, true);
                    WriteLiteral("عدم تایید");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2593, 881, true);
                WriteLiteral(@"
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id=""competencyRow""></div>


                    </div>
                    <!-- END FORM-->
                </div>

                <div class=""modal-footer"">
                    <button type=""submit"" class=""btn green input-sm input-small"" id=""performConfirmationBTN"">ذخیره</button>
                    <button type=""button"" class=""btn red input-sm input-small"" id=""cancel11"" data-dismiss=""modal""><i class=""fa fa-times""></i>انصراف</button>
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
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3481, 10538, true);
            WriteLiteral(@"
<script type=""text/html"" id=""appendCompetency"">
    <div class=""row"">
        <!--/span-->
        <div class=""col-md-4"">
            <div class=""form-group"">
                <label class=""control-label col-md-6"">
                    نام وظیفه:
                </label>
            </div>
        </div>
        <div class=""col-md-4"">
            <div class=""form-group"">
                <label evaluationCompetencyId=""{3}"" class=""control-label col-md-6"">
                    {1}
                </label>
            </div>
        </div>
        <div class=""col-md-4 hidden refutationCauseDiv"">
            <div class=""form-group"">
                <label class=""control-label col-md-4"">
                    علت
                    <span class=""required"">
                        *
                    </span>
                </label>
                <div class=""col-md-8"">
                    <div class=""input-icon right"">
                        <i class=""fa""></i>
                        <t");
            WriteLiteral(@"extarea name=""refutationCause{0}"" id=""refutationCause{0}"" class=""form-control refutationCause"" rows=""1""></textarea>
                    </div>
                </div>
            </div>
        </div>
    </div>
</script>
<script>

    $(document).ready(function () {
        var index;;
        var finalResult = false;
        var template = jQuery.validator.format($.trim($(""#appendCompetency"").html()));
        var length = dt.rows('.selected').data().length;
        var evaluationArr = [];
        var selector;
        if (length > 0) {
            selector = ""#CompetencyAssignmentTable >tbody>tr.selected"";
        } else {
            selector = ""#CompetencyAssignmentTable >tbody>tr"";
        }
        var row;
        $(selector).each(function (i, tr) {
            row = dt.row(tr);
            //console.log(dt.row(tr));
            //row.child(row.data().fullName).show()
            //alert(row.data().fullName)
            if (row.data().RoleName != ""PlanningAdmin"" && row.data()");
            WriteLiteral(@".RoleName != ""HRAdmin"" && (row.data().EvaluationAcceptanceStatusId == 1 || row.data().EvaluationAcceptanceStatusId == 4)) {
                index = i++;
                $(template(index, row.data().CompetencyTitle, row.data().BehaviouralCompetencyId, row.data().EvaluationBehaviouralCompetencyId, row.data().PeriodDefinitoionId)).appendTo(""#competencyRow"");
                evaluationArr.push(row.data().EvaluationBehaviouralCompetencyId);
                finalResult = true;
            }
        })
        //console.log(evaluationArr);
        if (!finalResult) {
            alert(""فقط شایستگی رفتاری که اختصاص دهنده آن مربی و وضعیت آن تفاهم یا نامشخص باشد قابلیت تایید یا عدم تایید دارند."");
        }
        else if (finalResult) {
            $('#performConfirmationModal').modal('show');

            $(""#performConfirmationBTN"").click(function () {
                //alert('dpCreationSubmit');
                //var postdata2 = $(this).serializeArray();
                var form = $(""#performConfi");
            WriteLiteral(@"rmationFrm"");
                form.validate({
                    //console.log($('#registerform').serializeArray());
                    errorElement: 'span', //default input error message container
                    errorClass: 'help-block', // default input error message class
                    focusInvalid: false, // do not focus the last invalid input
                    ignore: """",
                    rules: {
                        EvaluationAcceptanceStatusId: {
                            required: true,
                        }
                    },
                    messages: {
                        EvaluationAcceptanceStatusId: {
                            required: ""پر کردن این فیلد الزامی می باشد""
                        }
                    },
                    invalidHandler: function (event, validator) { //display error alert on form submit
                        //                    success2.hide();
                        //                    error2.show");
            WriteLiteral(@"();
                        //                    App.scrollTo(error2, -200);
                    },
                    errorPlacement: function (error, element) { // render error placement for each input type
                        var icon = $(element).parent('.input-icon').children('i');
                        icon.removeClass('fa-check').addClass(""fa-warning"");
                        icon.attr(""data-original-title"", error.text()).tooltip();
                    },
                    highlight: function (element) { // hightlight error inputs
                        $(element)
                            .closest('.form-group').addClass('has-error'); // set error class to the control group
                    },

                    unhighlight: function (element) { // revert the change done by hightlight

                    },

                    success: function (label, element) {
                        var icon = $(element).parent('.input-icon').children('i');
                 ");
            WriteLiteral(@"       $(element).closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
                        icon.removeClass(""fa-warning"").addClass(""fa-check"");
                    },

                    submitHandler: function (form) {
                        //                    success2.show();
                        //                    error2.hide();
                    }
                })
                if (form.valid() == false) {
                    //console.log($('#registerform').serializeArray());

                    return false;
                } else {
                    var PerformCompetencyConfirmationViewList = [];
                    //$(""#articleSubmit"").addClass('disabled');
                    // $(""#dpCreationSubmit"").attr(""disabled"", ""disabled"");
                    $(""#competencyRow .row"").each(function () {
                        var PerformCompetencyConfirmationView = {};
                        PerformCompe");
            WriteLiteral(@"tencyConfirmationView.EvaluationCompetencyId2 = $(this).find(""label[evaluationCompetencyId]"").attr(""evaluationCompetencyId"");
                        PerformCompetencyConfirmationView.EvaluationCompetencyAcceptanceStatusId = $('#EvaluationAcceptanceStatusId').children('option:selected').val();
                        if ($('#EvaluationAcceptanceStatusId').children('option:selected').val() == 4) {
                            PerformCompetencyConfirmationView.RefutationCause = $(this).find(""textarea"").val();
                        }
                        PerformCompetencyConfirmationViewList.push(PerformCompetencyConfirmationView);
                    });
                    var btn = $(""#performConfirmationBTN"");
                    btn.button('loading');
                    //var postdata = new FormData(this);
                    //var postdata = $('#scoreAssignmentFrm').serializeArray();

                    //console.log(postdata);
                    $.ajax(
                        {
    ");
            WriteLiteral(@"                        datatype: ""json"",
                            //data:postdata,
                            url: ""/EmployeeCompetencyAssignment/PerformCompetencyConfirmation"",
                            //url : formURL,
                            //                        data: postdata2,
                            data: JSON.stringify(PerformCompetencyConfirmationViewList),
                            //data: ""firstName="" + $(""fn"").val(),
                            //cache: false,
                            contentType: 'application/json; charset=utf-8',
                            //contentType: false,
                            //processData: false,
                            type: ""POST"",

                            success: function (data, textStatus, jqXHR) {
                                if (data > 0) {
                                    $(""#performConfirmationModal"").modal(""hide"")
                                    dt.ajax.url(""/EmployeeCompetencyAssignment/GetAssign");
            WriteLiteral(@"edCompetencyList"");
                                    dt.ajax.reload();
                                    //resetFormValidation();
                                    toastr.options.timeOut = ""15000"";
                                    toastr.options.positionClass = ""toast-top-center"";
                                    toastr.success(""اطلاعات مورد نظر با موفقیت ثبت گردید."");
                                } else if (data == 0) {
                                    toastr.options.timeOut = ""15000"";
                                    toastr.options.positionClass = ""toast-top-center"";
                                    toastr.error(""اطلاعات مورد نظر ثبت نگردید."");
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                alert(""erorr"");
                                alert(jqXHR);
                                alert(textStatus);
                            }
        ");
            WriteLiteral(@"                }).always(function () {
                            $(""#performConfirmationBTN"").button('reset');
                        });
                    //$.getScript('/assets/javascript/articleForm.js', function () {
                    //    ArticleForm.init(postdata);
                    //    $(""#articleForm"")[0].reset();
                    //});
                    // e.preventDefault(e);
                }
            });
            $(""#EvaluationAcceptanceStatusId"").on(""change"", function () {
                if ($(this).children('option:selected').val() == 4) {
                    $("".refutationCauseDiv"").removeClass(""hidden"");
                    $("".refutationCause"").removeAttr(""disabled"");

                } else {
                    $("".refutationCauseDiv"").addClass(""hidden"");
                    $("".refutationCause"").attr(""disabled"",""disabled"");
                }
            });
            $(""#performConfirmationBTN"").trigger(""click"");
            $('.refutationCau");
            WriteLiteral(@"se').each(function (i, d) {
                $(this).rules(""add"", {
                    required: true,
                    messages: {
                        required: ""پرکردن این فیلد الزامی می باشد"",
                    }
                });
            });
        }
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
