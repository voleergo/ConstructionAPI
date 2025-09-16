using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Construction.DomainModel
{
    public class EmailModel : BasicModel
    {
        [JsonProperty("id")]
        public Int64 ID_EmailNotification { get; set; }
        [JsonProperty("fk_Module")]
        public Int64 FK_Module { get; set; }
        [JsonProperty("fk_User")]
        public Int64 FK_User { get; set; }
        [JsonProperty("fk_Trans")]
        public Int64 FK_Trans { get; set; }
        [JsonProperty("transType")]
        public string TransType { get; set; }
        [JsonProperty("templateType")]
        public string TemplateType { get; set; }
        [JsonProperty("emailID")]
        public string EmailID { get; set; }
        [JsonProperty("mobileNo")]
        public string MobileNo { get; set; }
        [JsonProperty("isRead")]
        public Boolean IsRead { get; set; }
        [JsonProperty("isSend")]
        public Boolean IsSend { get; set; }
        [JsonProperty("isCounted")]
        public Boolean IsCounted { get; set; }
        [JsonProperty("toWhom")]
        public string ToWhom { get; set; }
        [JsonProperty("cc")]
        public string CC { get; set; }
        [JsonProperty("bcc")]
        public string Bcc { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("textBody")]
        public string TextBody { get; set; }
        [JsonProperty("htmlBody")]
        public string HtmlBody { get; set; }
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }
        [JsonProperty("errorDetails")]
        public string ErrorDetails { get; set; }
        [JsonProperty("isAuto")]
        public Boolean IsAuto { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Head { get; set; }
        public string AttachmentFile { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("userImage")]
        public string UserImage { get; set; }
        public string AttachmentFileName { get; set; }
        public string ClientUrl { get; set; }

        public EmailAddress FromEmailAddress { get; set; }
        public List<EmailAddress> ToEmailAddress { get; set; }
        public string Token { get; set; }
        public EmailModel()
        {
            ID_EmailNotification = 0;
            FK_Module = 0;
            FK_User = 0;
            FK_Trans = 0;
            TransType = string.Empty;
            TemplateType = string.Empty;
            EmailID = string.Empty;
            MobileNo = string.Empty;
            IsRead = false;
            IsSend = false;
            IsCounted = false;
            ToWhom = string.Empty;
            CC = string.Empty;
            Bcc = string.Empty;
            Subject = string.Empty;
            TextBody = string.Empty;
            HtmlBody = string.Empty;
            ErrorCode = string.Empty;
            IsAuto = false;
            ErrorDetails = string.Empty;
            FromAddress = string.Empty;
            ToAddress = string.Empty;
            Head = string.Empty;
            AttachmentFile = string.Empty;
            UserName = string.Empty;
            UserImage = string.Empty;
            FromEmailAddress = new EmailAddress();
            AttachmentFileName = string.Empty;
            ToEmailAddress = new List<EmailAddress>();
            Token = string.Empty;
        }
    }
}