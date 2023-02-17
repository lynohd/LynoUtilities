using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyno.Utilities.DependencyInjection.Installers.Attributes;
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class InstallerAttribute : Attribute
{
    public InstallerAttribute(bool enabled)
    {
        Enabled = enabled;
    }

    public bool Enabled { get; }
}