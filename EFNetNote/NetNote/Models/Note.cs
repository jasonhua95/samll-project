using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Models
{
    /// <summary>
    /// 笔记 笔记和笔记类型属于一对一的关系
    /// </summary>
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime Create { get; set; }

        public int TypeId { get; set; }

        public NoteType Type { get; set; }
    }

    /// <summary>
    /// 笔记类型 笔记类型和笔记属于一对多的关系
    /// </summary>
    public class NoteType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Note> Notes { get; set; }
    }

}
