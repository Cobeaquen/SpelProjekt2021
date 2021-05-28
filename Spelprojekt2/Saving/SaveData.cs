using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Spelprojekt2.Saving
{
    [ProtoContract]
    public struct SaveData
    {
        [ProtoMember(1)]
        public TowerSaveData[] towers;

        public SaveData(TowerSaveData[] towers)
        {
            this.towers = towers;
        }
    }
}
