#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\Coacher\Calculation\Calculation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dd4a23b42667dc29c0637d90f242ec40da8156fb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Coacher_Calculation_Calculation), @"mvc.1.0.view", @"/Views/Coacher/Calculation/Calculation.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Coacher/Calculation/Calculation.cshtml", typeof(AspNetCore.Views_Coacher_Calculation_Calculation))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dd4a23b42667dc29c0637d90f242ec40da8156fb", @"/Views/Coacher/Calculation/Calculation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_Coacher_Calculation_Calculation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("calculationForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\Coacher\Calculation\Calculation.cshtml"
  
    ViewData["Title"] = "محاسبه";

#line default
#line hidden
            DefineSection("Styles", async() => {
                BeginContext(58, 2, true);
                WriteLiteral("\r\n");
                EndContext();
                BeginContext(179, 106, true);
                WriteLiteral("    <link rel=\"stylesheet\" type=\"text/css\" href=\"/assets/plugins/bootstrap-toastr/toastr-rtl.min.css\" />\r\n");
                EndContext();
                BeginContext(383, 109, true);
                WriteLiteral("    <link rel=\"stylesheet\" type=\"text/css\" href=\"/assets/plugins/select2v4/select2/dist/css/select2.css\" />\r\n");
                EndContext();
                BeginContext(858, 1986, true);
                WriteLiteral(@"    <link rel=""stylesheet"" href=""/assets/plugins/jQuery-tagEditor-master/jquery.tag-editor.css"" />

    <link rel=""stylesheet"" href=""/assets/advancedDataTable/jquery.dataTables.css"" />
    <link rel=""stylesheet"" href=""/assets/advancedDataTable/buttons.dataTables.css"" />
    <style>
        .tag-editor li {
            float: right;
        }

        .input-icon.right i {
            left: auto !important;
        }

        .tag-editor {
            line-height: 27px;
            /*max-width: 300px;*/
            overflow: auto;
        }

        .btn-sm, .btn-xs {
            margin: 2px;
        }

        td.details-control {
            background: url('/assets/img/details_open.png') no-repeat center center;
            cursor: pointer;
        }

        tr.details td.details-control {
            background: url('/assets/img/details_close.png') no-repeat center center;
        }

        .dataTable .details td, .dataTable .details th {
            padding: 8px;
      ");
                WriteLiteral(@"      background: none;
            border: 0;
        }

        .input-DT {
            margin-left: 5px;
        }

        .select2-container .select2-choice > .select2-chosen {
            /*display: block;*/
            overflow: initial;
        }

        #criteria-list {
            z-index: 99999 !important;
        }

        .btn-add, .h3-section {
            margin-right: 10px !important;
        }

        .input-icon.right i {
            z-index: +100;
        }

        .select2-container--open .select2-dropdown {
            z-index: 99999999
        }

        #dataTables_filter_subSet {
            margin-top: 32px;
        }
        /*generated dynamically in textTaskIndirect and because of bug had set width*/
        #participantt2Select21 {
            width: 176px;
        }

        table.dataTable tbody tr.selected td {
            background-color: #B0BED9;
        }
    </style>
");
                EndContext();
            }
            );
            BeginContext(2847, 1135, true);
            WriteLiteral(@"<!-- BEGIN PAGE HEADER-->
<div class=""row"">
    <div class=""col-md-12"">
        <!-- BEGIN PAGE TITLE & BREADCRUMB-->
        <h3 class=""page-title"">
            <small></small>

        </h3>
        <div class=""clearfix""></div>
        <ul class=""page-breadcrumb breadcrumb"">
            <li>
                <i class=""fa fa-home""></i>
                <a href=""/home"">
                    خانه
                </a>
                <i class=""fa fa-angle-left""></i>
            </li>
            <li>
                <a href=""#"">
                    مسیر مورد نظر
                </a>
            </li>
            <li class=""pull-right"">
                <div id=""dashboard-report-range"" class=""dashboard-date-range tooltips"" data-placement=""top"" data-original-title=""Change dashboard date range"">
                    <i class=""fa fa-calendar""></i>
                    <span>
                    </span>
                    <i class=""fa fa-angle-down""></i>
                </div>
            </l");
            WriteLiteral("i>\r\n        </ul>\r\n        <!-- END PAGE TITLE & BREADCRUMB-->\r\n    </div>\r\n</div>\r\n<!-- END PAGE HEADER-->\r\n\r\n");
            EndContext();
            BeginContext(3982, 1758, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dd4a23b42667dc29c0637d90f242ec40da8156fb9945", async() => {
                BeginContext(4047, 1686, true);
                WriteLiteral(@"
    <div class=""modal fade"" id=""calculationModal"" aria-hidden=""true"" data-backdrop=""static"">
        <div class=""modal-dialog "">
            <div class=""modal-content"">
                <div class=""modal-header bg-blue"">
                    <button type=""button"" id=""modal-close"" class=""close""
                            data-dismiss=""modal"" aria-hidden=""true""></button>
                    <h4 class=""modal-title"">
                        <i class=""fa fa-file-o""></i>
                        محاسبه نمرات زیرمجموعه
                    </h4>
                </div>
                <div class=""modal-body"">
                    <!-- BEGIN FORM-->
                    <div class=""form-body"">
                        <div id=""alert_danger"" class=""alert alert-danger display-hide"">
                            <button class=""close"" data-close=""alert""></button>
                            پر کردن فيلدهاي ستاره دار اجباري مي باشد.
                        </div>
                        <b>آیا جهت انجام محاسبا");
                WriteLiteral(@"ت پایانی اطمینان دارید؟</b>
                        <!--/row-->
                    </div>
                    <!-- END FORM-->


                </div>
                <div class=""modal-footer"">
                    <button type=""submit"" class=""btn green input-sm input-small"" id=""performCalculation"">بلی</button>
                    <button type=""button"" class=""btn red input-sm input-small"" id=""cancel"" data-dismiss=""modal""><i class=""fa fa-times""></i>خیر</button>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div id=""critriaPlaceHolder""></div>
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
            BeginContext(5740, 1040, true);
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-md-12"">
        <div class=""portlet blue box"">
            <div class=""portlet-title"">
                <div class=""caption"">
                    <i class=""fa fa-cogs""></i>مشاهده محاسبات ارزیابی
                </div>
                <div class=""tools"">
                    <a href=""javascript:;"" class=""collapse"">
                    </a>
                    <a href=""#portlet-config"" data-toggle=""modal"" class=""config"">
                    </a>
                    <a href=""javascript:;"" id=""reload"" class=""reload"">
                    </a>
                    <a href=""javascript:;"" class=""remove"">
                    </a>
                </div>
            </div>
            <div class=""portlet-body"">

                <a id=""calculationBtn"" class=""btn blue btn-sm pull-left"" data-toggle=""modal""
                   href=""#calculationModal"">محاسبات پایانی</a>

                <a id=""rollBackBtn"" class=""btn blue btn-sm pull-left"" data-toggle=""modal"">عقبگرد");
            WriteLiteral(" محاسبات</a>\r\n\r\n");
            EndContext();
            BeginContext(6909, 1870, true);
            WriteLiteral(@"
                <div class=""clearfix"">
                </div>
                <table class=""table table-striped table-bordered table-hover"" id=""calculationTable"">
                    <thead>
                        <tr>
                            <th>
                                کد دوره
                            </th>
                            <th>
                                نام دوره
                            </th>
                            <th>
                                نام کارمند
                            </th>
                            <th>
                                واحد سازمانی
                            </th>
                            <th>
                                نام مربی
                            </th>
                            <th>
                                واحد سازمانی مربی
                            </th>
                            <th>
                                اصلی/فرعی
                            </th>
     ");
            WriteLiteral(@"                       <th>
                                نمره کل فرد در بعد وظایف
                            </th>
                            <th>
                                نمره کل فرد در بعد رفتاری
                            </th>
                            <th>
                                نمره کل با اعمال کردن وزن در بعد وظایف
                            </th>
                            <th>
                                نمره کل با اعمال کردن وزن در بعد شایستگی
                            </th>
                            <th>
                                نمره کلی فرد از واحد مربوطه
                            </th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
</div>

");
            EndContext();
            DefineSection("Plugins", async() => {
                BeginContext(8796, 259, true);
                WriteLiteral(@"
    <script src=""/assets/plugins/jquery-validation/dist/jquery.validate.min.js"" type=""text/javascript""></script>
    <script src=""/assets/plugins/bootstrap-toastr/toastr.min.js""></script>
    <script src=""/assets/scripts/custom/ui-toastr.js""></script>

");
                EndContext();
                BeginContext(9160, 414, true);
                WriteLiteral(@"
    <script type=""text/javascript"" src=""/assets/plugins/select2v4/select2/dist/js/select2.min.js""></script>
    <script src=""/assets/plugins/jQuery-tagEditor-master/jquery.caret.min.js""></script>
    <script src=""/assets/plugins/jQuery-tagEditor-master/jquery.tag-editor.min.js""></script>


    <script src=""/assets/plugins/jquery-validation/dist/jquery.validate.min.js"" type=""text/javascript""></script>

");
                EndContext();
                BeginContext(9811, 770, true);
                WriteLiteral(@"
    <script src=""/assets/advancedDataTable/DataTables-1.10.19.js"" type=""text/javascript""></script>
    <script src=""/assets/advancedDataTable/Buttons-for-DataTables-1.5.2.js"" type=""text/javascript""></script>
    <script src=""/assets/advancedDataTable/buttons.print.js"" type=""text/javascript""></script>
    <script src=""/assets/advancedDataTable/buttons.ColVis.js"" type=""text/javascript""></script>
    <script src=""/assets/advancedDataTable/jsZip.js"" type=""text/javascript""></script>
    <script src=""/assets/advancedDataTable/pdfmake.js"" type=""text/javascript""></script>
    <script src=""/assets/advancedDataTable/pdfmake.font.js"" type=""text/javascript""></script>
    <script src=""/assets/advancedDataTable/buttons.html5.js"" type=""text/javascript""></script>

");
                EndContext();
            }
            );
            BeginContext(10584, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(10603, 16172, true);
                WriteLiteral(@"
    <script>
        $(""#calculationForm"").submit(function (e) {
            //alert('dpCreationSubmit');
            //var postdata2 = $(this).serializeArray();
            //debugger;

            var form = $('#calculationForm');

            //$(""#articleSubmit"").addClass('disabled');
            $(""#performCalculation"").attr(""disabled"", ""disabled"");
            $(""#cancel"").attr(""disabled"", ""disabled"");
            $(""#modal-close"").attr(""disabled"", ""disabled"");

            var btn = $(""#performCalculation"");
            btn.button('loading');
            //var postdata = new FormData(this);
            //listOfSubTask = JSON.stringify(listOfSubTask);
            //var postdata = $(this).serializeArray();
            //postdata.append(JSON.stringify(listOfSubTask));
            //console.log(postdata);
            $.ajax(
                {
                    url: '/Calculation/FinalCalculation',
                    //url : formURL,
                    //data: postdata,
      ");
                WriteLiteral(@"              //data: JSON.stringify(CoveredEmployee),
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
                            message += ""<span>محاسبات با موفقیت انجام گردید</span><br><br>"";
                            toastr.options.timeOut = ""15000"";
                            toastr.options.positionClass = ""toast-top-center"";
                            toastr.success(message);
                            dt.ajax.url(""/Calculation/GetCalculationScoreList"");
                            dt.ajax.reload();
                        }
                        else if (data == 0)");
                WriteLiteral(@" {
                            message += ""<span>محاسبات موفقبت آمیز نبود</span><br><br>"";
                            toastr.options.timeOut = ""15000"";
                            toastr.options.positionClass = ""toast-top-center"";
                            toastr.info(message);
                        } else if (data == -1) {
                            message += ""<span>امکان محاسبه مجدد وجود ندارد زیرا محاسبات دوره جاری نهایی گردیده</span><br><br>"";
                            toastr.options.timeOut = ""15000"";
                            toastr.options.positionClass = ""toast-top-center"";
                            toastr.info(message);
                        } else if (data == -2) {
                            message += ""<span>امکان محاسبه مجدد وجود ندارد ابتدا محاسبات را عقبگرد نمایید و سپس محاسبه مجدد انجام نمایید.</span><br><br>"";
                            toastr.options.timeOut = ""15000"";
                            toastr.options.positionClass = ""toast-top-center"";
               ");
                WriteLiteral(@"             toastr.info(message);
                        }

                        //resetFormValidation();
                        $(""#calculationForm"")[0].reset();
                        $(""#calculationModal"").modal(""hide"");
                        //$(""#periodDefinitionTable"").DataTable().destroy();
                        //show_dataTable();

                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert(""erorr00000"");
                        alert(jqXHR);
                        alert(textStatus);
                    }
                }).always(function () {
                    $(""#performCalculation"").button('reset');
                    $(""#cancel"").removeAttr(""disabled"");
                    $(""#modal-close"").removeAttr(""disabled"");
                });
            //$.getScript('/assets/javascript/articleForm.js', function () {
            //    ArticleForm.init(postdata);
            //    $(""#articleForm"")[0].rese");
                WriteLiteral(@"t();
            //});
            e.preventDefault(e);

        });

        var dt;
        function show_dataTable() {
            dt = $('#calculationTable').DataTable({
                dom: 'CT<""clearfix"">lBfrtip',
                buttons: [
                    'copy', 'csv', 'excel',
                    'pdf',
                    {
                        extend: 'pdfHtml5',
                        download: 'open',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ],
                columnDefs: [
                    {
                        targets: [-1],
                        visible: false
                    }
                ],
      ");
                WriteLiteral(@"          ""fnInitComplete"": function (oSettings, json) {

                },
                ""fnDrawCallback"": function (oSettings) {

                    //// Array to track the ids of the details displayed rows
                    //var detailRows = [];
                    //$('.details-control').on('click', function () {
                    //    var tr = $(this).closest('tr');
                    //    var row = dt.row(tr);
                    //    var idx = $.inArray(tr.attr('id'), detailRows);

                    //    if (row.child.isShown()) {
                    //        tr.removeClass('details');
                    //        row.child.hide();

                    //        // Remove from the 'open' array
                    //        detailRows.splice(idx, 1);
                    //    }
                    //    else {
                    //        tr.addClass('details');
                    //        row.child(format(row.data())).show();

                    //        //");
                WriteLiteral(@" Add to the 'open' array
                    //        if (idx === -1) {
                    //            detailRows.push(tr.attr('id'));
                    //        }
                    //    }
                    //});
                    //// On each draw, loop over the `detailRows` array and show any child rows
                    //dt.on('draw', function () {
                    //    $.each(detailRows, function (i, id) {
                    //        $('#' + id + ' td.details-control').trigger('click');
                    //    });
                    //});
                },
                ""pagingType"": ""full_numbers"",
                ""lengthMenu"": [
                    [10, 25, 50, -1],
                    [10, 25, 50, ""All""]
                ],
                ""columnDefs"": [
                    { ""orderable"": false, ""targets"": [0] }
                ],
                ""processing"": true,
                ""serverSide"": true,
                ""ajax"": {
                    ""ur");
                WriteLiteral(@"l"": ""/Calculation/GetCalculationScoreList"",
                    ""type"": ""Post"",
                    ""data"": function (d) {
                        d.periodDefinitionIdDT2 = $('#periodDefinitionIdDT').children('option:selected').val(),
                            d.departmentIdDT = $('#departmentId').children('option:selected').val()
                    },
                    //""contentType"": 'application/json; charset=utf-8',
                    //""cache"": false,
                    ""dataType"": ""json"",
                    //'data': function (data) {
                    //    data = JSON.stringify(data)
                    //    return data;
                    //}
                },
                ""aoColumns"": [
                    {
                        ""mData"": ""PeriodCode"",
                    },
                    {
                        ""mData"": ""PeriodTitle""
                    },
                    {
                        ""mData"": ""employeeFullName""
                    ");
                WriteLiteral(@"},
                    {
                        ""mData"": ""employeeDepartmentName""
                    },
                    {
                        ""mData"": ""coacherFullName""
                    },
                    {
                        ""mData"": ""coacherDepartmentName""
                    },
                    {
                        ""mData"": ""coacherPositionType"",
                        ""render"": function (data, type, row, meta) {
                            if (data == ""1"") {
                                data = ""اصلی"";
                            }
                            else if (data == ""2"") {
                                data = ""فرعی"";
                            }
                            return data;
                        }
                    },
                    {
                        ""mData"": ""finalTaskScore"",
                        ""render"": function (data, type, row, meta) {
                            if (data == null) {
              ");
                WriteLiteral(@"                  data = ""فاقد ارزیابی"";
                            }
                            return data;
                        }
                    },
                    {
                        ""mData"": ""finalCompetencyScore"",
                        ""render"": function (data, type, row, meta) {
                            if (data == null) {
                                data = ""فاقد ارزیابی"";
                            }
                            return data;
                        }
                    },
                    {
                        ""mData"": ""applyPercentToFinalTaskScore"",
                        ""render"": function (data, type, row, meta) {
                            if (data == null) {
                                data = ""فاقد ارزیابی"";
                            }
                            return data;
                        }
                    },
                    {
                        ""mData"": ""applyPercentToFinalCompetencyScor");
                WriteLiteral(@"e"",
                        ""render"": function (data, type, row, meta) {
                            if (data == null) {
                                data = ""فاقد ارزیابی"";
                            }
                            return data;
                        }
                    },
                    {
                        ""mData"": ""finalScoreDepartment"",
                        ""render"": function (data, type, row, meta) {
                            if (data == null) {
                                data = ""فاقد ارزیابی"";
                            }
                            return data;
                        }
                    }
                    //{
                    //    ""class"": ""details-control"",
                    //    ""orderable"": false,
                    //    ""data"": null,
                    //    ""defaultContent"": """"
                    //}
                ]
            });
        }
        show_dataTable();

        $(""#calculationT");
                WriteLiteral(@"able_filter"").after(""<div id='departmentIdDT' class='dataTables_filter input-DT'><label>انتخاب رده سازمانی:<select name='departmentId' id='departmentId' class='form-control input-medium'></select></label></div>"");

        $(""#calculationTable_filter"").after(""<div class='dataTables_filter input-DT'><label>انتخاب دوره:<select name='periodDefinitionIdDT' id='periodDefinitionIdDT' class='form-control input---medium'></select></label></div>"");

        $(""#calculationTable_filter"").after(""<div class='clearfix'></div>"");

        $.ajax({
            type: 'get',
            url: '/share/GetPeriodDefinitionList',
            //data: { roleId: roleId },
            success: function (periodDefinition) {
                $(""#periodDefinitionIdDT"").empty();
                //$(""#periodDefinitionIdDT"").append(""<option></option>"");
                var max = 0;
                $(periodDefinition).each(function (i, p) {
                    //$(""#parentName"").select2(""val"", d.departmentId);
                ");
                WriteLiteral(@"    if (p.periodDefinitoionId > max) {
                        max = p.periodDefinitoionId;
                    }
                    $(""#periodDefinitionIdDT"").append(""<option value='"" + p.periodDefinitoionId + ""'>"" + p.periodTitle + "" ("" + p.periodCode + "")</option>"");
                })
                $(""#periodDefinitionIdDT"").val(max);
                $('#periodDefinitionIdDT').trigger('change');
            },
            error: function (status) {
                alert(""Error"");
            }
        })
        $('#periodDefinitionIdDT').on('change', function () {
            $.ajax({
                type: 'get',
                url: '/TaskAssignment/GetDepartmentResponsibiltyList',
                data: { periodDefinitionId: $('#periodDefinitionIdDT').children('option:selected').val() },
                success: function (DepartmentResponsibilty) {

                    $(""#departmentIdDT #departmentId"").empty();
                    //$(""#departmentIdDT #departmentId"").append(""<opt");
                WriteLiteral(@"ion value='"" + null + ""'></option>"");

                    $(DepartmentResponsibilty).each(function (i, p) {
                        //$(""#parentName"").select2(""val"", d.departmentId);
                        $(""#departmentId"").append(""<option value='"" + p.evaluationHierarchyId + ""'>"" + p.shortName + ""</option>"");
                        if (p.positionType == 1) {
                            $(""#departmentId"").val(p.evaluationHierarchyId);
                            $(""#departmentId"").trigger('change');
                        }
                    })
                },
                error: function (status) {
                    alert(""Error"");
                }
            })

        });
        $('#departmentId').on('change', function () {
            dt.ajax.url(""/Calculation/GetCalculationScoreList"");
            dt.ajax.reload();
        });

        $(""#rollBackBtn"").click(function () {
            $.ajax(
                {
                    url: ""/Calculation/RollBack"",
");
                WriteLiteral(@"                    contentType: 'aplication/json;charset=utf-8',
                    type: ""GET"",
                    datatype: 'html',

                    success: function (data, textStatus, jqXHR) {
                        $(""#modalPlace"").html(data);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert(""erorr"");
                        alert(jqXHR);
                        alert(textStatus);
                    }
                });
        });
        $(""#finalizeCalculationBtn"").click(function () {
            $.ajax(
                {
                    url: ""/Calculation/FinalizeCalculationForm"",
                    contentType: 'aplication/json;charset=utf-8',
                    type: ""GET"",
                    datatype: 'html',

                    success: function (data, textStatus, jqXHR) {
                        $(""#modalPlace"").html(data);
                    },
                    error: functi");
                WriteLiteral(@"on (jqXHR, textStatus, errorThrown) {
                        alert(""erorr"");
                        alert(jqXHR);
                        alert(textStatus);
                    }
                });
        });
            //$(""#reload"").click(function () {
            //    $(""#employeeId"").val("""");
            //    $(""#departmentId"").val("""");
            //    dt.ajax.url(""/TaskAssignment/GetAssignedTaskList"");
            //    dt.ajax.reload();
            //    $(""#periodDefinitionIdDT"").val(null);
            //    $(""#subSetDepartmentIdDT"").val("""").select2();
            //    $(""#dataTables_filter_employee"").addClass(""hidden"");
            //    $(""#dataTables_filter_subSet"").addClass(""hidden"");
            //    $(""#departmentId"").empty();
            //})
    </script>
");
                EndContext();
            }
            );
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
