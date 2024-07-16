using ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.ConcreteSettings
{
    internal class ActionLevel02 : AbstractAction
    {
        public virtual int Level => 1;
    }
}
