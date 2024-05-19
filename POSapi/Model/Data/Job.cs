using System.ComponentModel.DataAnnotations;

namespace POSapi.Model.Data
{
    public class Job
    {
        [Key]
        public int JobID { get; set; }
        public string JobTitle { get; set; }
        public float Salary { get; set; }
    }
}
