using Microsoft.AspNetCore.Mvc;
using Store.BLL;
using Store.Common;

namespace Store.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryManager categoryManager, ILogger<CategoryController> logger)
        {
            _categoryManager = categoryManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<GetCategoryDtos>>>> GetAllAsync()
        {
            _logger.LogInformation("Start Fetching all Category");
            var res = await _categoryManager.GetCategoriesAsync();
            _logger.LogInformation("Successfull Fetching All Category");
            return Ok(res);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<GetCategoryDtos>>> GetByIdAsync([FromRoute] int id)
        {
            _logger.LogInformation($"Fetching Category details for ID: {id}");
            var res = await _categoryManager.GetCategoryByIdAsync(id);
            if(!res.Success)
            {
                _logger.LogError($"An error occurred while fetching Category with ID: {id}");
                return NotFound();
            }
            _logger.LogInformation($"Successfully retrieved Category details for ID: {id}");
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResult<GetCategoryDtos>>> CreateAsync([FromBody] CreateCategoryDtos catDto)
        {
            var cat = await _categoryManager.InsertAsync(catDto);
            if (!cat.Success)
            {
                return BadRequest(cat);
            }
            return Ok(cat);

        }

        [HttpPut]
        public async Task<ActionResult<GeneralResult<GetCategoryDtos>>> UpdateAsync([FromBody] GetCategoryDtos catDto)
        {
            var cat = await _categoryManager.GetCategoryByIdAsync(catDto.Id);
            if (!cat.Success)
            {
                return BadRequest(cat);
            }
            return Ok(cat);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralResult>> DeleteAsync([FromRoute]int id)
        {
            var cat = await _categoryManager.DeleteAsync(id);
            if (!cat.Success)
            {
                return NotFound();
            }
            return Ok(cat);
        }
    }
}
