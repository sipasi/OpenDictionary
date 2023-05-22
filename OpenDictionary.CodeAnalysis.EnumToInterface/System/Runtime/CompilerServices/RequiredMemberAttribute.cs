﻿namespace System.Runtime.CompilerServices;

#if !NET7_0_OR_GREATER 

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
internal sealed class RequiredMemberAttribute : Attribute { }

#endif // !NET7_0_OR_GREATER 