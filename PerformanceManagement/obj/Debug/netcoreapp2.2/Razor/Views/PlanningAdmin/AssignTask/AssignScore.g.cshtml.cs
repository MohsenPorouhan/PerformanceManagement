#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\PlanningAdmin\AssignTask\AssignScore.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bf27c6b48b3c3e0739f09172e6aea894c814ad63"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PlanningAdmin_AssignTask_AssignScore), @"mvc.1.0.view", @"/Views/PlanningAdmin/AssignTask/AssignScore.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/PlanningAdmin/AssignTask/AssignScore.cshtml", typeof(AspNetCore.Views_PlanningAdmin_AssignTask_AssignScore))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bf27c6b48b3c3e0739f09172e6aea894c814ad63", @"/Views/PlanningAdmin/AssignTask/AssignScore.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_PlanningAdmin_AssignTask_AssignScore : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("scoreAssignmentFrm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\PlanningAdmin\AssignTask\AssignScore.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(27, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(29, 2063, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bf27c6b48b3c3e0739f09172e6aea894c814ad635956", async() => {
                BeginContext(97, 1988, true);
                WriteLiteral(@"
    <div class=""modal fade department modalClass"" id=""scoreAssignmentModal"" tabindex="""" aria-hidden=""true"">
        <div class=""modal-dialog modal-wide"">
            <div class=""modal-content"">
                <div class=""modal-header bg-blue"">
                    <button type=""button"" id=""modal-close"" class=""close""
                            data-dismiss=""modal"" aria-hidden=""true""></button>
                    <h4 class=""modal-title"">
                        <i class=""fa fa-file-o""></i>
                        ثبت نمره عملکردی
                    </h4>
                </div>

                <div class=""modal-body"">
                    <div class=""form-body"">

                        <div id=""alert_danger"" class=""alert alert-danger display-hide"">
                            <button class=""close"" data-close=""alert""></button>
                            پر کردن فيلدهاي ستاره دار اجباري مي باشد.
                        </div>

                        <div class=""alert alert-block alert-in");
                WriteLiteral(@"fo fade in"">
                            <button type=""button"" class=""close"" data-dismiss=""alert""></button>
                            <h4 class=""alert-heading"">روش نمره دهی</h4>
                            <p id=""weightWayInfo"">

                            </p>
                        </div>

                        <div id=""taskScoreRow""></div>


                    </div>
                    <!-- END FORM-->
                </div>

                <div class=""modal-footer"">
                    <button type=""submit"" class=""btn green input-sm input-small"" id=""assignScoreBTN"">ذخيره</button>
                    <button type=""button"" class=""btn red input-sm input-small"" id=""cancel11"" data-dismiss=""modal""><i class=""fa fa-times""></i>انصراف</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div id=""critriaScorePlaceHolder""></div>
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
            BeginContext(2092, 99, true);
            WriteLiteral("\r\n<script type=\"text/html\" id=\"appendTaskForScore\">\r\n    <fieldset>\r\n        <legend>{1}</legend>\r\n");
            EndContext();
            BeginContext(3623, 21531, true);
            WriteLiteral(@"        <div class=""row taskItem"">
            <!--/span-->
            <div class=""col-md-4"">
                <div class=""form-group"">
                    <label class=""control-label col-md-6"">
                        {1}
                        <span class=""required"">
                            *
                        </span>
                    </label>
                    <div class=""col-md-6"">
                        <div class=""input-icon right"">
                            <i class=""fa""></i>
                            <input type=""text"" value=""{5}"" name=""taskWeight{0}"" id=""taskWeight{0}"" taskId=""{2}"" evaluationId=""{3}"" periodDefinitionId=""{4}"" class=""form-control taskWeight input-small input-sm"" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class=""row criteriaItems"" id=""appendCriteriaScoreHolder{0}"" index=""{0}"" evaluationId=""{3}"" taskId=""{2}"" loadCriteria=""false""></div>
    </fieldset>
</script");
            WriteLiteral(@">
<script type=""text/html"" id=""appendCriteriaScoreRow"">
    <div class=""col-md-4"">
        <div class=""form-group"">
            <label class=""control-label col-md-6"">
                {1}
                <span class=""required"">
                    *
                </span>
            </label>
            <div class=""col-md-6"">
                <div class=""input-icon right"">
                    <i class=""fa""></i>
                    <input type=""text"" value=""{3}"" name=""criteriaWeight{0}"" id=""criteriaWeight{0}"" criteriaId=""{2}"" class=""form-control criteriaWeight input-small input-sm"" />
                </div>
            </div>
        </div>
    </div>
</script>
<script>
    //  $(document).ready(function () {
    var i = 1;
    var index;
    var j = 1;
    var index2;
    var template = jQuery.validator.format($.trim($(""#appendTaskForScore"").html()));

    var template3 = jQuery.validator.format($.trim($(""#appendCriteriaScoreRow"").html()));
    var taskWeightIndex = 0;
    var tas");
            WriteLiteral(@"kWeight0Value;
    var isTrue = false;
    var row;
    var allocatorId;
    var evalScore;
    var criteriaScore;

    $.ajax({
        type: 'get',
        url: '/Share/GetCurrentUserInfo',
        async: false,
        success: function (data) {
            allocatorId = data;
        },
        error: function (status) {
            alert(""Error"");
        }
    })
    function GetEvaluationScore(evaluationId) {
        $.ajax({
            type: 'get',
            url: '/AssignTask/GetEvaluationScore',
            async: false,
            data: { evaluationId: evaluationId },
            success: function (data) {
                //console.log(data)
                if (data != null) {
                    evalScore = data.score;
                } else {
                    evalScore = null;
                }
            },
            error: function (status) {
                alert(""Error"");
            }
        })
    }
    var length = dt.rows('.selected').data().");
            WriteLiteral(@"length;
    var selector;
    if (length > 0) {
        selector = ""#taskAssignmentTable >tbody>tr.selected"";
    } else {
        selector = ""#taskAssignmentTable >tbody>tr"";
    }
    $(selector).each(function (i, tr) {
        row = dt.row(tr);
        var viewScoreParam = {};

        viewScoreParam.TaskId = row.data().TaskId;
        viewScoreParam.allocatorRoleName = row.data().allocatorRoleName;
        viewScoreParam.EvaluationId = row.data().EvaluationId;
        viewScoreParam.RecieverAllocationEvaluationHierarchyId = row.data().RecieverAllocationEvaluationHierarchyId;
        viewScoreParam.RecieverAllocationPersonId = row.data().RecieverAllocationPersonId;
        viewScoreParam.index = i;
        //console.log(viewScoreParam);
        //row.child(row.data().fullName).show()
        //alert(row.data().fullName)
        GetEvaluationScore(row.data().EvaluationId);
        index = taskWeightIndex++;
        $(template(index, row.data().Title, row.data().TaskId, row.data().Evalu");
            WriteLiteral(@"ationId, row.data().PeriodDefinitoionId, ((evalScore == null || evalScore == '-1') ? '' : evalScore), JSON.stringify(viewScoreParam))).appendTo(""#taskScoreRow"");
        //$(template2(index, row.data().TaskId, row.data().Title)).appendTo(""#critriaScorePlaceHolder"");
        $.ajax({
            type: 'get',
            url: '/Share/CriteiaCount',
            data: { taskId: row.data().TaskId },
            async: false,
            success: function (data) {
                if (data >= 1) {
                    if (index == 0) {
                        isTrue = true;
                        taskWeight0Value = ((evalScore == null || evalScore == '-1') ? '' : evalScore);
                    } else {
                        $(""#taskWeight"" + index).attr(""disabled"", ""disabled"");
                        $(""#taskWeight"" + index).closest('div.row').addClass(""hidden"");
                    }
                } else if (data == 0) {
                    $(""#criteriaScoreBtn"" + index).addClass(""disabled"");");
            WriteLiteral(@"
                }
            },
            error: function (status) {
                alert(""Error"");
            }
        })
    })
    var weightWay = """";
    var numberScaleList = [];
    $.ajax({
        type: 'get',
        url: '/Share/GetScoreWeightWay',
        data: { periodDefinitionId: row.data().PeriodDefinitoionId },
        async: false,
        success: function (data) {
            weightWay = data;
            if (data == 1) {
                $(""#weightWayInfo"").html(""نمره دهی به صورت درصدی و بازه مجاز از 1 تا 100 می باشد."");
            } else if (data == 2) {
                $.ajax({
                    type: 'get',
                    url: '/Share/GetScoreLikertScale',
                    data: { periodDefinitionId: row.data().PeriodDefinitoionId },
                    async: false,
                    success: function (data) {
                        $(""#weightWayInfo"").html(""نمره دهی به صورت طیف لیکرت می باشد. و بازه های مجاز جهت نمره دهی "");
             ");
            WriteLiteral(@"           $(data).each(function (i, d) {
                            numberScaleList.push(d.numberScale);
                            $(""#weightWayInfo"").append(d.numberScale + "":"" + d.titleScale + ""  "");
                        });
                    },
                    error: function (status) {
                        alert(""Error"");
                    }
                })
            }
        },
        error: function (status) {
            alert(""Error"");
        }
    })


    $('.modalClass .select2').select2({
        placeholder: ""انتخاب کنید"",
        allowClear: true
    });
    function GetCriteriaWeightScore(criteriaWeightId) {
        $.ajax({
            type: 'get',
            url: '/AssignTask/GetCriteriaWeightScore',
            async: false,
            data: { criteriaWeightId: criteriaWeightId },
            success: function (data) {
                if (data != null) {
                    criteriaScore = data.score;
                } else {
       ");
            WriteLiteral(@"             criteriaScore = null;
                }
            },
            error: function (status) {
                alert(""Error"");
            }
        })
    }


    $(""#assignScoreBTN"").click(function () {
        //alert('dpCreationSubmit');
        //var postdata2 = $(this).serializeArray();
        var form = $(""#scoreAssignmentFrm"");
        form.validate({
            //console.log($('#registerform').serializeArray());
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: """",
            rules: {
                taskWeight0: {
                    required: true,
                }
            },
            messages: {
                taskWeight0: {
                    required: ""پر کردن این فیلد الزامی می باشد""
                }
            },
            invalidHandler: function ");
            WriteLiteral(@"(event, validator) { //display error alert on form submit
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

            },

            success: function (label, element) {
                var icon = $(element).parent('.in");
            WriteLiteral(@"put-icon').children('i');
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
            var listOfEvaluations = [];
            var listOfCriteria = [];
            var listOfScore = [];
            var listOfCriteriaScore = [];
            $(""#taskScoreRow fieldset .taskItem input[type=text]"").each(function () {
                var evaluation = {};
                var score = {}
                evaluation.TaskId = $(this).attr(""taskId"");

                if ($(this).attr(""disabled"") == ""disable");
            WriteLiteral(@"d"") {
                    score.score = -1;
                }
                else {
                    score.score = $(this).val();
                }
                listOfScore.push(score);
                evaluation.EvaluationId = $(this).attr(""evaluationId"");
                evaluation.PeriodDefinitoionId = $(this).attr(""periodDefinitionId"");
                evaluation.EvaluationScores = listOfScore;
                listOfScore = [];
                //$(""#critriaScorePlaceHolder"").find(""div[id^='criteriaModal'],div[taskId='3']"")
                var criteria = $(""#taskScoreRow"").find(""div.criteriaItems[taskId='"" + $(this).attr(""taskId"") + ""'] input"")
                $(criteria).each(function () {
                    var criteria = {};
                    var CriteriaWeightScore = {};
                    criteria.CriteriaId = $(this).attr(""criteriaId"");
                    //criteria.Weight = $(this).val();
                    CriteriaWeightScore.Score = $(this).val();
                  ");
            WriteLiteral(@"  listOfCriteriaScore.push(CriteriaWeightScore);
                    criteria.CriteriaWeightScores = listOfCriteriaScore;
                    listOfCriteriaScore = [];
                    listOfCriteria.push(criteria);
                });
                evaluation.CriteriaWeights = listOfCriteria;
                listOfCriteria = [];
                listOfEvaluations.push(evaluation);
            });
            //$(""#articleSubmit"").addClass('disabled');
            // $(""#dpCreationSubmit"").attr(""disabled"", ""disabled"");
            console.log(listOfEvaluations)
            var btn = $(""#assignScoreBTN"");
            btn.button('loading');
            //var postdata = new FormData(this);
            //var postdata = $('#scoreAssignmentFrm').serializeArray();

            //console.log(postdata);
            $.ajax(
                {
                    datatype: ""json"",
                    //data:postdata,
                    url: ""/AssignTask/AssignScore"",
                    //url ");
            WriteLiteral(@": formURL,
                    //                        data: postdata2,
                    data: JSON.stringify(listOfEvaluations),
                    //data: ""firstName="" + $(""fn"").val(),
                    //cache: false,
                    contentType: 'application/json; charset=utf-8',
                    //contentType: false,
                    //processData: false,
                    type: ""POST"",

                    success: function (data, textStatus, jqXHR) {
                        //if ($.trim(data) === ""yess"")
                        //i = 1;
                        //j = 1;
                        if (data.result > 0) {
                            //$(""#dpcreation"")[0].reset();
                            $(""#scoreAssignmentModal"").modal(""hide"");
                            dt.ajax.url(""/AssignTask/GetTaskAssignmentList"");
                            dt.ajax.reload();
                            //resetFormValidation();
                            toastr.options.timeO");
            WriteLiteral(@"ut = ""15000"";
                            toastr.options.positionClass = ""toast-top-center"";
                            toastr.success(""اطلاعات مورد نظر با موفقیت ثبت گردید."");
                        } else if (data.result == 0) {
                            toastr.options.timeOut = ""15000"";
                            toastr.options.positionClass = ""toast-top-center"";
                            toastr.error(""اطلاعات مورد نظر ثبت نگردید."");
                        }
                        else {
                            toastr.options.timeOut = ""15000"";
                            toastr.options.positionClass = ""toast-top-center"";
                            toastr.info(data.result);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert(""erorr"");
                        alert(jqXHR);
                        alert(textStatus);
                    }
                }).always(function () {
 ");
            WriteLiteral(@"                   $(""#assignScoreBTN"").button('reset');
                });
            //$.getScript('/assets/javascript/articleForm.js', function () {
            //    ArticleForm.init(postdata);
            //    $(""#articleForm"")[0].reset();
            //});
            // e.preventDefault(e);
        }
    });
    //Begin of validation of form
    $(""#assignScoreBTN"").trigger('click');

    $('.taskWeight').each(function (i, d) {
        $(this).rules(""add"", {
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
        if (weightWay == 1) {
            $(d).rules(""add"", {
                range: [1, 100],
                messages: {
                    range: ""لطفا عددی بین 1 تا 100 وارد نمایید"",
                }
            });
    ");
            WriteLiteral(@"    } else if (weightWay == 2) {
            $(this).rules(""add"", {
                range: [Math.min.apply(Math, numberScaleList), Math.max.apply(Math, numberScaleList)],
                messages: {
                    range: ""لطفا عددی بین "" + Math.min.apply(Math, numberScaleList) + "" تا "" + Math.max.apply(Math, numberScaleList) + "" وارد نمایید"",
                }
            });
        }
    });

    $("".criteriaItems"").each(function (ii, dd) {
        var index = $(this).attr(""index"");
        var selector = $(this);
        //$(""#criteriaModal"" + index).modal('show');
        if ($(this).attr(""loadCriteria"") == ""false"") {
            $.ajax({
                type: 'get',
                url: '/TaskAssignment/GetCriteriaDetails2',
                data: { taskId: $(this).attr(""taskId""), evaluationId: $(this).attr(""evaluationId"") },
                async: false,
                success: function (data) {
                    $(data).each(function (i, d) {
                        index2 ");
            WriteLiteral(@"= j++;
                        GetCriteriaWeightScore(d.CriteriaWeightId);
                        $(template3(index2, d.Title, d.CriteriaId, criteriaScore == null ? '' : criteriaScore)).appendTo(""#appendCriteriaScoreHolder"" + index);
                        //console.log($(""#criteriaWeight"" + index2));
                        $(""#criteriaWeight"" + index2).rules(""add"", {
                            required: true,
                            messages: {
                                required: ""پرکردن این فیلد الزامی می باشد"",
                            }
                        });
                        $(""#criteriaWeight"" + index2).rules(""add"", {
                            number: true,
                            messages: {
                                number: ""لطفا عدد وارد نمایید"",
                            }
                        });

                        if (weightWay == 1) {
                            //console.log(weightWay);
                            $(""#criteri");
            WriteLiteral(@"aWeight"" + index2).rules(""add"", {
                                range: [1, 100],
                                messages: {
                                    range: ""لطفا عددی بین 1 تا 100 وارد نمایید"",
                                }
                            });
                        } else if (weightWay == 2) {
                            $(""#criteriaWeight"" + index2).rules(""add"", {
                                range: [Math.min.apply(Math, numberScaleList), Math.max.apply(Math, numberScaleList)],
                                messages: {
                                    range: ""لطفا عددی بین "" + Math.min.apply(Math, numberScaleList) + "" تا "" + Math.max.apply(Math, numberScaleList) + "" وارد نمایید"",
                                }
                            });
                        }
                        //$('.criteriaWeight').each(function (i, d) {
                        //    $(this).rules(""add"", {
                        //        required: true,
            ");
            WriteLiteral(@"            //        messages: {
                        //            required: ""پرکردن این فیلد الزامی می باشد"",
                        //        }
                        //    });
                        //});
                    })
                    $(selector).attr(""loadCriteria"", ""true"");
                },
                error: function (status) {
                    alert(""Error"");
                }
            })
        }

    });
    $(""#assignScoreBTN"").trigger('click');
    if (isTrue) {
        $(""#taskWeight0"").val(taskWeight0Value);
        $(""#taskWeight0"").attr(""disabled"", ""disabled"");
        $(""#taskWeight0"").closest('div.row').addClass(""hidden"");
        $(""#taskWeight0"").prev().remove();
    }
    //End of validation of form
    //});
    $('#scoreAssignmentModal').modal('show');

    //$('.viewScore').one('click', function () {
    //    var data = JSON.parse($(this).attr('viewScoreParam'));

    //    var ScoreParameterView = {};
    //    ScoreParam");
            WriteLiteral(@"eterView.TaskId = data.TaskId;
    //    ScoreParameterView.allocatorRoleName = data.allocatorRoleName;
    //    ScoreParameterView.EvaluationId = data.EvaluationId;
    //    ScoreParameterView.RecieverAllocationEvaluationHierarchyId = data.RecieverAllocationEvaluationHierarchyId;
    //    ScoreParameterView.RecieverAllocationPersonId = data.RecieverAllocationPersonId;
    //    ScoreParameterView.index = data.index;

    //    var collapseId = $(this).attr('data-target');
    //    $.ajax(
    //        {
    //            datatype: ""json"",
    //            url: ""/TaskAssignment/ViewScore2"",
    //            data: JSON.stringify(ScoreParameterView),
    //            contentType: 'application/json; charset=utf-8',
    //            type: ""POST"",
    //            success: function (data, textStatus, jqXHR) {

    //                $(collapseId).html(data);
    //            },
    //            error: function (jqXHR, textStatus, errorThrown) {
    //                alert(""erorr"");
");
            WriteLiteral(@"    //                alert(jqXHR);
    //                alert(textStatus);
    //            }
    //        });
    //})

    //$('.viewSensibleEvent').one('click', function () {
    //    var collapseId = $(this).attr('data-target');
    //    $.ajax(
    //        {
    //            data:
    //            {
    //                evaluationId: $(this).attr('evaluationId'),
    //                index: $(this).attr('index')
    //            },
    //            url: ""/TaskAssignment/SensibleEventView"",
    //            contentType: 'aplication/json;charset=utf-8',
    //            type: ""GET"",
    //            datatype: 'html',

    //            success: function (data, textStatus, jqXHR) {

    //                $(collapseId).html(data);
    //            },
    //            error: function (jqXHR, textStatus, errorThrown) {
    //                alert(""erorr"");
    //                alert(jqXHR);
    //                alert(textStatus);
    //            }
    //     ");
            WriteLiteral("   });\r\n    //})\r\n</script>");
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
