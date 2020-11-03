﻿using PokemonRandomizer.Backend.EnumTypes;
using PokemonRandomizer.Backend.Utilities;
using System;
using System.Text;

namespace PokemonRandomizer.Backend.DataStructures
{
    public class RomMetadata
    {
        // Gen III Codes
        private const string gameCodeEm = "BPE";
        private const string gameCodeLg = "BPG";
        private const string gameCodeFr = "BPR";
        private const string gameCodeRu = "AXV";
        private const string gameCodeSp = "AXP";
        // Gen IV Codes
        private const string gameCodePt = "CPU";
        private const string gameCodeDi = "ADA";
        private const string gameCodePl = "APA";
        private const string gameCodeHg = "IPK";
        private const string gameCodeSs = "IPG";

        private const int gbaRomNameOffset = 0xA0;
        private const int gbaRomNameSize = 12;
        private const int gbaRomCodeOffset = 0xAC;
        private const int gbaRomCodeSize = 4;
        private const int gbaRomMakerOffset = 0xB0;
        private const int gbaRomMakerSize = 2;
        private const int gbaRomVersionOffset = 0xBC;
        private const int gbaRomVersionSize = 1;

        // DS Header Information From http://dsibrew.org/wiki/DSi_Cartridge_Header
        private const int ndsRomNameOffset = 0x0;
        private const int ndsRomNameSize = 12;
        private const int ndsRomCodeOffset = 0xC;
        private const int ndsRomCodeSize = 4;
        private const int ndsRomVersionOffset = 0x1E;
        private const int ndsRomVersionSize = 1;

        public bool IsFireRedOrLeafGreen => Gen == Generation.III && (MatchCode(gameCodeFr) || MatchCode(gameCodeLg));
        public bool IsRubySapphireOrEmerald => Gen == Generation.III && (MatchCode(gameCodeEm) || MatchCode(gameCodeRu) || MatchCode(gameCodeSp));
        public bool IsEmerald => Gen == Generation.III && MatchCode(gameCodeEm);
        public Generation Gen { get; private set; }
        public string Code { get; private set; }
        public int Version { get; private set; }
        public string Name { get; private set; }

        private bool MatchCode(string code)
        {
            return Code.Substring(0, 3) == code.Substring(0, 3);
        }

        public RomMetadata(byte[] rawRom)
        {
            InitGeneration(rawRom);
            InitMetaData(rawRom);
        }

        // set the Rom generation (from the file size)
        private void InitGeneration(byte[] rawRom)
        {
            switch (rawRom.Length)
            {
                case 1048576:    // 1mb
                    Gen = Generation.I;
                    break;
                case 2097152:    // 2mb
                    Gen = Generation.II;
                    break;
                case 16777216:   // 16mb
                    Gen = Generation.III;
                    break;
                case 67108864:   // 64mb  (diamond and pearl)
                case 134217728:  // 128mb (heart gold, soul silver, and platinum)
                    Gen = Generation.IV;
                    break;
                case 268435456:  // 256mb (black and white)
                case 536870912:  // 512mb (black 2 and white 2)
                    Gen = Generation.V;
                    break;
                default:
                    //Add fallback based on file extension?
                    //Add manual override?
                    throw new Exception("rom file is not a valid length, unable to detect generation");
            }
        }

        // read code, name and version info from rom
        private void InitMetaData(byte[] rawRom)
        {
            switch (Gen)
            {
                case Generation.I:
                    break;
                case Generation.II:
                    break;
                case Generation.III:
                    Name = Encoding.ASCII.GetString(rawRom.ReadBlock(gbaRomNameOffset, gbaRomNameSize));
                    Code = Encoding.ASCII.GetString(rawRom.ReadBlock(gbaRomCodeOffset, gbaRomCodeSize));
                    Version = rawRom[gbaRomVersionOffset]; // Version is one byte
                    break;
                case Generation.IV:
                    Name = Encoding.ASCII.GetString(rawRom.ReadBlock(ndsRomNameOffset, ndsRomNameSize));
                    Code = Encoding.ASCII.GetString(rawRom.ReadBlock(ndsRomCodeOffset, ndsRomCodeSize));
                    Version = rawRom[ndsRomVersionOffset]; // Version is one byte
                    break;
                case Generation.V:
                    break;
                case Generation.VI:
                    break;
                case Generation.VII:
                    break;
                default:
                    throw new Exception("Gen " + Gen.ToDisplayString() + " is not supported. unable to find metadata");
            }
            // Remove null terminators from name
            Name = Name.Replace("\0", string.Empty);
        }
    }
}
