namespace Sandy.Models.DomainModels
{
    public class Golfer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float HandicapIndex
        {
            get { return (float)HandicapIndex; }
            set
            {
                var scoreList = Scores;
                scoreList.OrderBy(score => score.Differential);
                if (scoreList.Count > 0 && scoreList.Count <= 3)
                {
                    HandicapIndex = Scores.Min().Differential - 2;
                }
                if (scoreList.Count == 4)
                {
                    HandicapIndex = Scores.Min().Differential - 1;
                }
                if (scoreList.Count == 5)
                {
                    HandicapIndex = Scores.Min().Differential;
                }
                if (scoreList.Count == 6)
                {
                    HandicapIndex = (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential) / 2 - 1;
                }
                if (scoreList.Count == 7 || scoreList.Count == 8)
                {
                    HandicapIndex = (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential) / 2;
                }
                if (scoreList.Count >= 9 || scoreList.Count <= 11)
                {
                    HandicapIndex = (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential + scoreList.ElementAt(2).Differential) / 3;
                }
                if (scoreList.Count >= 12 || scoreList.Count <= 14)
                {
                    HandicapIndex = (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential
                        + scoreList.ElementAt(2).Differential + scoreList.ElementAt(3).Differential) / 4;
                }
                if (scoreList.Count == 15 || scoreList.Count == 16)
                {
                    HandicapIndex = (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential
                        + scoreList.ElementAt(2).Differential + scoreList.ElementAt(3).Differential
                        + scoreList.ElementAt(4).Differential) / 5;
                }
                if (scoreList.Count == 17 || scoreList.Count == 18)
                {
                    HandicapIndex = (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential
                        + scoreList.ElementAt(2).Differential + scoreList.ElementAt(3).Differential
                        + scoreList.ElementAt(4).Differential + scoreList.ElementAt(5).Differential) / 6;
                }
                if (scoreList.Count == 19)
                {
                    HandicapIndex = (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential
                        + scoreList.ElementAt(2).Differential + scoreList.ElementAt(3).Differential
                        + scoreList.ElementAt(4).Differential + scoreList.ElementAt(5).Differential
                        + scoreList.ElementAt(6).Differential) / 7;
                }
                if (scoreList.Count >= 20)
                {
                    HandicapIndex = (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential
                        + scoreList.ElementAt(2).Differential + scoreList.ElementAt(3).Differential
                        + scoreList.ElementAt(4).Differential + scoreList.ElementAt(5).Differential
                        + scoreList.ElementAt(6).Differential + scoreList.ElementAt(7).Differential) / 8;
                }
            }
        }
        public string HomeCourse { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}
