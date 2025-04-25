// public class BigDataService : big_data.BigDataService.BigDataServiceBase
// {
//     private readonly YourDbContext _context;

//     public BigDataService(YourDbContext context)
//     {
//         _context = context;
//     }

//     public override async Task<CustomerResponse> GetCustomer(CustomerRequest request, ServerCallContext context)
//     {
//         var customer = await _context.Customers.FindAsync(request.Id);
//         return new CustomerResponse
//         {
//             Id = customer.Id,
//             Name = customer.Name,
//             Email = customer.Email
//         };
//     }

//     public override async Task<SaveCustomerResponse> SaveCustomer(SaveCustomerRequest request, ServerCallContext context)
//     {
//         var customer = new Company
//         {
//             Name = request.Name,
//             Email = request.Email
//         };

//         _context.Customers.Add(customer);
//         await _context.SaveChangesAsync();

//         return new SaveCustomerResponse { Success = true };
//     }
// }
