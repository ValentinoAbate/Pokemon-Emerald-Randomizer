﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEmeraldRandomizer.Backend
{
    public class WeightedSet<T> : IEnumerable<KeyValuePair<T, float>>
    {
        public delegate float Metric(T item);
        
        public T[] Items { get => items.Keys.ToArray(); }
        public float[] Weights { get => items.Values.ToArray(); }
        public float Count { get => items.Count; }
        public Dictionary<T, float> Percentages
        {
            get
            {
                var ret = new Dictionary<T, float>();
                float totalWieght = Weights.Aggregate((a, b) => a + b);
                foreach (var kvp in items)
                {
                    ret.Add(kvp.Key, (kvp.Value / totalWieght) * 100);
                }
                return ret;
            }
        }

        private Dictionary<T, float> items = new Dictionary<T, float>();

        #region Constructors
        public WeightedSet() {}
        public WeightedSet(IEnumerable<T> items, IEnumerable<float> weights)
        {
            IEnumerator<float> e = weights.GetEnumerator();
            e.MoveNext();
            foreach(T item in items)
            {
                Add(item, e.Current);
                e.MoveNext();
            }
        }
        public WeightedSet(IEnumerable<KeyValuePair<T, float>> pairs)
        {
            foreach (var kvp in pairs)
                Add(kvp.Key, kvp.Value);
        }
        #endregion

        public void Add(WeightedSet<T> set)
        {
            foreach(var item in set.Items)
            {
                Add(item, set[item]);
            }
        }
        public void Add(T item, float weight = 1)
        {
            if (items.ContainsKey(item))
                items[item] += weight;
            else
                items.Add(item, weight);
        }
        public void Remove(T item)
        {
            items.Remove(item);
        }
 
        public void Normalize()
        {
            float max = items.Max((kvp) => kvp.Value);
            foreach (var key in items.Keys.ToArray())
                items[key] = items[key] / max;
        }
        public void ApplyMetric(Metric m)
        {
            foreach(var key in items.Keys)
                items[key] += m(key);
        }

        public override string ToString()
        {
            List<string> ret = new List<string>();
            float totalWieght = Weights.Aggregate((a, b) => a + b);
            foreach(var kvp in items)
            {
                ret.Add(kvp.Key.ToString() + ": " + ((kvp.Value / totalWieght) * 100).ToString("#.0") + "%");
            }
            return ret.Aggregate((a,b) => a + b + "\n");
        }
        public float this[T item]
        {
            get => items[item];
        }

        #region IEnumerable Implementation
        public IEnumerator<KeyValuePair<T, float>> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
        #endregion
    }
}
