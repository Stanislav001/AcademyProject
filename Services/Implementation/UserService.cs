using Date;
using Microsoft.AspNetCore.Hosting;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class UserService : IUserService
    {
        private const string IMAGE_FOLDER_NAME = "/ImageForUser";
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;

        public UserService(ApplicationDbContext dbContext,
            IWebHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
        }

        public UserViewModel GetDetailsById(string id)
        {
            UserViewModel user = this.dbContext.Users
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    Country = user.Country,
                    Email = user.Email,
                    UserName = user.UserName,
                    ImageFile = user.ImageFile,
                    ImageName = user.ImageName,
                    Profession = user.Profession
                }).SingleOrDefault(user => user.Id == id);

            return user;
        }

        public UserViewModel UpdateById(string id)
        {
            UserViewModel user = this.dbContext.Users
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    Country = user.Country,
                    Email = user.Email,
                    ImageFile = user.ImageFile,
                    ImageName = user.ImageName,
                    UserName = user.UserName,
                    Profession = user.Profession
                }).SingleOrDefault(user => user.Id == id);

            return user;
        }


        public async Task UpdateAsync(UserViewModel model)
        {
            User user = this.dbContext.Users.Find(model.Id);

            bool isUserNull = user == null;
            if (isUserNull)
            {
                return;
            }

            user.UserName = model.UserName;
            user.Country = user.Country;
            user.Email = user.Email;
            user.Id = model.Id;
            user.ImageName = model.ImageName;
            user.ImageFile = model.ImageFile;
            user.Profession = model.Profession;

            if (user.ImageName != null && user.ImageName == null)
            {
                model.ImageName = user.ImageName;
            }
            if (model.ImageFile != null)
            {
                await SetImage(user);
            }

            this.dbContext.Users.Update(user);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task SetImage(User user)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
            string exension = Path.GetExtension(user.ImageFile.FileName);
            user.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + exension;
            string path = Path.Combine(wwwRootPath + IMAGE_FOLDER_NAME, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await user.ImageFile.CopyToAsync(fileStream);
            }
        }
    }
}
