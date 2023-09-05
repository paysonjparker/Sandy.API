namespace Sandy.Models.DataTransferObjects.ScoreDto
{
    public class AddScoreRequest
    {
        public int Total { get; set; }
        public float Differential { get; set; }
        public Guid GolferId { get; set; }
    }
}
