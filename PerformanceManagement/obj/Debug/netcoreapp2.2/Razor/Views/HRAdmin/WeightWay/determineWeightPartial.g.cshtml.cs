#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\WeightWay\determineWeightPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "21d4a722b658fdf1b66822551e1529f8eb21d975"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HRAdmin_WeightWay_determineWeightPartial), @"mvc.1.0.view", @"/Views/HRAdmin/WeightWay/determineWeightPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/HRAdmin/WeightWay/determineWeightPartial.cshtml", typeof(AspNetCore.Views_HRAdmin_WeightWay_determineWeightPartial))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"21d4a722b658fdf1b66822551e1529f8eb21d975", @"/Views/HRAdmin/WeightWay/determineWeightPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_HRAdmin_WeightWay_determineWeightPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("determineWeightForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\WeightWay\determineWeightPartial.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(27, 613, true);
            WriteLiteral(@"
<div class=""modal fade"" id=""determineWeight"" tabindex="""" role=""basic""
     aria-hidden=""true"">
    <div class=""modal-dialog "">
        <div class=""modal-content"">
            <div class=""modal-header bg-blue"">
                <button type=""button"" id=""modal-close"" class=""close""
                        data-dismiss=""modal"" aria-hidden=""true""></button>
                <h4 id=""sabt_hazine_personnel"" class=""modal-title"">
                    <i class=""fa fa-file-o""></i>
                    تعیین شیوه وزن دهی نمره عملکرد و وظایف و شایستگی رفتاری
                </h4>
            </div>
            ");
            EndContext();
            BeginContext(640, 4864, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "21d4a722b658fdf1b66822551e1529f8eb21d9756634", async() => {
                BeginContext(709, 4788, true);
                WriteLiteral(@"
                <div class=""modal-body"">
                    <div class=""form-body"">

                        <div id=""alert_danger"" class=""alert alert-danger display-hide"">
                            <button class=""close"" data-close=""alert""></button>
                            پر کردن فيلدهاي ستاره دار اجباري مي باشد.
                        </div>

                        <div class=""row"">
                            <div class=""col-md-6"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-5"">
                                        روش وزن دهی شایستگی رفتاری
                                        <span class=""required"">
                                            *
                                        </span>
                                    </label>
                                    <div class=""col-md-7"">
                                        <div class=""input-icon right"">
                       ");
                WriteLiteral(@"                     <i class=""fa""></i>
                                            <select name=""competencyWeight"" id=""competencyWeight"" class=""form-control""></select>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class=""col-md-6"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-5"">
                                        روش وزن دهی وظایف
                                        <span class=""required"">
                                            *
                                        </span>
                                    </label>
                                    <div class=""col-md-7"">
                                        <div class=""input-icon right"">
                                            <i class=""fa""></i>
                                   ");
                WriteLiteral(@"         <select name=""taskWeight"" id=""taskWeight"" class=""form-control""></select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-md-6"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-5"">
                                        روش وزن دهی نمره عملکرد
                                        <span class=""required"">
                                            *
                                        </span>
                                    </label>
                                    <div class=""col-md-7"">
                                        <div class=""input-icon right"">
                                            <i class=""fa""></i>
                                            ");
                WriteLiteral(@"<select name=""scoreWeight"" id=""scoreWeight"" class=""form-control""></select>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class=""col-md-6"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-5"">
                                        انتخاب دوره
                                        <span class=""required"">
                                            *
                                        </span>
                                    </label>
                                    <div class=""col-md-7"">
                                        <div class=""input-icon right"">
                                            <i class=""fa""></i>
                                            <select name=""periodDefinitionId"" id=""periodDefinitionId"" class=""form-control""></select>
  ");
                WriteLiteral(@"                                      </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <!--/row-->
                    </div>
                    <!-- END FORM-->


                </div>
                <div class=""modal-footer"">
                    <button type=""submit"" class=""btn green input-sm input-small"" id=""determineWeightSaveBtn"">ذخيره</button>
                    <button type=""button"" class=""btn red input-sm input-small"" id=""cancel"" data-dismiss=""modal""><i class=""fa fa-times""></i>انصراف</button>
                </div>
            ");
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
            BeginContext(5504, 9463, true);
            WriteLiteral(@"
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>

<script>
    $(document).ready(function () {

        $('#determineWeight').modal('show');
        $.ajax({
            type: 'get',
            url: '/WeightWay/GetCompetencyLikert',
            //data: { roleId: roleId },
            success: function (competencyLikert) {
                $(""#competencyWeight"").empty();
                $(""#competencyWeight"").append(""<option></option>"");
                $(""#competencyWeight"").append(""<option value='1000001'>درصدی</option>"");

                $(competencyLikert).each(function (i, c) {
                    //$(""#parentName"").select2(""val"", d.departmentId);
                    $(""#competencyWeight"").append(""<option value='"" + c.likertScaleId + ""'>"" + c.name + ""</option>"");
                })
            },
            error: function (status) {
                alert(""Error"");
            }
        })

        $.ajax({
            type: 'g");
            WriteLiteral(@"et',
            url: '/WeightWay/GetTaskLikert',
            //data: { roleId: roleId },
            success: function (competencyLikert) {
                $(""#taskWeight"").empty();
                $(""#taskWeight"").append(""<option></option>"");
                $(""#taskWeight"").append(""<option value='1000001'>درصدی</option>"");

                $(competencyLikert).each(function (i, c) {
                    //$(""#parentName"").select2(""val"", d.departmentId);
                    $(""#taskWeight"").append(""<option value='"" + c.likertScaleId + ""'>"" + c.name + ""</option>"");
                })
            },
            error: function (status) {
                alert(""Error"");
            }
        })

        $.ajax({
            type: 'get',
            url: '/WeightWay/GetScoreLikert',
            //data: { roleId: roleId },
            success: function (competencyLikert) {
                $(""#scoreWeight"").empty();
                $(""#scoreWeight"").append(""<option></option>"");
            ");
            WriteLiteral(@"    $(""#scoreWeight"").append(""<option value='1000001'>درصدی</option>"");
                $(competencyLikert).each(function (i, c) {
                    //$(""#parentName"").select2(""val"", d.departmentId);
                    $(""#scoreWeight"").append(""<option value='"" + c.likertScaleId + ""'>"" + c.name + ""</option>"");
                })
            },
            error: function (status) {
                alert(""Error"");
            }
        })
        $.ajax({
            type: 'get',
            url: '/WeightWay/GetPeriodDefinition',
            //data: { roleId: roleId },
            success: function (periodDefinition) {
                $(""#periodDefinitionId"").empty();
                $(""#periodDefinitionId"").append(""<option></option>"");

                $(periodDefinition).each(function (i, p) {
                    //$(""#parentName"").select2(""val"", d.departmentId);
                    $(""#periodDefinitionId"").append(""<option value='"" + p.periodDefinitoionId + ""'>"" + p.periodTitle + "" ("" ");
            WriteLiteral(@"+ p.periodCode + "")</option>"");
                })
            },
            error: function (status) {
                alert(""Error"");
            }
        })
        $(""#determineWeightForm"").submit(function (e) {
            //alert('dpCreationSubmit');
            //var postdata2 = $(this).serializeArray();
            //debugger;
            var form = $('#determineWeightForm');
            form.validate({
                //console.log($('#registerform').serializeArray());
                errorElement: 'span', //default input error message container
                errorClass: 'help-block', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: """",
                rules: {
                    competencyWeight: {
                        required: true
                    },
                    taskWeight: {
                        required: true
                    },
                    scoreWeigh");
            WriteLiteral(@"t: {
                        required: true
                    },
                    periodDefinitionId: {
                        required: true
                    }
                },
                messages: {
                    competencyWeight: {
                        required: ""پر کردن این فیلد الزامی می باشد""
                    },
                    taskWeight: {
                        required: ""پر کردن این فیلد الزامی می باشد""
                    },
                    scoreWeight: {
                        required: ""پر کردن این فیلد الزامی می باشد""
                    },
                    periodDefinitionId: {
                        required: ""پر کردن این فیلد الزامی می باشد""
                    }
                },
                invalidHandler: function (event, validator) { //display error alert on form submit
                    //                    success2.hide();
                    //                    error2.show();
                    //             ");
            WriteLiteral(@"       App.scrollTo(error2, -200);
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
                    $(element).closest('.form-group').removeClass('has-error').addClass('has-success'); // set succes");
            WriteLiteral(@"s class to the control group
                    icon.removeClass(""fa-warning"").addClass(""fa-check"");
                },

                submitHandler: function (form) {
                    //                    success2.show();
                    //                    error2.hide();
                }
            })
            if (form.valid() == false) {
                debugger
                //console.log($('#registerform').serializeArray());
                return false;
            } else {
                //$(""#articleSubmit"").addClass('disabled');
                $(""#determineWeightSaveBtn"").attr(""disabled"", ""disabled"");
                var btn = $(""#determineWeightSaveBtn"");
                btn.button('loading');
                var postdata = new FormData(this);
                //var postdata = $('#dpcreation').serializeArray();

                console.log(postdata);
                $.ajax(
                    {
                        //data:postdata,
                  ");
            WriteLiteral(@"      url: ""/WeightWay/AssignWeightWayToPeriod"",
                        //url : formURL,
                        //data: postdata2,
                        data: postdata,
                        //data: ""firstName="" + $(""fn"").val(),
                        cache: false,
                        contentType: false,
                        processData: false,
                        type: ""post"",

                        success: function (data, textStatus, jqXHR) {
                            //alert(data);
                            //if ($.trim(data) === ""yess"")
                            //alert(""That's one I wanted. Excellent"")
                            //alert(data);
                            var message = """"
                            if (data > 0) {
                                message += ""<span>اطلاعات  مورد نظر با موفقیت ثبت گردید</span><br><br>"";
                            }
                            else if (data == 0) {
                                message += ""<");
            WriteLiteral(@"span>اطلاعات  مورد نظر ثبت نگردید..</span><br><br>"";
                            }
                            toastr.options.timeOut = ""15000"";
                            toastr.options.positionClass = ""toast-top-center"";
                            toastr.success(message);
                            $(""#determineWeightForm"")[0].reset();
                            
                            $(""#determineWeight"").modal(""hide"");
                            $(""#weightWayTable"").DataTable().destroy();
                            show_dataTable();
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            alert(""erorr00000"");
                            alert(jqXHR);
                            alert(textStatus);
                        }
                    }).always(function () {
                        $(""#determineWeightSaveBtn"").button('reset');
                    });
                //$.getScript('/assets/javascr");
            WriteLiteral("ipt/articleForm.js\', function () {\r\n                //    ArticleForm.init(postdata);\r\n                //    $(\"#articleForm\")[0].reset();\r\n                //});\r\n                e.preventDefault(e);\r\n            }\r\n        });\r\n    });\r\n</script>");
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
