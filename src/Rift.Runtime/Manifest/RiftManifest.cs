﻿// ===========================================================================
// Rift
// Copyright (C) 2024 - Present laper32.
// All Rights Reserved
// ===========================================================================

using System.Text.Json;
using System.Text.Json.Serialization;
using Rift.Runtime.Abstractions.Manifest;

namespace Rift.Runtime.Manifest;

internal class RiftManifest<T> : IRiftManifest
{
    public RiftManifest(T manifest)
    {
        if (manifest is not PluginManifest)
        {
            throw new ArgumentException("Manifest must be of type RiftManifest");
        }

        Type = manifest switch
        {
            PluginManifest => ERiftManifest.Plugin,
            _              => throw new ArgumentException("Manifest must be of type RiftManifest")
        };

        Value = manifest;
    }

    public ERiftManifest Type { get; init; }

    [JsonIgnore]
    public T Value { get; init; }

    public string Name => Value switch
    {
        PluginManifest plugin => plugin.Name,
        _                     => throw new ArgumentException("Invalid manifest type.")
    };

    public List<string> Authors => Value switch
    {
        PluginManifest plugin => plugin.Authors,
        _                     => throw new ArgumentException("Invalid manifest type.")
    };

    public string Version => Value switch
    {
        PluginManifest plugin => plugin.Version,
        _                     => throw new ArgumentException("Invalid manifest type.")
    };

    public string? Description => Value switch
    {
        PluginManifest plugin => plugin.Description,
        _                     => throw new ArgumentException("Invalid manifest type.")
    };

    public string? Configure => Value switch
    {
        PluginManifest plugin => plugin.Configure,
        _                     => throw new ArgumentException("Invalid manifest type.")
    };

    public string? Dependencies => Value switch
    {
        PluginManifest plugin => plugin.Dependency,
        _                     => throw new ArgumentException("Invalid manifest type.")
    };

    public Dictionary<string, JsonElement> Others => Value switch
    {
        PluginManifest plugin => plugin.Others,
        _                     => throw new ArgumentException("Invalid manifest type.")
    };
}