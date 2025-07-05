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
    private const string hex = "19ca1f96e3289c5656d4c10352144f657c2a89428b7d4330f1e888e3b5b4c15f0de956aa5db71cb39c88cc5ff11e8df5787040891ffab3e2322331fe4e6201b02ffe5b0350192676ee46b31b41c8eaab8ff9122487b9a86b2147b709e37824d02d129f764acabf37e490ce7db53ce2b4209312ca1ad424ebe4eb2ffdaf952a1c1d2063094e546d7e3cbdd07683995f7cbeea09048a904620710923326bf480bd2ee7a2404a0ab6a5b20587081e2f0f630fcc68a139dc635f8207585b699423471ebb4494c2132e960ecee3fefe726c9e8eb207ec6fb922848c20f07f3b6d7ba82ed48a102edf74b6c2a0c3c00047d6678cbbb864407f6760c775ab1266a81c9f2af3cb009291b217d880b7ecf3b88892fde66c985b2b35301d57d9d318401d601e6a4284a43791de04f5c26f29e2e25cb24947adca7e56e1a1ba03cbcba210873004b001fee1e9c6bcd17fdc287f3fd7d4fb0e7f9bc7cd8913037bbb6cd274c21e18b879ae003f24a516097ab114e576c31fc502205e73c1683e0cbaf5fccfe4227f0999fa8cd65d01d281d748ead19540ad1357f3d274b9436d0613bb6482ca2e483b36141dd465a809d324e89a886b3f2a5ba534b0ed5cdead910976659e400f9f0c6e1f4818f8f3ece446e040cda8d27f2f4cfa38e5afb0ca691ab8d113912bd785bb3e736b6a7afacad81a158a57da7a97b1ee82552a94dc17ea082248871902e71297e8e6e985600d8f1deecdc5451f3479e26eea5b396c5352135dc756290ef2305fd43f9275f9b6178180c0ebf3491805239d48e69098c098bde9b6b020f6e519b7863257805e6ffd912294a88711da15864cb78e502c5b7b1c634549155ac0155509b12aaba95cdd1fceb1b5495d5939ea66d3eb606d69f55f22129717993d14d3e9ff2d2f28a318be897d946db05f16e0a705846da2456158d7b98516435dab869cae65a3c9ac4014e52bff7cf183dc4d82706c2ca79104e79aa37b04c3eac831729d837db141e3c7240a766af3fefd712513d9b7704e296ba3c66d2f5546d9a6e362e67593a4ff76ced1767499f61a0aaa35c9adc86e38c046fb0c1d550a18938b0b632ea33b06861bf18aa759f7d19948f4c24789908e40109c6d0ee52be9aa3f2c00131231041c97b788f3d548f8b28bad245a8d744a57bf5c9b137ca7b30ae3f2c8ae8fd3e84e2b8a4ccfe1904994adea9749f55ba7a9bdfef206d28044f2efdcce3ebfc443ae1d8052147eb6d42d0343c206e39292aea132250de83503b212199cf0bf67e3c2a54a54a30afe637ad5f6d74c7e0e0f4f881c1d1e05f6e8cec4135b5c008a1bb843dc3e857d52fcee2a520298bab9282ac416d30ee84e99ee78dfc286b28e60b703136a332de95908117b63c477f309b2ae97022ed6f1a54b994516164004cf5145fc37e8c4871a7055dd1668f797e6609b6d881f6794947b847bc4798f90f274abe605139ed0a8c08afdbe5fb842d7e0ad4e772cf7cb838b23413b68a82a4fd4cfd1f150cd6182df388bb9bfea8cb902ad77e108d7e448a55857369705cbe23efa58bcb60731c977443b7f3b7d78a144579b710ad1010ca4cb30c2d8e9f44f401461baf57093514172eec86e1f018971e28c8d2dd5b5b43873bc63509d967e2b30bd4bb162db4745faf47a64593bcb04c4dc342e7031b0e887aab9abfbe75f761421d85d00fa7a862eb129a79bc9a3dff1345b17b5e7516f4aabf9a49dcf75d6a53ad9420a22f793dd68d9c3aa7c942b6131fb01222d75b7a5d00f267c9bf23d1da0891520489adc3ba71e158d06e2b936a42915a91b7f99341ecbcd5f65b99e100a5d3f01031383ec05efa2ca4e8f8770283922c588ddd38c3efd160bdf296ae4aa7ba35c958fd9e2d77ab1774ec91a252906147e4d85e1493457ff7eaebb505f8522b556c4b6f487680fe918699737fc2dac2c517bf5f945c38479ceedab8ad69dfad8818d7f6986cdc40c40c7f254d837aa190518980d7b47c3078000f8fef69839a3946f26bcddce283c172a481ed4add416f39f0f0fe9c767d85d2c362ab71b74782c1cdb836a060da40adbdd3a401ee80727125b95da0ba13ce26d2dcf5e685924fca207e9600a9cdeb4a6e569638258300e435d19bca9105c292e6b1d3f4faa9c9640c014bbf0e537156e1140954609";
    private static readonly byte[] _input;

    static Comparison()
    {
        _input = Convert.FromHexString(hex);
    }
    
    [Benchmark(Description = "eth_pairings")]
    [IterationCount(100)]
    public void RunEthPairings()
    {
        var PairSize = 192;

        for (var j = 0; j < 1000; j++)
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
    [IterationCount(100)]
    public void RunMcl()
    {
        for (var j = 0; j < 1000; j++)
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
