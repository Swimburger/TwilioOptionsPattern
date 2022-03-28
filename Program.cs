using System.Reflection;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

IConfiguration config = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
	.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true, reloadOnChange: false)
	.AddEnvironmentVariables()
	.AddCommandLine(args)
	.Build();

var twilioAuthenticationOptions = new TwilioAuthenticationOptions();
config.GetSection("Twilio").Bind(twilioAuthenticationOptions);

var messageOptions = new MessageOptions();
config.GetSection("Message").Bind(messageOptions);

TwilioClient.Init(
	username: twilioAuthenticationOptions.AccountSid, 
	password: twilioAuthenticationOptions.AuthToken
);

MessageResource.Create(
	from: messageOptions.From,
	to: messageOptions.To,
	body: messageOptions.Body
);

public class TwilioAuthenticationOptions
{
	public string AccountSid { get; set; }
	public string AuthToken { get; set; }
}

public class MessageOptions
{
	public string From { get; set; }
	public string To { get; set; }
	public string Body { get; set; }
}
