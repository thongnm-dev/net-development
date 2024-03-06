using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core
{
    public partial class ISoftDeletedEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string? DeletedBy { get; set; }
    }
}
