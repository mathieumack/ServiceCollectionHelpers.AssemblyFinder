using ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Concrete
{
    internal abstract class AbstractAction : IAction
    {
        public virtual int Level => 0;
    }
}
