using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class BaseEntity
    {

        public BaseEntity()
        {
            this.CreateAt = DateTime.UtcNow;
        }
        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }
    }


}


