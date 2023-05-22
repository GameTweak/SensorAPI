using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SensorAPI.Database.Models;
using SensorAPI.Database;
using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Tokens;

namespace SensorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        // Declares the DbContext to the DB can be accessed through EF
        public SensorDbContext Context = new SensorDbContext();

        [HttpGet]
        public ActionResult<List<Sensor>> GetSensors()
        {
            // Retrives a list containing all sensors
            var sensor = Context.Sensors;

            // Ensures that the list isn't empty or null
            if (sensor.IsNullOrEmpty()) { return NotFound(); }

            // Returns the list
            return sensor.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Sensor> GetSensor(int id)
        {
            // Retrives a sensor by the given id
            var sensor = Context.Sensors.Where(s => s.SensorId == id).FirstOrDefault();

            // Ensures it isn't null
            if (sensor == null) { return NotFound(); }

            // Returns it
            return sensor;
        }

        [HttpPost("{sensorname}/{userid}")]
        public ActionResult CreateSensor(string sensorname, int userid)
        {
            // Creates a new sensor instance with the information provided
            Sensor newSensor = new Sensor()
            {
                SensorName = sensorname,
                DateInstalled = DateTime.Now,
                UserId = userid
            };

            // Adds the sensor to the Sensor Table in the DB
            Context.Sensors.Add(newSensor);

            // Commits the changes to the DB
            Context.SaveChanges();

            // Returns that the action succeed
            return Ok(newSensor);
        }

        [HttpPut]
        [Route("{sensorid}")]
        public ActionResult EditSensor(int sensorid, [Optional] string sensorname, [Optional] DateTime? dateInstalled, [Optional] int? userid)
        {
            // Gets the sensor matching the sensor id provided
            Sensor? editedSensor = Context.Sensors.Where(s => s.SensorId == sensorid).FirstOrDefault();

            // Ensures it isn't null
            if(editedSensor == null) { return NotFound(); }

            // Updates the sensor name, if a new one is provided
            if (sensorname !=  null) editedSensor.SensorName = sensorname;

            // Updates the date of installation, if a new one is provided
            if(dateInstalled != null) editedSensor.DateInstalled = (DateTime) dateInstalled;
            
            // Updates the user id, if a new one is provided
            if(userid != null) editedSensor.UserId = userid;

            // Updates the record in the db with the new information
            Context.Update(editedSensor);

            // Commits the changes to the DB
            Context.SaveChanges();

            // Returns that the action succeed
            return Ok(editedSensor);
        }

        [HttpDelete("{sensorid}")]
        public ActionResult DeleteSensor(int sensorid)
        {
            // Makes a new sensor instance containing the sensor matching the id
            Sensor? deleteSensor = Context.Sensors.Where(s => s.SensorId == sensorid).FirstOrDefault();

            // Ensures that the providd sensor exists
            if(deleteSensor == null) { return NotFound(); }

            // Removes the object from the Sensor table
            Context.Sensors.Remove(deleteSensor);

            // Commits the changes
            Context.SaveChanges();

            // Returns that the action succeed
            return Ok(deleteSensor);
        }

    }
}
