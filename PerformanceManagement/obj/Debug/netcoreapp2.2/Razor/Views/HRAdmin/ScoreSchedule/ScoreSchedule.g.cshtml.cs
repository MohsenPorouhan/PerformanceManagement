#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\ScoreSchedule\ScoreSchedule.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3b9f74efa236725cf0229e428e746e3b6da78bd2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HRAdmin_ScoreSchedule_ScoreSchedule), @"mvc.1.0.view", @"/Views/HRAdmin/ScoreSchedule/ScoreSchedule.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/HRAdmin/ScoreSchedule/ScoreSchedule.cshtml", typeof(AspNetCore.Views_HRAdmin_ScoreSchedule_ScoreSchedule))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b9f74efa236725cf0229e428e746e3b6da78bd2", @"/Views/HRAdmin/ScoreSchedule/ScoreSchedule.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_HRAdmin_ScoreSchedule_ScoreSchedule : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\ScoreSchedule\ScoreSchedule.cshtml"
  
    ViewData["Title"] = "زمان بندی نمره دهی";

#line default
#line hidden
            DefineSection("Styles", async() => {
                BeginContext(70, 1310, true);
                WriteLiteral(@"
<link rel=""stylesheet"" type=""text/css"" href=""/assets/plugins/select2/select2-rtl.css"" />
<link id=""select2-metronic"" rel=""stylesheet"" type=""text/css"" href=""/assets/plugins/select2/select2-metronic-rtl.css"" />
<link rel=""stylesheet"" type=""text/css"" href=""/assets/plugins/jquery-multi-select/css/multi-select-rtl.css"" />
<link rel=""stylesheet"" type=""text/css"" href=""/assets/plugins/bootstrap-toastr/toastr-rtl.min.css"" />
<link href=""/assets/plugins/persian-datepicker/bootstrap-datepicker.min.css"" rel=""stylesheet"" type=""text/css"" />

<link rel=""stylesheet"" href=""/assets/advancedDataTable/jquery.dataTables.css"" />
<link rel=""stylesheet"" href=""/assets/advancedDataTable/buttons.dataTables.css"" />
<style>
    .btn-sm, .btn-xs {
        margin: 2px;
    }

    td.details-control {
        background: url('/assets/img/details_open.png') no-repeat center center;
        cursor: pointer;
    }

    tr.details td.details-control {
        background: url('/assets/img/details_close.png') no-repeat center");
                WriteLiteral(@" center;
    }

    .dataTable .details td, .dataTable .details th {
        padding: 8px;
        background: none;
        border: 0;
    }

    .select2-container .select2-choice > .select2-chosen {
        /*display: block;*/
        overflow: initial;
    }
</style>
");
                EndContext();
            }
            );
            BeginContext(1383, 3316, true);
            WriteLiteral(@"

<!-- BEGIN PAGE HEADER-->
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
                <a href=""index.html"">
                    خانه
                </a>
                <i class=""fa fa-angle-left""></i>
            </li>
            <li>
                <a href=""#"">
                    زمان بندی نمره دهی
                </a>
            </li>
            <li class=""pull-right"">
                <div id=""dashboard-report-range"" class=""dashboard-date-range tooltips"" data-placement=""top"" data-original-title=""Change dashboard date range"">
                    <i class=""fa fa-calendar""></i>
                    <span>
                    </span>
                    <i class=""fa fa-angle-down""></i>
                </div>
 ");
            WriteLiteral(@"           </li>
        </ul>
        <!-- END PAGE TITLE & BREADCRUMB-->
    </div>
</div>
<!-- END PAGE HEADER-->

<div class=""row"">
    <div class=""col-md-12"">
        <div class=""portlet blue box"">
            <div class=""portlet-title"">
                <div class=""caption"">
                    <i class=""fa fa-cogs""></i>زمان بندی نمره دهی
                </div>
                <div class=""tools"">
                    <a href=""javascript:;"" class=""collapse"">
                    </a>
                    <a href=""#portlet-config"" data-toggle=""modal"" class=""config"">
                    </a>
                    <a href=""javascript:;"" class=""reload"">
                    </a>
                    <a href=""javascript:;"" class=""remove"">
                    </a>
                </div>
            </div>
            <div class=""portlet-body"">

                <a id=""scoreScheduleBtn"" class=""btn blue btn-sm pull-left"" data-toggle=""modal""
                   href=""#"">زمان بندی نمره دهی</a>
");
            WriteLiteral(@"
                <div class=""clearfix"">
                </div>
                <table class=""table table-striped table-hover table-bordered"" id=""scoreScheduleTable"">
                    <thead>
                        <tr>
                            <th>
                                کد دوره
                            </th>
                            <th>
                                عنوان
                            </th>
                            <th>
                                شروع دوره
                            </th>
                            <th>
                                پایان دوره
                            </th>
                            <th>
                                شروع انتهای دوره
                            </th>
                            <th>
                                پایان انتهای دوره
                            </th>
                            <th>
                                ویرایش
                            </th>
     ");
            WriteLiteral("                       <th>\r\n                            </th>\r\n                        </tr>\r\n                    </thead>\r\n                    <tbody></tbody>\r\n                </table>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
            DefineSection("Plugins", async() => {
                BeginContext(4716, 1475, true);
                WriteLiteral(@"
<script type=""text/javascript"" src=""/assets/plugins/bootstrap-select/bootstrap-select.min.js""></script>
<script type=""text/javascript"" src=""/assets/plugins/select2/select2.min.js""></script>

<script type=""text/javascript"" src=""/assets/plugins/jquery-multi-select/js/jquery.multi-select.js""></script>
<script src=""/assets/plugins/jquery-validation/dist/jquery.validate.min.js"" type=""text/javascript""></script>
<script src=""/assets/plugins/bootstrap-toastr/toastr.min.js""></script>
<script src=""/assets/scripts/custom/ui-toastr.js""></script>
<script src=""/assets/plugins/persian-datepicker/jquery.ui.datepicker-cc.all.min.js"" type=""text/javascript""></script>

<script src=""/assets/advancedDataTable/DataTables-1.10.19.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDataTable/Buttons-for-DataTables-1.5.2.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDataTable/buttons.print.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDataTable/buttons.ColVis.js"" type");
                WriteLiteral(@"=""text/javascript""></script>
<script src=""/assets/advancedDataTable/jsZip.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDataTable/pdfmake.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDataTable/pdfmake.font.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDataTable/buttons.html5.js"" type=""text/javascript""></script>
<script src=""/assets/plugins/dist//jalali-moment.browser.js""></script>
");
                EndContext();
            }
            );
            BeginContext(6194, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(6213, 8889, true);
                WriteLiteral(@"
<script>
    var actionUrl = """";

    $(""#scoreScheduleBtn"").click(function () {
        $.ajax(
            {
                url: ""/ScoreSchedule/AddScoreSchedule"",
                contentType: 'aplication/json;charset=utf-8',
                type: ""GET"",
                datatype: 'html',
                //data: { CoacherType: $(this).attr(""coacherType"") },
                success: function (data, textStatus, jqXHR) {
                    actionUrl = ""/ScoreSchedule/AddScoreSchedule"";
                    $(""#modalPlace"").html(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(""erorr"");
                    alert(jqXHR);
                    alert(textStatus);
                }
            });
    });

    function GetRelatedScoreSchedule(d) {
        var r;
        $.ajax({
            url: ""/ScoreSchedule/GetRelatedScoreSchedule"",
            type: ""POST"",
            data: { periodDefinitionId: d.PeriodDefinitoi");
                WriteLiteral(@"onId },
            async: false,
            dataType: ""html"",
            success: function (result) {
                r = result;

            }
        });
        return r;
    }

    var dt;
    function show_dataTable() {
        dt = $('#scoreScheduleTable').DataTable({
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
            ");
                WriteLiteral(@"],
            //                ""fnInitComplete"": function (oSettings, json) {
            //
            //                },
            ""fnDrawCallback"": function (oSettings) {
                $("".scoreScheduleEditionBtn"").click(function () {
                    var tr2 = $(this).closest('tr');
                    var row2 = dt.row(tr2);
                    $.ajax(
                        {
                            url: ""/ScoreSchedule/AddScoreSchedule"",
                            contentType: 'aplication/json;charset=utf-8',
                            type: ""GET"",
                            datatype: 'html',
                            async: false,
                            data: {
                                periodDefinitoionId: row2.data().PeriodDefinitoionId
                            },
                            success: function (data, textStatus, jqXHR) {
                                $(""#modalPlace"").html(data);
                                GetRelatedScoreS");
                WriteLiteral(@"cheduleJsonFormat(row2.data().PeriodDefinitoionId);
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                alert(""erorr"");
                                alert(jqXHR);
                                alert(textStatus);
                            }
                        });
                });
                $("".periodDeletionBtn"").click(function () {
                    var tr3 = $(this).closest('tr');
                    var row3 = dt.row(tr3);
                    $.ajax(
                        {
                            url: ""/PeriodDefinition/DeletionPeriodDefinition"",
                            contentType: 'aplication/json;charset=utf-8',
                            type: ""GET"",
                            datatype: 'html',
                            data: {
                                periodDefinitoionId: row3.data().periodDefinitoionId,
                                periodTitle: r");
                WriteLiteral(@"ow3.data().periodTitle
                            },
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
                // Array to track the ids of the details displayed rows
                var detailRows = [];
                $('.details-control').on('click', function () {
                    var tr = $(this).closest('tr');
                    var row = dt.row(tr);
                    var idx = $.inArray(tr.attr('id'), detailRows);

                    if (row.child.isShown()) {
                        tr.removeClass('details');
                        row.child.hide();

  ");
                WriteLiteral(@"                      // Remove from the 'open' array
                        detailRows.splice(idx, 1);
                    }
                    else {
                        tr.addClass('details');
                        row.child(GetRelatedScoreSchedule(row.data())).show();

                        // Add to the 'open' array
                        if (idx === -1) {
                            detailRows.push(tr.attr('id'));
                        }
                    }
                });
                // On each draw, loop over the `detailRows` array and show any child rows
                dt.on('draw', function () {
                    $.each(detailRows, function (i, id) {
                        $('#' + id + ' td.details-control').trigger('click');
                    });
                });
            },
            ""pagingType"": ""full_numbers"",
            ""lengthMenu"": [
                [10, 25, 50, -1],
                [10, 25, 50, ""All""]
            ],
            ");
                WriteLiteral(@"""columnDefs"": [
                { ""orderable"": false, ""targets"": [0] }
            ],
            ""processing"": true,
            ""serverSide"": true,
            ""ajax"": {
                ""url"": ""/ScoreSchedule/GetRelatedScoreScheduleWithPeriodDefinitionList"",
                ""type"": ""Post"",
                //""contentType"": 'application/json; charset=utf-8',
                //""cache"": false,
                ""dataType"": ""json""
            },
            ""aoColumns"": [
                {
                    ""mData"": ""PeriodCode""
                },
                { ""mData"": ""PeriodTitle"" },
                {
                    ""mData"": ""DateFrom"",
                    ""render"": function (data, type, row, meta) {
                        data = moment(data, 'YYYY/MM/DD').locale('fa').format('YYYY/MM/DD');
                        return data;
                    }
                },
                {
                    ""mData"": ""DateTo"",
                    ""render"": function (data, type,");
                WriteLiteral(@" row, meta) {
                        data = moment(data, 'YYYY/MM/DD').locale('fa').format('YYYY/MM/DD');
                        return data;
                    }
                },
                {
                    ""mData"": ""EndSectionDateFrom"",
                    ""render"": function (data, type, row, meta) {
                        data = moment(data, 'YYYY/MM/DD').locale('fa').format('YYYY/MM/DD');
                        return data;
                    }
                },
                {
                    ""mData"": ""EndSectionDateTo"",
                    ""render"": function (data, type, row, meta) {
                        data = moment(data, 'YYYY/MM/DD').locale('fa').format('YYYY/MM/DD');
                        return data;
                    }
                },
                {
                    ""mData"": null,
                    ""render"": function (data, type, row, meta) {
                        data = ""<a href='#' data-toggle='modal' class='scoreScheduleEdition");
                WriteLiteral(@"Btn'>ویرایش</a>"";
                        return data;
                    }
                },
                //{
                //    ""mData"": null,
                //    ""render"": function (data, type, row, meta) {
                //        data = ""<a href='#' data-toggle='modal' class='periodDeletionBtn'>حذف</a>"";
                //        return data;
                //    }
                //},
                {
                    ""class"": ""details-control"",
                    ""orderable"": false,
                    ""data"": null,
                    ""defaultContent"": """"
                }
            ]
        });

    }

    show_dataTable();

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
