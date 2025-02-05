using System;
using System.Collections.Generic;
using CodeBase.Pin;

namespace CodeBase.SaveSystem
{
    [Serializable]
    public class Data
    {
        public string Name = "Test";
        public List<PinData> PinDatas;
    }
}