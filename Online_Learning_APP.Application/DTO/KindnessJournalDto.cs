using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class KindnessJournalDto
    {
        public Guid StudentId { get; set; }
        public string EntryText { get; set; }
        public string Emoji { get; set; }  // Optional

    }
}
