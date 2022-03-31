using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

using IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((context, services) =>
	{
		services.Configure<TwilioOptions>(context.Configuration.GetSection("Twilio"));
		services.Configure<MessageOptions>(context.Configuration.GetSection("Message"));
		services.AddTransient<MessageSender>();
	})
	.Build();

var twilioOptions = host.Services.GetRequiredService<IOptions<TwilioOptions>>().Value;
if (twilioOptions.CredentialType == CredentialType.ApiKey)
{
	TwilioClient.Init(
		username: twilioOptions.ApiKeySid,
		password: twilioOptions.ApiKeySecret,
		accountSid: twilioOptions.AccountSid
	);
}
else
{
	TwilioClient.Init(
		username: twilioOptions.AccountSid,
		password: twilioOptions.AuthToken
	);
}

var messageSender = host.Services.GetRequiredService<MessageSender>();
messageSender.SendMessage();

public class MessageSender
{
	private readonly MessageOptions messageOptions;

	public MessageSender(IOptions<MessageOptions> messageOptions)
	{
		this.messageOptions = messageOptions.Value;
	}

	public void SendMessage()
	{
		MessageResource.Create(
			from: messageOptions.From,
			to: messageOptions.To,
			body: messageOptions.Body
		);
	}
}

public enum CredentialType
{
	AuthToken,
	ApiKey
}

public class TwilioOptions
{
	public CredentialType CredentialType { get; set; } 
		= CredentialType.AuthToken;
	
	public string AccountSid { get; set; }
	public string AuthToken { get; set; }
	public string ApiKeySid { get; set; }
	public string ApiKeySecret { get; set; }
}

public class MessageOptions
{
	public string From { get; set; }
	public string To { get; set; }
	public string Body { get; set; }
}