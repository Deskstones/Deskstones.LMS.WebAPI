using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deskstones.LMS.Infrastructure.Models
{
    public class ModuleSeries
    {
        public int Id { get; set; }
        public int CohortModuleId { get; set; }
        public CohortModule CohortModule { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }
        public string? ExternalUrl { get; set; }
    }
}
