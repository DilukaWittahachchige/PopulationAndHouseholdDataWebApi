using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopulationAndHouseholdDataWebApi.Models
{
    /// <summary>
    ///  Custom Model Binder for pass state list
    /// </summary>
    public class CustomModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var values = bindingContext.ValueProvider.GetValue("state");

            if (values.Length == 0)
            {
                return Task.CompletedTask;
            }

            var valueList = values.ToList();

            var result = new StateQuery()
            {
                State = new List<int>()
            };

            foreach (var id in valueList)
            {
                result.State.Add(int.Parse(id));
            }
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
