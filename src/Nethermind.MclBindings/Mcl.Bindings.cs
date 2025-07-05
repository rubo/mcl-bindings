// SPDX-FileCopyrightText: 2025 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Nethermind.MclBindings;

/// <summary>
/// Provides P/Invoke signatures for MCL as defined in
/// <see href="https://github.com/herumi/mcl/blob/master/include/mcl/bn.h">bn.h</see>.
/// </summary>
/// <remarks>
/// See <see href="https://github.com/herumi/mcl/blob/master/api.md">C/C++ API</see>
/// </remarks>
public static partial class Mcl
{
    [LibraryImport(LibraryName)]
    public static partial int mclBn_getVersion();

    [LibraryImport(LibraryName)]
    public static partial int mclBn_init(int curve, int compiledTimeVar);

    [LibraryImport(LibraryName)]
    public static partial int mclBn_getCurveType();

    [LibraryImport(LibraryName)]
    public static partial int mclBn_getOpUnitSize();

    [LibraryImport(LibraryName)]
    public static partial int mclBn_getG1ByteSize();

    [LibraryImport(LibraryName)]
    public static partial int mclBn_getG2ByteSize();

    [LibraryImport(LibraryName)]
    public static partial int mclBn_getFrByteSize();

    [LibraryImport(LibraryName)]
    public static partial int mclBn_getFpByteSize();

    [LibraryImport(LibraryName)]
    public static partial nuint mclBn_getCurveOrder(nint buf, nuint maxBufSize);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBn_getFieldOrder(nint buf, nuint maxBufSize);

    [LibraryImport(LibraryName)]
    public static partial void mclBn_setETHserialization(int enable);

    [LibraryImport(LibraryName)]
    public static partial int mclBn_getETHserialization();

