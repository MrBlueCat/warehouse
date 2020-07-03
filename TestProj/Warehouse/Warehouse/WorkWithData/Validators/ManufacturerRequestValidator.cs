using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class ManufacturerRequestValidator : AbstractValidator<RequestManufacturerModel>
    {
        public ManufacturerRequestValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Name).Cascade(CascadeMode.StopOnFirstFailure).NotNull().WithMessage("'{PropertyName}' should not be null").NotEmpty();
        }
    }
}
