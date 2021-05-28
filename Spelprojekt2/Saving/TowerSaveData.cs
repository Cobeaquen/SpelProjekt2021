using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Xna.Framework;
using ProtoBuf;
using Spelprojekt2.Towers;

namespace Spelprojekt2.Saving
{
    [ProtoContract]
    public struct TowerSaveData
    {
        [ProtoMember(1)]
        public float x;
        [ProtoMember(2)]
        public float y;
        //[ProtoInclude(0, "float")]
        //public float y;
        [ProtoMember(3)]
        public int path;
        [ProtoMember(4)]
        public int tier;
        [ProtoMember(5)]
        public Tower.TowerInfo towerInfo;

        public TowerSaveData(float x, float y, int path, int tier, Tower.TowerInfo ti)
        {
            this.x = x;
            this.y = y;
            this.path = path;
            this.tier = tier;
            this.towerInfo = ti;
        }
        public Tower GetTower()
        {
            return (Tower)Type.GetType(towerInfo.type).GetConstructors()[0].Invoke(new object[] { new Vector2(x, y), towerInfo, path, tier });
        }
    }
}
