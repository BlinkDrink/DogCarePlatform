﻿namespace DogCarePlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using DogCarePlatform.Data.Models;
    using DogCarePlatform.Web.ViewModels.Owner;

    public interface IOwnersService
    {
        Task AddPersonalInfoAsync(string address, string firstName, string middleName, string lastName, Gender gender, string imageUrl, string phoneNumber, string userId, string dogsDescription);
        Owner GetOwnerById(string id);
        Task UpdateCurrentLoggedInUserInfoAsync(string id, string firstName, string middleName, string lastName, string address, string description, string imageUrl);
    }
}
