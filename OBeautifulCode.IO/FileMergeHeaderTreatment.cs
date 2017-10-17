﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileMergeHeaderTreatment.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.IO source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.IO.Recipes
{
    /// <summary>
    /// Determines what to do with the header line of the bottom file when merging two files.
    /// </summary>
#if !OBeautifulCodeIORecipesProject
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.IO", "See package version number")]
#endif
    internal enum FileMergeHeaderTreatment
    {
        /// <summary>
        /// Delete the header of the bottom file
        /// </summary>
        DeleteBottomFileHeader,

        /// <summary>
        /// keep the header of the bottom file (i.e. take the file completely as-is)
        /// </summary>
        KeepBottomFileHeader
    }
}
