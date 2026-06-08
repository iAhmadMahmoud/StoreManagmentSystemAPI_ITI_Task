using Microsoft.AspNetCore.Mvc;
using Store.BLL;
using Store.Common;

namespace Store.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<GetProductDtos>>>> GetAllAsync()
        {
            var prod = await _productManager.GetProductsAsync();
            return Ok(prod);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<GetProductDtos>>> GetProductByIdAsync([FromRoute] int id)
        {
            var prod = await _productManager.GetProductByIdAsync(id);
            if (!prod.Success)
            {
                return NotFound(prod);
            }
            return Ok(prod);    
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResult<CreateProductDtos>>> CreateAsync([FromBody]CreateProductDtos productDtos)
        {
            var add = await _productManager.InsertAsync(productDtos);
            if (!add.Success)
            {
                return BadRequest(add);
            }
            return CreatedAtAction("GetProductById", new {id = add.Data!.Id},add);
        }

        [HttpPut]
        public async Task<ActionResult<GeneralResult<EditProductDtos>>> EditAsync([FromBody] EditProductDtos productDtos)
        {
            var prod = await _productManager.EditAsync(productDtos);
            if (!prod.Success)
            {
                return BadRequest(prod);
            }
            return Ok(prod);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralResult>> DeleteAsync([FromRoute] int id)
        {
            var res = await _productManager.DeleteAsync(id);
            if (!res.Success)
            {
                return NotFound(res);
            }
            return Ok(res);
        }

    }
}
