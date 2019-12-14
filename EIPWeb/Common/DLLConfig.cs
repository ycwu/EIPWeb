using System.Configuration;

namespace EIPWeb
{
    public class DLLConfig
    {
        Properties.Settings settings = Properties.Settings.Default;
        static Properties.Settings settings1 = Properties.Settings.Default;
        public DLLConfig()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        //public string DBConnectionString
        //{
        //    get { return settings.EIPDB.ToString(); }
        //}

        public string AdminMailList
        {
            get { return "ycwu@machvision.com.tw"; }
        }

        public string MailSender
        {
            get { return "mvmis@machvision.com.tw"; }
        }

        public string MailPassword
        {
            get { return "mv1234"; }
        }

        public string MailSubject
        {
            get { return "供應鏈系統-"; }
        }

        public string SMTPServer
        {
            get { return "mail.machvision.com.tw"; }
            //get { return "192.168.222.16"; }
        }

        public static bool DebugMode
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["DebugMode"].ToString());}
            //get { return settings.DebugMode; }
            //get { return true; }
        }

        public int CommandTimeout
        {
            get { return 60; }  //second
        }

        public string ProgramCode
        {
            get { return "SCMWeb"; }
        }

        public string FaxHost
        {
            get { return settings.FaxHost; }
        }

        public string FaxAdminMail
        {
            get { return settings.FaxAdminMail; }
        }

        public string SCMAdminMail
        {
            get { return settings.SCMAdminMail; }
            //get { return ""; }
        }

        public string SCMBccMail
        {
            get { return settings.SCMBccMail; }
        }

        public string DCMAdminMail
        {
            get { return settings.DCMAdminMail; }
        }

        //上簽核董事長採購金額
        public int UpperPrice
        {
            get { return 50000; }
        }

        //允許上傳檔案格式
        public static string AllowedFileExt
        {
            get { return settings1.AllowedFileExt; }
        }

        //是否寄送系統通知
        public static bool IsSendMail
        {
            get { return settings1.IsSendMail; }
        }
    }
}