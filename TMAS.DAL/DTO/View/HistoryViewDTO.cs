using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models.Enums;

namespace TMAS.DAL.DTO.View
{
    public class HistoryViewDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserActions ActionType { get; set; }
        public string ActionObject { get; set; }
        public int? SourceAction { get; set; }
        public int? DestinationAction { get; set; }
        public Guid AuthorId { get; set; }
    }
}
