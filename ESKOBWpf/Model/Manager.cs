using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESKOBWpf.Model
{
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }
        public string TenantReference { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
