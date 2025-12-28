using Microsoft.Extensions.Logging;
using Identity.Abstractions.Abstractions;

namespace Notifications.Providers.Console;

/// <summary>
/// Console-based notification service for development and testing.
/// Logs notifications to the console instead of sending real messages.
/// </summary>
public class ConsoleNotificationService : INotificationService
{
  private readonly ILogger<ConsoleNotificationService> _logger;

  /// <summary>
  /// Initializes a new instance of the <see cref="ConsoleNotificationService"/> class.
  /// </summary>
  public ConsoleNotificationService(ILogger<ConsoleNotificationService> logger)
  {
    _logger = logger;
  }

  /// <inheritdoc />
  public Task<bool> SendSmsAsync(
    string phoneNumber,
    string message,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("📱 [CONSOLE SMS] To: {PhoneNumber}, Message: {Message}",
      phoneNumber, message);

    System.Console.WriteLine($"📱 SMS to {phoneNumber}: {message}");

    return Task.FromResult(true);
  }

  /// <inheritdoc />
  public Task<bool> SendEmailAsync(
    string email,
    string subject,
    string body,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("📧 [CONSOLE EMAIL] To: {Email}, Subject: {Subject}, Body: {Body}",
      email, subject, body);

    System.Console.WriteLine($"📧 Email to {email}");
    System.Console.WriteLine($"   Subject: {subject}");
    System.Console.WriteLine($"   Body: {body}");

    return Task.FromResult(true);
  }
}
