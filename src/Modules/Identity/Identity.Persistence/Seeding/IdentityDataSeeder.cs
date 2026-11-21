using Framwork.Extensions;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedKernel.Constants;

namespace Identity.Persistence.Seeding;

public sealed class IdentityDataSeeder(
    IServiceScopeFactory scopeFactory,
    IConfiguration configuration,
    ILogger<IdentityDataSeeder> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await EnsureRoleExistsAsync(roleManager, AppRoles.User);
        await EnsureRoleExistsAsync(roleManager, AppRoles.Admin);

        await SeedAccountAsync(userManager, configuration.GetSection("SeedUsers:User"), AppRoles.User);
        await SeedAccountAsync(userManager, configuration.GetSection("SeedUsers:Admin"), AppRoles.Admin);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task EnsureRoleExistsAsync(
        RoleManager<IdentityRole<long>> roleManager,
        string roleName)
    {
        if (await roleManager.RoleExistsAsync(roleName))
            return;

        var result = await roleManager.CreateAsync(new IdentityRole<long>(roleName));
        EnsureSucceeded(result, $"Creating role '{roleName}'");
    }

    private async Task SeedAccountAsync(
        UserManager<ApplicationUser> userManager,
        IConfigurationSection accountConfiguration,
        string roleName)
    {
        var mobile = accountConfiguration["Mobile"];
        var password = accountConfiguration["Password"];

        if (string.IsNullOrWhiteSpace(mobile) && string.IsNullOrWhiteSpace(password))
            return;

        if (string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                $"Both Mobile and Password must be configured for {accountConfiguration.Path}.");
        }

        mobile = mobile.NormalizePhoneNumber();
        var user = await userManager.FindByNameAsync(mobile);

        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = mobile,
                PhoneNumber = mobile,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var createResult = await userManager.CreateAsync(user, password);
            EnsureSucceeded(createResult, $"Creating seeded account for {mobile}");
        }

        if (!await userManager.IsInRoleAsync(user, roleName))
        {
            var roleResult = await userManager.AddToRoleAsync(user, roleName);
            EnsureSucceeded(roleResult, $"Assigning role '{roleName}' to {mobile}");
        }

        logger.LogInformation("Ensured configured identity account has role {Role}.", roleName);
    }

    private static void EnsureSucceeded(IdentityResult result, string operation)
    {
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(error => error.Description));
            throw new InvalidOperationException($"{operation} failed: {errors}");
        }
    }
}
