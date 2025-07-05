// SPDX-FileCopyrightText: 2025 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace Nethermind.MclBindings;

[StructLayout(LayoutKind.Sequential)]
public ref struct mclBnGT
{
    public mclBnFp d0;
    public mclBnFp d1;
    public mclBnFp d2;
    public mclBnFp d3;
    public mclBnFp d4;
    public mclBnFp d5;
    public mclBnFp d6;
    public mclBnFp d7;
    public mclBnFp d8;
    public mclBnFp d9;
    public mclBnFp d10;
    public mclBnFp d11;
}
