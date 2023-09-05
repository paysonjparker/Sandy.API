using Sandy.Data;
using Sandy.Models.DataTransferObjects.ScoreDto;

namespace Sandy.Business
{
    public class ScoreService
    {
        private readonly SandyDbContext _dbContext;

        public ScoreService(SandyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Score AddScore(AddScoreRequest addScoreRequest)
        {
            // Convert DTO to Domain Model
            var score = new Score
            {
                Total = addScoreRequest.Total,
                Differential = addScoreRequest.Differential,
                GolferId = addScoreRequest.GolferId,
            };

            _dbContext.Scores.Add(score);
            _dbContext.SaveChanges();

            return score;
        }

        public List<Score> GetAllScoresByGolfer(Guid golferId)
        {

            var scores = _dbContext.Scores.ToList();

            var scoresDTO = new List<Score>();
            foreach (var score in scores)
            {
                if (score.GolferId == golferId)
                {
                    scoresDTO.Add(new Score
                    {
                        Id = score.Id,
                        Total = score.Total,
                        Differential = score.Differential,
                        GolferId = score.GolferId,
                    });
                }
            }

            return scoresDTO;
        }

        public bool DeleteScore(Guid id)
        {
            var existingScore = _dbContext.Scores.Find(id);

            if (existingScore != null)
            {
                _dbContext.Scores.Remove(existingScore);
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
