using Humanizer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeroomRedux.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        [Display(Name = "Letter Grade")]
        public string LetterGrade { get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsGraded { get; set; }
        public DateTime SubmissionCreateDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? GradedDate { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public ICollection<SubmissionFile> SubmissionFiles { get; set; }

        [NotMapped]
        public string SubmissionDateHumanized => SubmissionCreateDate.Humanize();
        [NotMapped]
        public string SubmittedDateHumanized => SubmittedDate.Humanize();
        [NotMapped]
        public string GradedDateHumanized => GradedDate.Humanize();

        public Submission SetSubmissionAndDate(Submission model)
        {
            model.IsSubmitted = true;
            model.SubmittedDate = DateTime.UtcNow;

            return model;
        }

        public Submission SetSubGrade(Submission modelToUpdate, Submission updatedModel)
        {
            modelToUpdate.Grade = updatedModel.Grade;
            modelToUpdate.LetterGrade = updatedModel.LetterGrade;
            modelToUpdate.GradedDate = DateTime.UtcNow;
            modelToUpdate.IsGraded = true;

            return updatedModel;
        }
    }
}