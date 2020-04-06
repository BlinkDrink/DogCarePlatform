﻿namespace DogCarePlatform.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DogCarePlatform.Common;
    using DogCarePlatform.Data.Common.Repositories;
    using DogCarePlatform.Data.Models;
    using DogCarePlatform.Services.Data;
    using DogCarePlatform.Web.Utilities;
    using DogCarePlatform.Web.ViewModels.Owner;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Owner")]
    public class OwnerController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IDogsittersService dogsitterService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOwnersService ownerService;

        public OwnerController(UserManager<ApplicationUser> userManager, IOwnersService ownerService, IUsersService usersService, IDogsittersService dogsitterService)
        {
            this.userManager = userManager;
            this.ownerService = ownerService;
            this.usersService = usersService;
            this.dogsitterService = dogsitterService;
        }

        public IActionResult AddInfo()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddInfo(AddInfoInputModel input)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    return this.View(input);
            //}

            //var user = await this.userManager.GetUserAsync(this.User);
            //await this.ownerService.AddPersonalInfoAsync(input.Address, input.FirstName, input.MiddleName, input.LastName, input.Gender,  input.ImageUrl, input.PhoneNumber, user.Id, input.Description);

            //return this.Redirect("/");

            throw new NotImplementedException();
        }

        public async Task<IActionResult> FindDogsitter()
        {
            var dogsitters = await this.userManager.GetUsersInRoleAsync(GlobalConstants.DogsitterRoleName);
            dogsitters.OrderBy(a => a.Dogsitters);

            var viewModel = new ListDogsittersViewModel
            {
                Dogsitters = this.ownerService.GetDogsittersAsync(dogsitters),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> SendRequestToDogsitter() 
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> DogsitterDetails(string id)
        {
            var dogsitterViewModel = this.ownerService.DogsitterDetailsById<DogsitterInfoViewModel>(id);

            return this.View(dogsitterViewModel);
        }
    }
}