using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GeradorTestes.Dominio
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> target, int count = 0)
        {
            Random r = new Random();

            if (count == 0)
                return target.OrderBy(x => r.Next());
            else
                return target.OrderBy(x => r.Next())
                    .Take(count);
        }
    }

    public static class CharExtension
    {
        public static char Next(this char instancia)
        {
            int numero = instancia;

            numero++;

            return (char)numero;
        }
    }

    public static class EnumExtension
    {
        public static string GetDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());

            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                return attribute.Description;

            return "Anotação não informada";
        }
    }
}
