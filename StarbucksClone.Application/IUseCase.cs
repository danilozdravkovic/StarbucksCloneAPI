using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.Application
{
    public interface IUseCase
    {
        int Id { get; }
        string Name { get; }
    }
}
