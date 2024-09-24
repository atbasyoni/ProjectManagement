namespace ProjectsManagement.ViewModels.RabbitMQMessages
{
    public enum MessageType
    {
        Instructor,
        Course,
        Exam
    }

    public class BaseMessage
    {
        public DateTime SentDate { get; set; }

        public string Sender { get; set; }
        public string Action { get; set; }

        public virtual string Type { get; set; }
    }
}
