using Expense.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using static Expense.Models.ExpenseDbcontext;

namespace Expense.Controllers
{
    public class ExpenseController : Controller
    {
        ExpenseDbcontext dbobj = new ExpenseDbcontext();

        public ActionResult Deshboard()
        {
            return View();
        }

        

        [HttpGet]
        public ActionResult Total_limit()
        {
            var list = dbobj.Total_expenseLimit.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        // GET: Expense

        [HttpGet]
        public ActionResult Totallimit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTotallimit(Total_expenseLimit model)
        {
            
            if (ModelState.IsValid)
            {
                Total_expenseLimit obj = new Total_expenseLimit();
               
                obj.Total_limit = model.Total_limit;

                dbobj.Total_expenseLimit.Add(obj);
                TempData["denger"] = "Total Limit Added";
                dbobj.SaveChanges();

            }
            ModelState.Clear();
            return View("Totallimit");

        }



        public ActionResult Category()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category model)
        {
            Total_expenseLimit totallimit = dbobj.Total_expenseLimit.FirstOrDefault(h => h.id == 1);
            //TempData["denger"] = Convert.ToInt32(totallimit.Total_limit);

            if (Convert.ToInt32(model.Cexpense_limit) < Convert.ToInt32(totallimit.Total_limit))
            {
                if (ModelState.IsValid)
                {
                    Category obj = new Category();
                    obj.CategoryName = model.CategoryName;
                    obj.Cexpense_limit = model.Cexpense_limit;

                    dbobj.Categories.Add(obj);
                    TempData["denger"] = "Category Added";
                    dbobj.SaveChanges();

                }
                ModelState.Clear();
            }
            else
            {
                    TempData["denger"] = "CategoryLimit higher than total Limit";
            }

            return View("Category");

        }

        [HttpGet]
        public ActionResult CategoryList()
        {
            var list = dbobj.Categories.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult deletecategory(int id)
        {
            var dlt = dbobj.Categories.Where(x => x.CategoryId == id).First();
            dbobj.Categories.Remove(dlt);
            dbobj.SaveChanges();

            var list = dbobj.Categories.ToList();

            return RedirectToAction("CategoryList", list);
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var row = dbobj.Categories.Where(h => h.CategoryId == id).FirstOrDefault();
            return View(row);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            Category C = new Category();
            if (ModelState.IsValid)
            {
                C.CategoryId = category.CategoryId;
                C.CategoryName = category.CategoryName;
                C.Cexpense_limit = category.Cexpense_limit;

                dbobj.Entry(C).State = EntityState.Modified;
                dbobj.SaveChanges();
                TempData["msg"] = "Category Updated Successful...";

                return RedirectToAction("CategoryList");
            }
            ModelState.Clear();
            return View("Create");
            // var row = Db_Conn.Model_Category.Where(h => h.Id == id).FirstOrDefault();
        }



        //Expence
        public ActionResult ExpenseC()
        {
            var clist = dbobj.Categories.ToList();
            ViewBag.CategoryId = new SelectList(clist, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult AddExpense(ExpenseData e)
        {
            ExpenseData Eobj = new ExpenseData();
            if (ModelState.IsValid)
            {
                var delist = dbobj.Categories.ToList();
                ViewBag.CategoryId = new SelectList(delist, "CategoryId", "CategoryName");

                Eobj.Title = e.Title;
                Eobj.Description = e.Description;
                Eobj.Amount = e.Amount;
                Eobj.CategoryId = e.CategoryId;
                Eobj.date_and_time = Convert.ToString(DateTime.Now);

                dbobj.ExpenseDatas.Add(Eobj);
                TempData["AlertMsg"] = "New Category Inserted...";
                dbobj.SaveChanges();


                return RedirectToAction("ExpenseList");
            }
            ModelState.Clear();
            return View("ExpenseList");
        }


        public ActionResult ExpenseList()
        {

            List<ExpenseData> ExpD = dbobj.ExpenseDatas.ToList();
            List<Category> Cat = dbobj.Categories.ToList();

            var list = from e in ExpD
                       join c in Cat on e.CategoryId equals c.CategoryId into row
                       from d in row.ToList()
                       select new ViewModel
                       {
                           exData = e,
                           category = d
                       };

            return View(list);
        }

        [HttpGet]
        public ActionResult ExpenseEdit(int id) {
            var clist = dbobj.Categories.ToList();
            ViewBag.CategoryId = new SelectList(clist, "CategoryId", "CategoryName");

            var expdta = dbobj.ExpenseDatas.Where(x=>x.ExpenseId==id).FirstOrDefault();
            return View(expdta);
        }

        [HttpPost]
        public ActionResult ExpenseEdit(ExpenseData Ed)
        {
            var clist = dbobj.Categories.ToList();
            ViewBag.CategoryId = new SelectList(clist, "CategoryId", "CategoryName");

            ExpenseData eobj = new ExpenseData();
            if(ModelState.IsValid)
            {
                eobj.ExpenseId = Ed.ExpenseId;
                eobj.Title= Ed.Title;
                eobj.Description = Ed.Description;
                eobj.Amount = Ed.Amount;
                eobj.CategoryId = Ed.CategoryId;
                eobj.date_and_time = Convert.ToString(DateTime.Now);

                dbobj.Entry(eobj).State = EntityState.Modified;
                dbobj.SaveChanges();
                TempData["msg"] = "Record Updated Successful";

                return RedirectToAction("ExpenseList");

            }
            ModelState.Clear(); 
            return View("ExpenseList");
        }

        [HttpPost]
        public ActionResult ExpenseDelete(int id)
        {
            var dlt = dbobj.ExpenseDatas.Where(x=>x.ExpenseId == id).First();
            dbobj.ExpenseDatas.Remove(dlt);
            dbobj.SaveChanges();

            List<ExpenseData> ExpD = dbobj.ExpenseDatas.ToList();
            List<Category> Cat = dbobj.Categories.ToList();

            var list = from e in ExpD
                       join c in Cat on e.CategoryId equals c.CategoryId into row
                       from d in row.ToList()
                       select new ViewModel
                       {
                           exData = e,
                           category = d
                       };
            return RedirectToAction("ExpenseList", list);
        }

    }
}