﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonRandomizer.Backend.EnumTypes;
using PokemonRandomizer.Backend.Utilities;

namespace PokemonRandomizer.Backend.DataStructures
{
    public class Encounter
    {
        public PokemonSpecies pokemon;
        public int level;
        public int maxLevel;
        public Encounter(PokemonSpecies pokemon, int level, int maxLevel)
        {
            this.pokemon = pokemon;
            this.level = level;
            this.maxLevel = maxLevel;
        }
        public override string ToString()
        {
            return pokemon.ToDisplayString() + " Lv" + (level == maxLevel ? level.ToString() : level + "-" + maxLevel);
        }
    }
}