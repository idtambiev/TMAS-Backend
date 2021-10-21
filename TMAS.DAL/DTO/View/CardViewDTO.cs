using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DAL.DTO.View
{
    public class CardViewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public int ColumnId { get; set; }
        public int SortBy { get; set; }

    }
}
