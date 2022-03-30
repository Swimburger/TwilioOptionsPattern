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

var twilioOptions = config.GetSection("Twilio").Get<TwilioOptions>();
TwilioClient.Init(
	username: twilioOptions.AccountSid,
	password: twilioOptions.AuthToken
);

var messageOptions = config.GetSection("Message").Get<MessageOptions>();
MessageResource.Create(
	from: messageOptions.From,
	to: messageOptions.To,
	body: messageOptions.Body
);

public class TwilioOptions
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