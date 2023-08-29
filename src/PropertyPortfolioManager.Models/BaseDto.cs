using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyPortfolioManager.Models
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int AmandUserId { get; set; }
        public DateTime AmendDate { get; set; }
    }
}
