// SPDX-FileCopyrightText: 2025 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using Nethermind.MclBindings;
using System.Runtime.CompilerServices;

namespace Nethermind.Crypto.Precompiles;

using static Mcl;

[SkipLocalsInit]
public static unsafe class BN254
{
    private const int PairSize = 192;

    static BN254()
    {
        if (mclBn_init(MCL_BN_SNARK1, MCLBN_COMPILED_TIME_VAR) != 0)
            throw new InvalidOperationException("MCL initialization failed");
    }

    public static bool Add(Span<byte> input, Span<byte> output)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(output.Length, 64);

        output.Clear();

        if (input.Length != 128)
            return false;

        if (!DeserializeG1(input[0..64], out mclBnG1 x))
            return false;

        if (!DeserializeG1(input[64..128], out mclBnG1 y))
            return false;

        mclBnG1_add(ref x, x, y); // x += y
        mclBnG1_normalize(ref x, x);

        return SerializeG1(x, output);
    }

    public static bool Mul(Span<byte> input, Span<byte> output)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(output.Length, 64);

        output.Clear();

        if (input.Length != 96)
            return false;

        if (!DeserializeG1(input[0..64], out mclBnG1 x))
            return false;

        Span<byte> yData = input[64..];
        yData.Reverse(); // To little-endian

        mclBnFr y = default;

        fixed (byte* ptr = yData)
        {
            if (mclBnFr_setLittleEndianMod(ref y, (nint)ptr, 32) == -1 || mclBnFr_isValid(y) == 0)
                return false;
        }

        mclBnG1_mul(ref x, x, y);  // x *= y
        mclBnG1_normalize(ref x, x);

        return SerializeG1(x, output);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    /// <remarks>Modifies the <c>input</c> parameter.</remarks>
    public static bool Pairing(Span<byte> input, Span<byte> output)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(output.Length, 32);

        output.Clear();

        if (input.Length == 0)
        {
            output[31] = 1;
            return true;
        }

        if (input.Length % PairSize != 0)
            return false;

        mclBnGT gt = default;
        Unsafe.SkipInit(out mclBnGT previous);

        for (int i = 0, count = input.Length; i < count; i += PairSize)
        {
            int i64 = i + 64;

            if (!DeserializeG1(input[i..i64], out mclBnG1 g1))
                return false;

            if (!DeserializeG2(input[i64..(i64 + 128)], out mclBnG2 g2))
                return false;

            if (mclBnG1_isZero(g1) == 1 || mclBnG2_isZero(g2) == 1)
                continue;

            mclBn_pairing(ref gt, g1, g2);

            // Skip multiplication for the first iteration as there's no previous result
            if (i != 0)
                mclBnGT_mul(ref gt, gt, previous); // gt *= previous

            previous = gt;
        }

        // If gt is zero, then no pairing was computed, and it's considered valid
        if (mclBnGT_isOne(gt) == 1 || mclBnGT_isZero(gt) == 1)
        {
            output[31] = 1;
            return true;
        }

        return mclBnGT_isValid(gt) == 1;
    }

    private static bool DeserializeG1(Span<byte> data, out mclBnG1 point)
    {
        point = default;
        
        // Check for all-zero data
        if (data.IndexOfAnyExcept((byte)0) == -1)
            return true;

        Span<byte> x = data[0..32];
        x.Reverse(); // To little-endian

        fixed (byte* ptr = x)
            mclBnFp_setLittleEndian(ref point.x, (nint)ptr, 32);

        Span<byte> y = data[32..64];
        y.Reverse(); // To little-endian

        fixed (byte* ptr = y)
            mclBnFp_setLittleEndian(ref point.y, (nint)ptr, 32);

        mclBnFp_setInt32(ref point.z, 1);

        return mclBnG1_isValid(point) == 1;
    }

    private static bool DeserializeG2(Span<byte> data, out mclBnG2 point)
    {
        point = default;

        // Check for all-zero data
        if (data.IndexOfAnyExcept((byte)0) == -1)
            return true;

        Span<byte> x0 = data[32..64];
        Span<byte> x1 = data[0..32];
        x0.Reverse(); // To little-endian
        x1.Reverse(); // To little-endian

        fixed (byte* ptr0 = x0)
        fixed (byte* ptr1 = x1)
        {
            mclBnFp_setLittleEndian(ref point.x.d0, (nint)ptr0, 32);
            mclBnFp_setLittleEndian(ref point.x.d1, (nint)ptr1, 32);
        }

        Span<byte> y0 = data[96..128];
        Span<byte> y1 = data[64..96];
        y0.Reverse(); // To little-endian
        y1.Reverse(); // To little-endian

        fixed (byte* ptr0 = y0)
        fixed (byte* ptr1 = y1)
        {
            mclBnFp_setLittleEndian(ref point.y.d0, (nint)ptr0, 32);
            mclBnFp_setLittleEndian(ref point.y.d1, (nint)ptr1, 32);
        }

        mclBnFp_setInt32(ref point.z.d0, 1);

        return mclBnG2_isValid(point) == 1 && mclBnG2_isValidOrder(point) == 1;
    }

    private static bool SerializeG1(in mclBnG1 point, Span<byte> output)
    {
        Span<byte> x = output[0..32];

        fixed (byte* ptr = x)
        {
            nuint length = mclBnFp_getLittleEndian((nint)ptr, 32, point.x);

            if (length == nuint.Zero)
                return false;
        }

        Span<byte> y = output[32..64];

        fixed (byte* ptr = y)
        {
            nuint length = mclBnFp_getLittleEndian((nint)ptr, 32, point.y);

            if (length == nuint.Zero)
                return false;
        }

        x.Reverse(); // To big-endian
        y.Reverse(); // To big-endian

        return true;
    }
}
