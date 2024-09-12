using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DTOs;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.IRepositories;
using ProSoft.EF.Models;
using System.Xml;

namespace ProSoft.UI.Controllers
{
    [Authorize]
    public class PriceController : Controller
    {
        private readonly IPriceRepo _priceRepo;

        public PriceController(IPriceRepo priceRepo)
        {
            _priceRepo = priceRepo;
        }

        public async Task<IActionResult> Index()
        {
            var priceList = await _priceRepo.GetAllAsync();
            PriceDTO priceDTO = new()
            {
                Prices = priceList
            };
            return View(priceDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePrice(PriceDTO model)
        {
            var price = await _priceRepo.GetByIdAsync(model.Id);

            if (price != null)
            {
                Console.WriteLine("sss" + price.Pricee);
                Console.WriteLine("sss" + model.Pricee);

                price.Name = model.Name;
                price.Pricee = model.Pricee;

                await _priceRepo.UpdateAsync(price);
                await _priceRepo.SaveChangesAsync();
                return Ok(new { success = true, message = "User updated successfully" });
            }

            return BadRequest(new { success = false, message = "User not found" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateRecords([FromBody] List<PriceDTO> records)
        {
            try
            {

                foreach (var record in records)
                {
                    var product = await _priceRepo.GetByIdAsync(record.Id);
                    if (product != null)
                    {
                        product.Name = record.Name;
                        product.Pricee = record.Pricee;
                        // Update other fields as needed
                    }
                }

                await _priceRepo.SaveChangesAsync();


                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete([FromBody] PriceDTO model)
        {
            Console.WriteLine(model.Id);
            try
            { 
                var product = await _priceRepo.GetByIdAsync(model.Id);
                if (product == null)
                {
                    return Json(new { success = false, message = "Product not found" });
                }
                await _priceRepo.DeleteAsync(product);
                await _priceRepo.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SaveRecords([FromBody] PriceRecordsDTO payload)
        {
            if (payload == null)
            {
                return BadRequest("No data received.");
            }

            try
            {
                Console.WriteLine(payload.InsertData[0].Name);

                // Insert new records
                if (payload.InsertData != null && payload.InsertData.Any())
                {
                    foreach (var record in payload.InsertData)
                    {
                        // Map DTO to entity and insert
                        var newEntity = new Price
                        {
                            Name = record.Name,
                            Pricee = record.Pricee,
                            // Map other fields...
                        };
                        await _priceRepo.AddAsync(newEntity);

                    }
                    await _priceRepo.SaveChangesAsync();

                }

                // Update existing records
                if (payload.UpdateData != null && payload.UpdateData.Any())
                {
                    foreach (var record in payload.UpdateData)
                    {
                        var existingEntity = await _priceRepo.GetByIdAsync(record.Id);
                        if (existingEntity != null)
                        {
                            existingEntity.Name = record.Name;
                            existingEntity.Pricee = record.Pricee;
                            await _priceRepo.UpdateAsync(existingEntity);
                        }
                       

                    }
                    await _priceRepo.SaveChangesAsync();

                }

                return Ok(new { message = "Records saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while saving records.");
            }
        }

    }
}
