﻿// ===========================================================================
// Rift
// Copyright (C) 2024 - Present laper32.
// All Rights Reserved
// ===========================================================================

namespace Rift.Runtime.Abstractions.Fundamental;

// ReSharper disable once IdentifierTypo
public interface IInitializable
{
    bool Init();

    void PostInit()
    {

    }
    void Shutdown();
}