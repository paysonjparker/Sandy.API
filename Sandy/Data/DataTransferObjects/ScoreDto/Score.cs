namespace Sandy.Data.DataTransferObjects.ScoreDto
{
    public class Score
    {
        public Guid Id { get; set; }
        public int Total { get; set; }
        public float Differential { get; set; }
        public Guid GolferId { get; set; }
    }
}
