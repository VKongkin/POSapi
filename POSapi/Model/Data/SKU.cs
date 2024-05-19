using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSapi.Model.Data
{
    public class SKU
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int skuID {  get; set; }
        public string skuName { get; set; }
        public string skuDescription { get; set; }
    }
}
