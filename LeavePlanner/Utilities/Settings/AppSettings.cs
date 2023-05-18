

namespace LeavePlanner.Utilities.Settings
{
    public class AppSettings
    {
        public Application Application { get; set; }
        public Author Author { get; set; }
        public CompanyDataData CompanyData { get; set; }
        public GoogleRecaptcha GoogleRecaptcha { get; set; }
    }

    public class Application
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string HelpdeskContact { get; set; }
        public string Version { get; set; }
        public bool IsTest { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CompanyDataData
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }

    public class GoogleRecaptcha
    {
        public string RecaptchaSiteKey { get; set; }
        public string RecaptchaSecretKey { get; set; }
        public string RecaptchaVerifyUrl { get; set; }
        public string BaseUrl { get; set; }
        public string RecaptchaVerifyEndpoint { get; set; }
    }
}
