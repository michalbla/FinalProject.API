using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI_FinalProject.Model.Dtos.UserDto;
using TNAI_FinalProject.Repository.Users;

namespace TNAI_FinalProject.Dto.UserDto
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterInputUserDto>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserDtoValidator(IUserRepository userRepository) 
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .CustomAsync(async (value, context, cancellation) =>
                {
                    var emailExists = await _userRepository.EmailExistAsync(value);
                    if (emailExists)
                    {
                        context.AddFailure("Email", "Email address is already in use.");
                    }
                });

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password).WithMessage("Passwords do not match");
        }
    }
}
