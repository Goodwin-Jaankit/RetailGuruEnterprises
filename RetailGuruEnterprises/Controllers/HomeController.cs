using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RetailGuruEnterprises.Controllers
{
    public class HomeController : Controller
    {
        private RetailGuruEntities db = new RetailGuruEntities();
        public ActionResult Login()
        {
            // Clear session on loading login page
            ClearSession();
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(string email, string password)
        {
            ClearSession();
            try
            {

                if (email != null && password != null)
                {
                    var finduser = db.UserTables.Where(u => u.EmailAddress == email && u.Password == password).ToList();
                    if (finduser.Count() == 1)
                    {
                        Session["UserID"] = finduser[0].UserID;
                        Session["UserTypeID"] = finduser[0].UserType_ID;
                        Session["FullName"] = finduser[0].FullName;
                        Session["UserName"] = finduser[0].UserName;
                        Session["Password"] = finduser[0].Password;
                        Session["ContactNo"] = finduser[0].ContactNo;
                        Session["EmailAddress"] = finduser[0].EmailAddress;
                        Session["Address"] = finduser[0].Address;
                        var userid = finduser[0].UserID;
                        var employeephoto = db.EmployeeTables
                            .Where(s => s.User_ID == userid)
                            .FirstOrDefault();

                        if (employeephoto != null)
                        {
                            Session["Photo"] = employeephoto.Photo;
                        }
                        else
                        {
                          
                                // Set the session photo to the path of the default photo
                                Session["Photo"] = "~/Content/EmployeePhotos/5.png";
                            
                        }





                        string url = string.Empty;
                        if (finduser[0].UserType_ID == 2)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserType_ID == 3)
                        {
                            return RedirectToAction("Index", "EmployeeTables");
                        }
                        else if (finduser[0].UserType_ID == 4)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserType_ID == 1)
                        {
                            return RedirectToAction("About");
                        }
                        else
                        {
                            url = "About";
                        }
                        return RedirectToAction(url);
                    }
                    else
                    {
                        ClearSession();
                        ModelState.Clear();
                        TempData["ErrorMessage"] = "Invalid email or password.";
                    }
                }
                else
                {
                    ClearSession();
                    ModelState.Clear();
                    TempData["ErrorMessage"] = "Email and password are required.";
                }
            }
            catch (Exception ex)
            {
                ClearSession();
                ModelState.Clear();
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
            }

            return RedirectToAction("Login");
        }

        private void ClearSession()
        {

            Session["UserID"] = null;
            Session["UserTypeID"] = null;
            Session["FullName"] = null;
            Session["UserName"] = null;
            Session["Password"] = null;
            Session["ContactNo"] = null;
            Session["EmailAddress"] = null;
            Session["Address"] = null;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            ClearSession();
            Session.Abandon();
            return RedirectToAction("Login");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePasswordU(string oldpassword, string newpassword, string confirmpassword)
        {
            if (newpassword != confirmpassword)
            {
                ViewBag.Message = "Not Matched!";
                return View("ChangePassword");
            }

            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            var getuser = db.UserTables.Find(userid);
            if (getuser.Password == oldpassword.Trim())
            {
                getuser.Password = newpassword.Trim();

            }
            db.Entry(getuser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Logout");
        }

        private static Dictionary<string, string> otpStore = new Dictionary<string, string>();

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RequestOtp(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["ErrorMessage"] = "Email is required.";
                return RedirectToAction("ForgotPassword");
            }

            // Check if the email exists in UserTable
            var user = db.UserTables.FirstOrDefault(u => u.EmailAddress == email);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Email does not exist in our records.";
                return RedirectToAction("ForgotPassword");
            }

            // Generate OTP
            var otp = new Random().Next(100000, 999999).ToString();

            // Save OTP to a temporary store (in-memory dictionary for example purposes)
            otpStore[email] = otp;

            // Send OTP to user's email
            try
            {
                SendOtpEmail(email, otp);
                TempData["Message"] = "OTP has been sent to your email.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error sending email. Please try again later.";
          
                Console.WriteLine($"Exception: {ex.Message}");
               
            }

            return RedirectToAction("ForgotPassword");
        }

        private void SendOtpEmail(string email, string otp)
        {
            var fromAddress = new MailAddress("goodwinjacky233@gmail.com", "RetailGuru");
            var toAddress = new MailAddress(email);
            const string fromPassword = "rsba fwdi voiu nsti"; // Replace this with your actual password or app-specific password
            const string subject = "Your OTP Code";
            string body = $"Your OTP code is {otp}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            try
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (SmtpFailedRecipientsException ex)
            {
                foreach (SmtpFailedRecipientException t in ex.InnerExceptions)
                {
                    var status = t.StatusCode;
                    Console.WriteLine($"Failed to deliver message to {t.FailedRecipient}: {status}");
                }
                throw;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }


       
        [HttpPost]
        public ActionResult VerifyOtp(string otp)
        {
            if (string.IsNullOrEmpty(otp))
            {
                TempData["OtpError"] = "OTP is required.";
                return RedirectToAction("ForgotPassword");
            }

            // Check OTP (ensure the correct email is checked as well)
            if (otpStore.Values.Contains(otp))
            {
                // OTP is correct, store the verified email in session
                var email = otpStore.FirstOrDefault(x => x.Value == otp).Key;
                Session["VerifiedEmail"] = email;

                TempData["OtpSuccess"] = "OTP verified successfully. You can now reset your password.";
                return RedirectToAction("NewPassword");
            }
            else
            {
                TempData["OtpError"] = "Invalid OTP.";
                return RedirectToAction("ForgotPassword");
            }
        }

        public ActionResult NewPassword()
        {
            // Ensure the user is coming from a verified OTP session
            if (Session["VerifiedEmail"] == null)
            {
                TempData["ErrorMessage"] = "You must verify your OTP before resetting your password.";
                return RedirectToAction("ForgotPassword");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPassword(string newPassword, string confirmPassword)
        {
            if (Session["VerifiedEmail"] == null)
            {
                TempData["ErrorMessage"] = "You must verify your OTP before resetting your password.";
                return RedirectToAction("ForgotPassword");
            }

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                TempData["ErrorMessage"] = "Password and Confirm Password fields are required.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                TempData["ErrorMessage"] = "Passwords do not match.";
                return View();
            }

            // Get the email from the session (this was set after OTP verification)
            var email = Session["VerifiedEmail"].ToString();
            var user = db.UserTables.FirstOrDefault(u => u.EmailAddress == email);

            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("ForgotPassword");
            }

            // Update the user's password
            user.Password = newPassword; // Consider hashing the password before storing
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            // Clear the session after password reset
            Session["VerifiedEmail"] = null;

            TempData["SuccessMessage"] = "Your password has been reset successfully.";
            return RedirectToAction("Login");
        }

    }
}