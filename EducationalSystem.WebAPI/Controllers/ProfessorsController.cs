using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        DBContext db;
        public ProfessorsController(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ContentResult Get()
        {
            return Content("Здесь будет список активных professors");
        }
    }
}