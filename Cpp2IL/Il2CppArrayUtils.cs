﻿using System;
using System.Collections.Generic;
using System.Linq;
using LibCpp2IL;
using Mono.Cecil;

namespace Cpp2IL
{
    public static class Il2CppArrayUtils
    {
        public static readonly List<UsefulOffset> UsefulOffsets = new List<UsefulOffset>
        {
            //32-bit offsets:
            new UsefulOffset("length", 0xC, typeof(int), true),
            
            //64-bit offsets:
            new UsefulOffset("length", 0x18, typeof(int), false)
        };

        public static string? GetOffsetName(uint offset)
        {
            var is32Bit = LibCpp2IlMain.ThePe!.is32Bit;

            return UsefulOffsets.FirstOrDefault(o => o.is32Bit == is32Bit && o.offset == offset)?.name;
        }

        public static PropertyDefinition? GetLengthProperty()
        {
            var arrayType = Utils.TryLookupTypeDefKnownNotGeneric("System.Array");

            return arrayType?.Properties.First(p => p.Name == nameof(Array.Length));
        }
        
        public class UsefulOffset
        {
            public UsefulOffset(string name, uint offset, Type type, bool is32Bit)
            {
                this.name = name;
                this.offset = offset;
                this.type = type;
                this.is32Bit = is32Bit;
            }

            public string name;
            public uint offset;
            public Type type;
            public bool is32Bit;
        }
    }
}