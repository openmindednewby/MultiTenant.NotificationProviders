namespace Notifications.Providers.Twilio;

/// <summary>
/// Configuration options for Twilio SMS provider.
/// </summary>
public class TwilioOptions
{
  /// <summary>
  /// Gets or sets the Twilio account SID.
  /// </summary>
  public string? AccountSid { get; set; }

  /// <summary>
  /// Gets or sets the Twilio auth token.
  /// </summary>
  public string? AuthToken { get; set; }

  /// <summary>
  /// Gets or sets the phone number to send SMS from.
  /// </summary>
  public string? FromNumber { get; set; }
}
