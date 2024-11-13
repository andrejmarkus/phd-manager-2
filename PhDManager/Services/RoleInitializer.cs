using Microsoft.AspNetCore.Identity;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services
{
    public class RoleInitializer(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task InitializeAsync()
        {
            string[] roles = { "Admin", "User", "Teacher", "Officer", "Vicedean", "Graduant", "Reviewer" };

            foreach (string role in roles)
            {
                if (await _roleManager.FindByNameAsync(role) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Temporary just for testing
            if ((await _unitOfWork.StudyPrograms.GetByCodeAsync("INF")) is null)
            {
                await _unitOfWork.StudyPrograms.AddAsync(new Models.StudyProgram { Code = "INF", Name = "Informatika" });
            }
            if ((await _unitOfWork.StudyPrograms.GetByCodeAsync("MAN")) is null)
            {
                await _unitOfWork.StudyPrograms.AddAsync(new Models.StudyProgram { Code = "MAN", Name = "Manažment" });
            }
            await _unitOfWork.CompleteAsync();
        }
    }
}
