namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Price <100)
            {
                yield return new ValidationResult("價格過低", new string[] { "Price" });
            }

            if (this.Stock < 100)
            {
                yield return new ValidationResult("庫存過低", new string[] { "Stock" });
            }
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [至要要兩個空白(ErrorMessage = "至少要兩個空白")]
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }


}
