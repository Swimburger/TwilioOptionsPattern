using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

var twilioAccountSid = Environment.GetEnvironmentVariable("TwilioAccountSid");
var twilioAuthToken = Environment.GetEnvironmentVariable("TwilioAuthToken");
var twilioPhoneNumber = Environment.GetEnvironmentVariable("TwilioPhoneNumber");
var toPhoneNumber = Environment.GetEnvironmentVariable("ToPhoneNumber");

TwilioClient.Init(twilioAccountSid, twilioAuthToken);

MessageResource.Create(
	from: new PhoneNumber(twilioPhoneNumber),
	to: new PhoneNumber(toPhoneNumber),
	body: "Ahoy!"
);