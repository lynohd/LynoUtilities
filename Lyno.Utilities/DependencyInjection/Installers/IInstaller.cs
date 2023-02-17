using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyno.Utilities.DependencyInjection.Installers;
public interface IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration config);
}
