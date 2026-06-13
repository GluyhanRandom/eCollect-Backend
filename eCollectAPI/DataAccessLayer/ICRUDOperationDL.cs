using eCollectAPI.DTO;
using eCollectAPI.Model;

namespace eCollectAPI.DataAccessLayer
{
    public interface ICRUDOperationDL
    {
        public Task<GenericResponse> CreateUser(InsertUserRequest user);
        public Task<List<User>> GetAllUsers();
        public Task<GenericResponse> DeleteUser(string id);
    }
}
