using Expense.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Expense.Controllers
{
    public class CategoryAPIController : ApiController
    {
        ExpenseDbcontext dbcontext = new ExpenseDbcontext();
        public IEnumerable<Category> Get()
        {
            using (ExpenseDbcontext dbcontext = new ExpenseDbcontext())
            {
                return dbcontext.Categories.ToList();
            }
        }

        public Category Get(int id)
        {
            using (ExpenseDbcontext dbcontext = new ExpenseDbcontext())
            {
                return dbcontext.Categories.FirstOrDefault(e => e.CategoryId == id);
            }
        }
    }
}
