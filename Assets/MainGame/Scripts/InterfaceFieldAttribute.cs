using System;
using UnityEngine;

namespace MainGame.Scripts
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InterfaceFieldAttribute : PropertyAttribute
    {
        public Type InterfaceType { get; }

        public InterfaceFieldAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }
    }
}