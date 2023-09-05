﻿using Sandy.Data;
using Sandy.Models.DataTransferObjects.GolferDto;
using Sandy.Models.DataTransferObjects.ScoreDto;

namespace Sandy.Business
{
    public class GolferService
    {
        private readonly SandyDbContext _dbContext;

        public GolferService(SandyDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public Golfer AddGolfer(AddGolferRequest request)
        {
            var golfer = new Golfer
            {
                Name = request.Name,
                HomeCourse = request.HomeCourse,
            };

            _dbContext.Golfers.Add(golfer);
            _dbContext.SaveChanges();

            return golfer;
        }

        public List<Golfer> GetAllGolfers()
        {
            var scoreService = new ScoreService(_dbContext);

            var golfers = _dbContext.Golfers.ToList();

            var golfersDTO = new List<Golfer>();
            foreach (var golfer in golfers)
            {
                golfersDTO.Add(new Golfer
                {
                    Id = golfer.Id,
                    Name = golfer.Name,
                    HandicapIndex = CalculateHandicapIndex(golfer.Id),
                    HomeCourse = golfer.HomeCourse,
                    Scores = (ICollection<Models.DomainModels.Score>)scoreService.GetAllScoresByGolfer(golfer.Id),
                });
            }

            return golfersDTO;
        }

        public Golfer GetGolferById(Guid id)
        {
            var scoreService = new ScoreService(_dbContext);

            var golferDomainObject = _dbContext.Golfers.Find(id);

            if (golferDomainObject != null)
            {
                var golferDTO = new Golfer
                {
                    Id = golferDomainObject.Id,
                    Name = golferDomainObject.Name,
                    HandicapIndex = CalculateHandicapIndex(golferDomainObject.Id),
                    HomeCourse = golferDomainObject.HomeCourse,
                    Scores = (ICollection<Models.DomainModels.Score>)scoreService.GetAllScoresByGolfer(golferDomainObject.Id),
                };

                return golferDTO;
            }

            return null;
        }

        public Golfer UpdateGolfer(Guid id, UpdateGolferRequest updateGolferRequest)
        {
            var exisitngGolfer = _dbContext.Golfers.Find(id);

            if (exisitngGolfer != null)
            {
                exisitngGolfer.Name = updateGolferRequest.Name;
                exisitngGolfer.HomeCourse = updateGolferRequest.HomeCourse;

                _dbContext.SaveChanges();
                return exisitngGolfer;
            }

            return null;
        }

        public bool DeleteGolfer(Guid id)
        {
            var existingGolfer = _dbContext.Golfers.Find(id);

            if (existingGolfer != null)
            {
                _dbContext.Golfers.Remove(existingGolfer);
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public float CalculateHandicapIndex(Guid golferId)
        {
            var scoreService = new ScoreService(_dbContext);

            var scoreList = scoreService.GetAllScoresByGolfer(golferId);
            scoreList.OrderBy(score => score.Differential);
            if (scoreList.Count > 0 && scoreList.Count <= 3)
            {
                return scoreList.Min().Differential - 2;
            }
            if (scoreList.Count == 4)
            {
                return scoreList.Min().Differential - 1;
            }
            if (scoreList.Count == 5)
            {
                return scoreList.Min().Differential;
            }
            if (scoreList.Count == 6)
            {
                return (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential) / 2 - 1;
            }
            if (scoreList.Count == 7 || scoreList.Count == 8)
            {
                return (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential) / 2;
            }
            if (scoreList.Count >= 9 || scoreList.Count <= 11)
            {
                return (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential + scoreList.ElementAt(2).Differential) / 3;
            }
            if (scoreList.Count >= 12 || scoreList.Count <= 14)
            {
                return (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential
                    + scoreList.ElementAt(2).Differential + scoreList.ElementAt(3).Differential) / 4;
            }
            if (scoreList.Count == 15 || scoreList.Count == 16)
            {
                return (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential
                    + scoreList.ElementAt(2).Differential + scoreList.ElementAt(3).Differential
                    + scoreList.ElementAt(4).Differential) / 5;
            }
            if (scoreList.Count == 17 || scoreList.Count == 18)
            {
                return (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential
                    + scoreList.ElementAt(2).Differential + scoreList.ElementAt(3).Differential
                    + scoreList.ElementAt(4).Differential + scoreList.ElementAt(5).Differential) / 6;
            }
            if (scoreList.Count == 19)
            {
                return (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential
                    + scoreList.ElementAt(2).Differential + scoreList.ElementAt(3).Differential
                    + scoreList.ElementAt(4).Differential + scoreList.ElementAt(5).Differential
                    + scoreList.ElementAt(6).Differential) / 7;
            }
            if (scoreList.Count >= 20)
            {
                return (scoreList.ElementAt(0).Differential + scoreList.ElementAt(1).Differential
                    + scoreList.ElementAt(2).Differential + scoreList.ElementAt(3).Differential
                    + scoreList.ElementAt(4).Differential + scoreList.ElementAt(5).Differential
                    + scoreList.ElementAt(6).Differential + scoreList.ElementAt(7).Differential) / 8;
            }
            return 0;
        }
    }
}