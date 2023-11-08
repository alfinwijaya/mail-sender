# Mail Sender Console Application

A C# console application for sending emails using .NET 7 and SMTP protocol.

## Prerequisites

Before using this application, make sure you have the following prerequisites in place:

- .NET 7: You need to have .NET 7 installed on your system to run this application.

- Email Server: Ensure you have access to a valid email server that can be used for sending emails. You will need the server's SMTP settings to configure this application.

## Getting Started

To get started with this application, follow these steps:

1. Restore NuGet packages:

   ```
   dotnet restore
   ```
   
    Note: If additional NuGet packages are required in the future, make sure to run dotnet restore to install them.

2. Fill the value of Host, Mail, Username, and Password in the `appsettings.json`

3. Navigate to the root directory of your project where the `.csproj` file is located.

4. Open a terminal. Run the following command to publish the application:

   ```
   dotnet publish -c Release -r win-x64 --self-contained true -output <YourPublishDirectory>
   ```
5. Navigate to `<YourPublishDirectory>` and run terminal in there. You can use `MailSender -h` or `MailSender --help` to see the available options.

   <img width="359" alt="image" src="https://github.com/alfinwijaya/mail-sender/assets/77500112/b052f8fd-4bcd-4e0a-8bd7-5214cfe98b72">

6. Example to execute the application.
   
    <img width="850" alt="image" src="https://github.com/alfinwijaya/mail-sender/assets/77500112/061e3726-bf38-410f-80d1-d262df6db712">

    Note: -f, -p, -t are censored for security issue

7. The mail has arrived !!!


    <img width="515" alt="image" src="https://github.com/alfinwijaya/mail-sender/assets/77500112/750daa49-a47c-4ec5-b5eb-0449c4d5671e">


9. Take a note if you don't provide -f, -p, -s, -b the application will use default one from `appsettings.json`

## Idea: Secure Credentials Storage

I think it is necessary to implement a more secure method for storing mail server credentials. This enhancement aims to enhance the overall security of the application by adopting best practices for handling sensitive information, such as email usernames and passwords. 


### That's it for now. If you have any idea and feedback please let me know.
