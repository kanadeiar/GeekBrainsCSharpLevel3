using System;
using System.Linq;

namespace MailSender.lib.Service
{
    /// <summary> Инструмент для копирования объектов </summary>
    public static class ObjectCopyer
    {
        /// <summary> Копирование объекта </summary>
        /// <param name="From">Откуда</param>
        /// <param name="To">Куда</param>
        public static void CopyTo(this object From, object To)
        {
            if (From is null) throw new ArgumentNullException(nameof(CopyTo));
            if (To is null) throw new ArgumentNullException(nameof(To));

            var fromType = From.GetType();
            var toType = To.GetType();

            var destinationProps = toType.GetProperties().Where(p => p.CanWrite).ToDictionary(p => p.Name);
            foreach (var fromProp in fromType.GetProperties().Where(p => p.CanRead))
            {
                if (fromProp.Name == "Id") continue;
                if (!destinationProps.TryGetValue(fromProp.Name, out var toProp)) continue;
                if (!toProp.PropertyType.IsAssignableTo(fromProp.PropertyType)) continue;
                var value = fromProp.GetValue(From);
                toProp.SetValue(To, value);
            }
        }
    }
}
