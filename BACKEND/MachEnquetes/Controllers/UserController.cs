using Google.Protobuf.WellKnownTypes;
using MachEnquetes.Entities;
using MachEnquetes.Repositories;
using MachEnquetes.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace MachEnquetes.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        public UserRepository UserRepository { get; set; }
        private  MachEnquetesContext MachEnquetesContext;
        //public UserController(IOptions<DatabaseSettings> options)
        //{
        //    UserRepository = new UserRepository(options);
        //}

        public UserController(MachEnquetesContext machEnquetesContext)
        {
            this.MachEnquetesContext = machEnquetesContext;
            UserRepository = new UserRepository(MachEnquetesContext);
        }

        #region UserMethods
        [Route("[controller]")]
        [HttpGet]
        [SwaggerOperation(Summary = "Gets all users saved.", Description = "Returns a list with all users saved.")]
        public IActionResult GetAll()
        {
            //var users = UserRepository.GetAll();
            return null;
        }

        //[Route("[controller]/{id}")]
        //[HttpGet()]
        //[SwaggerOperation(Summary = "Gets one user filtered by id.", Description = "Returns a user object.")]
        //public IActionResult GetById(string id)
        //{
        //    //var user = UserRepository.GetById(id);
        //    return user;
        //}

        //[HttpPost]
        //[Route("[controller]")]
        //[SwaggerOperation(Summary = "Creates one user object", Description = "Returns a user object.")]
        //public IActionResult CreateOne([FromBody] User user)
        //{
        //    try
        //    {
        //        //var createdUser = UserRepository.Create(user);

        //        return createdUser;
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex);
        //    }
        //}

        //[HttpPost]
        //[Route("[controller]/login")]
        //[SwaggerOperation(Summary = "Signs in one user.", Description = "Returns a list with all notes of user saved")]
        //public IActionResult Login([FromBody] User user)
        //{
        //   // var foundUser = UserRepository.Create(user);

        //    return foundUser;
        //}

        //[HttpPut]
        //[Route("[controller]/{id}")]
        //[SwaggerOperation(Summary = "Updates one user filtered by id.", Description = "Returns user object updated.")]
        //public IActionResult UpdateOne([FromBody] User user, string id)
        //{
        //   // var updatedUser = UserRepository.Update(id, user);

        //    return updatedUser;
        //}

        //[HttpDelete]
        //[Route("[controller]")]
        //[SwaggerOperation(Summary = "Deletes all users saved.", Description = "Returns nothing")]
        //public IActionResult DeleteAll()
        //{
        //   // var updatedUser = UserRepository.DeleteAll();

        //    return updatedUser;
        //}

        //[HttpDelete]
        //[Route("[controller]/{id}")]
        //[SwaggerOperation(Summary = "Deletes one user filtered by id .", Description = "Returns nothing")]
        //public IActionResult DeleteOne(string id)
        //{
        //    //var updatedUser = UserRepository.DeleteById(id);

        //    return updatedUser;
        //}
        #endregion

    }
}
