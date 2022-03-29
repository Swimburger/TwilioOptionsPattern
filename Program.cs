using Twilio;
using Twilio.Rest.Api.V2010.Account;

var twilioAccountSid = Environment.GetEnvironmentVariable("TwilioAccountSid");
var twilioAuthToken = Environment.GetEnvironmentVariable("TwilioAuthToken");
var fromPhoneNumber = Environment.GetEnvironmentVariable("FromPhoneNumber");
var toPhoneNumber = Environment.GetEnvironmentVariable("ToPhoneNumber");

TwilioClient.Init(
	username: twilioAccountSid, 
	password: twilioAuthToken
);

MessageResource.Create(
	from: fromPhoneNumber,
	to: toPhoneNumber,
	body: "Ahoy!"
);