﻿using Microsoft.AspNetCore.Mvc;
using EShop.Application.Service;
using EShop.Domain.Models;
using System.Threading.Tasks;

namespace EShopService.Controllers             //poprawic tego controllera jeszcze!
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //podlaczenie serwisu do controllera
        //[HttpGet("{id}")]
        //public async Task<ActionResult>Get(int id)
        //{
        //    var result = await _productService.GetAsync(id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(result);
        //}
    }
}
