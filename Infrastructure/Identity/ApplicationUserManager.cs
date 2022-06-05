using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity;
public class ApplicationUserManager : UserManager<ApplicationUser>
{
    private readonly IRepository<Customer> _customerRepository;

    public ApplicationUserManager (IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
        IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<ApplicationUserManager> logger, IRepository<Customer> customerRepository)
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators,
            keyNormalizer, errors, services, logger)
    {
        _customerRepository = customerRepository;
    }

    public override async Task<IdentityResult> CreateAsync (ApplicationUser user)
    {
        var result = await base.CreateAsync(user);
        if (!result.Succeeded)
        {
            return result;
        }

        var customer = new Customer
        {
            IdentityId = user.Id
        };

        await _customerRepository.AddAsync(customer);

        user.Customer = customer;

        return result;
    }
}
