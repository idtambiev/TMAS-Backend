using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models.Enums;
using TMAS.DB.Models.Interfaces;

namespace TMAS.DB.Models
{
    public class History : IEntity, IAuditTabeEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public UserActions ActionType { get; set; }
        public string ActionObject { get; set; }
        public int? SourceAction { get; set; }
        public int? DestinationAction { get; set; }
        public Guid AuthorId { get; set; }
        public User User { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
