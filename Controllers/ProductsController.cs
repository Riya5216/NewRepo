using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> logger;
        public ProductsController(IProductService _productService)
        {
            this.productService = _productService;
        }

        private readonly  Shopbridge_Context _context;

        public ProductsController(Shopbridge_Context context)
        {
            _context = context;
        }
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            try
            {
                var allProductList = _context.Product.Where(x=>x.Product_Stock == 1).ToList();
                if (allProductList.Count > 0)
                {
                    return Ok(allProductList);
                }
                else
                {
                    return Ok("There is no product in the database");
                }
            }
            catch(Exception exe)
            {
                return BadRequest(exe);
            }

        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = _context.Product.FirstOrDefault(x => x.Product_Id == id && x.Product_Stock == 1);
                if (product!= null)
                {
                    return Ok(product);
                }
                else
                {
                    return Ok("No product found in the database with id= "+ id);
                }
            }
            catch (Exception exe)
            {
                return BadRequest(exe);
            }
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            try
            {
                var productDetail = _context.Product.FirstOrDefault(x => x.Product_Id == id && x.Product_Stock == 1);
                if (productDetail!= null)
                {
                    productDetail.Product_Name = product.Product_Name;
                    productDetail.Product_Brand = product.Product_Brand;
                    productDetail.Product_Fabric = product.Product_Fabric;
                    productDetail.Product_Price = product.Product_Price;
                    productDetail.Product_Stock = 1;
                    _context.SaveChanges();
                    return Ok("Product are updated with given details of" + id);
                }
                else
                {
                    Product pr = new Product();
                    pr.Product_Name = product.Product_Name;
                    pr.Product_Brand = product.Product_Brand;
                    pr.Product_Fabric = product.Product_Fabric;
                    pr.Product_Price = product.Product_Price;
                    pr.Product_Stock = 1;
                    _context.Add(pr);
                    _context.SaveChanges();
                    return Ok("New Product has been  inserted with given details of" + id);
                }
            }
            catch (Exception exe)
            {
                return BadRequest(exe);
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                var productDetail = _context.Product.FirstOrDefault(x => x.Product_Id == product.Product_Id && x.Product_Stock == 1);
                if (productDetail == null)
                {
                    Product pr = new Product();
                    pr.Product_Name = product.Product_Name;
                    pr.Product_Brand = product.Product_Brand;
                    pr.Product_Fabric = product.Product_Fabric;
                    pr.Product_Price = product.Product_Price;
                    pr.Product_Stock = 1;
                    _context.Add(pr);
                    _context.SaveChanges();
                    return Ok("New Product has been  inserted with id= " + product.Product_Id);
                }
                return Ok("New Product has been  inserted with id= " + product.Product_Id);
            }
            catch (Exception exe)
            {
                return BadRequest(exe);
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = _context.Product.Where(x => x.Product_Id == id).ToList();
                if (product.Count > 0)
                {
                    Product pr = new Product();
                    pr.Product_Stock = 0;
                   _context.SaveChanges();
                    return Ok("Product are currently not in stock with id= "+id);
                }
                else
                {
                    return Ok("No product found in the database with id= " + id);
                }
            }
            catch (Exception exe)
            {
                return BadRequest(exe);
            }

        }

    }
}
