
namespace Expense.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    
    public partial class Category
    {
        public Category()
        {
            this.ExpenseDatas = new HashSet<ExpenseData>();
        }

      
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name Required")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Category Limit Required")]
        public Nullable<int> Cexpense_limit { get; set; }

        [JsonIgnore]
        public virtual ICollection<ExpenseData> ExpenseDatas { get; set; }
    }
}
