using EasyCashIdentityProject.DToLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrate;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class RegisterController : Controller
	{
		private readonly UserManager<AppUser> _userMenager;
		public RegisterController(UserManager<AppUser> userManager)
		{
			_userMenager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
		{
			if (ModelState.IsValid)
			{
				Random random = new Random();
				int code;
				code = random.Next(100000, 1000000);

				AppUser appUser = new AppUser()
				{
					UserName = appUserRegisterDto.Username,
					Name = appUserRegisterDto.Name,
					Surname = appUserRegisterDto.Surname,
					Email = appUserRegisterDto.Email,
					District = "aaaaa",
					City = "bbbbb",
					ImageUrl = "cccc",
					ConfirmCode = code
				};
				var result = await _userMenager.CreateAsync(appUser, appUserRegisterDto.Password);
				if (result.Succeeded)
				{
					MimeMessage mimeMessage = new MimeMessage();
					MailboxAddress mailboxAddressFrom = new MailboxAddress("Easy Cash Admin", "gorkemkorkmaz522@gmail.com");
					MailboxAddress mailboxAddressTo = new MailboxAddress("User", appUser.Email);

					mimeMessage.From.Add(mailboxAddressFrom);
					mimeMessage.To.Add(mailboxAddressTo);

					var bodyBuilder = new BodyBuilder();
					bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz: " + code;
					mimeMessage.Body = bodyBuilder.ToMessageBody();

					mimeMessage.Subject = "Easy Cash Onay Kodu";

					SmtpClient smtpClient = new SmtpClient();
					smtpClient.Connect("smtp.gmail.com", 587, false);
					smtpClient.Authenticate("gorkemkorkmaz522@gmail.com", "yelpgzprsdwukmiq");
					smtpClient.Send(mimeMessage);
					smtpClient.Disconnect(true);

					TempData["Mail"] = appUserRegisterDto.Email;

					return RedirectToAction("Index", "ConfirmMail");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}
			}
			return View();
		}
	}
}
