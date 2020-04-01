namespace DogCarePlatform.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using DogCarePlatform.Common;
    using DogCarePlatform.Data.Models;
    using DogCarePlatform.Services.Data;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class RegisterDogsitterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterDogsitterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage ="���� �������� ���������� ����")]
            [EmailAddress(ErrorMessage = "���� �� � ������� ���������� ����")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "���� �������� ������")]
            [StringLength(100, ErrorMessage = "������ {0} ������ �� ������� �� {2} �� {1} �������.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "������")]
            public string Password { get; set; }

            [Required(ErrorMessage ="���� ��������� ��������")]
            [DataType(DataType.Password)]
            [Display(Name = "������������� �� ��������")]
            [Compare("Password", ErrorMessage = "�������� ������ �� ��������.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "���� �������� ��������� �����")]
            [RegularExpression(@"^([+]?359)|0?(|-| )8[789]\d{1}(|-| )\d{3}(|-| )\d{3}$", ErrorMessage = "��������� ��������� ��������� �����")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "���� ��������� ������")]
            [StringLength(500, MinimumLength = 50, ErrorMessage = "������ ������ �� ������� ����� 50 �� 500 �������")]
            [RegularExpression("^[�-� �-� 0-9_.,-]*$", ErrorMessage = "���� ������ �� ��������")]
            [DisplayName("������ 1")]
            public string Question1 { get; set; }

            [Required(ErrorMessage = "���� ��������� ������")]
            [StringLength(500, MinimumLength = 50, ErrorMessage = "������ ������ �� ������� ����� 50 �� 500 �������")]
            [RegularExpression("^[�-� �-� 0-9_.,-]*$", ErrorMessage = "���� ������ �� ��������")]
            [DisplayName("������ 2")]
            public string Question2 { get; set; }

            [Required(ErrorMessage = "���� ��������� ������")]
            [StringLength(500, MinimumLength = 50, ErrorMessage = "������ ������ �� ������� ����� 50 �� 500 �������")]
            [RegularExpression("^[�-� �-� 0-9_.,-]*$", ErrorMessage = "���� ������ �� ��������")]
            [DisplayName("������ 3")]
            public string Question3 { get; set; }

            [Required(ErrorMessage = "���� ��������� ������")]
            [StringLength(500, MinimumLength = 50, ErrorMessage = "������ ������ �� ������� ����� 50 �� 500 �������")]
            [RegularExpression("^[�-� �-� 0-9_.,-]*$", ErrorMessage = "���� ������ �� ��������")]
            [DisplayName("������ 4")]
            public string Question4 { get; set; }

            [Required(ErrorMessage = "���� ��������� ������")]
            [StringLength(500, MinimumLength = 50, ErrorMessage = "������ ������ �� ������� ����� 50 �� 500 �������")]
            [RegularExpression("^[�-� �-� 0-9_.,-]*$", ErrorMessage = "���� ������ �� ��������")]
            [DisplayName("������ 5")]
            public string Question5 { get; set; }

            [Required(ErrorMessage = "���� ��������� ������")]
            [StringLength(500, MinimumLength = 50, ErrorMessage = "������ ������ �� ������� ����� 50 �� 500 �������")]
            [RegularExpression("^[�-� �-� 0-9_.,-]*$", ErrorMessage = "���� ������ �� ��������")]
            [DisplayName("������ 6")]
            public string Question6 { get; set; }

            [Required(ErrorMessage = "���� ��������� ������")]
            [StringLength(500, MinimumLength = 50, ErrorMessage = "������ ������ �� ������� ����� 50 �� 500 �������")]
            [RegularExpression("^[�-� �-� 0-9_.,-]*$", ErrorMessage = "���� ������ �� ��������")]
            [DisplayName("������ 7")]
            public string Question7 { get; set; }

            [Required(ErrorMessage = "���� ��������� ������")]
            [StringLength(500, MinimumLength = 50, ErrorMessage = "������ ������ �� ������� ����� 50 �� 500 �������")]
            [RegularExpression("^[�-� �-� 0-9_.,-]*$", ErrorMessage = "���� ������ �� ��������")]
            [DisplayName("������ 8")]
            public string Question8 { get; set; }

            [Required(ErrorMessage = "���� ��������� ������")]
            [StringLength(500, MinimumLength = 50, ErrorMessage = "������ ������ �� ������� ����� 50 �� 500 �������")]
            [RegularExpression("^[�-� �-� 0-9_.,-]*$", ErrorMessage = "���� ������ �� ��������")]
            [DisplayName("������ 9")]
            public string Question9 { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Dogsitter/SuccessfullySentApplication");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, PhoneNumber = Input.PhoneNumber};

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                         $"���� ���������� ���� ������ �� ��� <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'></a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await this._userManager.AddToRoleAsync(user, GlobalConstants.UnapprovedUserRoleName);
                        user.QuestionsAnswers.Add(new QuestionAnswer { Question = nameof(Input.Question1), Answer = Input.Question1});
                        user.QuestionsAnswers.Add(new QuestionAnswer { Question = nameof(Input.Question2), Answer = Input.Question2});
                        user.QuestionsAnswers.Add(new QuestionAnswer { Question = nameof(Input.Question3), Answer = Input.Question3});
                        user.QuestionsAnswers.Add(new QuestionAnswer { Question = nameof(Input.Question4), Answer = Input.Question4});
                        user.QuestionsAnswers.Add(new QuestionAnswer { Question = nameof(Input.Question5), Answer = Input.Question5});
                        user.QuestionsAnswers.Add(new QuestionAnswer { Question = nameof(Input.Question6), Answer = Input.Question6});
                        user.QuestionsAnswers.Add(new QuestionAnswer { Question = nameof(Input.Question7), Answer = Input.Question7});
                        user.QuestionsAnswers.Add(new QuestionAnswer { Question = nameof(Input.Question8), Answer = Input.Question8});
                        user.QuestionsAnswers.Add(new QuestionAnswer { Question = nameof(Input.Question9), Answer = Input.Question9});

                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
