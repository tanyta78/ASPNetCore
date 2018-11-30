namespace EventWebApp.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Models.Orders;

    public class AvailableTicketsAttribute : Attribute, IModelValidator
    {
        private int availableNumber;
        
        public string ErrorMessage { get; set; }

        public AvailableTicketsAttribute(int number)
        {
            this.availableNumber = number;
        }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var order = (OrderViewModel)context.Model;

            if (order.TicketsCount < this.availableNumber)
            {
                return Enumerable.Empty<ModelValidationResult>();
            }

            return new List<ModelValidationResult>
            {
                new ModelValidationResult(context.ModelMetadata.PropertyName, this.ErrorMessage)
            };
        }

        
    }
}
