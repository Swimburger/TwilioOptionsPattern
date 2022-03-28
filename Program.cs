using System.Reflection;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

// Build a config object, using env vars and JSON providers.
IConfiguration config = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
	.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true, reloadOnChange: false)
	.AddEnvironmentVariables()
	.AddCommandLine(args)
	.Build();

var twilioAccountSid = config["Twilio:AccountSid"];
var twilioAuthToken = config["Twilio:AuthToken"];
var twilioPhoneNumber = config["Twilio:PhoneNumber"];
var toPhoneNumber = config["ToPhoneNumber"];

TwilioClient.Init(twilioAccountSid, twilioAuthToken);

MessageResource.Create(
	from: new PhoneNumber(twilioPhoneNumber),
	to: new PhoneNumber(toPhoneNumber),
	body: "Ahoy!"
);
