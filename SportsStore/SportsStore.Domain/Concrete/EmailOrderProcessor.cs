using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using System.Net;
using System.Net.Mail;

namespace SportsStore.Domain.Concrete
{
   public class EmailOrderProcessor:IOrderProcessor
    {
       private emailsetting emailSetting;
       public EmailOrderProcessor(emailsetting setting)
       {
           emailSetting = setting;
       }


       public void ProcessOrder(Cart cart, ShippingDetails ship)
       {
           using (var SmtpClient = new SmtpClient())
           {
               SmtpClient.EnableSsl = emailSetting.usessl;
               SmtpClient.Host = emailSetting.servername;
               SmtpClient.Port = emailSetting.serverport;
               SmtpClient.UseDefaultCredentials = false;
               SmtpClient.Credentials = new NetworkCredential(emailSetting.username, emailSetting.password);
               if (emailSetting.writeasfile)
               {
                   SmtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                   SmtpClient.PickupDirectoryLocation = emailSetting.filelocation;
                   SmtpClient.EnableSsl = false;
               }
               StringBuilder body = new StringBuilder()
               .AppendLine("a new order has been submitted")
               .AppendLine("---")
               .AppendLine("Items:");
               foreach (var line in cart.Lines)
               {
                   var subTotal = line.product.Price * line.quantity;
                   body.AppendFormat("{0}*{1}={2:c}", line.quantity, line.product.Price, subTotal);
               }
               body.AppendFormat("Total Order Value: {0:c}", cart.ComputeTotalVlue())
               .AppendLine("---")
               .AppendLine("Ship to")
               .AppendLine(ship.Name)
               .AppendLine(ship.Line1)
               .AppendLine(ship.Line2 ?? "")
               .AppendLine(ship.Line3 ?? "")
               .AppendLine(ship.State)
               .AppendLine(ship.City)
               .AppendLine(ship.Zip)
               .AppendLine("---")
               .AppendFormat("Gift Warp:{0}", ship.GiftWrap ? "yes" : "no");
               MailMessage mailAddress = new MailMessage(emailSetting.mailfromaddress,
                   emailSetting.mailtoaddress,
                   "New Order submitted!",
                   body.ToString());

               SmtpClient.Send(mailAddress);


           }
       }
    }
   public class emailsetting
   {
       public string mailtoaddress = "order@example.com";
       public string mailfromaddress = "sportstore@example.com";
       public bool usessl = true;
       public string username = "mysmptusername";
       public string password = "mysmptpassword";
       public string servername = "smpt.example.com";
       public int serverport = 587;
       public bool writeasfile = false;
       public string filelocation = @"c:\sports_store_email";
   }
}
