using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCRMProject.Entities
{
    public enum OperationType : byte
    {
        InitialCall, Callback, OfferRejected, OfferAccepted
    }

    [Table("ClientOperations")]
    public class Operation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ClientId { get; set; }

        public DateTime Performed { get; set; }

        public OperationType Type { get; set; }

        public DateTime? Callback { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        [Column(TypeName = "text")]
        public string Comment { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
    }
}
