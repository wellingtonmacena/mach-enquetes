using MachEnquetes.Entities;
using MachEnquetes.Models;
using MachEnquetes.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MachEnquetes.Controllers
{
    [ApiController]
    public class SurveyController : Controller
    {
        public SurveyRepository SurveyRepository { get; set; }
        public SurveyController(MachEnquetesContext machEnquetesContext)
        {
            SurveyRepository = new SurveyRepository(machEnquetesContext);
        }

        #region UserMethods
        [Route("[controller]")]
        [HttpGet]
        [SwaggerOperation(Summary = "Gets all surveys saved.", Description = "Returns a list with all surveys saved.")]
        public IActionResult GetAll()
        {
            var users = SurveyRepository.GetAll().Result;
            return users;
        }

        [Route("[controller]/all/{id}")]
        [HttpGet()]
        [SwaggerOperation(Summary = "Gets one survey filtered by id.", Description = "Returns a survey object.")]
        public IActionResult GetAllSurveysFromUserId(int id)
        {
            var user = SurveyRepository.GetAllSurveysFromUserId(id).Result;
            return user;
        }

        [Route("[controller]/{id}")]
        [HttpGet()]
        [SwaggerOperation(Summary = "Gets one survey filtered by id.", Description = "Returns a survey object.")]
        public IActionResult GetById(int id)
        {
            var user = SurveyRepository.GetById(id).Result;
            return user;
        }

        [HttpPost]
        [Route("[controller]")]
        [SwaggerOperation(Summary = "Creates one survey object", Description = "Returns a survey object.")]
        public IActionResult CreateOne([FromBody] Survey survey)
        {
            try
            {
                var createdUser = SurveyRepository.Create(survey).Result;

                return createdUser;
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        [Route("[controller]/{id}")]
        [SwaggerOperation(Summary = "Updates one survey filtered by id.", Description = "Returns survey object updated.")]
        public IActionResult UpdateOne([FromBody] Survey survey, int id)
        {
            var updatedUser = SurveyRepository.Update(id, survey).Result;

            return updatedUser;
        }

        [HttpDelete]
        [Route("[controller]")]
        [SwaggerOperation(Summary = "Deletes all surveys saved.", Description = "Returns nothing")]
        public IActionResult DeleteAll()
        {
            var updatedUser = SurveyRepository.DeleteAll().Result;

            return updatedUser;
        }

        [HttpDelete]
        [Route("[controller]/{id}")]
        [SwaggerOperation(Summary = "Deletes one survey filtered by id .", Description = "Returns nothing")]
        public IActionResult DeleteOne(int id)
        {
            var updatedUser = SurveyRepository.DeleteById(id).Result;

            return updatedUser;
        }
        #endregion
    }
}

