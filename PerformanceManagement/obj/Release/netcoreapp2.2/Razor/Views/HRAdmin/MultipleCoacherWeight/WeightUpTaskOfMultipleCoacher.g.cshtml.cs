#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\MultipleCoacherWeight\WeightUpTaskOfMultipleCoacher.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7875bdf20bcdbe15350193127f035cfa604096b3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HRAdmin_MultipleCoacherWeight_WeightUpTaskOfMultipleCoacher), @"mvc.1.0.view", @"/Views/HRAdmin/MultipleCoacherWeight/WeightUpTaskOfMultipleCoacher.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/HRAdmin/MultipleCoacherWeight/WeightUpTaskOfMultipleCoacher.cshtml", typeof(AspNetCore.Views_HRAdmin_MultipleCoacherWeight_WeightUpTaskOfMultipleCoacher))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7875bdf20bcdbe15350193127f035cfa604096b3", @"/Views/HRAdmin/MultipleCoacherWeight/WeightUpTaskOfMultipleCoacher.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_HRAdmin_MultipleCoacherWeight_WeightUpTaskOfMultipleCoacher : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("multipleTaskCoacherForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\MultipleCoacherWeight\WeightUpTaskOfMultipleCoacher.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(27, 4332, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7875bdf20bcdbe15350193127f035cfa604096b36043", async() => {
                BeginContext(100, 4252, true);
                WriteLiteral(@"
    <div class=""modal fade"" id=""multipleTaskCoacherModal"" aria-hidden=""true"">
        <div class=""modal-dialog modal-wide"">
            <div class=""modal-content"">
                <div class=""modal-header bg-blue"">
                    <button type=""button"" id=""modal-close"" class=""close""
                            data-dismiss=""modal"" aria-hidden=""true""></button>
                    <h4 id=""modalTitle"" class=""modal-title"">
                        <i class=""fa fa-file-o""></i>

                    </h4>
                </div>
                <div class=""modal-body"">
                    <div class=""form-body"">
                        <div id=""alert_danger"" class=""alert alert-danger display-hide"">
                            <button class=""close"" data-close=""alert""></button>
                            پر کردن فيلدهاي ستاره دار اجباري مي باشد.
                        </div>
                        <div id=""coacherTaskPlaceHolder"">
                            <div class=""row"">
                ");
                WriteLiteral(@"                <!--/span-->
                                <div class=""col-md-2"">
                                    <div class=""form-group"">
                                        <label class=""control-label col-md-12"">
                                            نام:
                                        </label>
                                    </div>
                                </div>
                                <div class=""col-md-2"">
                                    <div class=""form-group"">
                                        <label class=""control-label col-md-12"">
                                            واحد سازمانی:
                                        </label>
                                    </div>
                                </div>
                                <div class=""col-md-2"">
                                    <div class=""form-group"">
                                        <label class=""control-label col-md-12"">
                    ");
                WriteLiteral(@"                        واحد سازمانی کارمند:
                                        </label>
                                    </div>
                                </div>
                                <div class=""col-md-2"">
                                    <div class=""form-group"">
                                        <label class=""control-label col-md-12"">
                                            سطح:
                                        </label>
                                    </div>
                                </div>
                                <div class=""col-md-2"">
                                    <div class=""form-group"">
                                        <label class=""control-label col-md-12"">
                                            نمره:
                                        </label>
                                    </div>
                                </div>
                                <div class=""col-md-2"">
                     ");
                WriteLiteral(@"               <div class=""form-group"">
                                        <label id=""mojri_name_label"" class=""control-label col-md-12"">
                                            تخصیص وزن:
                                            <span class=""required"">
                                                *
                                            </span>
                                        </label>
                                    </div>
                                </div>
                                <!--/span-->
                            </div>
                        </div>
                        <!--/row-->
                    </div>
                </div>
                <div class=""modal-footer"">
                    <button type=""submit"" class=""btn green input-sm input-small"" id=""multipleTaskCoacherBtn"">ذخيره</button>
                    <button type=""button"" class=""btn red input-sm input-small"" id=""cancel"" data-dismiss=""modal""><i class=""fa fa-times""></i>انصراف");
                WriteLiteral("</button>\r\n                </div>\r\n\r\n            </div>\r\n            <!-- /.modal-content -->\r\n        </div>\r\n        <!-- /.modal-dialog -->\r\n    </div>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4359, 12978, true);
            WriteLiteral(@"
<script type=""text/html"" id=""appendTaskCoacher"">
    <div class=""row"">
        <!--/span-->
        <div class=""col-md-2"">
            <div class=""form-group"">
                <label class=""control-label col-md-12"">
                    {3}
                </label>
            </div>
        </div>
        <div class=""col-md-2"">
            <div class=""form-group"">
                <label class=""control-label col-md-12"">
                    {2}
                </label>
            </div>
        </div>
        <div class=""col-md-2"">
            <div class=""form-group"">
                <label class=""control-label col-md-12"">
                    {6}
                </label>
            </div>
        </div>
        <div class=""col-md-2"">
            <div class=""form-group"">
                <label class=""control-label col-md-12"">
                    {5}
                </label>
            </div>
        </div>
        <div class=""col-md-2"">
            <div class=""form-group"">
 ");
            WriteLiteral(@"               <label class=""control-label col-md-12"">
                    {4}
                </label>
            </div>
        </div>
        <div class=""col-md-2"">
            <div class=""form-group"">
                <div class=""col-md-12"">
                    <div class=""input-icon right"">
                        <i class=""fa""></i>
                        <input name=""adminWeight{0}"" finalScoreCalculationId=""{1}"" employeeId=""{8}"" periodDefinitionId=""{7}"" type=""text"" class=""form-control input-sm input-small adminWeight"" value="""">
                    </div>
                </div>
            </div>
        </div>
        <!--/span-->
    </div>
</script>
<script>

    var roleName;
    var finalScore;
    var length = dt.rows('.selected').data().length;
    var selector;
    if (length == 1) {
        selector = ""#multipleCoacherWeightTable >tbody>tr.selected"";
    }
    if (selector === undefined) {
        alert(""کارمند مورد نظر را انتخاب نمایید"")
    } else {
        var r");
            WriteLiteral(@"ow = dt.row(selector);
        if (row.data().TaskCoacherNumber > 1) {
            $('#multipleTaskCoacherModal').modal('show');

            var template = jQuery.validator.format($.trim($(""#appendTaskCoacher"").html()));
            $.ajax({
                type: 'get',
                url: '/MultipleCoacherWeight/GetTaskCoacherList',
                data: {
                    periodDefinitionId: row.data().PeriodDefinitoionId,
                    employeeId: row.data().RecieverAllocationPersonId
                },
                async: false,
                success: function (data) {
                    $('#modalTitle').text(""تعیین وزن نمرات نهایی وظایف مربی های آقای "" + row.data().FullName)
                    $(data).each(function (i, d) {
                        switch (d.RoleName) {
                            case 'PlanningAdmin': roleName = 'ادمین برنامه ریزی';
                                break;
                            default:
                        }
                ");
            WriteLiteral(@"        if (d.FinalScore == null) {
                            finalScore = 'فاقد ارزیابی';
                        } else {
                            finalScore = d.FinalScore;
                        }
                        if (d.AllocatorEvaluationHierarchyId != null) {
                            $(template(i, d.FinalScoreCalculationId, d.CoacherDepartmentName, d.CoacherFullName, finalScore, d.AllocatorLevel, d.EmployeeDepartmentName, d.PeriodDefinitoionId, d.RecieverAllocationPersonId)).appendTo(""#coacherTaskPlaceHolder"");
                        } else {
                            $(template(i, d.FinalScoreCalculationId, roleName, roleName, finalScore, '---', d.EmployeeDepartmentName, d.PeriodDefinitoionId, d.RecieverAllocationPersonId)).appendTo(""#coacherTaskPlaceHolder"");
                        }
                    })
                },
                error: function (status) {
                    alert(""Error"");
                }
            })
            $(""#multipleTaskCoa");
            WriteLiteral(@"cherForm"").submit(function (e) {
                //alert('dpCreationSubmit');
                //var postdata2 = $(this).serializeArray();
                //debugger;
                var form = $('#multipleTaskCoacherForm');
                form.validate({
                    //console.log($('#registerform').serializeArray());
                    errorElement: 'span', //default input error message container
                    errorClass: 'help-block', // default input error message class
                    focusInvalid: false, // do not focus the last invalid input
                    ignore: """",
                    rules: {
                        adminWeight1: {
                            required: true
                        }
                    },
                    messages: {
                        adminWeight1: {
                            required: ""پر کردن این فیلد الزامی می باشد""
                        }
                    },
                    invalidHandler: function");
            WriteLiteral(@" (event, validator) { //display error alert on form submit
                        //                    success2.hide();
                        //                    error2.show();
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

    ");
            WriteLiteral(@"                },

                    success: function (label, element) {
                        var icon = $(element).parent('.input-icon').children('i');
                        $(element).closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
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
                    var listOfMultipleTaskCoacher = [];
                    $(""#coacherTaskPlaceHolder input[type=text]"").each(function () {
                        var MultipleTaskCoacherView = {};
");
            WriteLiteral(@"                        MultipleTaskCoacherView.FinalScoreCalculationId = $(this).attr(""finalScoreCalculationId"");
                        MultipleTaskCoacherView.EmployeeId = $(this).attr(""employeeId"");
                        MultipleTaskCoacherView.PeriodDefinitoionId = $(this).attr(""periodDefinitionId"");
                        MultipleTaskCoacherView.TaskWeight = $(this).val();

                        listOfMultipleTaskCoacher.push(MultipleTaskCoacherView);
                    });

                    //$(""#articleSubmit"").addClass('disabled');
                    $(""#multipleTaskCoacherBtn"").attr(""disabled"", ""disabled"");
                    var btn = $(""#multipleTaskCoacherBtn"");
                    btn.button('loading');
                    ///var postdata = new FormData(this);
                    //var postdata = $('#dpcreation').serializeArray();

                    console.log(listOfMultipleTaskCoacher);
                    $.ajax(
                        {
                      ");
            WriteLiteral(@"      datatype: ""json"",
                            //data:postdata,
                            url: ""/MultipleCoacherWeight/WeightUpTaskOfMultipleCoacher"",
                            //url : formURL,
                            //                        data: postdata2,
                            data: JSON.stringify(listOfMultipleTaskCoacher),
                            //data: ""firstName="" + $(""fn"").val(),
                            //cache: false,
                            contentType: 'application/json; charset=utf-8',
                            //contentType: false,
                            //processData: false,
                            type: ""POST"",

                            success: function (data, textStatus, jqXHR) {
                                var message = """";
                                var duplication = """";
                                var overSummation = """";
                                if (data.result > 0) {
                                    m");
            WriteLiteral(@"essage += ""<span>اطلاعات  مورد نظر با موفقیت ثبت گردید</span><br><br>"";
                                    toastr.options.timeOut = ""15000"";
                                    toastr.options.positionClass = ""toast-top-center"";
                                    toastr.success(message);
                                    $(""#multipleTaskCoacherForm"")[0].reset();
                                    $(""#multipleTaskCoacherModal"").modal(""hide"");
                                    dt.ajax.url(""/MultipleCoacherWeight/GetMultipleCoacherWeightList"");
                                    dt.ajax.reload();
                                }
                                else if (data.result == 0) {
                                    message += ""اطلاعات  مورد نظر ثبت نگردید."";
                                    toastr.options.timeOut = ""15000"";
                                    toastr.options.positionClass = ""toast-top-center"";
                                    toastr.info(message);
            ");
            WriteLiteral(@"                    }
                                if (data.duplication == ""duplication"") {
                                    duplication += ""اطلاعات ارسالی در دوره مورد نظر قبلا ثبت شده است"";
                                    toastr.options.timeOut = ""15000"";
                                    toastr.options.positionClass = ""toast-top-center"";
                                    toastr.info(duplication);
                                }
                                if (data.overSummation == ""over"") {
                                    overSummation += ""جمع وزن ها باید برابر با 100 باشد"";
                                    toastr.options.timeOut = ""15000"";
                                    toastr.options.positionClass = ""toast-top-center"";
                                    toastr.info(overSummation);
                                }

                                //$(""#periodDefinitionTable"").DataTable().destroy();
                                //show_dataTable();
      ");
            WriteLiteral(@"                      },
                            error: function (jqXHR, textStatus, errorThrown) {
                                alert(""erorr00000"");
                                alert(jqXHR);
                                alert(textStatus);
                            }
                        }).always(function () {
                            $(""#multipleTaskCoacherBtn"").button('reset');
                        });
                    //$.getScript('/assets/javascript/articleForm.js', function () {
                    //    ArticleForm.init(postdata);
                    //    $(""#articleForm"")[0].reset();
                    //});
                    e.preventDefault(e);
                }
            });
            $(""#multipleTaskCoacherBtn"").trigger('click');
            $('.adminWeight').each(function (i, d) {
                $(this).rules(""add"", {
                    required: true,
                    messages: {
                        required: ""پرکردن این فیلد الز");
            WriteLiteral(@"امی می باشد"",
                    }
                });
                $(this).rules(""add"", {
                    number: true,
                    messages: {
                        number: ""لطفا عدد وارد نمایید"",
                    }
                });
                $(this).rules(""add"", {
                    range: [1, 100],
                    messages: {
                        range: ""لطفا عددی بین 1 تا 100 وارد نمایید"",
                    }
                });
            });
            $(""#multipleTaskCoacherBtn"").trigger('click');
        } else {
            alert(""وظایفی که بیشتر از 1 مربی دارند را انتخاب نمایید"")
        }

    }
</script>
");
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