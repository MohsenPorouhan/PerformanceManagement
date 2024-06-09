using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.HRAdmin;
using Microsoft.AspNetCore.Http;
using System.Collections;
using PerformanceManagement.Util;
using System.Security.Claims;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.Models.HRAdmin.View;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.SqlClient;
using System.Transactions;
using System.Data;
using System.Data.Common;

namespace PerformanceManagement.Controllers
{
    [Authorize(Roles = "HRAdmin")]
    public class EvaluationHierarchyController : Controller
    {
        private readonly AppDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConnProvider connProvider;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConnProviderOBI connProviderOBI;

        public EvaluationHierarchyController(AppDbContext applicationDbContext, UserManager<IdentityUser> userManager, IConnProvider connProvider, RoleManager<IdentityRole> roleManager, IConnProviderOBI connProviderOBI)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.connProvider = connProvider;
            this.roleManager = roleManager;
            this.connProviderOBI = connProviderOBI;
        }
        public IActionResult Chart()
        {
            //return View("/Views/HRAdmin/department/Chart.cshtml");
            return View();
        }
        public IActionResult DepartmentCreationPartial()
        {
            //return View("/Views/HRAdmin/department/Chart.cshtml");
            return PartialView();
        }
        public IActionResult ChangeSupervisorPartial()
        {
            //return View("/Views/HRAdmin/department/Chart.cshtml");
            return PartialView();
        }
        public IActionResult SwitchDepartmentPartial()
        {
            //return View("/Views/HRAdmin/department/Chart.cshtml");
            return PartialView();
        }
        public IActionResult PersonalAssignPartial()
        {
            //return View("/Views/HRAdmin/department/Chart.cshtml");
            return PartialView();
        }
        public IActionResult EmployeeStatusChangePartial()
        {
            //return View("/Views/HRAdmin/department/Chart.cshtml");
            return PartialView();
        }
        public IActionResult EmployeeStatusChangeOtherCausePartial()
        {
            //return View("/Views/HRAdmin/department/Chart.cshtml");
            return PartialView();
        }
        public IActionResult DeleteDepartmentPartial()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            //EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(applicationDbContext, null, roleManager, userManager);
            //return Json(evaluationHierarchyService.DeleteDepartment(departmentId));
            using (IDbContextTransaction transaction = applicationDbContext.Database.BeginTransaction())
            {
                IDbConnection conn = connProviderOBI.Connection;
                IDbTransaction transactionOBI = null;
                try
                {
                    string message = "";
                    EvaluationHierarchy evaluationHierarchy = applicationDbContext.evaluationHierarchies.Where(c => c.DepartmentId == departmentId && c.EffectiveEndDate == null).SingleOrDefault();
                    bool hasEvaluation = applicationDbContext.Evaluation.Where(c => c.AllocatorEvaluationHierarchyId == evaluationHierarchy.EvaluationHierarchyId || c.RecieverAllocationEvaluationHierarchyId == evaluationHierarchy.EvaluationHierarchyId).Any();
                    bool hasCompetency = applicationDbContext.EvaluationBehaviouralCompetency.Where(c => c.AllocatorEvaluationBehaviouralHierarchyId == evaluationHierarchy.EvaluationHierarchyId || c.RecieverAllocationEvaluationBehaviouralHierarchyId == evaluationHierarchy.EvaluationHierarchyId).Any();
                    bool isParent = applicationDbContext.evaluationHierarchies.Where(c => c.ParentEvaluationHierarchyId == evaluationHierarchy.EvaluationHierarchyId && c.EffectiveEndDate == null).Any();

                    applicationDbContext.People.ToList();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var personIdBy = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

                    List<IdentityUser> userOfEmployeeList = new List<IdentityUser>();
                    List<IdentityUser> userOfSupervisorList = new List<IdentityUser>();

                    if (!isParent)
                    {
                        List<People> people = applicationDbContext.People.Where(c => c.EvaluationHierarchyID == evaluationHierarchy.EvaluationHierarchyId && c.EffectiveEndDate == null).ToList();

                        if (!hasEvaluation && !hasCompetency)
                        {
                            foreach (var item in people)
                            {
                                applicationDbContext.Remove(applicationDbContext.ChartInfo.Where(c => c.EvaluationHierarchyId == evaluationHierarchy.EvaluationHierarchyId && c.PeopleId == item.PeopleId && c.EffectiveEndDate == null).SingleOrDefault());
                                applicationDbContext.SaveChanges();

                                int employeeCount = applicationDbContext.People.Where(c =>/* c.EvaluationHierarchyID == evaluationHierarchy.EvaluationHierarchyId &&*/ c.PeopleId == item.PeopleId).Count();
                                if (employeeCount > 1)
                                {
                                    applicationDbContext.Remove(applicationDbContext.People.Where(c => c.EvaluationHierarchyID == evaluationHierarchy.EvaluationHierarchyId && c.PeopleId == item.PeopleId && c.EffectiveEndDate == null).SingleOrDefault());
                                }
                                else
                                {
                                    item.EffectiveEndDate = null;
                                    item.PositionType = null;
                                    item.EvaluationHierarchyID = null;
                                    item.EvaluationHierarchyDate = null;
                                    item.changeStatus = null;
                                    item.JobId = null;
                                    applicationDbContext.Update(item);
                                }

                                int supervisorCount = applicationDbContext.evaluationHierarchies.Where(c => c.SupervisorId == item.PeopleId && c.EffectiveEndDate == null).Count();
                                int employeeCount2 = applicationDbContext.People.Where(c => c.PeopleId == item.PeopleId && c.EffectiveEndDate == null).Count();
                                if (supervisorCount == 1)
                                {
                                    var userIdOfSupervisor = applicationDbContext.applicationUsers.Where(c => c.People.PeopleId == item.PeopleId).FirstOrDefault().Id;
                                    var user = await userManager.FindByIdAsync(userIdOfSupervisor);
                                    userOfSupervisorList.Add(user);
                                }
                                if (employeeCount2 == 1)
                                {
                                    var userIdOfEmployee = applicationDbContext.applicationUsers.Where(c => c.People.PeopleId == item.PeopleId).FirstOrDefault().Id;
                                    var user = await userManager.FindByIdAsync(userIdOfEmployee);
                                    userOfEmployeeList.Add(user);
                                }
                            }
                            if (applicationDbContext.evaluationHierarchies.Where(c => c.EvaluationHierarchyId == evaluationHierarchy.EvaluationHierarchyId).Count() > 1)
                            {
                                EvaluationHierarchy evaluationHierarchy2 = applicationDbContext.evaluationHierarchies.Where(c => c.DepartmentId == evaluationHierarchy.EvaluationHierarchyId && c.EffectiveEndDate == null).SingleOrDefault();
                                evaluationHierarchy2.EffectiveEndDate = DateTime.Now;
                                evaluationHierarchy2.LastUpdatedById = personIdBy;
                                evaluationHierarchy2.LastUpdatedDate = DateTime.Now;

                                Department department1 = applicationDbContext.Departments.Where(c => c.DepartmentId == departmentId && c.EffectiveEndDate == null).SingleOrDefault();
                                department1.EffectiveEndDate = DateTime.Now;
                            }
                            else
                            {
                                applicationDbContext.Remove(applicationDbContext.evaluationHierarchies.Where(c => c.DepartmentId == evaluationHierarchy.EvaluationHierarchyId && c.EffectiveEndDate == null).SingleOrDefault());

                                applicationDbContext.Remove(applicationDbContext.Departments.Where(c => c.DepartmentId == departmentId && c.EffectiveEndDate == null).SingleOrDefault());
                            }
                        }
                        else if (hasEvaluation || hasCompetency)
                        {
                            if (applicationDbContext.People.Where(c => c.EvaluationHierarchyID == evaluationHierarchy.EvaluationHierarchyId && c.EffectiveEndDate == null).Count() > 1)
                            {
                                message = "در صورتی که برای واحد سازمانی مورد نظر، زیر مجموعه تعریف کرده باشید، قابل حذف نمی باشد";
                                return Json(message);
                            }
                            else
                            {
                                ChartInfo supervisorChartInfo = applicationDbContext.ChartInfo.Where(c => c.EvaluationHierarchyId == evaluationHierarchy.EvaluationHierarchyId && c.PeopleId == evaluationHierarchy.SupervisorId && c.EffectiveEndDate == null).SingleOrDefault();
                                supervisorChartInfo.EffectiveEndDate = DateTime.Now;
                                supervisorChartInfo.LastUpdatedById = personIdBy;
                                supervisorChartInfo.LastUpdatedDate = DateTime.Now;
                                applicationDbContext.Update(supervisorChartInfo);


                                People supervisor = applicationDbContext.People.Where(c => c.EvaluationHierarchyID == evaluationHierarchy.EvaluationHierarchyId && c.PeopleId == evaluationHierarchy.SupervisorId && c.EffectiveEndDate == null).SingleOrDefault();
                                supervisor.EffectiveEndDate = DateTime.Now;
                                supervisor.changeStatus = 100;//100 means logical remove
                                applicationDbContext.Update(supervisor);

                                applicationDbContext.SaveChanges();

                                int supervisorCount = applicationDbContext.evaluationHierarchies.Where(c => c.SupervisorId == evaluationHierarchy.SupervisorId && c.EffectiveEndDate == null).Count();

                                if (supervisorCount == 1)
                                {
                                    var userIdOfSupervisor = applicationDbContext.applicationUsers.Where(c => c.People.PeopleId == evaluationHierarchy.SupervisorId).FirstOrDefault().Id;
                                    var user = await userManager.FindByIdAsync(userIdOfSupervisor);
                                    userOfSupervisorList.Add(user);
                                }

                                EvaluationHierarchy evaluationHierarchy1 = applicationDbContext.evaluationHierarchies.Where(c => c.DepartmentId == departmentId && c.EffectiveEndDate == null).SingleOrDefault();
                                evaluationHierarchy1.EffectiveEndDate = DateTime.Now;
                                evaluationHierarchy1.LastUpdatedById = personIdBy;
                                evaluationHierarchy1.LastUpdatedDate = DateTime.Now;

                                Department department = applicationDbContext.Departments.Where(c => c.DepartmentId == departmentId && c.EffectiveEndDate == null).SingleOrDefault();
                                department.EffectiveEndDate = DateTime.Now;
                            }
                        }

                    }
                    else
                    {
                        message = "در صورتی که برای واحد سازمانی مورد نظر، زیر مجموعه تعریف کرده باشید، قابل حذف نمی باشد";
                        return Json(message);
                    }

                    int result = 0;
                    result = applicationDbContext.SaveChanges();
                    //IdentityResult identityResult = null;
                    //IdentityResult identityResult2 = null;

                    conn.Open();
                    transactionOBI = conn.BeginTransaction();
                    EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, null, null, null);
                    var OBICoacherGroupId = evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "Coacher").Id;
                    var OBIEmployeeGroupId = evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "Employee").Id;

                    if (userOfEmployeeList.Count > 0)
                    {
                        foreach (var user in userOfEmployeeList)
                        {
                            if ((await userManager.IsInRoleAsync(user, "Employee")))
                            {
                                evaluationHierarchyService.OBIDeleteRole(conn, transactionOBI, user.Id, OBIEmployeeGroupId);

                                //identityResult = 
                                await userManager.RemoveFromRoleAsync(user, "Employee");
                            }
                        }
                    }
                    if (userOfSupervisorList.Count > 0)
                    {
                        foreach (var user2 in userOfSupervisorList)
                        {
                            if ((await userManager.IsInRoleAsync(user2, "Coacher")))
                            {
                                evaluationHierarchyService.OBIDeleteRole(conn, transactionOBI, user2.Id, OBICoacherGroupId);

                                //identityResult2 = 
                                await userManager.RemoveFromRoleAsync(user2, "Coacher");
                            }
                        }
                    }
                    //if ((identityResult != null && identityResult.Succeeded) || (identityResult2 != null && identityResult2.Succeeded))
                    //{
                    //    //nothing special
                    //}
                    transaction.Commit();
                    transactionOBI.Commit();
                    conn.Dispose();
                    return Json(result.ToString());
                }

                catch (DbUpdateException due)
                {
                    transaction.Rollback();
                    transactionOBI.Rollback();
                    DbUpdateException du2 = due;
                    string message = "در صورتی که برای واحد سازمانی مورد نظر، زیر مجموعه تعریف کرده باشید، قابل حذف نمی باشد" + Environment.NewLine + Environment.NewLine + due.InnerException.Message;

                    return Json(message);
                }
                catch (Exception e2)
                {
                    transaction.Rollback();
                    transactionOBI.Rollback();
                    var exc = e2.StackTrace;
                    var exc2 = e2;
                    return Json(e2.Message);
                }
            }

        }
        public IActionResult EditDepartmentPartial()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult EditDepartment(int departmentId, string departmentName)
        {
            EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(applicationDbContext, null, null, null);
            return Json(evaluationHierarchyService.EditDepartment(departmentId, departmentName));
        }
        public ActionResult GetPeople()
        {
            //var model = new SelectList(applicationDbContext.People.ToList(), "id", "firstName");
            var model = applicationDbContext.People.Select(p => new
            {
                p.PeopleId,
                p.FirstName,
                p.LastName
            }).Distinct();
            return Json(model);
        }
        public ActionResult GetCurrentPEmployee()
        {
            ////var model = new SelectList(applicationDbContext.People.ToList(), "id", "firstName");
            //var model = applicationDbContext.People/*.Where(c => c.EffectiveEndDate == null)*/.Select(p => new
            //{
            //    p.PeopleId,
            //    p.FirstName,
            //    p.LastName
            //}).Distinct();
            //return Json(model);
            EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, connProvider, null, null);

            return Json(evaluationHierarchyService.GetCurrentPEmployee());
        }
        public ActionResult GetCurrentPEmployee2()
        {
            EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, connProvider, null, null);

            return Json(evaluationHierarchyService.GetCurrentPEmployee2());
        }
        public ActionResult GetPositionList()
        {
            //var model = new SelectList(applicationDbContext.People.ToList(), "id", "firstName");
            var model = applicationDbContext.Job.Select(j => new
            {
                j.JobId,
                j.Title
            });
            return Json(model);
        }
        public ActionResult GetDepartment()
        {
            //var model = new SelectList(applicationDbContext.People.ToList(), "id", "firstName");

            //var model = applicationDbContext.Departments.Where(c => c.EffectiveEndDate == null).Select(d => new
            //{
            //    d.DepartmentId,
            //    d.ShortName
            //});

            EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, connProvider, null, null);
            return Json(evaluationHierarchyService.GetAllDepartment());
            //return Json(model);
        }
        public ActionResult GetPoolDepartment()
        {
            //var query = from i in applicationDbContext.evaluationHierarchies
            //            join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
            //            //select new{ id2=i,dep=d.}
            //            where (d.EffectiveEndDate == null && i.EffectiveEndDate == null && i.DepartmentType == 2)
            //            select new { i.DepartmentId, d.ShortName };
            //return Json(query);
            EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, connProvider, null, null);
            return Json(evaluationHierarchyService.GetPoolDepartment());
        }
        public ActionResult GetCurrentSupervisor(int departmentId)
        {
            //var query = applicationDbContext.evaluationHierarchies.Include(c => c.Department).Include(c => c.Supervisor).
            //   Where(c => c.Department.DepartmentId == departmentId && c.Supervisor.EffectiveEndDate == null
            //   && c.EffectiveEndDate == null && c.Department.EffectiveEndDate == null).SingleOrDefault();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var query = from i in applicationDbContext.evaluationHierarchies
                        join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                        join p in applicationDbContext.People on i.SupervisorId equals p.PeopleId
                        //select new{ id2=i,dep=d.}
                        where (d.EffectiveEndDate == null && d.DepartmentId == departmentId && i.EffectiveEndDate == null && p.EffectiveEndDate == null)
                        select new { p.PeopleId, p.FirstName, p.LastName }
                    ;
            var fullName = "";
            int peopleId = 0;
            if (query != null)
            {


                foreach (var item in query)
                {
                    fullName = string.Format("{0} {1}", item.FirstName, item.LastName);
                    peopleId = item.PeopleId;
                }
            }
            else
            {
                fullName = "رکورد مورد نظر یافت نشد.";
            }
            dictionary.Add("fullname", fullName);
            dictionary.Add("peopleId", peopleId);
            return Json(dictionary);
        }
        public async Task<ActionResult> departmentCreation(int departmentCause, string deptName, int parentName, int supervisor,
            int departmentType, int positionType, int positionId)
        {
            var commit = 0;
            int result = 0;
            ArrayList arrList = new ArrayList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            //var id = applicationDbContext.evaluationHierarchies.Include(c => c.Department).
            //   Where(c => c.Department.DepartmentId == parentName && c.EffectiveEndDate == null && c.Department.EffectiveEndDate == null).SingleOrDefault()
            //  .EvaluationHierarchyId;
            var query = from i in applicationDbContext.evaluationHierarchies
                        join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                        //select new{ id2=i,dep=d.}
                        where (d.EffectiveEndDate == null && d.DepartmentId == parentName && i.EffectiveEndDate == null)
                        select new { i.EvaluationHierarchyId, i.EffectiveStartDate };

            var maxid = applicationDbContext.evaluationHierarchies.Max(c => c.EvaluationHierarchyId);
            var maxid2 = applicationDbContext.Departments.Max(c => c.DepartmentId);
            //var parentEffectiveStartDate = applicationDbContext.evaluationHierarchies.Include(c => c.Department).
            //    Where(c => c.Department.DepartmentId == parentName && c.EffectiveEndDate == null && c.Department.EffectiveEndDate == null)
            //    .SingleOrDefault().EffectiveStartDate;
            //var createdById = applicationDbContext.applicationUsers.Where(c => c.Id == HttpContext.Session.GetString("userId")).SingleOrDefault().People.Id;

            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
            var createdById = personId;

            var userIdOfSupervisor = applicationDbContext.applicationUsers.Where(c => c.People.PeopleId == supervisor).FirstOrDefault().Id;
            var roleId = applicationDbContext.Roles.Where(c => c.Name == "Coacher").SingleOrDefault().Id;
            var role = await roleManager.FindByIdAsync(roleId);
            var user = await userManager.FindByIdAsync(userIdOfSupervisor);

            //var createdById = int.Parse(HttpContext.Session.GetString("userId"));
            //var createdByEffectiveStartDate = applicationDbContext.People.Where(c => c.PeopleId == createdById && c.EffectiveEndDate == null).SingleOrDefault().EffectiveStartDate;
            //var supervisorEffectiveStartDate = applicationDbContext.People.Where(c => c.PeopleId == supervisor && c.EffectiveEndDate == null).SingleOrDefault().EffectiveStartDate;
            DateTime evalStartDate = DateTime.Now;
            EvaluationHierarchy evaluationHierarchy;

            if (departmentCause == 1)
            {
                using (IDbContextTransaction transaction = applicationDbContext.Database.BeginTransaction())
                {
                    IDbConnection conn = connProviderOBI.Connection;
                    IDbTransaction transactionOBI = null;
                    try
                    {
                        evaluationHierarchy = new EvaluationHierarchy
                        {
                            Department = new Models.HRAdmin.Department
                            {
                                DepartmentId = maxid2 + 1,
                                EffectiveStartDate = DateTime.Now,
                                ShortName = deptName,
                                LongName = deptName,
                                CreationDate = DateTime.Now
                            },
                            EvaluationHierarchyId = maxid + 1,
                            EffectiveStartDate = evalStartDate,
                            ParentEffectiveStartDate = query.SingleOrDefault().EffectiveStartDate,
                            ParentEvaluationHierarchyId = query.SingleOrDefault().EvaluationHierarchyId,
                            CreatedById = createdById,
                            //CreatedByEffectiveStartDate = createdByEffectiveStartDate,
                            CreationDate = DateTime.Now,
                            DepartmentType = departmentType,
                            SupervisorId = supervisor,

                            //SupervisorEffectiveStartDate = supervisorEffectiveStartDate
                        };

                        applicationDbContext.evaluationHierarchies.Add(evaluationHierarchy);

                        DateTime? effectiveEndDate = null;
                        var peopleMaxDate = applicationDbContext.People.Where(c => c.PeopleId == supervisor).Max(c => c.EffectiveEndDate);
                        if (peopleMaxDate != null)
                        {
                            effectiveEndDate = peopleMaxDate;
                        }
                        var people = applicationDbContext.People.Where(c => c.PeopleId == supervisor && c.EffectiveEndDate == effectiveEndDate).FirstOrDefault();
                        People supervisorAssign;
                        if (people.PositionType == null)
                        {
                            commit = 1;
                            people.EvaluationHierarchyID = evaluationHierarchy.EvaluationHierarchyId;
                            people.PositionType = positionType;
                            people.JobId = positionId;
                            applicationDbContext.People.Update(people);
                        }//Prevent a person to assign to two primary positon
                        else if (positionType == 1 && applicationDbContext.People.Where(c => c.PeopleId == supervisor && c.PositionType == 1 && c.EffectiveEndDate == null).SingleOrDefault() != null)
                        {
                            arrList.Add(string.Format("{0} {1} ", people.FirstName, people.LastName));

                        }
                        else
                        {
                            supervisorAssign = new People
                            {
                                PeopleId = people.PeopleId,
                                EffectiveStartDate = DateTime.Now,
                                FirstName = people.FirstName,
                                LastName = people.LastName,
                                PositionType = positionType,
                                JobId = positionId,
                                EvaluationHierarchyID = evaluationHierarchy.EvaluationHierarchyId
                            };
                            applicationDbContext.People.Add(supervisorAssign);
                            commit = 1;
                        }

                        if (commit == 1)
                        {
                            //applicationDbContext.Database.UseTransaction((DbTransaction)transaction);
                            result = applicationDbContext.SaveChanges();

                            ////Start chartInfo 
                            //EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, connProvider, null, null);
                            //HierarchyWithSeparatorDepartmentView hierarchyWithSeparatorDepartmentView = evaluationHierarchyService.AscendHierarchyWithSeparatorDepartment(supervisor, evaluationHierarchy.EvaluationHierarchyId);
                            var userIdParam = new SqlParameter("userIdd", supervisor);
                            var departmentIdParam = new SqlParameter("departmentIdd", evaluationHierarchy.EvaluationHierarchyId);

                            var separatorDepartment = applicationDbContext.AscendHierarchyWithSeparatorDepartmentSqlQuery.FromSql(new AscendHierarchyWithSeparatorDepartmentSqlQuery().Select(), userIdParam, departmentIdParam).SingleOrDefault();
                            ChartInfo chartInfo = new ChartInfo();
                            chartInfo.PeopleId = separatorDepartment.PeopleId;
                            chartInfo.PeopleEffectiveStartDate = null;
                            chartInfo.EvaluationHierarchyId = separatorDepartment.EvalHierarchyId;
                            chartInfo.EvalEffectiveStartDate = null;
                            chartInfo.Department = separatorDepartment.Department;
                            chartInfo.Chairmanship = separatorDepartment.Chairmanship;
                            chartInfo.Management = separatorDepartment.Management;
                            chartInfo.VicePresident = separatorDepartment.VicePresident;
                            chartInfo.Level = separatorDepartment.Levell;
                            chartInfo.Intermediary = separatorDepartment.Intermediary == "1" ? true : false;
                            chartInfo.EffectiveStartDate = evalStartDate;
                            chartInfo.CreatedById = createdById;
                            chartInfo.CreatedDate = DateTime.Now;
                            applicationDbContext.ChartInfo.Add(chartInfo);
                            result = applicationDbContext.SaveChanges();
                            ////End chartInfo

                            conn.Open();
                            transactionOBI = conn.BeginTransaction();
                            EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, null, null, null);

                            if (!(await userManager.IsInRoleAsync(user, role.Name)))
                            {
                                evaluationHierarchyService.OBIAssignRole(conn, transactionOBI, evaluationHierarchyService.OBIGetUser(conn, transactionOBI, user.UserName).Id,
                                    evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "Coacher").Id);
                                IdentityResult identityResult = await userManager.AddToRoleAsync(user, role.Name);
                            }
                            if (!(await userManager.IsInRoleAsync(user, "Employee")))
                            {
                                evaluationHierarchyService.OBIAssignRole(conn, transactionOBI, evaluationHierarchyService.OBIGetUser(conn, transactionOBI, user.UserName).Id,
                                    evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "Employee").Id);
                                IdentityResult identityResult = await userManager.AddToRoleAsync(user, "Employee");
                            }
                            transaction.Commit();
                            transactionOBI.Commit();
                            conn.Dispose();
                        }
                        dictionary.Add("saveChangeResult", result.ToString());
                        dictionary.Add("message", arrList);
                        return Json(dictionary);
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        transactionOBI.Rollback();
                    }

                }

            }
            return Json(null);
        }
        public async Task<ActionResult> ChangeSupervisor(int departmentCause, int selectedDepartment, int newSupervisor, int positionType, int currentSupervisorId, int positionId2)
        {
            using (IDbContextTransaction transaction = applicationDbContext.Database.BeginTransaction())
            {
                IDbConnection conn = connProviderOBI.Connection;
                IDbTransaction transactionOBI = null;
                try
                {
                    //var id1 = applicationDbContext.evaluationHierarchies.Include(c => c.Department).
                    //   Where(c => c.Department.DepartmentId == selectedDepartment && c.EffectiveEndDate == null && c.Department.EffectiveEndDate == null).SingleOrDefault()
                    //   .EvaluationHierarchyId;
                    ArrayList arrList = new ArrayList();
                    int commit = 0;
                    int result = 0;
                    Dictionary<object, object> dictionary = new Dictionary<object, object>();
                    var id1 = from i in applicationDbContext.evaluationHierarchies
                              join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                              //select new{ id2=i,dep=d.}
                              where (d.EffectiveEndDate == null && d.DepartmentId == selectedDepartment && i.EffectiveEndDate == null)
                              select new { i.EvaluationHierarchyId }
                            ;
                    var id2 = from i in applicationDbContext.evaluationHierarchies
                              join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                              //select new{ id2=i,dep=d.}
                              where (d.EffectiveEndDate == null && d.DepartmentId == selectedDepartment && i.EffectiveEndDate == null)
                              select new { i.EffectiveStartDate }
                            ;

                    //var id2 = applicationDbContext.evaluationHierarchies.Include(c => c.Department).
                    //   Where(c => c.Department.DepartmentId == selectedDepartment && c.EffectiveEndDate == null && c.Department.EffectiveEndDate == null).SingleOrDefault()
                    //   .EffectiveStartDate;
                    var people = applicationDbContext.People.ToList();
                    //var newSupervisorEffectiveStartDate = applicationDbContext.applicationUsers.Where(c => c.People.PeopleId == newSupervisor).SingleOrDefault().People.EffectiveStartDate;
                    var eval = applicationDbContext.evaluationHierarchies.Find(id1.SingleOrDefault().EvaluationHierarchyId, id2.SingleOrDefault().EffectiveStartDate);
                    applicationDbContext.People.ToList();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
                    var lastUpdatedbyId = personId;
                    //var lastUpdatedbyId = int.Parse(HttpContext.Session.GetString("userId"));
                    //var lastUpdatedbyEffectiveStartDate = applicationDbContext.applicationUsers.Where(c => c.People.PeopleId == lastUpdatedbyId).SingleOrDefault().People.EffectiveStartDate;
                    //eval.SupervisorId = newSupervisor;

                    var userIdOfCurrentSupervisor = applicationDbContext.applicationUsers.Where(c => c.People.PeopleId == currentSupervisorId).FirstOrDefault().Id;
                    var userIdOfNewSupervisor = applicationDbContext.applicationUsers.Where(c => c.People.PeopleId == newSupervisor).FirstOrDefault().Id;

                    var userOfCurrentSupervisor = await userManager.FindByIdAsync(userIdOfCurrentSupervisor);
                    var userOfNewSupervisor = await userManager.FindByIdAsync(userIdOfNewSupervisor);


                    var datenow = DateTime.Now;
                    var datenow2 = DateTime.Now;

                    eval.EffectiveEndDate = datenow2;
                    applicationDbContext.Update(eval);

                    ChartInfo currentChartInfo = applicationDbContext.ChartInfo.Where(c => c.PeopleId == currentSupervisorId && c.EvaluationHierarchyId == id1.SingleOrDefault().EvaluationHierarchyId && c.EffectiveEndDate == null).SingleOrDefault();
                    currentChartInfo.EffectiveEndDate = datenow2;
                    currentChartInfo.LastUpdatedById = lastUpdatedbyId;
                    currentChartInfo.LastUpdatedDate = datenow2;
                    applicationDbContext.Update(currentChartInfo);

                    int? parentEvaluationHierarchyId = null;
                    DateTime? parentEffectiveStartDate = null;
                    if (eval.ParentEvaluationHierarchyId == null && eval.ParentEffectiveStartDate == null)
                    {
                        parentEvaluationHierarchyId = null;
                        parentEffectiveStartDate = null;
                    }
                    else
                    {
                        parentEvaluationHierarchyId = eval.ParentEvaluationHierarchyId;
                        parentEffectiveStartDate = eval.ParentEffectiveStartDate;
                    }
                    EvaluationHierarchy evaluationHierarchy = new EvaluationHierarchy
                    {
                        EvaluationHierarchyId = eval.EvaluationHierarchyId,
                        EffectiveStartDate = datenow,
                        ParentEffectiveStartDate = parentEffectiveStartDate,
                        ParentEvaluationHierarchyId = parentEvaluationHierarchyId,
                        CreatedById = eval.CreatedById,
                        CreatedByEffectiveStartDate = eval.CreatedByEffectiveStartDate,
                        LastUpdatedById = lastUpdatedbyId,
                        //LastUpdatedByEffectiveStartDate = lastUpdatedbyEffectiveStartDate,
                        CreationDate = eval.CreationDate,
                        DepartmentType = eval.DepartmentType,
                        DepartmentId = eval.DepartmentId,
                        DepartmentEffectiveStartDate = eval.DepartmentEffectiveStartDate,
                        SupervisorId = newSupervisor,
                        //SupervisorEffectiveStartDate = newSupervisorEffectiveStartDate,
                        LastUpdatedDate = DateTime.Now

                    };
                    applicationDbContext.evaluationHierarchies.Add(evaluationHierarchy);

                    People supervisorAssign;
                    var people2 = applicationDbContext.People.Where(c => c.PeopleId == currentSupervisorId && c.EvaluationHierarchyID == id1.SingleOrDefault().EvaluationHierarchyId && c.EffectiveEndDate == null).SingleOrDefault();

                    people2.EffectiveEndDate = DateTime.Now;
                    applicationDbContext.Update(people2);
                    DateTime? effectiveEndDate = null;
                    var peopleMaxDate = applicationDbContext.People.Where(c => c.PeopleId == newSupervisor).Max(c => c.EffectiveEndDate);
                    if (peopleMaxDate != null)
                    {
                        effectiveEndDate = peopleMaxDate;
                    }
                    var people3 = applicationDbContext.People.Where(c => c.PeopleId == newSupervisor && c.PositionType == 1 && c.EffectiveEndDate == null)
                        .SingleOrDefault();
                    var people3_1 = applicationDbContext.People.Where(c => c.PeopleId == newSupervisor && c.EffectiveEndDate == effectiveEndDate
                    && (c.changeStatus == 0 || c.changeStatus == null)).FirstOrDefault();//SingleOrDefault

                    var people3_2 = applicationDbContext.People.Where(c => c.PeopleId == newSupervisor && c.EffectiveEndDate == effectiveEndDate).FirstOrDefault();//SingleOrDefault
                                                                                                                                                                   //Prevent a person to assign to two primary positon
                    if (positionType == 1 && people3 != null)
                    {
                        arrList.Add(string.Format("{0} {1} ", people3.FirstName, people3.LastName));

                    }
                    else if (people3_1 != null && people3_1.PositionType == null)
                    {
                        people3_1.EvaluationHierarchyID = id1.SingleOrDefault().EvaluationHierarchyId;
                        people3_1.PositionType = positionType;
                        people3_1.JobId = positionId2;
                        people3_1.changeStatus = 0;
                        applicationDbContext.People.Update(people3_1);
                        commit = 1;
                    }
                    else
                    {
                        supervisorAssign = new People
                        {
                            PeopleId = newSupervisor,
                            EffectiveStartDate = DateTime.Now,
                            FirstName = people3_2.FirstName,
                            LastName = people3_2.LastName,
                            PositionType = positionType,
                            JobId = positionId2,
                            EvaluationHierarchyID = id1.SingleOrDefault().EvaluationHierarchyId,
                            changeStatus = 0
                        };
                        applicationDbContext.People.Add(supervisorAssign);
                        commit = 1;
                    }
                    if (commit == 1)
                    {
                        var currentSupervisorCount = applicationDbContext.evaluationHierarchies.Where(c => c.SupervisorId == currentSupervisorId && c.EffectiveEndDate == null).Count();
                        var employeeRoleOfcurrentSupervisorCount = applicationDbContext.People.Where(c => c.PeopleId == currentSupervisorId && c.EffectiveEndDate == null).Count();

                        result = applicationDbContext.SaveChanges();

                        ////Start chartInfo 
                        //EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, connProvider, null, null);
                        //HierarchyWithSeparatorDepartmentView hierarchyWithSeparatorDepartmentView = evaluationHierarchyService.AscendHierarchyWithSeparatorDepartment(supervisor, evaluationHierarchy.EvaluationHierarchyId);
                        var userIdParam = new SqlParameter("userIdd", newSupervisor);
                        var departmentIdParam = new SqlParameter("departmentIdd", evaluationHierarchy.EvaluationHierarchyId);

                        var separatorDepartment = applicationDbContext.AscendHierarchyWithSeparatorDepartmentSqlQuery.FromSql(new AscendHierarchyWithSeparatorDepartmentSqlQuery().Select(), userIdParam, departmentIdParam).SingleOrDefault();
                        ChartInfo chartInfo = new ChartInfo();
                        chartInfo.PeopleId = separatorDepartment.PeopleId;
                        chartInfo.PeopleEffectiveStartDate = null;
                        chartInfo.EvaluationHierarchyId = separatorDepartment.EvalHierarchyId;
                        chartInfo.EvalEffectiveStartDate = null;
                        chartInfo.Department = separatorDepartment.Department;
                        chartInfo.Chairmanship = separatorDepartment.Chairmanship;
                        chartInfo.Management = separatorDepartment.Management;
                        chartInfo.VicePresident = separatorDepartment.VicePresident;
                        chartInfo.Level = separatorDepartment.Levell;
                        chartInfo.Intermediary = separatorDepartment.Intermediary == "1" ? true : false;
                        chartInfo.EffectiveStartDate = evaluationHierarchy.EffectiveStartDate;
                        chartInfo.CreatedById = personId;
                        chartInfo.CreatedDate = DateTime.Now;
                        applicationDbContext.ChartInfo.Add(chartInfo);
                        result = applicationDbContext.SaveChanges();
                        ////End chartInfo

                        conn.Open();
                        transactionOBI = conn.BeginTransaction();
                        EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, null, null, null);
                        var OBICurrentSupervisorId = evaluationHierarchyService.OBIGetUser(conn, transactionOBI, userOfCurrentSupervisor.UserName).Id;
                        var OBINewSupervisorId = evaluationHierarchyService.OBIGetUser(conn, transactionOBI, userOfNewSupervisor.UserName).Id;
                        var OBICoacherGroupId = evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "Coacher").Id;
                        var OBIEmployeeGroupId = evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "Employee").Id;

                        if (await userManager.IsInRoleAsync(userOfCurrentSupervisor, "Coacher") && currentSupervisorCount == 1)
                        {
                            evaluationHierarchyService.OBIDeleteRole(conn, transactionOBI, OBICurrentSupervisorId, OBICoacherGroupId);

                            IdentityResult identityResult = await userManager.RemoveFromRoleAsync(userOfCurrentSupervisor, "Coacher");
                        }
                        if (await userManager.IsInRoleAsync(userOfCurrentSupervisor, "Employee") && employeeRoleOfcurrentSupervisorCount == 1)
                        {
                            evaluationHierarchyService.OBIDeleteRole(conn, transactionOBI, OBICurrentSupervisorId, OBIEmployeeGroupId);

                            IdentityResult identityResult = await userManager.RemoveFromRoleAsync(userOfCurrentSupervisor, "Employee");
                        }
                        if (!(await userManager.IsInRoleAsync(userOfNewSupervisor, "Coacher")))
                        {
                            evaluationHierarchyService.OBIAssignRole(conn, transactionOBI, OBINewSupervisorId, OBICoacherGroupId);

                            IdentityResult identityResult = await userManager.AddToRoleAsync(userOfNewSupervisor, "Coacher");
                        }
                        if (!(await userManager.IsInRoleAsync(userOfNewSupervisor, "Employee")))
                        {
                            evaluationHierarchyService.OBIAssignRole(conn, transactionOBI, OBINewSupervisorId, OBIEmployeeGroupId);

                            IdentityResult identityResult = await userManager.AddToRoleAsync(userOfNewSupervisor, "Employee");
                        }
                        transaction.Commit();
                        transactionOBI.Commit();
                        conn.Dispose();
                    }
                    dictionary.Add("saveChangeResult", result.ToString());
                    dictionary.Add("message", arrList);
                    return Json(dictionary);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    transactionOBI.Rollback();
                }
                return null;
            }
        }
        public async Task<ActionResult> personalAssign(int departmentId, int positionType, int[] employeesList, int positionId)
        {
            using (IDbContextTransaction transaction = applicationDbContext.Database.BeginTransaction())
            {
                IDbConnection conn = connProviderOBI.Connection;
                IDbTransaction transactionOBI = null;
                try
                {
                    var query = from i in applicationDbContext.evaluationHierarchies
                                join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                                //select new{ id2=i,dep=d.}
                                where (d.EffectiveEndDate == null && i.EffectiveEndDate == null && i.DepartmentId == departmentId)
                                select new { i.EvaluationHierarchyId, i.DepartmentType, i.EffectiveStartDate };
                    int evaluationHierarchyId = query.SingleOrDefault().EvaluationHierarchyId;
                    //Prevent to assign more than one employee to the apartment with DepartmentType 1
                    var query2 = from i in applicationDbContext.evaluationHierarchies
                                 join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                                 join p in applicationDbContext.People on i.EvaluationHierarchyId equals p.EvaluationHierarchyID
                                 //select new{ id2=i,dep=d.}
                                 where (d.EffectiveEndDate == null && i.EffectiveEndDate == null && i.DepartmentId == departmentId &&
                                 p.EvaluationHierarchyID == evaluationHierarchyId && p.EffectiveEndDate == null && i.DepartmentType == 1)
                                 select new { i.EvaluationHierarchyId, i.DepartmentType };
                    ArrayList arrList = new ArrayList();
                    ArrayList arrList2 = new ArrayList();
                    ArrayList arrList3 = new ArrayList();
                    List<IdentityUser> userOfEmployeeList = new List<IdentityUser>();
                    string userIdOfEmployee = "";
                    IdentityUser userOfEmployee;
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    People people = null;
                    People employeeAssign;
                    var saveChangeResult = 0;
                    applicationDbContext.People.ToList();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
                    if (employeesList.Length > 1 && query.SingleOrDefault().DepartmentType == 1)
                    {
                        dictionary.Add("message2", "false");
                    }
                    else
                    {
                        foreach (var item in employeesList)
                        {
                            people = applicationDbContext.People.Where(c => c.PeopleId == item && c.EffectiveEndDate == null).FirstOrDefault();
                            //////////////////////////////////if (people != null)
                            //////////////////////////////////{
                            if (people != null && people.PositionType == null)
                            {
                                people.EvaluationHierarchyID = evaluationHierarchyId;
                                people.PositionType = positionType;
                                people.JobId = positionId;
                                applicationDbContext.People.Update(people);
                                saveChangeResult = applicationDbContext.SaveChanges();

                                ////Start chartInfo 
                                //EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, connProvider, null, null);
                                //HierarchyWithSeparatorDepartmentView hierarchyWithSeparatorDepartmentView = evaluationHierarchyService.AscendHierarchyWithSeparatorDepartment(supervisor, evaluationHierarchy.EvaluationHierarchyId);
                                var userIdParam = new SqlParameter("userIdd", item);
                                var departmentIdParam = new SqlParameter("departmentIdd", evaluationHierarchyId);

                                var separatorDepartment = applicationDbContext.AscendHierarchyWithSeparatorDepartmentSqlQuery.FromSql(new AscendHierarchyWithSeparatorDepartmentSqlQuery().Select(), userIdParam, departmentIdParam).SingleOrDefault();
                                ChartInfo chartInfo = new ChartInfo();
                                chartInfo.PeopleId = separatorDepartment.PeopleId;
                                chartInfo.PeopleEffectiveStartDate = null;
                                chartInfo.EvaluationHierarchyId = separatorDepartment.EvalHierarchyId;
                                chartInfo.EvalEffectiveStartDate = null;
                                chartInfo.Department = separatorDepartment.Department;
                                chartInfo.Chairmanship = separatorDepartment.Chairmanship;
                                chartInfo.Management = separatorDepartment.Management;
                                chartInfo.VicePresident = separatorDepartment.VicePresident;
                                chartInfo.Level = separatorDepartment.Levell;
                                chartInfo.Intermediary = separatorDepartment.Intermediary == "1" ? true : false;
                                chartInfo.EffectiveStartDate = DateTime.Now;
                                chartInfo.CreatedById = personId;
                                chartInfo.CreatedDate = DateTime.Now;
                                applicationDbContext.ChartInfo.Add(chartInfo);
                                applicationDbContext.SaveChanges();
                                ////End chartInfo

                                userIdOfEmployee = applicationDbContext.applicationUsers.Where(c => c.People.PeopleId == people.PeopleId).FirstOrDefault().Id;
                                userOfEmployee = await userManager.FindByIdAsync(userIdOfEmployee);
                                userOfEmployeeList.Add(userOfEmployee);
                            }
                            //Prevent a person to assign to two primary positon
                            else if (positionType == 1 && applicationDbContext.People.Where(c => c.PeopleId == item && c.PositionType == 1 && c.EffectiveEndDate == null).SingleOrDefault() != null)
                            {
                                arrList.Add(string.Format("{0} {1} ", people.FirstName, people.LastName));

                            }
                            else if (applicationDbContext.People.Where(c => c.PeopleId == item && c.EffectiveEndDate == null && c.EvaluationHierarchyID == evaluationHierarchyId).SingleOrDefault() != null)
                            {
                                arrList2.Add(string.Format("{0} {1} ", people.FirstName, people.LastName));
                            }
                            //Prevent to assign more than one employee to the apartment with DepartmentType 1
                            else if (query2.SingleOrDefault() != null)
                            {
                                arrList3.Add(string.Format("{0} {1} ", people.FirstName, people.LastName));
                            }
                            else
                            {
                                People people2 = applicationDbContext.People.Where(c => c.PeopleId == item).FirstOrDefault();
                                employeeAssign = new People
                                {
                                    PeopleId = people2.PeopleId,
                                    EffectiveStartDate = DateTime.Now,
                                    FirstName = people2.FirstName,
                                    LastName = people2.LastName,
                                    PositionType = positionType,
                                    JobId = positionId,
                                    EvaluationHierarchyID = evaluationHierarchyId
                                };
                                applicationDbContext.People.Add(employeeAssign);
                                saveChangeResult += applicationDbContext.SaveChanges();

                                ////Start chartInfo 
                                //EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, connProvider, null, null);
                                //HierarchyWithSeparatorDepartmentView hierarchyWithSeparatorDepartmentView = evaluationHierarchyService.AscendHierarchyWithSeparatorDepartment(supervisor, evaluationHierarchy.EvaluationHierarchyId);
                                var userIdParam = new SqlParameter("userIdd", item);
                                var departmentIdParam = new SqlParameter("departmentIdd", evaluationHierarchyId);

                                var separatorDepartment = applicationDbContext.AscendHierarchyWithSeparatorDepartmentSqlQuery.FromSql(new AscendHierarchyWithSeparatorDepartmentSqlQuery().Select(), userIdParam, departmentIdParam).SingleOrDefault();
                                ChartInfo chartInfo = new ChartInfo();
                                chartInfo.PeopleId = separatorDepartment.PeopleId;
                                chartInfo.PeopleEffectiveStartDate = null;
                                chartInfo.EvaluationHierarchyId = separatorDepartment.EvalHierarchyId;
                                chartInfo.EvalEffectiveStartDate = null;
                                chartInfo.Department = separatorDepartment.Department;
                                chartInfo.Chairmanship = separatorDepartment.Chairmanship;
                                chartInfo.Management = separatorDepartment.Management;
                                chartInfo.VicePresident = separatorDepartment.VicePresident;
                                chartInfo.Level = separatorDepartment.Levell;
                                chartInfo.Intermediary = separatorDepartment.Intermediary == "1" ? true : false;
                                chartInfo.EffectiveStartDate = DateTime.Now;
                                chartInfo.CreatedById = personId;
                                chartInfo.CreatedDate = DateTime.Now;
                                applicationDbContext.ChartInfo.Add(chartInfo);
                                applicationDbContext.SaveChanges();
                                ////End chartInfo 

                                userIdOfEmployee = applicationDbContext.applicationUsers.Where(c => c.People.PeopleId == people2.PeopleId).FirstOrDefault().Id;
                                userOfEmployee = await userManager.FindByIdAsync(userIdOfEmployee);
                                userOfEmployeeList.Add(userOfEmployee);
                            }

                            //////////////////////////////}
                        }
                    }
                    dictionary.Add("message", arrList);
                    dictionary.Add("duplicationEvaluationHierarchy", arrList2);
                    dictionary.Add("departmentType", arrList3);

                    //var saveChangeResult = applicationDbContext.SaveChanges();
                    conn.Open();
                    transactionOBI = conn.BeginTransaction();
                    EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, null, null, null);
                    var OBIEmployeeGroupId = evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "Employee").Id;

                    if (userOfEmployeeList.Count > 0)
                    {
                        foreach (var user in userOfEmployeeList)
                        {
                            if (!(await userManager.IsInRoleAsync(user, "Employee")))
                            {
                                evaluationHierarchyService.OBIAssignRole(conn, transactionOBI, user.Id, OBIEmployeeGroupId);

                                IdentityResult identityResult = await userManager.AddToRoleAsync(user, "Employee");
                            }
                        }
                    }
                    //arrList.Add(saveChangeResult);
                    dictionary.Add("saveChangeResult", saveChangeResult.ToString());
                    transaction.Commit();
                    transactionOBI.Commit();
                    conn.Dispose();
                    return Json(dictionary);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    transactionOBI.Rollback();
                }
            }
            return null;
        }

        public async Task<JsonResult> EmployeeStatusChangeOtherCause(string effectiveEndDate, int cause, int choiceEmployee2)
        {
            using (IDbContextTransaction transaction = applicationDbContext.Database.BeginTransaction())
            {
                IDbConnection conn = connProviderOBI.Connection;
                IDbTransaction transactionOBI = null;
                try
                {
                    var query = from i in applicationDbContext.evaluationHierarchies
                                join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                                join p in applicationDbContext.People on i.EvaluationHierarchyId equals p.EvaluationHierarchyID
                                where (d.EffectiveEndDate == null && i.EffectiveEndDate == null && p.EffectiveEndDate == null &&
                                p.PeopleId == choiceEmployee2 && i.SupervisorId == choiceEmployee2
                                && DateTime.Now >= p.EffectiveStartDate
                                && DateTime.Now <= (p.EffectiveEndDate ?? Convert.ToDateTime("9999-11-27 15:59:38.4669923")))
                                select new { d.ShortName, p.FirstName, p.LastName };
                    var people = query.ToList();
                    applicationDbContext.People.ToList();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
                    DateTimeCustom dateTimeCustom = new DateTimeCustom();
                    TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
                    ArrayList departmentName = new ArrayList();
                    ArrayList departmentSupervisor = new ArrayList();
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    if (people.Count != 0)
                    {
                        foreach (var item in people)
                        {
                            departmentSupervisor.Add(string.Format("{0} {1} ", item.FirstName, item.LastName));
                            departmentName.Add(item.ShortName);
                        }
                    }
                    else
                    {
                        var employeeEnd = applicationDbContext.People.Where(c => c.EffectiveEndDate == null && c.PeopleId == choiceEmployee2).ToList();
                        if (employeeEnd != null)
                        {
                            foreach (var item in employeeEnd)
                            {
                                item.EffectiveEndDate = dateTimeCustom.Shamsi2Miladi(effectiveEndDate) + timeSpan;
                                item.changeStatus = cause;
                                applicationDbContext.Update(item);
                            }

                            var currentChartInfo = applicationDbContext.ChartInfo.Where(c => c.PeopleId == choiceEmployee2 && c.EffectiveEndDate == null).ToList();
                            foreach (var item2 in currentChartInfo)
                            {
                                item2.EffectiveEndDate = dateTimeCustom.Shamsi2Miladi(effectiveEndDate) + timeSpan;
                                item2.LastUpdatedById = personId;
                                item2.LastUpdatedDate = DateTime.Now;
                                applicationDbContext.Update(item2);
                            }
                        }
                    }
                    var saveChangeResult = applicationDbContext.SaveChanges();
                    dictionary.Add("departmentSupervisor", departmentSupervisor);
                    dictionary.Add("departmentName", departmentName);
                    dictionary.Add("saveChangeResult", saveChangeResult);

                    var userIdOfEmployee = applicationDbContext.applicationUsers.Where(c => c.People.PeopleId == choiceEmployee2).FirstOrDefault().Id;
                    var userOfEmployee = await userManager.FindByIdAsync(userIdOfEmployee);

                    conn.Open();
                    transactionOBI = conn.BeginTransaction();
                    EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, null, null, null);
                    var OBIEmployeeGroupId = evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "Employee").Id;
                    var OBICoacherGroupId = evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "Coacher").Id;
                    var OBIHRAdminGroupId = evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "HRAdmin").Id;
                    var OBIPlanningAdminGroupId = evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "PlanningAdmin").Id;
                    var OBIICTAdminGroupId = evaluationHierarchyService.OBIGetGroup(conn, transactionOBI, "ICTAdmin").Id;


                    if (await userManager.IsInRoleAsync(userOfEmployee, "Coacher"))
                    {
                        evaluationHierarchyService.OBIDeleteRole(conn, transactionOBI, userOfEmployee.Id, OBICoacherGroupId);

                        IdentityResult identityResult = await userManager.RemoveFromRoleAsync(userOfEmployee, "Coacher");
                    }
                    if (await userManager.IsInRoleAsync(userOfEmployee, "Employee"))
                    {
                        evaluationHierarchyService.OBIDeleteRole(conn, transactionOBI, userOfEmployee.Id, OBIEmployeeGroupId);

                        IdentityResult identityResult = await userManager.RemoveFromRoleAsync(userOfEmployee, "Employee");
                    }
                    if (await userManager.IsInRoleAsync(userOfEmployee, "HRAdmin"))
                    {
                        evaluationHierarchyService.OBIDeleteRole(conn, transactionOBI, userOfEmployee.Id, OBIHRAdminGroupId);

                        IdentityResult identityResult = await userManager.RemoveFromRoleAsync(userOfEmployee, "HRAdmin");
                    }
                    if (await userManager.IsInRoleAsync(userOfEmployee, "PlanningAdmin"))
                    {
                        evaluationHierarchyService.OBIDeleteRole(conn, transactionOBI, userOfEmployee.Id, OBIPlanningAdminGroupId);

                        IdentityResult identityResult = await userManager.RemoveFromRoleAsync(userOfEmployee, "PlanningAdmin");
                    }
                    if (await userManager.IsInRoleAsync(userOfEmployee, "ICTAdmin"))
                    {
                        evaluationHierarchyService.OBIDeleteRole(conn, transactionOBI, userOfEmployee.Id, OBIICTAdminGroupId);

                        IdentityResult identityResult = await userManager.RemoveFromRoleAsync(userOfEmployee, "ICTAdmin");
                    }
                    transaction.Commit();
                    transactionOBI.Commit();
                    conn.Dispose();
                    return Json(dictionary);

                    //return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    transactionOBI.Rollback();
                }
                return null;
            }
        }
        public async Task<ActionResult> SwitchDepartment(int selectedDepartment2, int parentName2, int positionType2, int positionId3, int departmentType2, bool immediateDepartment)
        {
            applicationDbContext.People.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createdOrUpdatedby = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;

            EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(applicationDbContext, connProvider, roleManager, userManager);

            return Json(evaluationHierarchyService.SwitchDepartment(selectedDepartment2, parentName2, positionType2, positionId3, departmentType2, createdOrUpdatedby));
        }
        public JsonResult GetArrivalDepartments(int peopleId)
        {
            var query2 = from i in applicationDbContext.evaluationHierarchies
                         join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                         join p in applicationDbContext.People on i.EvaluationHierarchyId equals p.EvaluationHierarchyID
                         //select new{ id2=i,dep=d.}
                         where (d.EffectiveEndDate == null && i.EffectiveEndDate == null /*&& (p.changeStatus == 0 || p.changeStatus == null)*/
                         && p.PeopleId == peopleId && p.EffectiveEndDate == null)
                         select new { d.DepartmentId, d.ShortName };

            return Json(query2.ToList());
        }
        public JsonResult GetPositionType(int departmentId, int peopleId)
        {
            var query2 = from i in applicationDbContext.evaluationHierarchies
                         join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                         join p in applicationDbContext.People on i.EvaluationHierarchyId equals p.EvaluationHierarchyID
                         //select new{ id2=i,dep=d.}
                         where (d.EffectiveEndDate == null && i.EffectiveEndDate == null /*&& (p.changeStatus == 0 || p.changeStatus == null)*/
                         && p.PeopleId == peopleId && p.EffectiveEndDate == null && d.DepartmentId == departmentId)
                         select new { p.PositionType };

            return Json(query2.SingleOrDefault());

            //return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult EmployeeStatusChange(int cause, int choiceEmployee, string effectiveEndDateArrival, string effectiveStartDateDeparture, int choiceArrivalDepartment, int choiceDepartureDepartment, int DepartureDepartmentPositionType, int positionType, int positionId)
        {
            using (IDbContextTransaction transaction = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    var query = from i in applicationDbContext.evaluationHierarchies
                                join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                                //select new{ id2=i,dep=d.}
                                where (d.EffectiveEndDate == null && i.EffectiveEndDate == null && (i.DepartmentId == choiceArrivalDepartment ||
                                i.DepartmentId == choiceDepartureDepartment))
                                select new { i.EvaluationHierarchyId, i.DepartmentType };
                    int evaluationArrivalHierarchyId = query.Where(c => c.EvaluationHierarchyId == choiceArrivalDepartment).SingleOrDefault().EvaluationHierarchyId;
                    int evaluationDepartureHierarchyId = query.Where(c => c.EvaluationHierarchyId == choiceDepartureDepartment).SingleOrDefault().EvaluationHierarchyId;

                    var query2 = from i in applicationDbContext.evaluationHierarchies
                                 join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                                 join p in applicationDbContext.People on i.EvaluationHierarchyId equals p.EvaluationHierarchyID
                                 //select new{ id2=i,dep=d.}
                                 where (d.EffectiveEndDate == null && i.EffectiveEndDate == null && i.DepartmentId == choiceDepartureDepartment &&
                                 p.EvaluationHierarchyID == evaluationDepartureHierarchyId && p.EffectiveEndDate == null && i.DepartmentType == 1)
                                 select new { i.EvaluationHierarchyId, i.DepartmentType };

                    var query3 = from i in applicationDbContext.evaluationHierarchies
                                 join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                                 join p in applicationDbContext.People on i.EvaluationHierarchyId equals p.EvaluationHierarchyID
                                 where (d.EffectiveEndDate == null && i.EffectiveEndDate == null && p.EffectiveEndDate == null &&
                                 p.PeopleId == choiceEmployee && i.SupervisorId == choiceEmployee && d.DepartmentId == choiceArrivalDepartment)
                                 select new { i.EvaluationHierarchyId, i.DepartmentType };

                    var singleDepartment = "";
                    var withOutAssign = "";
                    var preventTwoPrimaryPosition = "";
                    var preventLossPrimaryPosition = "";
                    var duplicate = "";
                    var departmentSupervisor = "";
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    applicationDbContext.People.ToList();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var personId = applicationDbContext.applicationUsers.Where(c => c.Id == userId).SingleOrDefault().People.PeopleId;
                    var saveChangeResult = 0;
                    People people;
                    People people2;
                    People employeeAssign;
                    DateTimeCustom dateTimeCustom = new DateTimeCustom();
                    TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
                    TimeSpan timeSpan2 = new TimeSpan(0, 0, 0, 0, DateTime.Now.Millisecond);
                    //var test = DateTime.Now.ToLongTimeString();
                    //DateTime test = dateTimeCustom.Shamsi2Miladi("1398/09/05");
                    //TimeSpan timeSpan = new TimeSpan(DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second);
                    //var r = test.Date + timeSpan;
                    var maxEffectiveStartDate = applicationDbContext.People.Where(c => c.PeopleId == choiceEmployee && (c.changeStatus == 0 || c.changeStatus == 1 || c.changeStatus == null))
                        .Max(c => c.EffectiveStartDate);
                    people = applicationDbContext.People.Where(c => c.PeopleId == choiceEmployee && c.EffectiveStartDate == maxEffectiveStartDate).SingleOrDefault();
                    people2 = applicationDbContext.People.Where(c => c.PeopleId == choiceEmployee && c.EffectiveEndDate == null &&
                      c.EvaluationHierarchyID == evaluationArrivalHierarchyId).SingleOrDefault();
                    if (people != null)
                    {
                        //Prevent to switch an employee that never have an assign
                        if (people.EvaluationHierarchyID == null)
                        {
                            withOutAssign = string.Format("{0} {1} ", people.FirstName, people.LastName);
                        }
                        //Prevent a person to assign to two primary positon
                        else if (DepartureDepartmentPositionType == 1 && positionType == 2 && applicationDbContext.People.Where(c => c.PeopleId == choiceEmployee && c.PositionType == 1 && c.EffectiveEndDate == null).SingleOrDefault() != null)
                        {
                            preventTwoPrimaryPosition = string.Format("{0} {1} ", people.FirstName, people.LastName);

                        }
                        //Prevent to duplicate department for a employee
                        else if (applicationDbContext.People.Where(c => c.PeopleId == choiceEmployee && c.EffectiveEndDate == null && c.EvaluationHierarchyID == evaluationDepartureHierarchyId).SingleOrDefault() != null)
                        {
                            duplicate = string.Format("{0} {1} ", people.FirstName, people.LastName);
                        }
                        //To prevent two employee to single department 
                        else if (query2.SingleOrDefault() != null)
                        {
                            singleDepartment = string.Format("{0} {1} ", people.FirstName, people.LastName);
                        }
                        else if (query3.SingleOrDefault() != null)
                        {
                            departmentSupervisor = string.Format("{0} {1} ", people.FirstName, people.LastName);
                        }
                        //A person must have only one primary position so prevent a person to loss primary positon
                        else if (DepartureDepartmentPositionType == 2 && positionType == 1 && applicationDbContext.People.Where(c => c.PeopleId == choiceEmployee && c.PositionType == 1 && c.EffectiveEndDate == null).SingleOrDefault() != null)
                        {
                            preventLossPrimaryPosition = string.Format("{0} {1} ", people.FirstName, people.LastName);

                        }
                        else
                        {
                            people2.EffectiveEndDate = dateTimeCustom.Shamsi2Miladi(effectiveEndDateArrival) + timeSpan;
                            people2.changeStatus = cause;
                            applicationDbContext.Update(people2);

                            ChartInfo currentChartInfo = applicationDbContext.ChartInfo.Where(c => c.PeopleId == choiceEmployee && c.EvaluationHierarchyId == evaluationArrivalHierarchyId && c.EffectiveEndDate == null).SingleOrDefault();
                            currentChartInfo.EffectiveEndDate = dateTimeCustom.Shamsi2Miladi(effectiveEndDateArrival) + timeSpan;
                            currentChartInfo.LastUpdatedById = personId;
                            currentChartInfo.LastUpdatedDate = DateTime.Now;
                            applicationDbContext.Update(currentChartInfo);

                            employeeAssign = new People
                            {
                                PeopleId = people.PeopleId,
                                EffectiveStartDate = dateTimeCustom.Shamsi2Miladi(effectiveStartDateDeparture) + timeSpan2,
                                FirstName = people.FirstName,
                                LastName = people.LastName,
                                PositionType = DepartureDepartmentPositionType,
                                JobId = positionId,
                                EvaluationHierarchyID = evaluationDepartureHierarchyId
                            };
                            applicationDbContext.People.Add(employeeAssign);
                            saveChangeResult = applicationDbContext.SaveChanges();

                            ////Start chartInfo 
                            //EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, connProvider, null, null);
                            //HierarchyWithSeparatorDepartmentView hierarchyWithSeparatorDepartmentView = evaluationHierarchyService.AscendHierarchyWithSeparatorDepartment(supervisor, evaluationHierarchy.EvaluationHierarchyId);
                            var userIdParam = new SqlParameter("userIdd", employeeAssign.PeopleId);
                            var departmentIdParam = new SqlParameter("departmentIdd", employeeAssign.EvaluationHierarchyID);

                            var separatorDepartment = applicationDbContext.AscendHierarchyWithSeparatorDepartmentSqlQuery.FromSql(new AscendHierarchyWithSeparatorDepartmentSqlQuery().Select(), userIdParam, departmentIdParam).SingleOrDefault();
                            ChartInfo chartInfo = new ChartInfo();
                            chartInfo.PeopleId = separatorDepartment.PeopleId;
                            chartInfo.PeopleEffectiveStartDate = null;
                            chartInfo.EvaluationHierarchyId = separatorDepartment.EvalHierarchyId;
                            chartInfo.EvalEffectiveStartDate = null;
                            chartInfo.Department = separatorDepartment.Department;
                            chartInfo.Chairmanship = separatorDepartment.Chairmanship;
                            chartInfo.Management = separatorDepartment.Management;
                            chartInfo.VicePresident = separatorDepartment.VicePresident;
                            chartInfo.Level = separatorDepartment.Levell;
                            chartInfo.Intermediary = separatorDepartment.Intermediary == "1" ? true : false;
                            chartInfo.EffectiveStartDate = DateTime.Now;
                            chartInfo.CreatedById = personId;
                            chartInfo.CreatedDate = DateTime.Now;
                            applicationDbContext.ChartInfo.Add(chartInfo);
                            applicationDbContext.SaveChanges();
                            ////End chartInfo 

                        }
                    }
                    dictionary.Add("singleDepartment", singleDepartment);
                    dictionary.Add("withOutAssign", withOutAssign);
                    dictionary.Add("preventTwoPrimaryPosition", preventTwoPrimaryPosition);
                    dictionary.Add("preventLossPrimaryPosition", preventLossPrimaryPosition);
                    dictionary.Add("duplicate", duplicate);
                    dictionary.Add("departmentSupervisor", departmentSupervisor);
                    //var saveChangeResult = applicationDbContext.SaveChanges();
                    dictionary.Add("saveChangeResult", saveChangeResult.ToString());
                    transaction.Commit();
                    return Json(dictionary);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
                return null;
            }
        }
        public JsonResult GetRoot()
        {
            EvaluationHierarchyService evaluationHierarchyService = new EvaluationHierarchyService(null, connProvider, null, null);
            return Json(evaluationHierarchyService.GetTree());
        }
        public JsonResult GetRootOld()
        {
            List<Chart> items = GetTreeOld(applicationDbContext);

            return Json(items);

            //return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //public JsonResult GetChildren(string id)
        //{
        //    List<DepartmentHierarchyModel> items = GetTree(id, applicationDbContext);

        //    return Json(items);
        //    //return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        static List<Chart> GetTreeOld(AppDbContext applicationDbContext)
        {
            var items = from i in applicationDbContext.evaluationHierarchies
                        join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                        join p in applicationDbContext.People on i.EvaluationHierarchyId equals p.EvaluationHierarchyID
                        where (i.EffectiveEndDate == null && d.EffectiveEndDate == null && p.EffectiveEndDate == null && i.SupervisorId == p.PeopleId)
                        //select new{ id2=i,dep=d.}
                        select new
                        {
                            i.EvaluationHierarchyId,
                            ParentEvaluationHierarchyId = (i.ParentEvaluationHierarchyId == (int?)null ? -1 : Convert.ToInt32(i.ParentEvaluationHierarchyId)),
                            ShortName = string.Format("{0} ({1} {2})", d.ShortName, p.FirstName, p.LastName),
                            p.PositionType
                        };
            var items2 = from i in applicationDbContext.evaluationHierarchies
                         join d in applicationDbContext.Departments on i.DepartmentId equals d.DepartmentId
                         join p in applicationDbContext.People on i.EvaluationHierarchyId equals p.EvaluationHierarchyID
                         where (i.EffectiveEndDate == null && d.EffectiveEndDate == null && p.EffectiveEndDate == null && p.EvaluationHierarchyID != null && i.SupervisorId != p.PeopleId)
                         //select new{ id2=i,dep=d.}
                         select new { EvaluationHierarchyId = p.PeopleId, ParentEvaluationHierarchyId = i.EvaluationHierarchyId, ShortName = string.Format("{0} {1}", p.FirstName, p.LastName), p.PositionType };
            int j = 10000000;
            List<Chart2> chart2 = new List<Chart2>();
            foreach (var item in items2.ToList())
            {
                chart2.Add(new Chart2 { EvaluationHierarchyId = item.EvaluationHierarchyId + j, ParentEvaluationHierarchyId = item.ParentEvaluationHierarchyId, ShortName = item.ShortName, PositionType = item.PositionType });

                j++;
            }


            var finalChart = chart2.ToList().Select(c => new { c.EvaluationHierarchyId, c.ParentEvaluationHierarchyId, c.ShortName, c.PositionType }).Union(items.ToList()).OrderByDescending(c => c.ShortName);
            var itemss = new List<EvaluationHierarchy>();
            // itemss = applicationDbContext.evaluationHierarchies.Include(d => d.Department).Where(c => c.EffectiveEndDate == null && c.Department.EffectiveEndDate == null).ToList();
            //var items = itemss.Select(e => new
            //{ e.EvaluationHierarchyId, e.ParentEvaluationHierarchyId, e.Department.ShortName });
            // set items in here
            List<Chart> chart = new List<Chart>();
            foreach (var item in finalChart)
            {
                if (item.ParentEvaluationHierarchyId == -1)
                {
                    chart.Add(new Chart { Id = item.EvaluationHierarchyId, Text = item.ShortName, Parent = "#", PositionType = item.PositionType });
                }
                else
                {
                    chart.Add(new Chart { Id = item.EvaluationHierarchyId, Text = item.ShortName, Parent = item.ParentEvaluationHierarchyId.ToString(), PositionType = item.PositionType });
                }
            }
            return chart;
        }

        //static List<DepartmentHierarchyModel> GetTree(string id, AppDbContext applicationDbContext)
        //{
        //    var items = new List<DepartmentHierarchyModel>();
        //    items = applicationDbContext.DepartmentHierarchyModels.ToList();
        //    // set items in here

        //    return items;
        //}
    }
}
