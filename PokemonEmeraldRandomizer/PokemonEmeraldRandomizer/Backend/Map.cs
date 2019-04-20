﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEmeraldRandomizer.Backend
{
    public class Map
    {
        // Header data!

        //Example from https://datacrystal.romhacking.net/wiki/Pok%C3%A9mon_3rd_Generation
        //      Data Type      |           Content          |   Example (from fire red)   |
        //---------------------|----------------------------|-----------------------------|
        //Pointer               Map data                     0x082DD4C0
        //Pointer               Event data                   0x083B4E50
        //Pointer               Map scripts                  0x0816545A
        //Pointer               Connections                  0x0835276C
        //Little endian Short   Music index                  0x012C (Pallet Town)
        //Little endian Short   Map pointer index?           0x004E
        //Byte                  Label index                  0x58 ("Pallet Town")
        //Byte                  Visibility(i.e HM Flash)     0x00
        //Byte                  Weather                      0x02
        //Byte                  Map type(City? Village? etc) 0x01
        //Little endian Short   ???                          0x0601
        //Byte                  Show label on entry          0
        //Byte                  In-battle field model id     0
        
        public int mapDataOffset;
        public int eventDataOffset;
        public int mapScriptsOffset;
        public int connectionOffset;
        public int music;
        public int mpInd;
        public byte labelIndex;
        /// <summary>
        /// Vibility/Flash usage state
        /// In Emerald (info from advance-map: http://ampage.no-ip.info/):
        /// 0x00: normal visibility,
        /// 0x01: dark, flash usable,
        /// 0x02: dark, flash unusable,
        /// 0x03 - 0xFF: unknown/unused
        /// </summary>
        public byte visibility;
        /// <summary>
        /// Weather type. I am currently unsure if this affects in-battle weather
        /// In Emerald (info from advance-map: http://ampage.no-ip.info/): 
        /// 0x00: In-house weather,
        /// 0x01: Sunny weather with clouds in water,
        /// 0x02: Regular weather,
        /// 0x03: Rainy weather,
        /// 0x04: Three snow flakes,
        /// 0x05: Rain with thunder storm,
        /// 0x06: Steady mist,
        /// 0x07: Steady snow,
        /// 0x08: Sand storm,
        /// 0x09: Mist from top right corner,
        /// 0x0A: Dense bright mist,
        /// 0x0B: Cloudy,
        /// 0x0C: Underground flashes,
        /// 0x0D: Heavy rain with thunderstorm,
        /// 0x0E: Underwater mist,
        /// 0x0F - 0xFF: Unknown/unused,
        /// </summary>
        public byte weather;
        /// <summary>
        /// I'm not sure what this value actually changes. Maybe label style?
        /// In Emerald (info from advance-map: http://ampage.no-ip.info/): 
        /// 0x00: Unknown/unused,
        /// 0x01: Village,
        /// 0x02: City,
        /// 0x03: Route,
        /// 0x04: Underground,
        /// 0x05: Underwater,
        /// 0x06 - 0x07: Unknown/unused,
        /// 0x08: Inside,
        /// 0x09: Secret base,
        /// 0x0A - 0xFF: Unknown/unused
        /// </summary>
        public byte mapType;
        public byte unknown;
        public byte unknown2;
        /// <summary>
        /// I'm not sure what the difference between "show name" and "show village/city names is"
        /// In Emerald (info from advance-map: http://ampage.no-ip.info/): 
        /// 0x00: Do not show name,
        /// 0x01: Show name,
        /// 0x02 - 0x05: Unknown/unused,
        /// 0x06: Show village names,
        /// 0x07 - 0x0C: Unknown/unused,
        /// 0x0D: Show city names,
        /// 0x0E - 0xFF: Unknown/unused
        /// </summary>
        public byte showLabelOnEntry;
        /// <summary>
        /// I suspect this value is actually the battle transition type but I'm not sure
        /// In Emerald (info from advance-map: http://ampage.no-ip.info/): 
        /// 0x00: Random encounter,
        /// 0x01: Gym battle,
        /// 0x02: Rocket,
        /// 0x03: Unknown/unused
        /// 0x04: 1. Top 4
        /// 0x05: 2. Top 4
        /// 0x06: 3. Top 4
        /// 0x07: 4. Top 4
        /// 0x08: Big Red Pokeball
        /// </summary>
        public byte battleField;

        // Actual connection data
        public ConnectionData connections;
        // The encounters associated with this map (filled externally in RomParser.cs right now)
        public List<EncounterSet> encounters = new List<EncounterSet>();

        public Map(Rom rom, int offset)
        {
            // Read Header Data
            rom.Seek(offset);
            mapDataOffset = rom.ReadPointer();
            eventDataOffset = rom.ReadPointer();
            mapScriptsOffset = rom.ReadPointer();
            connectionOffset = rom.ReadPointer();
            music = rom.ReadUInt16();
            mpInd = rom.ReadUInt16();
            labelIndex = rom.ReadByte();
            visibility = rom.ReadByte();
            weather = rom.ReadByte();
            mapType = rom.ReadByte();
            unknown = rom.ReadByte();
            unknown2 = rom.ReadByte();
            showLabelOnEntry = rom.ReadByte();
            battleField = rom.ReadByte();
            // Read actual data
            if (connectionOffset == Rom.nullPointer)
                return;
            connections = new ConnectionData(rom, connectionOffset);
        }
    }
}