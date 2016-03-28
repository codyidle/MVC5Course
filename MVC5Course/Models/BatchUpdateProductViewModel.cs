using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models
{
    public class BatchUpdateProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        [Range(0, 9999, ErrorMessage = "欄位Price需介於0~9999")]
        public Nullable<decimal> Price { get; set; }
        [Required(ErrorMessage = "Active必須選擇")]
        public Nullable<bool> Active { get; set; }
        [Range(0,9999, ErrorMessage ="欄位Stock需介於0~9999")]
        public Nullable<decimal> Stock { get; set; }
    }
}