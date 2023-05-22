using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SensorAPI.Database;
using SensorAPI.Database.Models;

namespace SensorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {

        public SensorDbContext Context = new SensorDbContext();

        [HttpGet("{userid}")]
        public ActionResult<List<DataTable>> DataBeloningToUser(int userid)
        {
            // Retrives a collection of the sensors ids associated with the user id
            List<int> sensorids = Context.Sensors
                .Where(ut => ut.UserId == userid)
                .Select(ut => ut.SensorId)
                .ToList();

            // Checks if the list is null or empty
            if (sensorids.IsNullOrEmpty()) { return NotFound(); }

            // Makes a new collection instance that will contain the Data models
            List<DataTable> Data = new List<DataTable>();

            // Loops through all the sensors in collection
            foreach (int sensorid in sensorids)
            {
                // Loops through the DataTable associated with the sensor id
                foreach(DataTable dt in Context.DataTables.Where(dt => dt.SensorId == sensorid).ToList())
                {
                    // Adds the data model to the collection
                    Data.Add(dt);
                }
            }

            // Ensures that the collection isn't null or empty
            if(Data.IsNullOrEmpty()) { return NotFound(); }

            // Returns the models
            return Data;
        }

        [HttpGet("{fromDate}/{toDate}")]
        public ActionResult<List<DataTable>> GetAllDataBetweenDates(DateTime fromDate, DateTime toDate)
        {
            // Retrives the list of DataTables that are between the two given dates
            List<DataTable> dateTables = Context.DataTables.Where(dt => dt.DateReported >= fromDate && dt.DateReported <= toDate).ToList();

            // Checks if the collection is empty
            if(dateTables == null) { return NotFound(); }

            // Returns the collection
            return dateTables;
        }

    }
}
