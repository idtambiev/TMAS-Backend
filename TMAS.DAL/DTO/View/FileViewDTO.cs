using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DAL.DTO.View
{
    public class FileViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Path { get; set; }
        public int CardId { get; set; }
        public string FileUrl { get; set; }
    }
}
