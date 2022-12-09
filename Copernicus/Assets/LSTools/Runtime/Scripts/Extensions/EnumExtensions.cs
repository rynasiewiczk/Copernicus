namespace LazySloth.Utilities
{
    using System;
    using System.Linq;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public static class EnumExtensions
    {
        public static T Next<T>(T src, bool loop = true) where T : struct
        {
            if (!typeof(T).IsEnum) { throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName)); }

            var array = (T[])Enum.GetValues(src.GetType());
            var j = Array.IndexOf(array, src) + 1;
            if (loop)
            {
                return (array.Length == j) ? array.First() : array[j];
            }
            else
            {
                return array[Mathf.Clamp(j, 0, array.Length - 1)];
            }
        }

        public static T Previous<T>(T src, bool loop = true) where T : struct
        {
            if (!typeof(T).IsEnum) { throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName)); }

            var array = (T[])Enum.GetValues(src.GetType());
            var j = Array.IndexOf(array, src) - 1;
            if (loop)
            {
                return (j == -1) ? array.Last() : array[j];
            }
            else
            {
                return array[Mathf.Clamp(j, 0, array.Length - 1)];
            }
        }

        public static int FindUnusedIntValue<T>(int digitsCount = 6) where T : Enum
        {
            digitsCount = Mathf.Max(1, digitsCount);

            var array = (T[])Enum.GetValues(typeof(T));
            var intValues = array.Select(x => x.GetHashCode());

            var safetyCounter = 100;

            var random = GenerateRandomNumber(digitsCount);
            while (EnumContainsValue(random) && safetyCounter > 0)
            {
                random = GenerateRandomNumber(digitsCount);
                safetyCounter--;
            }

            if (EnumContainsValue(random))
            {
                Debug.LogWarning($"Failed to find an unique value for enum type {nameof(T)}. Returning value {random}");
            }

            return random;

            static int GenerateRandomNumber(int digitsCount)
            {
                var s = "";
                for (int i = 0; i < digitsCount; i++)
                {
                    s += Random.Range(0, 10);
                }

                return int.Parse(s);
            }

            bool EnumContainsValue(int value)
            {
                return intValues.Contains(value);
            }
        }
    }
}