// SPDX-FileCopyrightText: 2025 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace Nethermind.MclBindings;

[StructLayout(LayoutKind.Sequential)]
public ref struct mclBnG1
{
    public mclBnFp x;
    public mclBnFp y;
    public mclBnFp z;
}
