// See https://aka.ms/new-console-template for more information
using Nethermind.Crypto.Precompiles;
using Nethermind.MclBindings;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;


var hex = "00000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000002198e9393920d483a7260bfb731fb5d25f1aa493335a9e71297e485b7aef312c21800deef121f1e76426a00665e5c4479674322d4f75edadd46debd5cd992f6ed090689d0585ff075ec9e99ad690c3395bc4b313370b38ef355acdadcd122975b12c85ea5db8c6deb4aab71808dcb408fe3d1e7690c43d37b4ce6cc0166fa7daa00000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000002198e9393920d483a7260bfb731fb5d25f1aa493335a9e71297e485b7aef312c21800deef121f1e76426a00665e5c4479674322d4f75edadd46debd5cd992f6ed275dc4a288d1afb3cbb1ac09187524c7db36395df7be3b99e673b13a075a65ec1d9befcd05a5323e6da4d435f3b617cdb3af83285c2df711ef39c01571827f9d";
Span<byte> input = Convert.FromHexString(hex);
var output = new byte[32];
Debug.WriteLine(BN254.Pairing(input, output));
Debug.WriteLine(Convert.ToHexStringLower(output));

//var hex = "089142debb13c461f61523586a60732d8b69c5b38a3380a74da7b2961d867dbf2d5fc7bbc013c16d7945f190b232eacc25da675c0eb093fe6b9f1b4b4e107b3625f8c89ea3437f44f8fc8b6bfbb6312074dc6f983809a5e809ff4e1d076dd5850b38c7ced6e4daef9c4347f370d6d8b58f4b1d8dc61a3c59d651a0644a2a27cf";
//Span<byte> input = Convert.FromHexString(hex);
//var output = new byte[64];
//BN254.Add(input, output);
//Debug.WriteLine(Convert.ToHexStringLower(output));

//var hex = "089142debb13c461f61523586a60732d8b69c5b38a3380a74da7b2961d867dbf2d5fc7bbc013c16d7945f190b232eacc25da675c0eb093fe6b9f1b4b4e107b36ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff";
//Span<byte> input = Convert.FromHexString(hex);
//var output = new byte[64];
//BN254.Mul(input, output);
//Debug.WriteLine(Convert.ToHexStringLower(output));

//TestPairing();

static unsafe void TestPairing()
{
    var pairingSize = 192;
    var hex = "00000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000002198e9393920d483a7260bfb731fb5d25f1aa493335a9e71297e485b7aef312c21800deef121f1e76426a00665e5c4479674322d4f75edadd46debd5cd992f6ed090689d0585ff075ec9e99ad690c3395bc4b313370b38ef355acdadcd122975b12c85ea5db8c6deb4aab71808dcb408fe3d1e7690c43d37b4ce6cc0166fa7daa00000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000002198e9393920d483a7260bfb731fb5d25f1aa493335a9e71297e485b7aef312c21800deef121f1e76426a00665e5c4479674322d4f75edadd46debd5cd992f6ed275dc4a288d1afb3cbb1ac09187524c7db36395df7be3b99e673b13a075a65ec1d9befcd05a5323e6da4d435f3b617cdb3af83285c2df711ef39c01571827f9d";
    Span<byte> input = Convert.FromHexString(hex);
    var s = Mcl.mclBn_init(Mcl.MCL_BN_SNARK1, Mcl.MCLBN_COMPILED_TIME_VAR);

    mclBnGT prev = default;
    mclBnGT result = default;

    //if (input.Length > pairingSize)
    //    Mcl.mclBnGT_setInt32(out prev, 1);

    for (int i = 0, count = input.Length; i < count; i += pairingSize)
    {
        var pair = input[i..(i + pairingSize)];

        ImportG1(pair[0..64], out var p1);
        ImportG2(pair[64..192], out var p2);

        Mcl.mclBn_pairing(ref result, p1, p2);
        Debug.WriteLine("GT valid: " + Mcl.mclBnGT_isValid(result));

        if (i > 0)
            Mcl.mclBnGT_mul(ref result, prev, result);

        prev = result;
    }

    //ImportG1(input[0..64], out var p1);
    //ImportG2(input[64..192], out var p2);

    //mclBnGT result = default;
    //Mcl.mclBn_pairing(ref result, p1, p2);
    //Debug.WriteLine("GT valid: " + Mcl.mclBnGT_isValid(result));
    //var input2 = input[192..];
    //ImportG1(input2[0..64], out var p1x);
    //ImportG2(input2[64..192], out var p2x);
    //mclBnGT result2 = default;
    //Mcl.mclBn_pairing(ref result2, p1x, p2x);
    //mclBnGT result3 = default;
    //Mcl.mclBnGT_mul(ref result3, result, result2);

    Debug.WriteLine("GT one: "+ Mcl.mclBnGT_isOne(result));
}

