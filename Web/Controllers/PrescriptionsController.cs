using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    [Route("api/prescriptions")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionsDbService _dbService;

        public PrescriptionsController(IPrescriptionsDbService dbService)
        {
            this._dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetPrescription(string lekarz)
        {
            var result = _dbService.GetPrescription(lekarz);
            if (result == null)
            {
                ObjectResult res = new ObjectResult(result);
                res.StatusCode = 400;
                return res;
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddPrescription(PrescriptionRequest prescription)
        {

            var res = _dbService.AddPrescription(prescription);
            if (res == null)
                return BadRequest();
            return Created("", res);


        }



    }
}