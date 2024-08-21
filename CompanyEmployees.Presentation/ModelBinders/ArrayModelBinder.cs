using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Reflection;

namespace CompanyEmployees.Presentation.ModelBinders
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var providedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
            if(string.IsNullOrEmpty(providedValue))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var genericType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
            var converter = TypeDescriptor.GetConverter(genericType);

            //var objectArray = providedValue.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries).Select(x => converter.ConvertFromString(x.Trim())).ToArray();
            var objectArray = providedValue.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim())
                                .Select(x => Guid.TryParse(x, out var guid) ? guid : (Guid?)null)
                                .Where(g => g.HasValue)
                                .Select(g => g.Value)
                                .ToArray();


            var guidArray = Array.CreateInstance(genericType, objectArray.Length);
            objectArray.CopyTo(guidArray, 0);
            bindingContext.Model = guidArray;

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
