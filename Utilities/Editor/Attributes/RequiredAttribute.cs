using System;
using UnityEngine;

namespace Jimothy.Utilities.Editor.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class RequiredAttribute : PropertyAttribute
    {
        
    }
}