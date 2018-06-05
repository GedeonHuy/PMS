namespace PMS.Resources
{
    public class RecommendationResource
    {
        public int RecommendationId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDone { get; set; }
        public int? BoardEnrollmentId { get; set; }
        public BoardEnrollmentResource BoardEnrollmentResource { get; set; }
        public RecommendationResource()
        {
            IsDone = false;
            IsDeleted = false;
        }
    }
}