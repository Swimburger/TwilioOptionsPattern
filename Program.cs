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

var twilioAccountSid = config["Twilio:AccountSid"];
var twilioAuthToken = config["Twilio:AuthToken"];

var fromPhoneNumber = config["Message:From"];
var toPhoneNumber = config["Message:To"];
var messageBody = config["Message:Body"];

TwilioClient.Init(
	username: twilioAccountSid,
	password: twilioAuthToken
);

MessageResource.Create(
	from: fromPhoneNumber,
	to: toPhoneNumber,
	body: messageBody
);