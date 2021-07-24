using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DB.DTO
{
    public class ColumnViewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SortBy { get; set; }
        public int BoardId { get; set; }
    }
}
