using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace practicaparcial1.Models
{
    public enum Op
    {
        add = 0,
        sub = 1,
        div = 2,
        mul = 3,
        sqr = 4,
        not = 5,
        and = 6, 
        or = 7
    }
    public class calculation
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int  CalculationID { get; set; }
        [Required]
        public Op Operation { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int NumberA { get; set; }
        
        public int NumberB { get; set; }
        public string? Result { get; set; }
    }
}
