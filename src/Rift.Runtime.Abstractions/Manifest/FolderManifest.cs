﻿// ===========================================================================
// Rift
// Copyright (C) 2024 - Present laper32.
// All Rights Reserved
// ===========================================================================

namespace Rift.Runtime.Abstractions.Manifest;

public sealed record FolderManifest(
    string Name,
    List<string> Members,
    List<string> Exclude
    );