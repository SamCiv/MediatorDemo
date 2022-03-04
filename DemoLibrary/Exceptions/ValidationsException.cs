using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Exceptions
{
    public class ValidationsException
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; } //contiene gli errori della validation 
    }
}
