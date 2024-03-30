using System;

namespace QuizAppSystem.DTOs
{
    public class ExamReportDto
    {
        public Guid Id { get; set; }
        public Guid ExamId { get; set; }
        public string ParticipantId { get; set; }
        public string ParticipantFirstName { get; set; }
        public string ParticipantLastName { get; set; }
        public string ParticipantEmail { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Score { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
