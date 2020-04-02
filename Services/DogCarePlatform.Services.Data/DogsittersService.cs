﻿using DogCarePlatform.Data.Common.Repositories;
using DogCarePlatform.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogCarePlatform.Services.Data
{
    public class DogsittersService : IDogsittersService
    {
        private readonly IDeletableEntityRepository<Dogsitter> dogsitterRepository;

        public DogsittersService(IDeletableEntityRepository<Dogsitter> dogsitterRepository)
        {
            this.dogsitterRepository = dogsitterRepository;
        }

        public async Task CurrentUserAddInfo(string userId, string firstName, string middleName, string lastName, DateTime dateOfBirth, Gender gender, string address, string description, string imageUrl)
        {
            var dogsitter = this.dogsitterRepository.All().Where(d => d.UserId == userId).FirstOrDefault();

            dogsitter.FirstName = firstName;
            dogsitter.MiddleName = middleName;
            dogsitter.LastName = lastName;
            dogsitter.DateOfBirth = dateOfBirth;
            dogsitter.Gender = gender;
            dogsitter.Address = address;
            dogsitter.Description = description;
            dogsitter.ImageUrl = imageUrl;

            await this.dogsitterRepository.SaveChangesAsync();
        }

        public Dogsitter GetDogsitterById(string id)
        {
            return this.dogsitterRepository.All().Where(d => d.UserId == id).FirstOrDefault();
        }
    }
}
