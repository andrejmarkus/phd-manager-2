namespace PhDManager.Models
{
    public class SystemState : BaseModel
    {
        public bool IsOpen { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
