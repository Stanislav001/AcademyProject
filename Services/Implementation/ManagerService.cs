using Date;
using Microsoft.AspNetCore.Hosting;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ManagerService : IManagerService
    {
        private const string IMAGE_FOLDER_NAME = "/ImageForManager";
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;

        public ManagerService(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
        }

        public IEnumerable<ManagerViewModel> GetAll()
        {
            IEnumerable<ManagerViewModel> managers = this.dbContext.Managers
                .Select(manager => new ManagerViewModel
                {
                    Id = manager.Id,
                    FirstName = manager.FirstName,
                    SecondName = manager.SecondName,
                    LastName = manager.LastName,
                    Email = manager.Email,
                    ImageFile = manager.ImageFile,
                    ImageName = manager.ImageName,
                    PhoneNumber = manager.PhoneNumber,
                    Salary = manager.Salary,
                }).ToList();

            return managers;
        }


        public ManagerViewModel GetDetailsById(string id)
        {
            ManagerViewModel model = this.dbContext.Managers
            .Select(manager => new ManagerViewModel
            {
                Id = manager.Id,
                FirstName = manager.FirstName,
                SecondName = manager.SecondName,
                LastName = manager.LastName,
                PhoneNumber = manager.PhoneNumber,
                Email = manager.Email,
                ImageFile = manager.ImageFile,
                ImageName = manager.ImageName,
                Salary = manager.Salary,
            }).SingleOrDefault(m => m.Id == id);

            return model;
        }

        public IEnumerable<ManagerViewModel> GetByFirstName()
        {
            IEnumerable<ManagerViewModel> managers = this.dbContext.Managers
                .Select(manager => new ManagerViewModel
                {
                    Id = manager.Id,
                    FirstName = manager.FirstName,
                    SecondName = manager.SecondName,
                    LastName = manager.LastName,
                    Salary = manager.Salary,
                    Email = manager.Email,
                    ImageFile = manager.ImageFile,
                    ImageName = manager.ImageName,
                    PhoneNumber = manager.PhoneNumber,

                }).OrderBy(manager => manager.FirstName).ToList();

            return managers;
        }

        public Manager GetByModelFirstName(string firstName)
        {
            Manager managerDb = this.dbContext.Managers
                .SingleOrDefault(manager => manager.FirstName == firstName);

            return managerDb;
        }

        public async Task CreateAsync(ManagerViewModel model)
        {
            Manager manager = new Manager();

            manager.Id = Guid.NewGuid().ToString();
            manager.FirstName = model.FirstName;
            manager.SecondName = model.SecondName;
            manager.LastName = model.LastName;
            manager.Salary = model.Salary;
            manager.PhoneNumber = model.PhoneNumber;
            manager.Email = model.Email;
            manager.ImageName = model.ImageName;
            manager.ImageFile = model.ImageFile;

            if (model.ImageFile != null)
            {
                await SetImage(manager);
            }

            await this.dbContext.Managers.AddAsync(manager);
            await this.dbContext.SaveChangesAsync();
        }

        public ManagerViewModel UpdateById(string id)
        {
            ManagerViewModel manager = this.dbContext.Managers
                .Select(manager => new ManagerViewModel
                {
                    FirstName = manager.FirstName,
                    SecondName = manager.SecondName,
                    LastName = manager.LastName,
                    Salary = manager.Salary,
                    Email = manager.Email,
                    PhoneNumber = manager.PhoneNumber,
                    ImageFile = manager.ImageFile,
                    ImageName = manager.ImageName,
                }).SingleOrDefault(manager => manager.Id == id);

            return manager;
        }

        public async Task UpdateAsync(ManagerViewModel model)
        {
            Manager manager = this.dbContext.Managers.Find(model.Id);

            bool isManagerNull = manager == null;
            if (isManagerNull)
            {
                return;
            }

            manager.FirstName = model.FirstName;
            manager.SecondName = model.SecondName;
            manager.LastName = model.LastName;
            manager.Salary = model.Salary;
            manager.PhoneNumber = model.PhoneNumber;
            manager.Email = model.Email;
            manager.ImageName = model.ImageName;
            manager.ImageFile = model.ImageFile;


            if (manager.ImageName != null)
            {
                model.ImageName = manager.ImageName;
            }
            if (model.ImageFile != null)
            {
                await SetImage(manager);
            }

            this.dbContext.Managers.Update(manager);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Manager manager = new Manager();
            manager = this.dbContext.Managers.Find(id);

            bool isManagerNull = manager == null;
            if (isManagerNull)
            { 
                return;
            }

            this.dbContext.Managers.Remove(manager);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task SetImage(Manager manager)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(manager.ImageFile.FileName);
            string exension = Path.GetExtension(manager.ImageFile.FileName);
            manager.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + exension;
            string path = Path.Combine(wwwRootPath + IMAGE_FOLDER_NAME, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await manager.ImageFile.CopyToAsync(fileStream);
            }
        }
    }
}
