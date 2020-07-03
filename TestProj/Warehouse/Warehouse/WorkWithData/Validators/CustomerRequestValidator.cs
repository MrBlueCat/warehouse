using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class CustomerRequestValidator : AbstractValidator<RequestCustomerModel>
    {
        public CustomerRequestValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Name).Cascade(CascadeMode.StopOnFirstFailure).NotNull().WithMessage("'{PropertyName}' should not be null").NotEmpty();
        }
    }
}
