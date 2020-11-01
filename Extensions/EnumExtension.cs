using System;
using System.ComponentModel;

namespace Extensions
{
    /// <summary>
    /// Расширение для Enum типов.
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Возвращает значение аттрибута для Enum поля.
        /// </summary>
        /// <typeparam name="T">Класс аттрибута.</typeparam>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        /// <summary>
        /// Возвращает значение аттрибута <see cref="DescriptionAttribute"/>.
        /// </summary>
        public static string ToName(this Enum value)
        {
            var attribute = value.GetAttributeOfType<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
