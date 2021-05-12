using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public class Burst
    {
        public int amount;
        public string enemyType;
        public float timeInterval;
        [JsonIgnore]
        public Enemy enemy;

        public Burst(int amount, string enemy, float timeInterval)
        {
            this.amount = amount;
            this.enemyType = enemy;
            this.timeInterval = timeInterval;
        }

        public Enemy GetEnemyDuplicate()
        {
            Type t = Type.GetType(enemyType);
            return (Enemy)t.GetConstructors()[0].Invoke(null);
        }
    }
}
