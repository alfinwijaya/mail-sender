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

2. Fill the value of Host, Mail, Username, and Password in the `appsettings.json` with your mail server information.

3. Navigate to the root directory of your project where the `.csproj` file is located.

4. Open a terminal. Run the following command to publish the application:

   ```
   dotnet publish -c Release -r win-x64 --self-contained true -output <YourPublishDirectory>
   ```
5. Navigate to `<YourPublishDirectory>` and run terminal in there. You can use `MailSender -h` or `MailSender --help` to see the available options.

   <img width="355" alt="image" src="https://github.com/alfinwijaya/mail-sender/assets/77500112/1d998ef4-c1cd-4c7c-bc88-51b48cd4750b">

6. Example to execute the application.
   
    <img width="850" alt="image" src="https://github.com/alfinwijaya/mail-sender/assets/77500112/c8c844ba-8b64-4c10-ac25-788eece5d565">

    Note: -f, -p, -t are censored for security issue

7. The mail has arrived !!!

    <img width="505" alt="image" src="https://github.com/alfinwijaya/mail-sender/assets/77500112/d0979c5a-692e-44a1-a2fc-f38b1234771e">

9. Take a note if you don't provide -f, -p, -s, -b the application will use default one from `appsettings.json`

## Idea: Secure Credentials Storage

I think it is necessary to implement a more secure method for storing mail server credentials. This enhancement aims to enhance the overall security of the application by adopting best practices for handling sensitive information, such as email usernames and passwords. 


## That's it for now. If you have any idea and feedback please let me know.
