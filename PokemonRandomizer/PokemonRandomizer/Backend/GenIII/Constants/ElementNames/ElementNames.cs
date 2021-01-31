﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRandomizer.Backend.GenIII.Constants.ElementNames
{
    /// <summary>
    /// Contains symbolic constants for referencing the names of elements in a Gen III info file.
    /// </summary>
    public static class ElementNames
    {
        // Map elements
        public const string mapLabels = "mapLabels";
        public const string mapBankPointers = "mapBankPointers";
        public const string maps = "maps";
        // Pokemon Elements
        public const string evolutions = "evolutions";
        public const string tmHmCompat = "tmHmCompat";
        public const string pokemonBaseStats = "pokemonBaseStats";
        public const string tutorCompat = "moveTutorCompat";
        public const string movesets = "movesets";
        public const string starterPokemon = "starterPokemon";
        // Data elements
        public const string moveData = "moveData";
        public const string tutorMoves = "moveTutorMoves";
        public const string tmMoves = "tmMoves";
        public const string hmMoves = "hmMoves";
        public const string itemData = "itemDefinitions";
        public const string trainerBattles = "trainerBattles";
        // Hacks and Tweaks / Misc
        public const string pcPotion = "pcPotion";
        public const string runIndoors = "runIndoors";
        public const string textSpeed = "textSpeed";
        public const string evolveWithoutNatDex = "evolveWithoutNationalDex";
    }
}
