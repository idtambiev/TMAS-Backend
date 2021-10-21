using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DB.Models
{
    public class Provider
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string UserInfoEndPoint { get; set; }
    }
}
