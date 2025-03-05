using Microsoft.Extensions.Localization;
using PhDManager.Locales;

namespace PhDManager.Services
{
    public class EnumService(IStringLocalizer<Enums> Localizer)
    {
        public List<(TEnum, string)> GetLocalizedEnumValues<TEnum>() where TEnum : struct, Enum
        {
            var values = Enum.GetValues<TEnum>();
            var result = new List<(TEnum, string)>();

            foreach (var value in values)
            {
                var key = $"{typeof(TEnum).Name}{value}";
                var localized = Localizer[key];
                result.Add((value, localized.ResourceNotFound ? value.ToString() : localized));
            }

            return result;
        }

        public string GetLocalizedEnumValue<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            var key = $"{typeof(TEnum).Name}{value}";
            var localized = Localizer[key];

            return localized.ResourceNotFound ? value.ToString() : localized;
        }
    }
}
