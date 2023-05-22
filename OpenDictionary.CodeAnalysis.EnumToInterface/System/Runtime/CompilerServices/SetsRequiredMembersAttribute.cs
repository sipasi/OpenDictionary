﻿namespace System.Runtime.CompilerServices;

#if !NET7_0_OR_GREATER

[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
internal sealed class SetsRequiredMembersAttribute : Attribute { }

#endif