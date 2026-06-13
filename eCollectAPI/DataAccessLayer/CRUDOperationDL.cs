using eCollectAPI.DTO;
using eCollectAPI.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace eCollectAPI.DataAccessLayer
{
    public class CRUDOperationDL: ICRUDOperationDL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<User> _userCollection;

        public CRUDOperationDL(MongoClient db, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._mongoClient = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            this._userCollection = _MongoDatabase.GetCollection<User>("Users");
        }

        public async Task<GenericResponse> CreateUser(InsertUserRequest request)
        {
            var response = new GenericResponse();

            try
            {
                var user = new User
                {
                    _id = ObjectId.GenerateNewId().ToString(),
                    dateCreated = DateTime.UtcNow,

                    firstName = request.firstName,
                    lastName = request.lastName,
                    age = request.age,
                    email = request.email
                };

                await _userCollection.InsertOneAsync(user);

                response.IsSuccess = true;
                response.Message = "Usuario creado correctamente.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error creando el usuario.";
            }

            return response;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userCollection.Find(_ => true).ToListAsync();
        }

        public async Task<GenericResponse> DeleteUser(string id)
        {
            var response = new GenericResponse();

            try
            {
                var result = await _userCollection.DeleteOneAsync(x => x._id == id);

                if (result.DeletedCount == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Usuario no encontrado.";

                    return response;
                }

                response.IsSuccess = true;
                response.Message = "Usuario eliminado correctamente.";
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Error eliminando el usuario.";
            }

            return response;
        }
    }
}
