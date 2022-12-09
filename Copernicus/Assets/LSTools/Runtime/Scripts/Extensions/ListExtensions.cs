namespace LazySloth.Utilities
{
    using System.Collections.Generic;

    public static class ListExtensions
    {
        /// <summary>
        /// Will add element only if list doesn't contain already same element
        /// </summary>
        /// <param name="list"></param>
        /// <param name="element">Element to add</param>
        /// <typeparam name="T"></typeparam>
        public static void AddUnique<T>(this List<T> list, T element)
        {
            if (!list.Contains(element))
            {
                list.Add(element);
            }
        }
    }
}