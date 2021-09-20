using WebApplication.CustomerAPI.Model;

namespace WebApplication.CustomerAPI.Data
{
    public interface ICustomerRepository
    {
        void CreateCustomer(Customer customer);
        bool IsEmailExist(string email);
        Customer GetCustomerByEmail(string email);
        Customer GetCustomerById(int id);
    }
}