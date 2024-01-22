using System;

namespace Stealthy.Persistence
{
    public interface IStealthyDataAcces
    {
        public StealthyTable Load(String path);
    }
}
