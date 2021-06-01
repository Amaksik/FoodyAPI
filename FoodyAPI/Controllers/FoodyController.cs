using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodyAPI.Models;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using FoodyAPI.Clients;
using FoodyAPI.Data;
using System.Text.Json;

namespace FoodyAPI.Controllers
{
    [ApiController]
    [Route("api")]

    public class FoodyController : Controller
    {
        private static DbController _dbController;
        public FoodyController(DbController dbController)
        {
            _dbController = dbController;
        }


        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var result = await _dbController.GetAllUsers() ;
            return result;
        }



        //meal recognition without proper user info
        [HttpPost("recognize")]
        public IActionResult Upload()
        {
            PhotoHandling ph = new PhotoHandling();
            try
            {
                var file = Request.Form.Files[0];

                var message = ph.FileUpload(file).Result;
                if (message != "notOk")
                {
                    return Ok(message);
                }
                else
                {
                    return BadRequest();
                }
                
                

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }


        }




        //meal info by name
        [HttpGet("100g/{name}")]
        public async Task<IActionResult> Consume100Info([FromQuery] string name)
        {
            if (name != null)
            {
                var client = new BarCodeClient();
                try
                {
                    var result = await client.Consume100Info(name);
                    var _string = System.Text.Json.JsonSerializer.Serialize(result);
                    return Ok(_string);
                }
                catch (Exception)
                {

                    return BadRequest("couldn't recognize it");
                }
            }
            else
            {
                return BadRequest("info not provided");
            }

        }




        //consuming 
        [HttpPost("natural/{query}")]
        public async Task<IActionResult> NaturalInfo(string query)
        {
            if (query != null)
            {
                var client = new BarCodeClient();
                try
                {
                    var result = await client.GetNaturalInfo(query);
                    var _string = System.Text.Json.JsonSerializer.Serialize(result);
                    return Ok(_string);
                }
                catch
                {

                    return BadRequest("couldn't recognize it");
                }
                
                

            }
            else
            {
                return BadRequest("no info provided");
            }

        }



        //info by barcode
        [HttpGet("barcode")]
        public async Task<IActionResult> BarcodeInfo([FromQuery] string barcode)
        {
            if (barcode  != null)
            {
                var client = new BarCodeClient();
                var result = await client.GetBarcodeInfo(barcode);
                var _string = System.Text.Json.JsonSerializer.Serialize(result);
                return Ok(_string);
               
            }
            else
            {
                return BadRequest("barcode not provided");
            }

        }



        //getting user info by id
        [HttpGet("{user_id}")]
        public async Task<IActionResult> GetUser(string user_id)
        {
            if (user_id != null)
            {
                var findet = await _dbController.GetUserById(user_id);
                if (findet != null)
                {
                    return Ok(JsonSerializer.Serialize(findet));
                }
                else
                {
                    return NotFound("user not found");
                }
            }
            else
            {
                return BadRequest("id not provided");
            }

        }



        ////adding users
        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user.ID != null && user.callories >= 300)
            {

                var findet = await _dbController.GetUserById(user.ID);
                if (findet == null)
                {

                    var result = await _dbController.AddUser(user);
                    return Ok("user has been added");
                }
                else
                {
                    return BadRequest("user already exist");
                }

            }
            else
            {
                return BadRequest("not enough info");
            }
        }



        //Getting Statistics of user per days
        [HttpGet("{user_id}/statistics/{days}")]
        public async Task<IActionResult> GetStatistics(string user_id, int days)
        {
            if (user_id != null)
            {
                var findet = await _dbController.GetUserById(user_id);
                if (findet != null && findet.Favourite.Count > 0)
                {
                    List<DayIntake> result = new List<DayIntake>();

                    foreach (var item in findet.Statistics)
                    {
                        result.Add(item);
                    }
                    if (days > result.Count)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        List<DayIntake> resulttosend = new List<DayIntake>();
                        for (int i = 0; i < days; i++)
                        {
                            resulttosend.Add(result[0]);
                        }
                        return Ok(JsonSerializer.Serialize( resulttosend));

                    }


                }
                else if (findet != null && findet.Favourite.Count == 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("user not found");
                }
            }
            else
            {
                return BadRequest("no id");
            }

        }



        //getting list of Favourite users products
        [HttpGet("{user_id}/favourite")]
        public async Task<IActionResult> Favourite(string user_id)
        {
            if (user_id != null)
            {
                var findet = await _dbController.GetUserById(user_id);
                if (findet != null && findet.Favourite.Count>0)
                {
                    List<Product> result = new List<Product>();

                    foreach (var item in findet.Favourite)
                    {
                        result.Add(item);
                    }

                    return Ok(JsonSerializer.Serialize(result)) ;
                }
                else if(findet != null && findet.Favourite.Count == 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("user not found");
                }
            }
            else
            {
                return BadRequest("no id provided");
            }
        }




        //adding product to list of Favourite users products
        [HttpPut("{user_id}/addproduct")]
        public async Task<IActionResult> ProductAdd(string user_id, [FromBody] Product prdct)
        {
            if (user_id != null && prdct.Check())
            {
                await _dbController.AddProduct(user_id, prdct);
                return Ok("product has been added");
            }
            else
            {
                return BadRequest();
            }
            
        }




        //adding product to list of Favourite users products
        [HttpPost("{user_id}/consume")]
        public async Task<IActionResult> Consume(string user_id, [FromBody] Consume _new)
        {

                await _dbController.ConsumeProduct(user_id, _new.callories, DateTime.Today);
                return Ok("consumed");

        }




        //removing product from list of Favourite users products
        [HttpDelete("{user_id}/removeproduct")]
        public async Task<IActionResult> ProductRemove(string user_id, [FromBody] Product prdct)
        {
            if (user_id != null && prdct.Check())
            {
                await _dbController.RemoveProduct(user_id, prdct);
                return Ok("product has been added");
            }
            else
            {
                return BadRequest();
            }
        }
        
        


        //removing users
        [HttpDelete("{user_id}/removeuser")]
        public async  Task<IActionResult> RemoveUser(string user_id)
        {
            if (user_id != null)
            {
                var findet = await _dbController.GetUserById(user_id);
                if (findet != null)
                {
                    await _dbController.RemoveUser(user_id);
                    return Ok("user has been removed");
                }
                else
                {
                    return NotFound("user not found");
                }
            }
            else
            {
                return BadRequest("id not provided");
            }

        }


    }
}