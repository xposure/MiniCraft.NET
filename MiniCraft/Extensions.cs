using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniCraft
{
    public static class Extensions
    {
        public static float nextFloat(this Random random)
        {
            return (float)random.NextDouble();
        }

        public static float nextGaussian(this Random random)
        {
            return (float)random.NextDouble();
        }

        public static int nextInt(this Random random, int max)
        {
            return random.Next(max);
        }

        public static bool nextbool(this Random random)
        {
            return random.NextDouble() >= 0.5;
        }

        public static int size<T>(this List<T> list)
        {
            return list.Count;
        }

        public static T get<T>(this List<T> list, int index)
        {
            return list[index];
        }

        public static void add<T>(this List<T> list, int index, T item)
        {
            list.Insert(index, item);
        }

        public static void add<T>(this List<T> list, T item)
        {
            list.Add(item);
        }

        public static void remove<T>(this List<T> list, T item)
        {
            list.Remove(item);
        }

        public static T remove<T>(this List<T> list, int index)
        {
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public static void clear<T>(this List<T> list)
        {
            list.Clear();
        }

        public static void addAll<T>(this List<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
                list.Add(item);
        }

        public static void removeAll<T>(this List<T> list, List<T> other)
        {
            foreach (var item in other)
                list.Remove(item);
        }

        public static int length(this string s)
        {
            return s.Length;
        }

    }
}
