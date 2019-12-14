using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Data;
using System.Reflection;
using System.Net.Mime;

namespace EIPWeb
{
    public class CommonFunction
    {
        public CommonFunction()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        #region 將系統的錯誤訊息發送給管理員
        /// <summary>
        /// 將系統的錯誤訊息發送給管理員
        /// </summary>
        /// <param name="GetError"></param>
        public static void SendMailToAdmin(System.Exception GetError)
        {
            try
            {
                //發送最底層的的問題訊息
                Exception InnerError = GetError.InnerException;
                while (InnerError != null)
                {
                    GetError = InnerError;
                    InnerError = GetError.InnerException;
                }

                //設定郵件資料
                MailMessage msg = new MailMessage();
                msg.BodyEncoding = Encoding.Unicode;
                msg.IsBodyHtml = true;
                //msg.SubjectEncoding = Encoding.Unicode;

                DLLConfig dllconfig = new DLLConfig();
                //寄件者
                msg.From = new MailAddress(dllconfig.MailSender);
                //收件者
                string strSystemMaintainerMailList = dllconfig.AdminMailList;
                string[] strPartMail = strSystemMaintainerMailList.Split(';');
                for (int iIndex = 0; iIndex < strPartMail.Length; iIndex++)
                {
                    msg.To.Add(strPartMail[iIndex]);
                }

                //標題
                msg.Subject = (DLLConfig.DebugMode ? "[SCM測試]" : "[SCM]") + dllconfig.MailSubject;

                //郵件內文
                StringBuilder sbBody = new StringBuilder();
                sbBody.Append("Message:");
                sbBody.Append(GetError.Message).Append("<br />");
                if (string.IsNullOrEmpty(GetError.Source) == false)
                {
                    sbBody.Append("Source:");
                    sbBody.Append(GetError.Source).Append("<br />");
                }
                if (string.IsNullOrEmpty(GetError.StackTrace) == false)
                {
                    sbBody.Append("StackTrace:");
                    sbBody.Append(GetError.StackTrace.Replace("\r\n", "<br />")).Append("<br />");
                }
                sbBody.Append("User:");
                if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated)
                    sbBody.Append(HttpContext.Current.User.Identity.Name).Append("<br />");
                else
                    sbBody.Append(Environment.UserName).Append("<br />");
                sbBody.Append("Host:");
                sbBody.Append(System.Net.Dns.GetHostName()).Append("<br />");    // HttpRequest.UserHostAddress
                msg.Body = sbBody.ToString();

                //發送郵件
                SmtpClient smtp = new SmtpClient(dllconfig.SMTPServer);
                smtp.Send(msg);
            }
            catch
            {
                //Do Nothing
            }
        }
        public static void SendMailToAdmin(System.Exception GetError, string ValidationError)
        {
            try
            {
                //發送最底層的的問題訊息
                Exception InnerError = GetError.InnerException;
                while (InnerError != null)
                {
                    GetError = InnerError;
                    InnerError = GetError.InnerException;
                }

                //設定郵件資料
                MailMessage msg = new MailMessage();
                msg.BodyEncoding = Encoding.Unicode;
                msg.IsBodyHtml = true;
                //msg.SubjectEncoding = Encoding.Unicode;

                DLLConfig dllconfig = new DLLConfig();
                //寄件者
                msg.From = new MailAddress(dllconfig.MailSender);
                //收件者
                string strSystemMaintainerMailList = dllconfig.AdminMailList;
                string[] strPartMail = strSystemMaintainerMailList.Split(';');
                for (int iIndex = 0; iIndex < strPartMail.Length; iIndex++)
                {
                    msg.To.Add(strPartMail[iIndex]);
                }

                //標題
                msg.Subject = (DLLConfig.DebugMode ? "[SCM測試]" : "[SCM]") + dllconfig.MailSubject;

                //郵件內文
                StringBuilder sbBody = new StringBuilder();
                sbBody.Append("Message:");
                sbBody.Append(GetError.Message).Append("<br />");
                if (string.IsNullOrEmpty(ValidationError) == false)
                {
                    sbBody.Append("ValidationError:");
                    sbBody.Append(ValidationError).Append("<br />");
                }
                if (string.IsNullOrEmpty(GetError.Source) == false)
                {
                    sbBody.Append("Source:");
                    sbBody.Append(GetError.Source).Append("<br />");
                }
                if (string.IsNullOrEmpty(GetError.StackTrace) == false)
                {
                    sbBody.Append("StackTrace:");
                    sbBody.Append(GetError.StackTrace.Replace("\r\n", "<br />")).Append("<br />");
                }
                sbBody.Append("User:");
                if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated)
                    sbBody.Append(HttpContext.Current.User.Identity.Name).Append("<br />");
                else
                    sbBody.Append(Environment.UserName).Append("<br />");
                sbBody.Append("Host:");
                sbBody.Append(System.Net.Dns.GetHostName()).Append("<br />");    // HttpRequest.UserHostAddress
                msg.Body = sbBody.ToString();

                //發送郵件
                SmtpClient smtp = new SmtpClient(dllconfig.SMTPServer);
                smtp.Send(msg);
            }
            catch
            {
                //Do Nothing
            }
        }
        #endregion

        #region 傳送訊息給系統管理者
        /// <summary>
        /// 傳送訊息給系統管理者
        /// </summary>
        /// <param name="message"></param>        
        public static void SendEmail(string message)
        {
            DLLConfig dllconfig = new DLLConfig();
            MailMessage Msg = new MailMessage();
            string[] sMail = dllconfig.AdminMailList.Split(';');
            for (int i = 0; i < sMail.Length; i++)
            {
                Msg.To.Add(new MailAddress(sMail[i]));
            }
            Msg.From = new MailAddress(dllconfig.MailSender);
            Msg.Subject = (DLLConfig.DebugMode ? "[SCM測試]" : "[SCM]") + dllconfig.MailSubject;
            Msg.Body = message;
            Msg.IsBodyHtml = false;
            SmtpClient client = new SmtpClient(dllconfig.SMTPServer);
            try
            {
                client.Send(Msg);
            }
            catch //(Exception ex)
            {
                //'"Send Email fails: " + ex.ToString())
            }
        }
        #endregion

        #region 傳送訊息給業務管理者
        /// <summary>
        /// 傳送訊息給業務管理者
        /// </summary>
        /// <param name="message"></param>        
        public static void SendSCMAdminEmail(string sSubject, string sMessage)
        {
            DLLConfig dllconfig = new DLLConfig();
            MailMessage Msg = new MailMessage();
            string[] sTo = (DLLConfig.DebugMode ? dllconfig.AdminMailList.Split(';') : dllconfig.SCMAdminMail.Split(';'));
            for (int i = 0; i < sTo.Length; i++)
            {
                if (!string.IsNullOrEmpty(sTo[i]))
                    Msg.To.Add(new MailAddress(sTo[i]));
            }
            string[] sBcc = dllconfig.SCMBccMail.Split(';');
            for (int i = 0; i < sBcc.Length; i++)
            {
                if (!string.IsNullOrEmpty(sBcc[i]))
                    Msg.Bcc.Add(new MailAddress(sBcc[i]));
            }
            Msg.From = new MailAddress(dllconfig.MailSender);
            Msg.Subject = (DLLConfig.DebugMode ? "[SCM測試]" : "[SCM]") + sSubject;
            Msg.Body = sMessage;
            Msg.IsBodyHtml = false;
            SmtpClient client = new SmtpClient(dllconfig.SMTPServer);
            try
            {
                client.Send(Msg);
            }
            catch //(Exception ex)
            {
                //'"Send Email fails: " + ex.ToString())
            }
        }
        public static void SendDCMAdminEMail(string sSubject, string sMessage)
        {
            DLLConfig dllconfig = new DLLConfig();
            MailMessage Msg = new MailMessage();
            string[] sTo = (DLLConfig.DebugMode ? dllconfig.AdminMailList.Split(';') : dllconfig.DCMAdminMail.Split(';'));
            for (int i = 0; i < sTo.Length; i++)
            {
                if (!string.IsNullOrEmpty(sTo[i]))
                    Msg.To.Add(new MailAddress(sTo[i]));
            }
            string[] sBcc = dllconfig.SCMBccMail.Split(';');
            for (int i = 0; i < sBcc.Length; i++)
            {
                if (!string.IsNullOrEmpty(sBcc[i]))
                    Msg.Bcc.Add(new MailAddress(sBcc[i]));
            }
            Msg.From = new MailAddress(dllconfig.MailSender);
            Msg.Subject = (DLLConfig.DebugMode ? "[SCM測試]" : "[SCM]") + sSubject;
            Msg.Body = sMessage;
            Msg.IsBodyHtml = false;
            SmtpClient client = new SmtpClient(dllconfig.SMTPServer);
            try
            {
                client.Send(Msg);
            }
            catch //(Exception ex)
            {
                //'"Send Email fails: " + ex.ToString())
            }
        }
        #endregion

        #region SCM郵件訊息給指定收件人
        public static bool SendEmailSCM(string sToMail, string sCcMail, string sBccMail, string sSubject, string sMessage, bool bIsHtml)
        {
            bool bResult = false;
            DLLConfig dllconfig = new DLLConfig();
            MailMessage Msg = new MailMessage();

            string[] sTo = (DLLConfig.DebugMode ? dllconfig.AdminMailList.Split(';') : sToMail.Split(';'));
            for (int i = 0; i < sTo.Length; i++)
            {
                if (!string.IsNullOrEmpty(sTo[i]))
                    Msg.To.Add(new MailAddress(sTo[i]));
            }
            string[] sCC = sCcMail.Split(';');
            for (int i = 0; i < sCC.Length; i++)
            {
                if (!string.IsNullOrEmpty(sCC[i]))
                    Msg.CC.Add(new MailAddress(sCC[i]));
            }
            string[] sBcc = sBccMail.Split(';');
            for (int i = 0; i < sBcc.Length; i++)
            {
                if (!string.IsNullOrEmpty(sBcc[i]))
                    Msg.Bcc.Add(new MailAddress(sBcc[i]));
            }
            #region 通知底圖
            //底圖檔案
            LinkedResource lrHead = new LinkedResource(@"D:\Schedule\SyncERP\Notify-Head.jpg");
            lrHead.ContentId = "Notify-Head";
            lrHead.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
            LinkedResource lrFooter = new LinkedResource(@"D:\Schedule\SyncERP\Notify-Footer.jpg");
            lrFooter.ContentId = "Notify-Footer";
            lrFooter.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
            //郵件格式
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(sMessage, null, "text/html");
            htmlView.LinkedResources.Add(lrHead);
            htmlView.LinkedResources.Add(lrFooter);
            Msg.AlternateViews.Add(htmlView);
            #endregion

            Msg.From = new MailAddress(dllconfig.MailSender);
            Msg.Subject = sSubject;//(dllconfig.DebugMode ? "[SCM測試]" : "[SCM]")
            Msg.Body = (DLLConfig.DebugMode ? "Original:" + sToMail : "") + Environment.NewLine + Environment.NewLine + sMessage;
            Msg.IsBodyHtml = bIsHtml;
            Msg.BodyEncoding = Encoding.UTF8;
            SmtpClient client = new SmtpClient(dllconfig.SMTPServer);
            try
            {
                client.Credentials = new System.Net.NetworkCredential(dllconfig.MailSender, dllconfig.MailPassword);
                client.Send(Msg);
                bResult = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
                CommonFunction cf = new CommonFunction();
                cf.SetLogFile(@"C:\inetpub\wwwroot\SCMWeb.log", ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
                throw new Exception(ex.Message);               
            }
            return bResult;
        }
        #endregion

        #region SCM郵件傳送訊息指定收件人可附件
        public static bool SendEmailSCM(string sToMail, string sCcMail, string sBccMail, string sSubject, string sMessage, bool bIsHtml, Dictionary<string, string> dAttchment)
        {
            bool bResult = false;
            DLLConfig dllconfig = new DLLConfig();
            MailMessage Msg = new MailMessage();

            string[] sTo = (DLLConfig.DebugMode ? dllconfig.AdminMailList.Split(';') : sToMail.Split(';'));
            for (int i = 0; i < sTo.Length; i++)
            {
                if (!string.IsNullOrEmpty(sTo[i]))
                    Msg.To.Add(new MailAddress(sTo[i]));
            }
            string[] sCC = sCcMail.Split(';');
            for (int i = 0; i < sCC.Length; i++)
            {
                if (!string.IsNullOrEmpty(sCC[i]))
                    Msg.CC.Add(new MailAddress(sCC[i]));
            }
            string[] sBcc = sBccMail.Split(';');
            for (int i = 0; i < sBcc.Length; i++)
            {
                if (!string.IsNullOrEmpty(sBcc[i]))
                    Msg.Bcc.Add(new MailAddress(sBcc[i]));
            }
            #region 通知底圖
            //底圖檔案
            LinkedResource lrHead = new LinkedResource(@"D:\Schedule\SyncERP\Notify-Head.jpg");
            lrHead.ContentId = "Notify-Head";
            lrHead.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
            LinkedResource lrFooter = new LinkedResource(@"D:\Schedule\SyncERP\Notify-Footer.jpg");
            lrFooter.ContentId = "Notify-Footer";
            lrFooter.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
            //郵件格式
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(sMessage, null, "text/html");
            htmlView.LinkedResources.Add(lrHead);
            htmlView.LinkedResources.Add(lrFooter);
            Msg.AlternateViews.Add(htmlView);
            #endregion

            Msg.From = new MailAddress(dllconfig.MailSender);
            Msg.Subject = sSubject;//(dllconfig.DebugMode ? "[SCM測試]" : "[SCM]")
            Msg.Body = (DLLConfig.DebugMode ? "Original:" + sToMail : "") + Environment.NewLine + Environment.NewLine + sMessage;
            Msg.IsBodyHtml = bIsHtml;
            Msg.BodyEncoding = Encoding.UTF8;
            byte[] bAttchment = File.ReadAllBytes(dAttchment.FirstOrDefault(d => d.Key == "FullName").Value);//Encoding.UTF8.GetBytes(
            MemoryStream msAttachment = new MemoryStream(bAttchment);
            Attachment attachment = new Attachment(msAttachment, $"{dAttchment.FirstOrDefault(d => d.Key == "OriginalName").Value}", dAttchment.FirstOrDefault(d => d.Key == "FileContent").Value);
            Msg.Attachments.Add(attachment);
            SmtpClient client = new SmtpClient(dllconfig.SMTPServer);
            try
            {
                client.Credentials = new System.Net.NetworkCredential(dllconfig.MailSender, dllconfig.MailPassword);
                client.Send(Msg);
                bResult = true;
            }
            catch (Exception ex)
            {
                //WriteEventLog(ex);
                CommonFunction cf = new CommonFunction();
                cf.SetLogFile(@"C:\inetpub\wwwroot\SCMWeb.log", ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return bResult;
        }
        #endregion

        #region SCM傳真訊息指定收件人可附件
        public bool SendFaxSCM_del(string sFaxNo, string sSubject, string sMessage, bool bIsHtml, string sAttchment)
        {
            bool bResult = false;
            DLLConfig dllconfig = new DLLConfig();
            MailMessage Msg = new MailMessage();
            string[] sMail = (DLLConfig.DebugMode ? dllconfig.FaxAdminMail.Split(';') : string.Format(dllconfig.FaxHost,sFaxNo).Split(';'));
            string sToMail = "";
            for (int i = 0; i < sMail.Length; i++)
            {
                Msg.To.Add(new MailAddress(sMail[i]));
                sToMail += sMail[i];
            }
            Msg.From = new MailAddress(dllconfig.MailSender);
            Msg.Subject = sSubject;//(dllconfig.DebugMode ? "[SCM測試]" : "[SCM]")
            Msg.Body = (DLLConfig.DebugMode ? "Original:" + sToMail : "") + Environment.NewLine + Environment.NewLine + sMessage;
            Msg.IsBodyHtml = bIsHtml;
            Msg.BodyEncoding = Encoding.UTF8;
            if (sAttchment != "")
            {
                //Get some binary data
                byte[] bAttchment = Encoding.UTF8.GetBytes(sAttchment);
                MemoryStream msAttachment = new MemoryStream(bAttchment);
                Msg.Attachments.Add(new Attachment(msAttachment, string.Format("{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmdd")), "text/plain"));
            }
            SmtpClient client = new SmtpClient(dllconfig.SMTPServer);
            try
            {
                client.Send(Msg);
                bResult = true;
            }
            catch (Exception ex)
            {
                //WriteEventLog(ex);
                CommonFunction cf = new CommonFunction();
                cf.SetLogFile(@"C:\inetpub\wwwroot\SCMWeb.log", ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return bResult;
        }
        #endregion

        #region Write EventLog
        public void WriteEventLog(Exception ex)
        {
            DLLConfig dllconfig = new DLLConfig();
            EventLog eventlog = new EventLog("Application");
            eventlog.Source = dllconfig.ProgramCode;
            eventlog.WriteEntry(string.Format("Message:{0}\nSource:{1}\nStackTrace:{2}\nStation:{3}", ex.Message, ex.Source, ex.StackTrace, HttpContext.Current.Request.UserHostAddress), EventLogEntryType.Error);//Environment.MachineName
        }
        #endregion

        #region 寫文字檔
        public void SetLogFile(string sLogfile, string msg)
        {
            using (System.IO.StreamWriter sw = new StreamWriter(sLogfile, true, Encoding.Unicode))
            {
                sw.WriteLine(string.Format("DateTime:{0},Station:{1}", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"), HttpContext.Current.Request.UserHostAddress));//System.Net.Dns.GetHostName()
                sw.WriteLine(msg);
                sw.WriteLine("");
                sw.Flush();
                //sw.Close();
            }
        }
        #endregion

        //產生Excel檔案
        public static string genExcelString(DataTable dtExport)
        {
            string sResult = "";
            int tRowCount = dtExport.Rows.Count;
            int tColumnCount = dtExport.Columns.Count;

            //引用這三個xmlns
            sResult += "<html xmlns:o='urn:schemas-microsoft-com:office:office'";
            sResult += "xmlns:x='urn:schemas-microsoft-com:office:excel'";
            sResult += "xmlns='http://www.w3.org/TR/REC-html40'>";
            sResult += "<meta http-equiv=Content-Type content=text/html;charset=utf-8>";

            //在head中加入xml定義
            sResult += "\n <head>";
            sResult += "\n <xml>";
            sResult += "\n <x:ExcelWorkbook>";
            sResult += "\n <x:ExcelWorksheets>";
            sResult += "\n <x:ExcelWorksheet>";
            sResult += "\n <x:Name>MFG01</x:Name>";
            //以下針對此工作表進行屬性設定
            sResult += "\n <x:WorksheetOptions>";
            sResult += "\n <x:FrozenNoSplit/>";
            //設定凍結行號
            sResult += "\n <x:SplitHorizontal>2</x:SplitHorizontal>";
            //設定卷軸起始行號
            sResult += "\n <x:TopRowBottomPane>2</x:TopRowBottomPane>";
            sResult += "\n <x:ActivePane>2</x:ActivePane>";
            sResult += "\n </x:WorksheetOptions>";
            sResult += "\n </xml>";
            sResult += "\n </head>";

            sResult += "\n <body>";
            sResult += "<Table borderColor=black border=1>";
            sResult += "\n <TR>";
            for (int i = 0; i < tColumnCount; i++)
            {
                sResult += "\n <TD  bgcolor = #fff8dc>";
                sResult += dtExport.Columns[i].ColumnName;
                sResult += "\n </TD>";
            }

            sResult += "\n </TR>";
            for (int j = 0; j < tRowCount; j++)
            {
                sResult += "\n <TR>";
                for (int k = 0; k < tColumnCount; k++)
                {
                    sResult += "\n <TD align=\"left\" x:num>";
                    sResult += dtExport.Rows[j][k].ToString();
                    sResult += "\n </TD>";
                }
                sResult += "\n </TR>";
            }

            sResult += "</Table>";
            sResult += "</body>";
            sResult += "</html>";
            return sResult;
        }

        public static string genExcelString<T>(IEnumerable<T> dtExport)
        {
            return genExcelString(LINQToDataTable(dtExport));
        }

        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

         public static string BuildExceptionMessage(Exception x)
        {
            Exception logException = x;
            if (x.InnerException != null)
            {
                logException = x.InnerException;
            }
            StringBuilder message = new StringBuilder();
            message.AppendLine();
            if (System.Web.HttpContext.Current != null)
                message.AppendLine("Error in Path : " + System.Web.HttpContext.Current.Request.Path);
            else
                message.AppendLine("Error in Path : " + System.Environment.CurrentDirectory);
            // Get the QueryString along with the Virtual Path
            if (System.Web.HttpContext.Current != null)
                message.AppendLine("Raw Url : " + System.Web.HttpContext.Current.Request.RawUrl);
            // Type of Exception
            message.AppendLine("Type of Exception : " + logException.GetType().Name);
            // Get the error message
            message.AppendLine("Message : " + logException.Message);
            // Source of the message
            message.AppendLine("Source : " + logException.Source);

            // Stack Trace of the error
            message.AppendLine("Stack Trace : " + logException.StackTrace);
            // Method where the error occurred
            message.AppendLine("TargetSite : " + logException.TargetSite);
            // Get the login userid
            if (System.Web.HttpContext.Current != null)
                message.AppendLine("Login User : " + System.Web.HttpContext.Current.User.Identity.Name);
            else
                message.AppendLine("Login User : " + System.Environment.UserName);
            // Get the login IP
            if (System.Web.HttpContext.Current != null)
                message.AppendLine("Login IP : " + HttpContext.Current.Request.UserHostAddress);
            else
                message.AppendLine("Login IP : " + System.Environment.MachineName);
            //Mail message
            DLLConfig dllconfig = new DLLConfig();
            if (DLLConfig.DebugMode == false || DLLConfig.IsSendMail) //正式區或是否寄送
                SendEmail(message.ToString());
            return message.ToString();
        }
    }
}