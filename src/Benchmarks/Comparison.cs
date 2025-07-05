// SPDX-FileCopyrightText: 2025 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using BenchmarkDotNet.Attributes;
using Nethermind.Crypto;
using Nethermind.Crypto.Precompiles;
using Nethermind.MclBindings;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks;

public class Comparison
{
    private const string hex = "00000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000002198e9393920d483a7260bfb731fb5d25f1aa493335a9e71297e485b7aef312c21800deef121f1e76426a00665e5c4479674322d4f75edadd46debd5cd992f6ed090689d0585ff075ec9e99ad690c3395bc4b313370b38ef355acdadcd122975b12c85ea5db8c6deb4aab71808dcb408fe3d1e7690c43d37b4ce6cc0166fa7daa00000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000002198e9393920d483a7260bfb731fb5d25f1aa493335a9e71297e485b7aef312c21800deef121f1e76426a00665e5c4479674322d4f75edadd46debd5cd992f6ed275dc4a288d1afb3cbb1ac09187524c7db36395df7be3b99e673b13a075a65ec1d9befcd05a5323e6da4d435f3b617cdb3af83285c2df711ef39c01571827f9d";
    private static readonly byte[] _input;

    static Comparison()
    {
        _input = Convert.FromHexString(hex);
    }
    
    [Benchmark(Description = "eth_pairings")]
    public void RunEthPairings()
    {
        var PairSize = 192;

        for (var j = 0; j < 10000; j++)
        {
            Span<byte> input = new byte[_input.Length];
            ReadOnlySpan<byte> inputDataSpan = _input.AsSpan();
            Span<byte> inputReshuffled = new byte[PairSize];
            for (int i = 0; i < _input.Length / PairSize; i++)
            {
                inputDataSpan.Slice(i * PairSize + 0, 64).CopyTo(inputReshuffled[..64]);
                inputDataSpan.Slice(i * PairSize + 64, 32).CopyTo(inputReshuffled.Slice(96, 32));
                inputDataSpan.Slice(i * PairSize + 96, 32).CopyTo(inputReshuffled.Slice(64, 32));
                inputDataSpan.Slice(i * PairSize + 128, 32).CopyTo(inputReshuffled.Slice(160, 32));
                inputDataSpan.Slice(i * PairSize + 160, 32).CopyTo(inputReshuffled.Slice(128, 32));
                inputReshuffled.CopyTo(input.Slice(i * PairSize, PairSize));
            }
            var output = new byte[32];
            var outcome = Pairings.Bn254Pairing(input, output);

            //if (outcome != true || output[31] != 1)
            //    throw new Exception("Pairing failed");
        }
        
    }

    [Benchmark(Description = "mcl")]
    public void RunMcl()
    {
        for (var j = 0; j < 10000; j++)
        {

            Span<byte> input = new byte[_input.Length];
            _input.CopyTo(input);

            var output = new byte[32];
            var outcome = BN254.Pairing(input, output);

            //if (outcome != true || output[31] != 1)
            //    throw new Exception("Pairing failed");
        }
    }
}