static unsafe void TestMul()
{
    Debug.WriteLine(Mcl.mclBn_getVersion());
    var hex = "089142debb13c461f61523586a60732d8b69c5b38a3380a74da7b2961d867dbf2d5fc7bbc013c16d7945f190b232eacc25da675c0eb093fe6b9f1b4b4e107b36ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff";
    Span<byte> input = Convert.FromHexString(hex);
    var s = Mcl.mclBn_init(Mcl.MCL_BN_SNARK1, Mcl.MCLBN_COMPILED_TIME_VAR);

    ImportG1(input[0..64], out var p1);

    mclBnFr fr = default;

    Span<byte> scalar = input[64..];

    //scalar.Reverse();

    fixed (byte* ptr = scalar)
    {
        var sss = Mcl.mclBnFr_setBigEndianMod(ref fr, (nint)ptr, (nuint)scalar.Length);
        Debug.WriteLine("Fr succeed: " + sss);
        Debug.WriteLine("Fr valid: " + Mcl.mclBnFr_isValid(fr));
    }
    mclBnG1 result = default;
    Mcl.mclBnG1_mul(ref result, p1, fr);
    Mcl.mclBnG1_normalize(ref result, result);

    var str = Marshal.AllocHGlobal(1024);

    var len = Mcl.mclBnG1_getStr(str, 1024, result, 16);

    Debug.WriteLine(Marshal.PtrToStringUTF8(str));

    Span<byte> output = stackalloc byte[32];
    fixed (byte* outputPtr = output)
    {
        var ss = Mcl.mclBnG1_serialize((nint)outputPtr, (nuint)output.Length, result);
        Debug.WriteLine(ss);
    }
    output.Reverse();
    Debug.WriteLine(Convert.ToHexStringLower(output));

    Span<byte> outX = stackalloc byte[32];
    Span<byte> outY = stackalloc byte[32];
    Span<byte> outZ = stackalloc byte[32];
    fixed (byte* outXPtr = outX)
    fixed (byte* outYPtr = outY)
    fixed (byte* outZPtr = outZ)
    {
        var ss = Mcl.mclBnFp_getLittleEndian((nint)outXPtr, (nuint)outX.Length, result.x);
        Debug.WriteLine(ss);
        ss = Mcl.mclBnFp_getLittleEndian((nint)outYPtr, (nuint)outY.Length, result.y);
        Debug.WriteLine(ss);
        ss = Mcl.mclBnFp_getLittleEndian((nint)outZPtr, (nuint)outZ.Length, result.z);
        Debug.WriteLine(ss);
    }
    outX.Reverse();
    outY.Reverse();
    Debug.WriteLine(Convert.ToHexStringLower(outX) + " " + Convert.ToHexStringLower(outY) + " " + Convert.ToHexStringLower(outZ));

    //LoadG1(input);
    //LoadG1_2(hex);
}

