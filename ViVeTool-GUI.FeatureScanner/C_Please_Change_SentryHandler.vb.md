# Please change SentryHandler.vb yourself. 

I do not wish to scramble my brain trying to figure out where ViVeTool GUI is crashing, before figuring out that that Crash Report came from a Git Clone or Fork.

## You can change the SendSentry() Sub of SentryHandler.vb if you want to add Custom Sentry Scopes:

Custom Scopes are used to Display User-configured settings within My.Settings, you can add your own using the following Template:

```vbnet
SentrySdk.ConfigureScope(
            Sub(Scope)
                Scope.Contexts("My Custom Category") = New With {
                    .MyCustomSetting = "My Custom Value",
                    ...
                }
            End Sub)
```

## Change the Sentry DSN within SentryHandler.DSN.vb

Pretty more or less self-explanatory. Create your own Sentry Account either on https://sentry.io or Self-hosted, create a Project and get it's DSN Link

```vbnet
Private Shared ReadOnly _DSN As String = "YOUR DSN HERE"
```

## Other Things to Note:

Refer to the [Sentry.NET Documentation](https://docs.sentry.io/platforms/dotnet/) for further Details, Functions, Methods and Properties that the Sentry SDK provides
