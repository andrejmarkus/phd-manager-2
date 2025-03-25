namespace PhDManager.Models.Roles
{
    public class ExternalTeacher : Teacher
    {
        public new const string Role = "Externý učiteľ";

        public virtual List<Comment> Comments { get; set; } = [];
    }
}
