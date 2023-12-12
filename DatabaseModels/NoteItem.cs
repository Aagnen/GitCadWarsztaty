using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseModels
{
    [Serializable]
    public class NoteItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        public List<Tag> TagList { get; set; }
    }
}
