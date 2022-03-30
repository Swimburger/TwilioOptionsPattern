using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

using IHost host = Host.CreateDefaultBuilder(args).Build();
var config = host.Services.GetRequiredService<IConfiguration>();

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