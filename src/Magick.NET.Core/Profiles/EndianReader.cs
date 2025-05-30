﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Text;

namespace ImageMagick;

internal sealed class EndianReader
{
    private readonly byte[] _data;

    public EndianReader(byte[] data)
    {
        Throw.IfNullOrEmpty(data);

        _data = data;
        Index = 0;
    }

    public uint Index { get; private set; }

    public bool IsLittleEndian { get; set; }

    public bool CanRead(uint length)
    {
        if (length > _data.Length)
            return false;

        return Index + length <= _data.Length;
    }

    public bool Seek(uint index)
    {
        if (index >= _data.Length)
            return false;

        Index = index;

        return true;
    }

    public bool Skip(uint value)
    {
        if (Index + value >= _data.Length)
            return false;

        Index += value;

        return true;
    }

    public byte? ReadByte()
    {
        if (Index >= _data.Length)
            return null;

        var result = _data[Index];

        Index++;

        return result;
    }

    public unsafe double? ReadDouble()
    {
        if (!CanRead(8))
            return null;

        return IsLittleEndian ? ReadDoubleLSB() : ReadDoubleMSB();
    }

    public uint? ReadLong()
    {
        if (!CanRead(4))
            return null;

        return IsLittleEndian ? ReadLongLSB() : ReadLongMSB();
    }

    public ushort? ReadShort()
    {
        if (!CanRead(2))
            return null;

        return IsLittleEndian ? ReadShortLSB() : ReadShortMSB();
    }

    public unsafe float? ReadFloat()
    {
        var result = ReadLong();
        if (result is null)
            return null;

        var value = result.Value;

        return *(float*)&value;
    }

    public string? ReadString(uint length)
    {
        if (length == 0)
            return string.Empty;

        if (!CanRead(length))
            return null;

        var result = Encoding.UTF8.GetString(_data, (int)Index, (int)length);
        var nullCharIndex = result.IndexOf('\0');
        if (nullCharIndex != -1)
            result = result.Substring(0, nullCharIndex);

        Index += length;

        return result;
    }

    private unsafe double ReadDoubleLSB()
    {
        ulong result = _data[Index];
        result |= (ulong)_data[Index + 1] << 8;
        result |= (ulong)_data[Index + 2] << 16;
        result |= (ulong)_data[Index + 3] << 24;
        result |= (ulong)_data[Index + 4] << 32;
        result |= (ulong)_data[Index + 5] << 40;
        result |= (ulong)_data[Index + 6] << 48;
        result |= (ulong)_data[Index + 7] << 56;

        Index += 8;

        return *(double*)&result;
    }

    private unsafe double ReadDoubleMSB()
    {
        var result = (ulong)_data[Index] << 56;
        result |= (ulong)_data[Index + 1] << 48;
        result |= (ulong)_data[Index + 2] << 40;
        result |= (ulong)_data[Index + 3] << 32;
        result |= (ulong)_data[Index + 4] << 24;
        result |= (ulong)_data[Index + 5] << 16;
        result |= (ulong)_data[Index + 6] << 8;
        result |= _data[Index + 7];

        Index += 8;

        return *(double*)&result;
    }

    private uint ReadLongLSB()
    {
        uint result = _data[Index];
        result |= (uint)_data[Index + 1] << 8;
        result |= (uint)_data[Index + 2] << 16;
        result |= (uint)_data[Index + 3] << 24;

        Index += 4;

        return result;
    }

    private uint ReadLongMSB()
    {
        var result = (uint)_data[Index] << 24;
        result |= (uint)_data[Index + 1] << 16;
        result |= (uint)_data[Index + 2] << 8;
        result |= _data[Index + 3];

        Index += 4;

        return result;
    }

    private ushort ReadShortLSB()
    {
        ushort result = _data[Index];
        result |= (ushort)(_data[Index + 1] << 8);

        Index += 2;

        return result;
    }

    private ushort ReadShortMSB()
    {
        var result = (ushort)(_data[Index] << 8);
        result |= _data[Index + 1];

        Index += 2;

        return result;
    }
}