static unsafe void TestAdd()
{
    Debug.WriteLine(Mcl.mclBn_getVersion());
    var hex = "089142debb13c461f61523586a60732d8b69c5b38a3380a74da7b2961d867dbf2d5fc7bbc013c16d7945f190b232eacc25da675c0eb093fe6b9f1b4b4e107b3625f8c89ea3437f44f8fc8b6bfbb6312074dc6f983809a5e809ff4e1d076dd5850b38c7ced6e4daef9c4347f370d6d8b58f4b1d8dc61a3c59d651a0644a2a27cf";
    Span<byte> input = Convert.FromHexString(hex);
    //var x1 = input[..32];
    //var y1 = input[32..64];
    //var x2 = input[64..96];
    //var y2 = input[96..128];

    //x1.Reverse();
    //y1.Reverse();
    //x2.Reverse();
    //y2.Reverse();

    var s = Mcl.mclBn_init(Mcl.MCL_BN_SNARK1, Mcl.MCLBN_COMPILED_TIME_VAR);
    //Mcl.mclBn_setETHserialization(1);
    //Mcl.mclBn_setMapToMode(0);

    //Span<byte> x = stackalloc byte[Mcl.MCLBN_FP_UNIT_SIZE * 6 * 3];
    //fixed(byte* str = Encoding.ASCII.GetBytes(Convert.ToHexString(input[..128])))

    //fixed (byte* xPtr = x)
    //fixed (byte* inputPtr = input)
    //{
    //    int ss = Mcl.mclBnG1_setStr((nint)xPtr, (nint)str, 64, 16);
    //}


    //Span<byte> xy = stackalloc byte[64];
    ////xy[0] = 0x01; // Uncompressed point prefix
    //x1.CopyTo(xy[0..32]);
    //y1.CopyTo(xy[32..]);
    //mclBnG1 p1 ;
    //fixed (byte* xyPtr = xy)
    //{
    //    var ss = Mcl.mclBnG1_deserialize(out p1, (nint)xyPtr, (nuint)xy.Length);
    //    Debug.WriteLine(ss);
    //}

    //Span<byte> xy2 = stackalloc byte[64];
    ////xy2[0] = 0x01; // Uncompressed point prefix
    //x2.CopyTo(xy2[0..32]);
    //y2.CopyTo(xy2[32..]);
    //mclBnG1 p2;
    //fixed (byte* xy2Ptr = xy2)
    //{
    //    var ss = Mcl.mclBnG1_deserialize(out p2, (nint)xy2Ptr, (nuint)xy2.Length);
    //    Debug.WriteLine(ss);
    //}
    //mclBnFp fpx = new();
    //FpFromStr(hex[..64],  fpx);
    //ToStr(fpx);

    ImportG1(input[0..64], out var p1);
    ImportG1(input[64..128], out var p2);
    mclBnG1 result = default;
    Mcl.mclBnG1_add(ref result, p1, p2);
    Mcl.mclBnG1_normalize(ref result, result);

    var str = Marshal.AllocHGlobal(1024);

    var len = Mcl.mclBnG1_getStr(str, 1024, result, 16);

    Debug.WriteLine(Marshal.PtrToStringUTF8(str));

    Span<byte> output = stackalloc byte[32];
    fixed (byte* outputPtr = output)
    {
        var ss = Mcl.mclBnG1_serialize((nint)outputPtr, (nuint)output.Length, result);
        Debug.WriteLine(ss);
    }
    output.Reverse();
    Debug.WriteLine(Convert.ToHexStringLower(output));

    Span<byte> outX = stackalloc byte[32];
    Span<byte> outY = stackalloc byte[32];
    Span<byte> outZ = stackalloc byte[32];
    fixed (byte* outXPtr = outX)
    fixed (byte* outYPtr = outY)
    fixed (byte* outZPtr = outZ)
    {
        var ss = Mcl.mclBnFp_getLittleEndian((nint)outXPtr, (nuint)outX.Length, result.x);
        Debug.WriteLine(ss);
        ss = Mcl.mclBnFp_getLittleEndian((nint)outYPtr, (nuint)outY.Length, result.y);
        Debug.WriteLine(ss);
        ss = Mcl.mclBnFp_getLittleEndian((nint)outZPtr, (nuint)outZ.Length, result.z);
        Debug.WriteLine(ss);
    }
    outX.Reverse();
    outY.Reverse();
    Debug.WriteLine(Convert.ToHexStringLower(outX) + " " + Convert.ToHexStringLower(outY) + " " + Convert.ToHexStringLower(outZ));

    //LoadG1(input);
    //LoadG1_2(hex);
}

