using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class KindnessJournal
    {
        [Key]
        public Guid JournalId { get; set; }  // ✅ This is the primary key

        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        [Required]
        public string EntryText { get; set; }

        public string Emoji { get; set; }  // emoji for kids

        public Student Student { get; set; } // optional navigation
    }
}
