using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spelprojekt2
{
    public class Wave
    {
        public List<Burst> bursts;
        [JsonIgnore]
        public int amountLeft;
        private int burstIndex;

        public Wave(List<Burst> bursts)
        {
            this.bursts = bursts;
            burstIndex = 0;
            amountLeft = bursts[0].amount;
        }

        public Enemy GetEnemy()
        {
            if (--amountLeft <= 0)
            { // Kör nästa burst
                if (bursts.Count > ++burstIndex)
                {
                    amountLeft = bursts[burstIndex].amount;
                }
                else
                { // Slut på denna wave
                    return null;
                }
            }
            return bursts[burstIndex].GetEnemyDuplicate();
        }
        public float GetCurrentTimeInterval()
        {
            return bursts[burstIndex].timeInterval;
        }
    }
}
