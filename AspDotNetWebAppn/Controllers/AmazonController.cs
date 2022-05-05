using BusinessLayer;
using DomainLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotNetWebAppn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AmazonController : ControllerBase
    {
      

        private readonly ILogger<AmazonController> _logger;
        private readonly ICountriesBusiness _countriesBusiness;

        public AmazonController(ILogger<AmazonController> logger, ICountriesBusiness countriesBusiness)
        {
            _logger = logger;
            _countriesBusiness = countriesBusiness;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json", Type = typeof(IEnumerable<Countries>))]
        [Route("get-all")]
        public async Task<ActionResult<IEnumerable<Countries>>> GetAll()
        {
            try
            {
                var resp = await _countriesBusiness.GetAllAmazonCountries();

                if (resp == null || resp.Count == 0)
                {
                    return StatusCode(404, "No Data available.");
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("insert-Countries")]
        public async Task<ActionResult> InsertAmazonCountry([FromBody] Countries countries)
        {
            try
            {
                await _countriesBusiness.InsertAmazonCountry(countries);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("delete-countries")]
        public async Task<ActionResult> DeleteAmazonCountry(int Id)
        {
            try
            {
                await _countriesBusiness.DeleteAmazonCountry(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

