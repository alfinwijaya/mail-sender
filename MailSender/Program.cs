using Microsoft.Extensions.Configuration;
using McMaster.Extensions.CommandLineUtils;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit.Text;

var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

MailSettings? emailSetting = configuration.GetSection("MailSetting").Get<MailSettings>();

var app = new CommandLineApplication();

app.HelpOption();

Email email = new();

var from = app.Option("-f|--from <from>", "From Email", CommandOptionType.SingleValue);
from.DefaultValue = string.Empty;

var password = app.Option("-pw|--password <password>", "From Email Password", CommandOptionType.SingleValue);
password.DefaultValue = string.Empty;

var to = app.Option("-t|--to <to>", "To Email", CommandOptionType.SingleValue);
to.DefaultValue = string.Empty;

var cc = app.Option("-c|--cc <cc>", "CC Email", CommandOptionType.SingleValue);
cc.DefaultValue = string.Empty;

var subject = app.Option("-s|--subject <subject>", "Mail Subject", CommandOptionType.SingleValue);
subject.DefaultValue = string.Empty;

var body = app.Option("-b|--body <body>", "Mail Body", CommandOptionType.SingleValue);
body.DefaultValue = string.Empty;

var filepath = app.Option("-a|--filepath <filepath>", "Attachment Path", CommandOptionType.SingleValue);
filepath.DefaultValue = string.Empty;

app.OnExecute(() =>
{
    try
    {
        email.From = from.Value() ?? string.Empty;
        email.To = (to.Value() ?? string.Empty).Split(',');
        email.CC = (cc.Value() ?? string.Empty).Split(',');
        email.Subject = !string.IsNullOrWhiteSpace(subject.Value()) ? subject.Value()! : emailSetting!.Subject ?? string.Empty;
        email.Body = !string.IsNullOrWhiteSpace(body.Value()) ? body.Value()! : emailSetting!.Body ?? string.Empty;
        email.FilePath = filepath.Value() ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(email.From) && !string.IsNullOrWhiteSpace(password.Value()))
        {
            emailSetting!.Mail = email.From;
            emailSetting.Username = email.From.Split('@')[0];
            emailSetting.Password = password.Value()!;
        }
        if (string.IsNullOrWhiteSpace(emailSetting!.Username) && string.IsNullOrWhiteSpace(emailSetting.Password))
            return;

        var mimeBuilder = new MimeMessage();
        mimeBuilder.From.Add(MailboxAddress.Parse(emailSetting.Mail));
        foreach (var address in email.To)
        {
            if (!string.IsNullOrWhiteSpace(address))
                mimeBuilder.To.Add(MailboxAddress.Parse(address));
        }
        if (email.CC?.Length > 0)
        {
            foreach (var address in email.CC)
            {
                if (!string.IsNullOrWhiteSpace(address))
                    mimeBuilder.Cc.Add(MailboxAddress.Parse(address));
            }
        }

        mimeBuilder.Subject = email.Subject;
        var mimeBody  = new TextPart(TextFormat.Html) { Text = email.Body };
        var multipart = new Multipart("mixed")
        {
            mimeBody
        };

        byte[] bytes = File.ReadAllBytes(email.FilePath);
        Stream stream = new MemoryStream(bytes);

        var attachment = new MimePart()
        {
            Content = new MimeContent(stream),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = Path.GetFileName(email.FilePath.Split('\\')[^1])
        };

        multipart.Add(attachment);
          
        mimeBuilder.Body = multipart;

        using var smtp = new SmtpClient();
        smtp.Connect(emailSetting.Host, emailSetting.Port, SecureSocketOptions.None);
        smtp.Authenticate(emailSetting.Username, emailSetting.Password);
        smtp.Send(mimeBuilder);
        smtp.Disconnect(true);
        stream.Dispose();
    }
    catch (Exception)
    {
        throw;
    }
});

return app.Execute(args);

internal record MailSettings
{
    public string Host { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}

internal record Email
{
    public string From { get; set; } = string.Empty;
    public string[] To { get; set; } = Array.Empty<string>();
    public string[] CC { get; set; } = Array.Empty<string>();
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}