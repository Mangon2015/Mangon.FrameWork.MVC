using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Sender
{
    /// <summary>
    /// 发送电子邮件
    /// </summary>
    public class EmailSend
    {
        private readonly string _host = ConfigurationManager.AppSettings["emailhost"];
        private readonly string _userName = ConfigurationManager.AppSettings["fromemailusername"];
        private readonly string _passWord = ConfigurationManager.AppSettings["fromemailpassword"];
        private readonly string _port = ConfigurationManager.AppSettings["emailport"];
        private readonly string _EnableSsl = ConfigurationManager.AppSettings["emailEnableSsl"];
        /// <summary>
        /// 邮件服务地址端口号
        /// </summary>
        private int Port
        {
            get
            {
                int port = 25;
                if (!string.IsNullOrWhiteSpace(_port) && !int.TryParse(_port, out port))
                {
                    return 25;
                }
                return port;
            }
        }
        /// <summary>
        /// 是否采用ssl登录
        /// </summary>
        private bool EnableSsl
        {
            get
            {
                bool _enableSsl = false;
                if (!Boolean.TryParse(_EnableSsl, out _enableSsl))
                {
                    _enableSsl = false;
                }
                return _enableSsl;
            }
        }
        /// <summary>
        /// 用户名，
        /// </summary>
        private string Name
        {
            get
            {
                int len = _userName.IndexOf('@');
                string name = _userName;
                if (len >= 0)
                {
                    name = _userName.Substring(0, len);
                }
                return name;
            }
        }

        public EmailSend()
        {
            if (string.IsNullOrWhiteSpace(_host))
            {
                _host = "mail.worldunion.com.cn";
            }
            if (string.IsNullOrWhiteSpace(_userName))
            {
                _userName = "slpgit@worldunion.com.cn";
            }
            if (string.IsNullOrWhiteSpace(_passWord))
            {
                _passWord = @"root\123";
            }
            if (string.IsNullOrWhiteSpace(_port))
            {
                _port = "25";
            }
        }
        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] 发送取消", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("信息已经发送");
            }
            mailSent = true;
        }
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="acceptEmail">接受者电子邮件</param>
        /// <param name="content">发送正文</param>
        /// <param name="subject">主题</param>
        /// <returns>是否成功</returns>
        public bool Send(string acceptEmail, string content, string subject)
        {
            bool success = false;

            if (string.IsNullOrEmpty(acceptEmail) || acceptEmail.Trim() == "")
            {
                throw new ArgumentNullException("接受用户地址不能为空");
            }
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("发送内容不能为空");
            }
            //如果主题为空则获取内容的前10个字符
            subject = subject ?? (content.Length > 10 ? content.Substring(0, 10) : content);
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient(_host, Port);
            // Specify the e-mail sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress(_userName, _userName, System.Text.Encoding.UTF8);
            // 设置接收的电子邮件地址
            MailAddress to = new MailAddress(acceptEmail);
            // 指定邮件内容。
            MailMessage message = new MailMessage(from, to);
            message.Body = content; //内容
            message.IsBodyHtml = true;
            message.Body += Environment.NewLine;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = subject; //主题
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            //当发送结束时，回调函数
            client.SendCompleted += new
            SendCompletedEventHandler(SendCompletedCallback);
            //用户状态可以允许你的回调方法来识别该发送操作的对象。对于此示例，该userToken是字符串常量。
            // string userState = "Send Message";
            // 客户端证书
            client.Credentials = new NetworkCredential(_userName, _passWord);

            if (EnableSsl)
            {
                client.EnableSsl = true;
            }
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            // client.SendAsync(message, userState);
            // client.Send(message);
            // string answer = Console.ReadLine();
            ////如果电子邮件没有发送出去,并且按下取消，然后取消挂起操作
            //if (answer.StartsWith("c") && mailSent == false)
            //{
            //    client.SendAsyncCancel();
            //}
            //清除.


            try
            {
                client.Send(message);
                success = true;
                message.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;

        }

    }
}
