using Microsoft.Extensions.DependencyInjection;
using Identity.Abstractions.Abstractions;
using Notifications.Providers.Console;
using Notifications.Providers.Twilio;

namespace Notifications.Extensions;

/// <summary>
/// Extension methods for registering notification services.
/// </summary>
public static class NotificationServiceExtensions
{
  /// <summary>
  /// Adds Twilio SMS notifications to the service collection.
  /// </summary>
  /// <param name="services">The service collection.</param>
  /// <param name="configureOptions">Action to configure Twilio options.</param>
  /// <returns>The service collection for chaining.</returns>
  public static IServiceCollection AddTwilioNotifications(
    this IServiceCollection services,
    Action<TwilioOptions> configureOptions)
  {
    services.Configure(configureOptions);
    services.AddScoped<INotificationService, TwilioSmsProvider>();

    return services;
  }

  /// <summary>
  /// Adds console-based notifications for development/testing.
  /// </summary>
  /// <param name="services">The service collection.</param>
  /// <returns>The service collection for chaining.</returns>
  public static IServiceCollection AddConsoleNotifications(this IServiceCollection services)
  {
    services.AddScoped<INotificationService, ConsoleNotificationService>();

    return services;
  }
}
