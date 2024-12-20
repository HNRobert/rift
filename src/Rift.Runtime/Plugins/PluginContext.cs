﻿// ===========================================================================
// Rift
// Copyright (C) 2024 - Present laper32.
// All Rights Reserved
// ===========================================================================

using System.Reflection;
using System.Runtime.Loader;

namespace Rift.Runtime.Plugins;

internal class PluginInstanceContext : AssemblyLoadContext
{
    public PluginInstanceContext() : base(true)
    {
        Resolving += (_, args) =>
            AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == args.Name);
    }
}

internal class PluginContext : PluginInstanceContext
{
    private readonly AssemblyDependencyResolver? _resolver;
    private readonly AssemblyLoadContext         _sharedContext;
    public readonly  PluginIdentity              Identity;

    public PluginContext(PluginIdentity identity, AssemblyLoadContext sharedContext)
    {
        _sharedContext = sharedContext;
        Identity       = identity;
        var entryPath = Identity.EntryPath;
        if (string.IsNullOrEmpty(entryPath))
        {
            Entry = null;
            return;
        }

        _resolver = new AssemblyDependencyResolver(entryPath);
        var asmName = AssemblyName.GetAssemblyName(entryPath);
        if (_sharedContext.Assemblies.FirstOrDefault(x => x.GetName().Name == asmName.Name) is { } asm)
        {
            Entry = asm;
            return;
        }

        Entry = LoadFromAssemblyPath(entryPath);
    }

    public Assembly? Entry { get; }

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        var ret = _sharedContext
            .Assemblies
            .FirstOrDefault(x => x.GetName().Name == assemblyName.Name);
        if (ret != null)
        {
            return ret;
        }

        var path = _resolver?.ResolveAssemblyToPath(assemblyName);
        return path == null ? null : LoadFromAssemblyPath(path);
    }
}