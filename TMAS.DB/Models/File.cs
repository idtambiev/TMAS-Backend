using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DB.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Path { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
