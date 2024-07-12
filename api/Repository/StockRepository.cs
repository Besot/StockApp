
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository

    {
            private readonly ApplicationDBContext _context;
            public StockRepository(ApplicationDBContext context)
            {
                _context = context; 
            }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
             var stockModel = await _context.Stocks.FirstOrDefaultAsync( x => x.Id == id);
        if(stockModel == null)
        {
            return null;
        }
        _context.Remove(stockModel);

        try
        {
        await _context.SaveChangesAsync ();

        return stockModel;
        }
        catch (Exception)
            {
            Console.WriteLine("An error occurred while updating the stock resource.");
            return null; 
        }
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
           return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRequestDto)
        {
            if (updateStockRequestDto == null)
        {
            return null;
        }

        // Find the stock resource with the specified ID
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null)
        {
            return null; // Return a 404 error if the stock resource is not found
        }

        // Update the stock resource with the new data
        stockModel.Symbol = updateStockRequestDto.Symbol;
        stockModel.CompanyName = updateStockRequestDto.CompanyName;
        stockModel.Purchase = updateStockRequestDto.Purchase;
        stockModel.LastDiv = updateStockRequestDto.LastDiv;
        stockModel.Industry = updateStockRequestDto.Industry;
        stockModel.MarketCap = updateStockRequestDto.MarketCap;

        try
        {
            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Return the updated stock model as a DTO
            return stockModel;
        }
        catch (Exception ex)
        {
            // Log the exception and return a server error response
            // You can replace this with your logging infrastructure
            Console.WriteLine(ex.Message);
            return null;
        }
        }
    }
}