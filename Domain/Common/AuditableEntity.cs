using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string? CreatedById { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public string? LastModifiedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public string? DeletedById { get; set; }
    }
}
