using eCollectAPI.DataAccessLayer;
using eCollectAPI.DTO;
using eCollectAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCollectAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CrudOperationController : ControllerBase
    {
        private readonly ICRUDOperationDL _crudOperationDL;

        public CrudOperationController(ICRUDOperationDL crudOperationDL)
        {
            this._crudOperationDL = crudOperationDL;
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser(InsertUserRequest insertRecordRequest)
        {
            var response = await _crudOperationDL.CreateUser(insertRecordRequest);

            try
            {

                return Ok(new GenericResponse
                {
                    IsSuccess = response.IsSuccess,
                    Message = response.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse
                {
                    IsSuccess = response.IsSuccess,
                    Message = response.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _crudOperationDL.GetAllUsers();

                return Ok(new
                {
                    isSuccess = true,
                    data = users,
                    message = "Usuarios obtenidos correctamente."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    isSuccess = false,
                    message = "Error obteniendo los usuarios."
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new
                {
                    isSuccess = false,
                    message = "El id es obligatorio."
                });
            }

            try
            {
                await _crudOperationDL.DeleteUser(id);

                return Ok(new
                {
                    isSuccess = true,
                    message = "Usuario eliminado correctamente."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    isSuccess = false,
                    message = "Error eliminando el usuario."
                });
            }
        }
    }
}
