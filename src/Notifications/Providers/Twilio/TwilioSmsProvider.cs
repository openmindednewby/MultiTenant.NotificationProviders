using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Identity.Abstractions.Abstractions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Notifications.Providers.Twilio;

/// <summary>
/// Twilio SMS provider implementation.
/// </summary>
public class TwilioSmsProvider : INotificationService
{
  private readonly TwilioOptions _options;
  private readonly ILogger<TwilioSmsProvider> _logger;
  private readonly bool _isConfigured;

  /// <summary>
  /// Initializes a new instance of the <see cref="TwilioSmsProvider"/> class.
  /// </summary>
  public TwilioSmsProvider(
    IOptions<TwilioOptions> options,
    ILogger<TwilioSmsProvider> logger)
  {
    _options = options.Value;
    _logger = logger;

    _isConfigured = !string.IsNullOrEmpty(_options.AccountSid) &&
                    !string.IsNullOrEmpty(_options.AuthToken);

    if (_isConfigured)
    {
      TwilioClient.Init(_options.AccountSid, _options.AuthToken);
    }
    else
    {
      _logger.LogWarning("Twilio is not configured. SMS notifications will not be sent.");
    }
  }

  /// <inheritdoc />
  public async Task<bool> SendSmsAsync(
    string phoneNumber,
    string message,
    CancellationToken cancellationToken = default)
  {
    if (!_isConfigured)
    {
      _logger.LogWarning("Twilio not configured. Would have sent SMS to {PhoneNumber}: {Message}",
        phoneNumber, message);
      return false;
    }

    try
    {
      if (string.IsNullOrEmpty(_options.FromNumber))
      {
        _logger.LogError("Twilio FromNumber is not configured");
        return false;
      }

      var messageResource = await MessageResource.CreateAsync(
        to: new PhoneNumber(phoneNumber),
        from: new PhoneNumber(_options.FromNumber),
        body: message
      );

      _logger.LogInformation("SMS sent successfully to {PhoneNumber}. SID: {MessageSid}",
        phoneNumber, messageResource.Sid);

      return messageResource.ErrorCode == null;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Failed to send SMS to {PhoneNumber}", phoneNumber);
      return false;
    }
  }

  /// <inheritdoc />
  public Task<bool> SendEmailAsync(
    string email,
    string subject,
    string body,
    CancellationToken cancellationToken = default)
  {
    _logger.LogWarning("Email sending via Twilio is not implemented. Use Twilio SendGrid for email.");
    return Task.FromResult(false);
  }
}
