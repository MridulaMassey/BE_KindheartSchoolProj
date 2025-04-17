using Online_Learning_App.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class Activity
{
    public Guid ActivityId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string PdfUrl { get; set; }

    public string? StudentPdfUrl { get; set; }
    public DateTime DueDate { get; set; }
    public string ClassLevel { get; set; }
    public double WeightagePercent { get; set; }
    // Define the foreign key for Teacher to Activity
    public Guid TeacherId { get; set; }
    public bool? HasFeedback { get; set; } = false;
    public string? Feedback { get; set; }

    // Teacher who assigned the activity

    public Teacher Teacher { get; set; }

    // Student who submitted the activity (nullable for teacher-assigned)
    public Guid? StudentId { get; set; }
    public Student Student { get; set; }
    public Guid Id { get; set; }
    public string ActivityName { get; set; }

  

    public Guid? ClassGroupId { get; set; }
    public ClassGroup ClassGroup { get; set; }

    [ForeignKey("Subject")]
    public Guid SubjectId { get; set; }
    public virtual Subject Subject { get; set; }

  
    public Guid? ClassGroupSubjectId { get; set; }
    public virtual ClassGroupSubject ClassGroupSubject { get; set; }

    // Activity Type: Assignment (Teacher) or Submission (Student)
    public ActivityType Type { get; set; }

    public ICollection<ActivityGrade> ActivityGrades { get; set; } = new List<ActivityGrade>();
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}


// Enum to define if it's a teacher-assigned activity or student submission
public enum ActivityType
{
    Assignment, // Assigned by teacher
    Submission  // Submitted by student
}
