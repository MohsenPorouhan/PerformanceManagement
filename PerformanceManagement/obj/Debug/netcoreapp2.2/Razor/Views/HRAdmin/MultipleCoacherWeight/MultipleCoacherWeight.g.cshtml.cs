#pragma checksum "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\MultipleCoacherWeight\MultipleCoacherWeight.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0c7ef48232d87c7b5db19293531c3927f28a02d3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HRAdmin_MultipleCoacherWeight_MultipleCoacherWeight), @"mvc.1.0.view", @"/Views/HRAdmin/MultipleCoacherWeight/MultipleCoacherWeight.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/HRAdmin/MultipleCoacherWeight/MultipleCoacherWeight.cshtml", typeof(AspNetCore.Views_HRAdmin_MultipleCoacherWeight_MultipleCoacherWeight))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0c7ef48232d87c7b5db19293531c3927f28a02d3", @"/Views/HRAdmin/MultipleCoacherWeight/MultipleCoacherWeight.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cbdcf2ba1ce3b535eb539d96aea4d66da299c9f", @"/Views/_ViewImports.cshtml")]
    public class Views_HRAdmin_MultipleCoacherWeight_MultipleCoacherWeight : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\PerformanceManagement\PerformanceManagement\Views\HRAdmin\MultipleCoacherWeight\MultipleCoacherWeight.cshtml"
  
    ViewData["Title"] = "تعیین وزن نفرات دارای چند مربی";

