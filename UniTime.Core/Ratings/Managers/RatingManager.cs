﻿using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.Ratings.Managers
{
    public class RatingManager : IRatingManager
    {
        private readonly IRepository<Rating, Guid> _ratingRepository;

        public RatingManager(
            IRepository<Rating, Guid> ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<Rating> GetAsync(Guid id)
        {
            var rating = await _ratingRepository.FirstOrDefaultAsync(id);

            if (rating == null) throw new UserFriendlyException("The rating with id = " + id + " does not exist.");

            return rating;
        }

        public async Task<Rating> CreateAsync(Rating rating)
        {
            rating.Id = await _ratingRepository.InsertAndGetIdAsync(rating);

            return rating;
        }
    }
}