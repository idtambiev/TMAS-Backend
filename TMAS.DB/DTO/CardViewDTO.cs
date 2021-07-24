using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DB.DTO
{
    public class CardViewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string  Text  { get; set; }
        public Boolean IsDone { get; set; }
        public int ColumnId { get; set; }
        public int SortBy { get; set; }
    }
}
