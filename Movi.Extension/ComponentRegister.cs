using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Movi.Extension;

namespace Movi.Extension
{
    public class ComponentRegister
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyNamed("Movi.Service")
                    .Pick()
                    .WithService.FirstNonGenericCoreInterface("Movi.Extension")
            );
        }
    }
}
