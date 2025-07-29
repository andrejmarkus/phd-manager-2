using Microsoft.Extensions.Localization;
using PhDManager.Locales;

namespace PhDManager.Services
{
    public class EnumService(IStringLocalizer<Enums> localizer)
    {
        public List<(TEnum, string)> GetLocalizedEnumValues<TEnum>() where TEnum : struct, Enum
        {
            var values = Enum.GetValues<TEnum>();

            return values.Select(value => (value, GetLocalizedEnumValue(value))).ToList();
        }

        public string GetLocalizedEnumValue<TEnum>(TEnum value)
        {
            var key = $"{typeof(TEnum).Name}{value}";
            var localized = localizer[key];

            return localized.ResourceNotFound ? value.ToString() : localized;
        }
    }
}
