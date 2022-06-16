using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BugTracker.Services
{
    public static class EnumNameService
    {
        public static string GetEnumDisplayName(this Enum enumType)
        {
            return enumType
                .GetType()
              .GetMember(enumType.ToString())
              .FirstOrDefault()
              .GetCustomAttribute<DisplayAttribute>()
              .Name!;
        }
    }
}
