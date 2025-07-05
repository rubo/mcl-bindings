// SPDX-FileCopyrightText: 2025 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Nethermind.MclBindings;

public static partial class Mcl
{
    public const int MCL_BN254 = 0;
    public const int MCL_BN_SNARK1 = 4;
    public const int MCL_BLS12_381 = 5;
    public const int MCL_MAP_TO_MODE_HASH_TO_CURVE = 5;
    public const int MCLBN_FP_UNIT_SIZE = 6;
    public const int MCLBN_FR_UNIT_SIZE = 4;
    public const int MCLBN_COMPILED_TIME_VAR = MCLBN_FR_UNIT_SIZE * 10 + MCLBN_FP_UNIT_SIZE;

    private const string LibraryName = "mcl";

    static Mcl() => AssemblyLoadContext.Default.ResolvingUnmanagedDll += OnResolvingUnmanagedDll;

    private static nint OnResolvingUnmanagedDll(Assembly context, string name)
    {
        if (context != typeof(Mcl).Assembly || !LibraryName.Equals(name, StringComparison.Ordinal))
            return nint.Zero;

        string platform;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            name = $"lib{name}.so";
            platform = "linux";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            name = $"lib{name}.dylib";
            platform = "osx";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            name = $"{name}.dll";
            platform = "win";
        }
        else
            throw new PlatformNotSupportedException();

        var arch = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();

        return NativeLibrary.Load($"runtimes/{platform}-{arch}/native/{name}", context, DllImportSearchPath.AssemblyDirectory);
    }
}
