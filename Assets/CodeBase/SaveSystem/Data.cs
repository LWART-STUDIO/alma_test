using System;
using System.Collections.Generic;

namespace CodeBase.SaveSystem
{
    [Serializable]
    public class Data
    {
        public string Name = "Test";
        public List<PinData> PinDatas;
    }
}