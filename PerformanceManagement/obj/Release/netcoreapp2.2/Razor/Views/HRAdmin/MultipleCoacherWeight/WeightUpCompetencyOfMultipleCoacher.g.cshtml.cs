#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\MultipleCoacherWeight\WeightUpCompetencyOfMultipleCoacher.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "40af222bcaefc35ddd3fa57b392c11c3cab2580a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HRAdmin_MultipleCoacherWeight_WeightUpCompetencyOfMultipleCoacher), @"mvc.1.0.view", @"/Views/HRAdmin/MultipleCoacherWeight/WeightUpCompetencyOfMultipleCoacher.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/HRAdmin/MultipleCoacherWeight/WeightUpCompetencyOfMultipleCoacher.cshtml", typeof(AspNetCore.Views_HRAdmin_MultipleCoacherWeight_WeightUpCompetencyOfMultipleCoacher))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"40af222bcaefc35ddd3fa57b392c11c3cab2580a", @"/Views/HRAdmin/MultipleCoacherWeight/WeightUpCompetencyOfMultipleCoacher.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_HRAdmin_MultipleCoacherWeight_WeightUpCompetencyOfMultipleCoacher : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("multipleCompetencyCoacherForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\MultipleCoacherWeight\WeightUpCompetencyOfMultipleCoacher.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(27, 4356, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "40af222bcaefc35ddd3fa57b392c11c3cab2580a6097", async() => {
                BeginContext(106, 4270, true);
                WriteLiteral(@"
    <div class=""modal fade"" id=""multipleCompetencyCoacherModal"" aria-hidden=""true"">
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
                        <div id=""coacherCompetencyPlaceHolder"">
                            <div class=""row"">
    ");
                WriteLiteral(@"                            <!--/span-->
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
                WriteLiteral(@"                                    واحد سازمانی کارمند:
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
                WriteLiteral(@"                           <div class=""form-group"">
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
                    <button type=""submit"" class=""btn green input-sm input-small"" id=""multipleCompetencyCoacherBtn"">ذخيره</button>
                    <button type=""button"" class=""btn red input-sm input-small"" id=""cancel"" data-dismiss=""modal""><i class=""fa fa");
                WriteLiteral("-times\"></i>انصراف</button>\r\n                </div>\r\n\r\n            </div>\r\n            <!-- /.modal-content -->\r\n        </div>\r\n        <!-- /.modal-dialog -->\r\n    </div>\r\n");
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
            BeginContext(4383, 13154, true);
            WriteLiteral(@"
<script type=""text/html"" id=""appendCompetencyCoacher"">
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
            <div class=""form-grou");
            WriteLiteral(@"p"">
                <label class=""control-label col-md-12"">
                    {4}
                </label>
            </div>
        </div>
        <div class=""col-md-2"">
            <div class=""form-group"">
                <div class=""col-md-12"">
                    <div class=""input-icon right"">
                        <i class=""fa""></i>
                        <input name=""adminWeight{0}"" finalScoreCompetencyCalculationId=""{1}"" employeeId=""{8}"" periodDefinitionId=""{7}"" type=""text"" class=""form-control input-sm input-small adminWeight"" value="""">
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
    } else ");
            WriteLiteral(@"{
        var row = dt.row(selector);
        if (row.data().CompetencyCoacherNumber > 1) {
            $('#multipleCompetencyCoacherModal').modal('show');

            var template = jQuery.validator.format($.trim($(""#appendCompetencyCoacher"").html()));
            $.ajax({
                type: 'get',
                url: '/MultipleCoacherWeight/GetCompetencyCoacherList',
                data: {
                    periodDefinitionId: row.data().PeriodDefinitoionId,
                    employeeId: row.data().RecieverAllocationPersonId
                },
                async: false,
                success: function (data) {
                    $('#modalTitle').text(""تعیین وزن نمرات نهایی شایستگی های مربی های آقای "" + row.data().FullName)
                    $(data).each(function (i, d) {
                        switch (d.RoleName) {
                            case 'PlanningAdmin': roleName = 'ادمین برنامه ریزی';
                                break;
                            default");
            WriteLiteral(@":
                        }
                        if (d.FinalScore == null) {
                            finalScore = 'فاقد ارزیابی';
                        } else {
                            finalScore = d.FinalScore;
                        }
                        if (d.AllocatorEvaluationHierarchyId != null) {
                            $(template(i, d.FinalScoreCompetencyCalculationId, d.CoacherDepartmentName, d.CoacherFullName, finalScore, d.AllocatorLevel, d.EmployeeDepartmentName, d.PeriodDefinitoionId, d.RecieverAllocationPersonId)).appendTo(""#coacherCompetencyPlaceHolder"");
                        } else {
                            $(template(i, d.FinalScoreCompetencyCalculationId, roleName, roleName, finalScore, '---', d.EmployeeDepartmentName, d.PeriodDefinitoionId, d.RecieverAllocationPersonId)).appendTo(""#coacherCompetencyPlaceHolder"");
                        }
                    })
                },
                error: function (status) {
                    alert");
            WriteLiteral(@"(""Error"");
                }
            })
            $(""#multipleCompetencyCoacherForm"").submit(function (e) {
                //alert('dpCreationSubmit');
                //var postdata2 = $(this).serializeArray();
                //debugger;
                var form = $('#multipleCompetencyCoacherForm');
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
     ");
            WriteLiteral(@"                   }
                    },
                    invalidHandler: function (event, validator) { //display error alert on form submit
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

           ");
            WriteLiteral(@"         unhighlight: function (element) { // revert the change done by hightlight

                    },

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
                    $(""#coacherCompetencyPlaceHolder in");
            WriteLiteral(@"put[type=text]"").each(function () {
                        var MultipleTaskCoacherView = {};
                        MultipleTaskCoacherView.FinalScoreCompetencyCalculationId = $(this).attr(""finalScoreCompetencyCalculationId"");
                        MultipleTaskCoacherView.EmployeeId = $(this).attr(""employeeId"");
                        MultipleTaskCoacherView.PeriodDefinitoionId = $(this).attr(""periodDefinitionId"");
                        MultipleTaskCoacherView.CompetencyWeight = $(this).val();

                        listOfMultipleTaskCoacher.push(MultipleTaskCoacherView);
                    });

                    //$(""#articleSubmit"").addClass('disabled');
                    $(""#multipleCompetencyCoacherBtn"").attr(""disabled"", ""disabled"");
                    var btn = $(""#multipleCompetencyCoacherBtn"");
                    btn.button('loading');
                    ///var postdata = new FormData(this);
                    //var postdata = $('#dpcreation').serializeArray();

     ");
            WriteLiteral(@"               console.log(listOfMultipleTaskCoacher);
                    $.ajax(
                        {
                            datatype: ""json"",
                            //data:postdata,
                            url: ""/MultipleCoacherWeight/WeightUpCompetencyOfMultipleCoacher"",
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
          ");
            WriteLiteral(@"                      var overSummation = """";
                                if (data.result > 0) {
                                    message += ""<span>اطلاعات  مورد نظر با موفقیت ثبت گردید</span><br><br>"";
                                    toastr.options.timeOut = ""15000"";
                                    toastr.options.positionClass = ""toast-top-center"";
                                    toastr.success(message);
                                    $(""#multipleCompetencyCoacherForm"")[0].reset();
                                    $(""#multipleCompetencyCoacherModal"").modal(""hide"");
                                    dt.ajax.url(""/MultipleCoacherWeight/GetMultipleCoacherWeightList"");
                                    dt.ajax.reload();
                                }
                                else if (data.result == 0) {
                                    message += ""اطلاعات  مورد نظر ثبت نگردید."";
                                    toastr.options.timeOut = ""15000"";
       ");
            WriteLiteral(@"                             toastr.options.positionClass = ""toast-top-center"";
                                    toastr.info(message);
                                }
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
                              ");
            WriteLiteral(@"  }

                                //$(""#periodDefinitionTable"").DataTable().destroy();
                                //show_dataTable();
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                alert(""erorr00000"");
                                alert(jqXHR);
                                alert(textStatus);
                            }
                        }).always(function () {
                            $(""#multipleCompetencyCoacherBtn"").button('reset');
                        });
                    //$.getScript('/assets/javascript/articleForm.js', function () {
                    //    ArticleForm.init(postdata);
                    //    $(""#articleForm"")[0].reset();
                    //});
                    e.preventDefault(e);
                }
            });
            $(""#multipleCompetencyCoacherBtn"").trigger('click');
            $('.adminWeight').each(function (i, d) {
");
            WriteLiteral(@"                $(this).rules(""add"", {
                    required: true,
                    messages: {
                        required: ""پرکردن این فیلد الزامی می باشد"",
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
            $(""#multipleCompetencyCoacherBtn"").trigger('click');
        } else {
            alert(""شایستگی هایی که بیشتر از 1 مربی دارند را انتخاب نمایید"")
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