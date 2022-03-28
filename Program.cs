using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

using IHost host = Host.CreateDefaultBuilder(args).Build();
var config = host.Services.GetRequiredService<IConfiguration>();

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
