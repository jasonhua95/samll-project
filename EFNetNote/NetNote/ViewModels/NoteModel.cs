using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.ViewModels
{
    /// <summary>
    /// 笔记 笔记和笔记类型属于一对一的关系
    /// </summary>
    public class NoteModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "内容")]
        public string Content { get; set; }

        [Display(Name ="类型")]
        public int Type { get; set; }
    }

}
