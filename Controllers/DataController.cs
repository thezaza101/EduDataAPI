using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EduDataAPI.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace EduDataAPI.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {

        private readonly EDUDataContext _context;

        public DataController(EDUDataContext context)
        {
            _context = context;    
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
           /* 
            StreamReader sr = new StreamReader("edu.json");
            string x = sr.ReadToEnd();
            sr.Close();
            Domain d = JsonConvert.DeserializeObject<Domain>(x);
            _context.Add(d);
            _context.SaveChanges();
            */

            return StatusCode(201, _context.Domain.Include("content").FirstOrDefault());
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {            
            if (id == null)
            {
                return StatusCode(400, "Provide ID for data");
            }            
            var dataElement = _context.DataElement.SingleOrDefault(u => u.identifier.Substring(u.identifier.LastIndexOf(@"/")+1).Equals(id));
            if (dataElement==null)
            {
                return StatusCode(400, "Data ID not found in database");
            }
                      
            return StatusCode(200, dataElement);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]dynamic value)
        {
            string s = value.ToString();
            UpdatedData d = JsonConvert.DeserializeObject<UpdatedData>(s);
            d.DataID = Guid.NewGuid().ToString();
            d.Active = true;
            d.UpdateDateTimeTicks = DateTime.Now.Ticks.ToString();
            try
            {
                UpdatedData lastUpdatedDataInDomain = _context.UpdatedData.Where(u => u.UpdatedDomain.Equals(d.UpdatedDomain)).OrderByDescending(t => t.UpdateDateTimeTicks).FirstOrDefault();
                lastUpdatedDataInDomain.NextDataID = d.DataID;
                _context.Update(lastUpdatedDataInDomain);
            }
            catch(System.NullReferenceException)
            {
                return StatusCode(401,"Domain does not exist");

            }
            _context.Add(d);
            _context.SaveChanges();
            return StatusCode(201,d.DataID);
        }

        
    }
}
