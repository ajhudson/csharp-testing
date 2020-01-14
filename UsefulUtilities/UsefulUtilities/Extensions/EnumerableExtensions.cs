using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulUtilities.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> WhereInDateRange<TSource>(this IEnumerable<TSource> source, DateTime? start, DateTime? end, Func<TSource, DateTime?> getDatePropertyFunc) where TSource : class, new()
        {
            DateTime? from;
            DateTime? to;

            if (start > end)
            {
                from = end;
                to = start;
            } 
            else
            {
                from = start;
                to = end;
            }

            bool fromDefined = from.HasValue;
            bool toDefined = to.HasValue;
            bool bothDefined = fromDefined && toDefined;
            bool onlyFromDefined = fromDefined && !toDefined;
            bool onlyToDefined = !fromDefined && toDefined;
            bool noRangeDefined = !fromDefined && !toDefined;
            
            foreach (var currentElement in source)
            {
                DateTime? propertyValue = getDatePropertyFunc(currentElement);

                if (!propertyValue.HasValue)
                {
                    yield return currentElement; // if property is null then return everything
                }

                bool isValid = false;
                bool fromInRange = from.HasValue && propertyValue >= from.Value;
                bool toInRange = to.HasValue && propertyValue <= to.Value;

                if (bothDefined)
                {
                    isValid = fromInRange && toInRange;
                }
                else if (onlyFromDefined)
                {
                    isValid = fromInRange;
                }
                else if (onlyToDefined)
                {
                    isValid = toInRange;
                } 
                else if (noRangeDefined)
                {
                    isValid = true;
                }

                if (isValid)
                {
                    yield return currentElement;
                }

            }
        }

    }
}