#line default
#line hidden
            DefineSection("Styles", async() => {
                BeginContext(82, 1410, true);
                WriteLiteral(@"
<link rel=""stylesheet"" type=""text/css"" href=""/assets/plugins/bootstrap-toastr/toastr-rtl.min.css"" />
<link rel=""stylesheet"" type=""text/css"" href=""/assets/plugins/select2/select2-rtl.css"" />
<link id=""select2-metronic"" rel=""stylesheet"" type=""text/css"" href=""/assets/plugins/select2/select2-metronic-rtl.css"" />
<link rel=""stylesheet"" href=""/assets/plugins/jQuery-tagEditor-master/jquery.tag-editor.css"" />

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
        background: url('/assets/img/details_close.png') no-repeat center center;
    }

    .dataTable .details td, .dataTable .details th {
        padding: 8px;
        background: none;
      ");
                WriteLiteral(@"  border: 0;
    }

    .select2-container-multi .select2-choices {
        min-height: 26px;
        max-height: 200px;
        overflow-y: auto;
    }

    .select2-container .select2-choice > .select2-chosen {
        /*display: block;*/
        overflow: initial;
    }

    table.dataTable tbody tr.selected td {
        background-color: #B0BED9;
    }
</style>
");
                EndContext();
            }
            );
            BeginContext(1495, 3449, true);
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
                <a href=""index.html"">
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
          ");
            WriteLiteral(@"  </li>
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
                    <i class=""fa fa-cogs""></i>تعیین وزن نفرات دارای چند مربی
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

                <a id=""multipleCoacherWeightBtn"" class=""btn blue btn-sm pull-left"" data-toggle=""modal"">تعیین وزن وظایف نفرات دارای چند مربی</a>

            WriteLiteral(@"
                <a id=""multipleCompetencyCoacherWeightBtn"" class=""btn blue btn-sm pull-left"" data-toggle=""modal"">تعیین وزن شایستگی های نفرات دارای چند مربی</a>

                <div class=""clearfix"">
                </div>
                <table class=""table table-striped table-bordered table-hover"" id=""multipleCoacherWeightTable"">
                    <thead>
                        <tr>
                            <th>
                                کد دوره
                            </th>
                            <th>
                                نام دوره
                            </th>
                            <th>
                                کارمند
                            </th>
                            <th>
                                تعداد مربی وظایف
                            </th>
                            <th>
                                تعداد مربی شایستگی ها
                            </th>
                            <th>
                  ");
            WriteLiteral(@"              نمره نهایی وظایف
                            </th>
                            <th>
                                نمره نهایی شایستگی ها
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
                BeginContext(4961, 1173, true);
                WriteLiteral(@"
<script src=""/assets/plugins/jquery-validation/dist/jquery.validate.min.js"" type=""text/javascript""></script>
<script src=""/assets/plugins/bootstrap-toastr/toastr.min.js""></script>
<script src=""/assets/scripts/custom/ui-toastr.js""></script>
<script type=""text/javascript"" src=""/assets/plugins/bootstrap-select/bootstrap-select.min.js""></script>
<script type=""text/javascript"" src=""/assets/plugins/select2/select2.min.js""></script>

<script src=""/assets/advancedDataTable/DataTables-1.10.19.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDataTable/Buttons-for-DataTables-1.5.2.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDataTable/buttons.print.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDataTable/buttons.ColVis.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDataTable/jsZip.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDataTable/pdfmake.js"" type=""text/javascript""></script>
<script src=""/assets/advancedDat");
                WriteLiteral("aTable/pdfmake.font.js\" type=\"text/javascript\"></script>\r\n<script src=\"/assets/advancedDataTable/buttons.html5.js\" type=\"text/javascript\"></script>\r\n");
                EndContext();
            }
            );
            BeginContext(6137, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(6156, 7432, true);
                WriteLiteral(@"
<script>

    var dt;
    function show_dataTable() {
        dt = $('#multipleCoacherWeightTable').DataTable({
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
            ""fnInitComplete"": function (oSettings, json) {

            },
            ""fnDrawCallback"": function (oSettings) {

                //// Array to track ");
                WriteLiteral(@"the ids of the details displayed rows
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
                //        row.child(relatedTaskCompetency(row.data())).show();

                //        // Add to the 'open' array
                //        if (idx === -1) {
                //            detailRows.push(tr.attr('id'));
                //        }
                //    }
                //});
              ");
                WriteLiteral(@"  //// On each draw, loop over the `detailRows` array and show any child rows
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
                ""url"": ""/MultipleCoacherWeight/GetMultipleCoacherWeightList"",
                ""type"": ""Post"",
                //""data"": function (d) {
                //    d.periodDefinitionIdDT = $('#periodDefinitionIdDT').children('option:selected').val(),
                //        d.departmentIdDT = $('#departmentId').children('op");
                WriteLiteral(@"tion:selected').val()
                //},
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
                    ""mData"": ""PeriodTitle"",
                },
                {
                    ""mData"": ""FullName"",
                },
                {
                    ""mData"": ""TaskCoacherNumber"",
                    ""render"": function (data, type, row, meta) {
                        if (data == null) {
                            data = '---'
                        }
                        return data;
                    }
                },
                {
                    ""mData"": ""CompetencyCoa");
                WriteLiteral(@"cherNumber"",
                    ""render"": function (data, type, row, meta) {
                        if (data == null) {
                            data = '---'
                        }
                        return data;
                    }
                },
                {
                    ""mData"": ""FinalTaskScore"",
                    ""render"": function (data, type, row, meta) {
                        if (data == '-1') {
                            data = 'فاقد ارزیابی '
                        }
                        return data;
                    }
                },
                {
                    ""mData"": ""FinalCompetencyScore"",
                    ""render"": function (data, type, row, meta) {
                        if (data == '-1') {
                            data = 'فاقد ارزیابی '
                        }
                        return data;
                    }
                }
                //{
                //    ""class"": ""details-control");
                WriteLiteral(@""",
                //    ""orderable"": false,
                //    ""data"": null,
                //    ""defaultContent"": """"
                //}
            ]
        });
    }
    show_dataTable();

    $(""#multipleCoacherWeightBtn"").click(function () {
        if (!dt.data().count()) {
            alert('وظیفه ای برای انتخاب وجود ندارد');
        }
        else {
            $.ajax(
                {
                    url: ""/MultipleCoacherWeight/WeightUpTaskOfMultipleCoacher"",
                    contentType: 'aplication/json;charset=utf-8',
                    type: ""GET"",
                    datatype: 'html',
                    data: { CoacherType: $(this).attr(""coacherType"") },
                    success: function (data, textStatus, jqXHR) {
                        $(""#modalPlace"").html(data);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert(""erorr"");
                        alert(jqXHR);
          ");
                WriteLiteral(@"              alert(textStatus);
                    }
                });
        }
    });

    $(""#multipleCompetencyCoacherWeightBtn"").click(function () {
        if (!dt.data().count()) {
            alert('وظیفه ای برای انتخاب وجود ندارد');
        }
        else {
            $.ajax(
                {
                    url: ""/MultipleCoacherWeight/WeightUpCompetencyOfMultipleCoacher"",
                    contentType: 'aplication/json;charset=utf-8',
                    type: ""GET"",
                    datatype: 'html',
                    data: { CoacherType: $(this).attr(""coacherType"") },
                    success: function (data, textStatus, jqXHR) {
                        $(""#modalPlace"").html(data);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert(""erorr"");
                        alert(jqXHR);
                        alert(textStatus);
                    }
                });
        }
    ");
                WriteLiteral(@"});

    $(""#multipleCoacherWeightTable tbody"").on('click', 'tr', function () {
        $(""#multipleCoacherWeightTable tbody tr"").removeClass('selected');
        $(this).addClass('selected');
        //$(this).toggleClass('selected');
    });

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