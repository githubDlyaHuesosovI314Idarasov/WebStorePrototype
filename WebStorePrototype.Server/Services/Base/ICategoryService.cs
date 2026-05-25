using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebStorePrototype.Server.Services.Base
{
    public interface ICategoryService
    {
        Task<ActionResult<Category?>> GetAsync(Guid id);
        Task<ActionResult<IEnumerable<Category>>> GetAllAsync();
        Task<IActionResult> Create(Category category);
        Task<IActionResult> Update(Category category);
        Task<IActionResult> Delete(Guid id);
    }
}