static unsafe void ImportG1(Span<byte> input, out mclBnG1 p)
{
    mclBnFp x = default;
    var xd = input[0..32];
    xd.Reverse();

    fixed (byte* inputPtr = xd)
    {
        var d = Mcl.mclBnFp_setLittleEndian(ref x, (nint)inputPtr, 32);
        Debug.Assert(d == 0);
        Debug.WriteLine("valid x: " + Mcl.mclBnFp_isValid(x));
    }
    ToStr(x);
    mclBnFp y = default;
    var yd = input[32..64];
    yd.Reverse();

    fixed (byte* inputPtr = yd)
    {
        var d = Mcl.mclBnFp_setLittleEndian(ref y, (nint)inputPtr, 32);
        Debug.Assert(d == 0);
        Debug.WriteLine("valid y: " + Mcl.mclBnFp_isValid(y));
    }
    ToStr(y);
    mclBnFp one = default;
    Mcl.mclBnFp_setInt32(ref one, 1);
    Debug.WriteLine("valid z: " + Mcl.mclBnFp_isValid(one));
    p = default;
    p.x = x;
    p.y = y;
    p.z = one;
    Mcl.mclBnG1_normalize(ref p, p);
    Debug.WriteLine("G1 valid " + Mcl.mclBnG1_isValid(p));
    Debug.WriteLine("G1 zero " + Mcl.mclBnG1_isZero(p));
}

static unsafe void ImportG2(Span<byte> input, out mclBnG2 p)
{
    p = default;
    mclBnFp2 x;
    var xd0 = input[32..64];
    var xd1 = input[0..32];
    xd0.Reverse();
    xd1.Reverse();

    fixed (byte* xd0Ptr = xd0)
    fixed (byte* xd1Ptr = xd1)
    {
        Mcl.mclBnFp_setLittleEndian(ref p.x.d0, (nint)xd0Ptr, 32);
        Mcl.mclBnFp_setLittleEndian(ref p.x.d1, (nint)xd1Ptr, 32);
    }

    mclBnFp2 y;
    var yd0 = input[96..128];
    var yd1 = input[64..96];
    yd0.Reverse();
    yd1.Reverse();

    fixed (byte* yd0Ptr = yd0)
    fixed (byte* yd1Ptr = yd1)
    {
        Mcl.mclBnFp_setLittleEndian(ref p.y.d0, (nint)yd0Ptr, 32);
        Mcl.mclBnFp_setLittleEndian(ref p.y.d1, (nint)yd1Ptr, 32);
    }

    Mcl.mclBnFp_setInt32(ref p.z.d0, 1);
    Mcl.mclBnG2_normalize(ref p, p);
    Debug.WriteLine("G2 valid " + Mcl.mclBnG2_isValid(p));
    Debug.WriteLine("G2 zero " + Mcl.mclBnG2_isZero(p));
}

static unsafe void FpFromStr(string hex, out mclBnFp x)
{
    x = default;

    fixed (byte* xstr = Encoding.UTF8.GetBytes(hex))
    {
        int ss = Mcl.mclBnFp_setStr(ref x, (nint)xstr, (nuint)hex.Length, 16);
    }
}

static unsafe void ToStr(mclBnFp x)
{
    var str = Marshal.AllocHGlobal(1024);

    var len = Mcl.mclBnFp_getStr(str, 1024, x, 16);

    Debug.WriteLine(Marshal.PtrToStringUTF8(str));


}


