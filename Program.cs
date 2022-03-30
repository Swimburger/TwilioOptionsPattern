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
TwilioClient.Init(
	username: twilioOptions.AccountSid,
	password: twilioOptions.AuthToken
);

var messageSender = host.Services.GetRequiredService<MessageSender>();
messageSender.SendMessage();

public class MessageSender
{
	private readonly MessageOptions messageOptions;

	public MessageSender(IOptions<MessageOptions>  messageOptions)
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