```bash
dotnet add package Twilio
```


```bash
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.CommandLine
dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
dotnet add package Microsoft.Extensions.Configuration.UserSecrets
dotnet add package Microsoft.Extensions.Configuration.Binder
```

```bash
dotnet user-secrets init
dotnet user-secrets set Twilio:AccountSid [YOUR_ACCOUNT_SID]
dotnet user-secrets set Twilio:AuthToken [YOUR_AUTH_TOKEN]
```

```bash
dotnet run --Message:From [YOUR_TWILIO_PHONE_NUMBER] --Message:To [TO_PHONE_NUMBER] --Message:Body Ahoy!
```

```bash
dotnet add package Microsoft.Extensions.Hosting
```


```bash
dotnet run --Environment Development --Message:From [YOUR_TWILIO_PHONE_NUMBER] --Message:To [TO_PHONE_NUMBER] --Message:Body Ahoy!
```