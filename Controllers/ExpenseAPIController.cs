using Expense.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Expense.Controllers
{
    public class ExpenseAPIController : ApiController
    {
        ExpenseDbcontext dbcontext = new ExpenseDbcontext();
        public IEnumerable<ExpenseData> Get()
        {
            using (ExpenseDbcontext dbcontext = new ExpenseDbcontext())
            {
                return dbcontext.ExpenseDatas.Include(e => e.Category).ToList();
            }
        }

        public ExpenseData Get(int id)
        {
            using (ExpenseDbcontext dbcontext = new ExpenseDbcontext())
            {
                
                return dbcontext.ExpenseDatas.Include(e => e.Category).FirstOrDefault(e => e.ExpenseId == id);
            }
        }

        [HttpGet]
        public IEnumerable<ExpenseData> CategoryVise(int id) {
            return dbcontext.ExpenseDatas.Include(e => e.Category).Where(e => e.CategoryId == id).ToList();

        }

       
    }
}
