using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PrintableResourceCreateRequest
{
   // public Guid? TeacherId;

    public string Title { get; set; }
    public string FileUrl { get; set; }
    public string? Description { get; set; }
    // public Guid UploadedByTeacherId { get; set; }
    public Guid TeacherId { get; set; }
 //   public Guid? ClassGroupId { get; set; }
  //  public Guid? SubjectId { get; set; }
}