    [LibraryImport(LibraryName)]
    public static partial int mclBn_setMapToMode(int mode);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFr_deserialize(ref mclBnFr x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnG1_deserialize(ref mclBnG1 x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnG2_deserialize(ref mclBnG2 x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnGT_deserialize(ref mclBnGT x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFp_deserialize(ref mclBnFp x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFp2_deserialize(ref mclBnFp2 x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFr_serialize(nint buf, nuint maxBufSize, in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnG1_serialize(nint buf, nuint maxBufSize, in mclBnG1 x);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnG2_serialize(nint buf, nuint maxBufSize, in mclBnG2 x);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnGT_serialize(nint buf, nuint maxBufSize, in mclBnGT x);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFp_serialize(nint buf, nuint maxBufSize, in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFp2_serialize(nint buf, nuint maxBufSize, in mclBnFp2 x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_setStr(ref mclBnFr x, nint buf, nuint bufSize, int ioMode);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG1_setStr(ref mclBnG1 x, nint buf, nuint bufSize, int ioMode);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG2_setStr(ref mclBnG2 x, nint buf, nuint bufSize, int ioMode);

    [LibraryImport(LibraryName)]
    public static partial int mclBnGT_setStr(ref mclBnGT x, nint buf, nuint bufSize, int ioMode);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_setStr(ref mclBnFp x, nint buf, nuint bufSize, int ioMode);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFr_getStr(nint buf, nuint maxBufSize, in mclBnFr x, int ioMode);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnG1_getStr(nint buf, nuint maxBufSize, in mclBnG1 x, int ioMode);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnG2_getStr(nint buf, nuint maxBufSize, in mclBnG2 x, int ioMode);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnGT_getStr(nint buf, nuint maxBufSize, in mclBnGT x, int ioMode);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFp_getStr(nint buf, nuint maxBufSize, in mclBnFp x, int ioMode);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFr_clear(ref mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp_clear(ref mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp2_clear(ref mclBnFp2 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFr_setInt(ref mclBnFr y, nuint x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFr_setInt32(ref mclBnFr y, int x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp_setInt(ref mclBnFp y, nuint x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp_setInt32(ref mclBnFp y, int x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_setLittleEndian(ref mclBnFr x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_setLittleEndian(ref mclBnFp x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFr_getLittleEndian(nint buf, nuint maxBufSize, in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFp_getLittleEndian(nint buf, nuint maxBufSize, in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_setLittleEndianMod(ref mclBnFr x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_setBigEndianMod(ref mclBnFr x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_setLittleEndianMod(ref mclBnFp x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_setBigEndianMod(ref mclBnFp x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_isValid(in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_isEqual(in mclBnFr x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_isZero(in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_isOne(in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_isOdd(in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_isNegative(in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_cmp(in mclBnFr x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_isValid(in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_isEqual(in mclBnFp x, in mclBnFp y);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_isZero(in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_isOne(in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_isOdd(in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_isNegative(in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_cmp(in mclBnFp x, in mclBnFp y);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp2_isEqual(in mclBnFp2 x, in mclBnFp2 y);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp2_isZero(in mclBnFp2 x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp2_isOne(in mclBnFp2 x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_setByCSPRNG(ref mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_setByCSPRNG(ref mclBnFp x);

    [LibraryImport(LibraryName)]
    public static unsafe partial void mclBn_setRandFunc(nint self, delegate* unmanaged[Cdecl]<nint, nint, nuint, int> readFunc);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_setHashOf(ref mclBnFr x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_setHashOf(ref mclBnFp x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_mapToG1(ref mclBnG1 y, in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp2_mapToG2(ref mclBnG2 y, in mclBnFp2 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFr_neg(ref mclBnFr y, in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFr_inv(ref mclBnFr y, in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFr_sqr(ref mclBnFr y, in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFr_add(ref mclBnFr z, in mclBnFr x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFr_sub(ref mclBnFr z, in mclBnFr x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFr_mul(ref mclBnFr z, in mclBnFr x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFr_div(ref mclBnFr z, in mclBnFr x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp_neg(ref mclBnFp y, in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp_inv(ref mclBnFp y, in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp_sqr(ref mclBnFp y, in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp_add(ref mclBnFp z, in mclBnFp x, in mclBnFp y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp_sub(ref mclBnFp z, in mclBnFp x, in mclBnFp y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp_mul(ref mclBnFp z, in mclBnFp x, in mclBnFp y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp_div(ref mclBnFp z, in mclBnFp x, in mclBnFp y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp2_neg(ref mclBnFp2 y, in mclBnFp2 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp2_inv(ref mclBnFp2 y, in mclBnFp2 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp2_sqr(ref mclBnFp2 y, in mclBnFp2 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp2_add(ref mclBnFp2 z, in mclBnFp2 x, in mclBnFp2 y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp2_sub(ref mclBnFp2 z, in mclBnFp2 x, in mclBnFp2 y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp2_mul(ref mclBnFp2 z, in mclBnFp2 x, in mclBnFp2 y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp2_div(ref mclBnFp2 z, in mclBnFp2 x, in mclBnFp2 y);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_squareRoot(ref mclBnFr y, in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_squareRoot(ref mclBnFp y, in mclBnFp x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp2_squareRoot(ref mclBnFp2 y, in mclBnFp2 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFr_pow(ref mclBnFr z, in mclBnFr x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnFp_pow(ref mclBnFp z, in mclBnFp x, in mclBnFp y);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFr_powArray(ref mclBnFr z, in mclBnFr x, nint y, nuint ySize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnFp_powArray(ref mclBnFp z, in mclBnFp x, nint y, nuint ySize);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_clear(ref mclBnG1 x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG1_isValid(in mclBnG1 x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG1_isEqual(in mclBnG1 x, in mclBnG1 y);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG1_isZero(in mclBnG1 x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG1_isValidOrder(in mclBnG1 x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG1_hashAndMapTo(ref mclBnG1 x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG1_hashAndMapToWithDst(ref mclBnG1 x, nint buf, nuint bufSize, nint dst, nuint dstSize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG1_setDst(nint dst, nuint dstSize);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_neg(ref mclBnG1 y, in mclBnG1 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_dbl(ref mclBnG1 y, in mclBnG1 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_normalize(ref mclBnG1 y, in mclBnG1 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_add(ref mclBnG1 z, in mclBnG1 x, in mclBnG1 y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_sub(ref mclBnG1 z, in mclBnG1 x, in mclBnG1 y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_mul(ref mclBnG1 z, in mclBnG1 x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_mulCT(ref mclBnG1 z, in mclBnG1 x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG2_clear(ref mclBnG2 x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG2_isValid(in mclBnG2 x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG2_isEqual(in mclBnG2 x, in mclBnG2 y);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG2_isZero(in mclBnG2 x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG2_isValidOrder(in mclBnG2 x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG2_hashAndMapTo(ref mclBnG2 x, nint buf, nuint bufSize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG2_hashAndMapToWithDst(ref mclBnG2 x, nint buf, nuint bufSize, nint dst, nuint dstSize);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG2_setDst(nint dst, nuint dstSize);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG2_neg(ref mclBnG2 y, in mclBnG2 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG2_dbl(ref mclBnG2 y, in mclBnG2 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG2_normalize(ref mclBnG2 y, in mclBnG2 x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG2_add(ref mclBnG2 z, in mclBnG2 x, in mclBnG2 y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG2_sub(ref mclBnG2 z, in mclBnG2 x, in mclBnG2 y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG2_mul(ref mclBnG2 z, in mclBnG2 x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG2_mulCT(ref mclBnG2 z, in mclBnG2 x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_clear(ref mclBnGT x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_setInt(ref mclBnGT y, nuint x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_setInt32(ref mclBnGT y, int x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnGT_isEqual(in mclBnGT x, in mclBnGT y);

    [LibraryImport(LibraryName)]
    public static partial int mclBnGT_isZero(in mclBnGT x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnGT_isOne(in mclBnGT x);

    [LibraryImport(LibraryName)]
    public static partial int mclBnGT_isValid(in mclBnGT x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_neg(ref mclBnGT y, in mclBnGT x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_sqr(ref mclBnGT y, in mclBnGT x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_add(ref mclBnGT z, in mclBnGT x, in mclBnGT y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_sub(ref mclBnGT z, in mclBnGT x, in mclBnGT y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_mul(ref mclBnGT z, in mclBnGT x, in mclBnGT y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_div(ref mclBnGT z, in mclBnGT x, in mclBnGT y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_inv(ref mclBnGT y, in mclBnGT x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_invGeneric(ref mclBnGT y, in mclBnGT x);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_powGeneric(ref mclBnGT z, in mclBnGT x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_pow(ref mclBnGT z, in mclBnGT x, in mclBnFr y);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_mulVec(ref mclBnG1 z, ref mclBnG1 x, in mclBnFr y, nuint n);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG2_mulVec(ref mclBnG2 z, ref mclBnG2 x, in mclBnFr y, nuint n);

    [LibraryImport(LibraryName)]
    public static partial void mclBnGT_powVec(ref mclBnGT z, in mclBnGT x, in mclBnFr y, nuint n);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_mulEach(ref mclBnG1 x, in mclBnFr y, nuint n);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFr_invVec(ref mclBnFr y, in mclBnFr x, nuint n);

    [LibraryImport(LibraryName)]
    public static partial nuint mclBnFp_invVec(ref mclBnFp y, in mclBnFp x, nuint n);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_normalizeVec(ref mclBnG1 y, in mclBnG1 x, nuint n);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG2_normalizeVec(ref mclBnG2 y, in mclBnG2 x, nuint n);

    [LibraryImport(LibraryName)]
    public static partial void mclBn_pairing(ref mclBnGT z, in mclBnG1 x, in mclBnG2 y);

    [LibraryImport(LibraryName)]
    public static partial void mclBn_finalExp(ref mclBnGT y, in mclBnGT x);

    [LibraryImport(LibraryName)]
    public static partial void mclBn_millerLoop(ref mclBnGT z, in mclBnG1 x, in mclBnG2 y);

    [LibraryImport(LibraryName)]
    public static partial void mclBn_millerLoopVec(ref mclBnGT z, in mclBnG1 x, in mclBnG2 y, nuint n);

    [LibraryImport(LibraryName)]
    public static partial void mclBn_millerLoopVecMT(ref mclBnGT z, in mclBnG1 x, in mclBnG2 y, nuint n, nuint cpuN);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG1_mulVecMT(ref mclBnG1 z, ref mclBnG1 x, in mclBnFr y, nuint n, nuint cpuN);

    [LibraryImport(LibraryName)]
    public static partial void mclBnG2_mulVecMT(ref mclBnG2 z, ref mclBnG2 x, in mclBnFr y, nuint n, nuint cpuN);

    [LibraryImport(LibraryName)]
    public static partial int mclBn_getUint64NumToPrecompute();

    [LibraryImport(LibraryName)]
    public static partial void mclBn_precomputeG2(nint Qbuf, in mclBnG2 Q);

    [LibraryImport(LibraryName)]
    public static partial void mclBn_precomputedMillerLoop(ref mclBnGT f, in mclBnG1 P, nint Qbuf);

    [LibraryImport(LibraryName)]
    public static partial void mclBn_precomputedMillerLoop2(ref mclBnGT f, in mclBnG1 P1, nint Q1buf, in mclBnG1 P2, nint Q2buf);

    [LibraryImport(LibraryName)]
    public static partial void mclBn_precomputedMillerLoop2mixed(ref mclBnGT f, in mclBnG1 P1, in mclBnG2 Q1, in mclBnG1 P2, nint Q2buf);

    [LibraryImport(LibraryName)]
    public static partial int mclBn_FrLagrangeInterpolation(ref mclBnFr @out, in mclBnFr xVec, in mclBnFr yVec, nuint k);

    [LibraryImport(LibraryName)]
    public static partial int mclBn_G1LagrangeInterpolation(ref mclBnG1 @out, in mclBnFr xVec, in mclBnG1 yVec, nuint k);

    [LibraryImport(LibraryName)]
    public static partial int mclBn_G2LagrangeInterpolation(ref mclBnG2 @out, in mclBnFr xVec, in mclBnG2 yVec, nuint k);

    [LibraryImport(LibraryName)]
    public static partial int mclBn_FrEvaluatePolynomial(ref mclBnFr @out, in mclBnFr cVec, nuint cSize, in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial int mclBn_G1EvaluatePolynomial(ref mclBnG1 @out, in mclBnG1 cVec, nuint cSize, in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial int mclBn_G2EvaluatePolynomial(ref mclBnG2 @out, in mclBnG2 cVec, nuint cSize, in mclBnFr x);

    [LibraryImport(LibraryName)]
    public static partial void mclBn_verifyOrderG1(int doVerify);

    [LibraryImport(LibraryName)]
    public static partial void mclBn_verifyOrderG2(int doVerify);

    [LibraryImport(LibraryName)]
    public static partial int mclBnG1_getBasePoint(ref mclBnG1 x);
}
