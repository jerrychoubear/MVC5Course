namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ValidationAttributes;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public int 訂單數量 {
            get
            {
                return this.OrderLine.Count;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Price > 1)
            {
                yield return new ValidationResult("Model Validation triggers! Price is too high!", new string[] { "price" });
            }
            yield break;
        }
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(30, ErrorMessage="欄位長度不得大於 80 個字元")]
        [DisplayName("商品中文名稱")]
        [商品名稱必須包含Will字串(ErrorMessage = "DataTypeAttribute triggers! 要包含Will字串!")]
        [MaxWords(20)]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 9999, ErrorMessage = "設定正確的商品價格範圍")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
