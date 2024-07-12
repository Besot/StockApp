using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select( S => S.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stocks = await _stockRepo.GetByIdAsync(id);
            if(stocks == null)
            {
                return NotFound();
            }
            return Ok(stocks.ToStockDto());
        }


        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto createStockRequestDto)
        {
            var stockModel = createStockRequestDto.ToStockFromCreateStockDto();
            await _stockRepo.CreateAsync(stockModel); 
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }


       [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
    {
       
        var stockModel = await _stockRepo.UpdateAsync(id, updateStockRequestDto);
        if (stockModel == null)
        {
            return NotFound(); // Return a 404 error if the stock resource is not found
        }
        return Ok(stockModel.ToStockDto());
       
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteById([FromBody] int id)
    {
        var stockModel = await _stockRepo.DeleteAsync(id);
        if(stockModel == null)
        {
            return NotFound();
        }
        return NoContent();
       
    }
}
}
