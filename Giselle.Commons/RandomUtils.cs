using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons
{
    public static class RandomUtils
    {
        public static Random Default { get; set; } = new Random();

        public static int NextDirection(this Random random)
        {
            var map = new Dictionary<int, int>
            {
                [+1] = 50,
                [-1] = 50
            };
            return map.FirstByWeight(random);
        }

        public static int NextInclusive(this Random random, int maxValue)
        {
            return random.Next(maxValue + 1);
        }

        public static int NextInclusive(this Random random, int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue + 1);
        }

        public static bool NextBoolean(this Random random)
        {
            return random.TestRatio(0.5F);
        }

        public static Random Read(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                var formatter = new BinaryFormatter();
                var random = formatter.Deserialize(stream) as System.Random;
                return random;
            }

        }

        public static byte[] Save(this Random random)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, random);
                return stream.ToArray();
            }

        }

        public static List<T> GetRandom<T>(this IList<T> list, Random random, int count)
        {
            var randoms = new List<T>();

            if (list != null)
            {
                random = random ?? Default;

                var copy = new List<T>(list);

                for (var i = 0; i < Math.Min(list.Count, count); i++)
                {
                    var index = random.Next(0, copy.Count);
                    var item = copy[index];

                    copy.RemoveAt(index);
                    randoms.Add(item);
                }

            }

            return randoms;
        }

        public static T GetRandom<T>(this IList<T> list, Random random = null, T fallback = default)
        {
            if (list == null)
            {
                return fallback;
            }

            var c = list.Count;

            if (c > 0)
            {
                random = random ?? Default;
                var index = random.Next(0, c);
                return list[index];
            }
            else
            {
                return fallback;
            }

        }

        public static T GetRandom<T>(this Random random, IList<T> list)
        {
            return list.GetRandom(random);
        }

        public static List<T> GetRandom<T>(this Random random, IList<T> list, int count)
        {
            return list.GetRandom(random, count);
        }

        public static bool TestRatio(this Random random, float chance)
        {
            if (chance <= 0.0F)
            {
                return false;
            }
            else if (chance >= 1.0F)
            {
                return true;
            }

            random = random ?? Default;

            var value = random.NextDouble();
            return value <= chance;
        }

        public static T FirstByWeight<T>(this Random random, Dictionary<T, int> map, T fallback = default)
        {
            return map.FirstByWeight(random, fallback);
        }

        public static T FirstByWeight<T>(this Dictionary<T, int> map, Random random, T fallback = default)
        {
            if (map.Count == 0)
            {
                return fallback;
            }

            random = random ?? Default;

            var totalWeight = map.Sum(p => p.Value);
            var choice = random.Next(totalWeight);
            var sum = 0;

            foreach (var pair in map)
            {
                var weight = pair.Value;

                for (var i = sum; i < weight + sum; i++)
                {
                    if (i >= choice)
                    {
                        return pair.Key;
                    }

                }

                sum += weight;
            }

            return fallback;
        }

    }

}
