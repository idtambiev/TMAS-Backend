using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TMAS.DB.Models.Interfaces;


namespace TMAS.DB.Models
{
    public class Board: IEntity, IAuditTabeEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public Guid BoardUserId { get; set; }
        public User User { get; set; }

        public List<Column> Columns { get; set; }
        public List<BoardsAccess> BoardsAccesses { get; set; }
        public List<History> Histories { get; set; }
    }
}
