// SPDX-FileCopyrightText: 2025 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace Nethermind.MclBindings;

[StructLayout(LayoutKind.Sequential)]

public readonly ref struct mclBnFr
{
    private readonly ulong d0;
    private readonly ulong d1;
    private readonly ulong d2;
    private readonly ulong d3;
}
