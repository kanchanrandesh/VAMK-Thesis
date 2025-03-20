namespace VAMK.FWMS.BizObjects.Resources
{
    public static class AntiForgeryDictionary
    {
        public const string INVALID_ANTI_FORGERY_TOKEN = "Invalid anti-forgery token";
        public const string CROSS_SITE_REQUEST_FORGERY_SALT = "Cross-SiteRequestForgerySalt";
        public const string CSRF_COOKIE = "CSRF_Cookie:{0}:Cookie";
        public const string CSRF_HIDDEN = "CSRF_Hidden:{0}:Hidden";
        public const string HTTP_HEADER_REQUEST_VERIFICATION_TOKEN = "csrftoken";
        public const string GENERAL_REQUEST_VERIFICATION_COOKIE = "generalcsrft";
        public const string DOCUMENTDESIGNER_REQUEST_VERIFICATION_COOKIE = "documentcsrft";
        public const string SCHEDULER_REQUEST_VERIFICATION_COOKIE = "schedulercsrft";
        public const string PLAININGBOARD_REQUEST_VERIFICATION_COOKIE = "plainingcsrft";
        public const string TRUSTEDURLSKEY = "TrustedUrls";
        public const string FILEMANAGER_REQUEST_VERIFICATION_COOKIE = "filemgrcsrft";
    }
}