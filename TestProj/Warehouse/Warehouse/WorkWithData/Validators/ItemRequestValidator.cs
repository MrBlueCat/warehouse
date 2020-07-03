using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class ItemRequestValidator : AbstractValidator<RequestItemModel>
    {
        public ItemRequestValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Name).Cascade(CascadeMode.StopOnFirstFailure).NotNull().WithMessage("'{PropertyName}' should not be null").NotEmpty();
            RuleFor(x => x.ManufacturerId).NotNull().WithMessage("'{PropertyName}' should be specified");
        }
    }
}
