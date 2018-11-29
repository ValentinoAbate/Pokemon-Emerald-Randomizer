﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEmeraldRandomizer.Backend
{
    // Normally effective attacks are not kept track of
    public enum TypeEffectiveness
    {
        // This does not appear in actual TypeEffectiveness definitions (used for random weightings)
        // Do not write this value to the rom
        Normal = -1,
        NoEffect,
        NotVeryEffective = 5,
        SuperEffective = 20,
    }

    public class TypeEffectivenessChart
    {
        public int Count { get => typeRelations.Count + ignoreAfterForesight.Count; }
        public int InitCount { get; set; }
        // All of the type relations except for those ignored after foresight is used
        private Dictionary<TypePair, TypeEffectiveness> typeRelations = new Dictionary<TypePair, TypeEffectiveness>();
        // The type relations that are ignored after foresight is used
        private Dictionary<TypePair, TypeEffectiveness> ignoreAfterForesight = new Dictionary<TypePair, TypeEffectiveness>();
        // Helper method to make it easier to add to the list
        public void Add(PokemonType atType, PokemonType dfType, TypeEffectiveness e, bool ignoreAfterForesight = false)
        {
            // Add the new type relation to the proper list
            (ignoreAfterForesight ? this.ignoreAfterForesight : typeRelations).Add(new TypePair(atType, dfType), e); 
        }
        // Set value of type relation (add if not present, else update)
        public void Set(PokemonType atType, PokemonType dfType, TypeEffectiveness e, bool ignoreAfterForesight = false)
        {
            var dict = (ignoreAfterForesight ? this.ignoreAfterForesight : typeRelations);
            var tPair = new TypePair(atType, dfType);
            if (dict.ContainsKey(tPair))
            {
                if (e == TypeEffectiveness.Normal)
                    dict.Remove(tPair);
                else
                    dict[tPair] = e;
            }
            else
                dict.Add(tPair, e);
        }
        public TypeEffectiveness GetEffectiveness(PokemonType atType, PokemonType dfType)
        {
            var tPair = new TypePair(atType, dfType);
            if (typeRelations.ContainsKey(tPair))
                return typeRelations[tPair];
            if (ignoreAfterForesight.ContainsKey(tPair))
                return ignoreAfterForesight[tPair];
            return TypeEffectiveness.Normal;
        }
        public bool ContainsRelation(PokemonType atType, PokemonType dfType)
        {
            var tPair = new TypePair(atType, dfType);
            return typeRelations.ContainsKey(tPair) || ignoreAfterForesight.ContainsKey(tPair);
        }
        public bool IgnoredAfterForesight(PokemonType atType, PokemonType dfType)
        {
            return ignoreAfterForesight.ContainsKey(new TypePair(atType, dfType));
        }
 
        public class TypePair : IEquatable<TypePair>
        {
            public PokemonType attackingType;
            public PokemonType defendingType;
            public TypePair(PokemonType atType, PokemonType dfType)
            {
                attackingType = atType;
                defendingType = dfType;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash = hash * 31 + attackingType.GetHashCode();
                    hash = hash * 31 + defendingType.GetHashCode();
                    return hash;
                }

            }
            public override string ToString()
            {
                return attackingType.ToDisplayString() + " vs " + defendingType.ToDisplayString();
            }
            public bool Equals(TypePair other)
            {
                return (attackingType == other.attackingType) && (defendingType == other.defendingType);
            }
        }
    }
}
