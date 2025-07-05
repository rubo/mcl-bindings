// SPDX-FileCopyrightText: 2025 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace Nethermind.MclBindings;

[StructLayout(LayoutKind.Sequential)]
public ref struct mclBnG2
{
    public mclBnFp2 x;
    public mclBnFp2 y;
    public mclBnFp2 z;
}
