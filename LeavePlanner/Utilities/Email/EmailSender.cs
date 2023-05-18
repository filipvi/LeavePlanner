using LeavePlanner.Utilities.Logger;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace LeavePlanner.Utilities.Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //TODO implement email sender 

            try
            {
                //var client = new SendGridClient(_emailSettings.SendGridApiKey);
                //var from = new EmailAddress(_emailSettings.EmailSender, "ZZJZ");
                //var tos = new List<EmailAddress>
                //{
                //    new EmailAddress(email)
                //};

                //// ReSharper disable once ConditionIsAlwaysTrueOrFalse
                //var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", message);
                //// ReSharper disable once UnusedVariable
                //var response = await client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {
                Log.RepositoryLog("EmailSender", "SendEmail", e, null);
            }

            return Task.CompletedTask;
        }

        #region Send confirmation mail

        //Mail se priprema nakon uspješnog kreiranja prijave za testiranje
        public async Task SendConfirmationEmailAsync()
        {
            try
            {
                //var client = new SendGridClient(_emailSettings.SendGridApiKey);
                //var from = new EmailAddress(_emailSettings.EmailSender, "ZZJZ");

                //var tos = new List<EmailAddress>
                //{
                //    new EmailAddress(mailModel.ApplicantEmail)
                //};

                ////Prepare mail content
                //var link = HtmlEncoder.Default.Encode(mailModel.CancelReservationLink);
                //var applicantIdentificationDocument = GenerateApplicantIdentificationDocument(mailModel);
                //var subject = GenerateSubjectForConfirmationEmail(mailModel);
                //var message = GenerateMessageForConfirmationEmail(mailModel, applicantIdentificationDocument, link);

                //var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", message);

                // ReSharper disable once UnusedVariable
                //var response = await client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {
                Log.RepositoryLog("EmailSender", "SendConfirmationEmailAsync", e, null);
            }
        }


        #endregion

    }
}
