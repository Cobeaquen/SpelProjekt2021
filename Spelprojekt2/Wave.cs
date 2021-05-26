using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spelprojekt2.Enemies;

namespace Spelprojekt2
{
    public class Wave
    {
        public List<Burst> bursts;
        public float restingTime;
        [JsonIgnore]
        public State state;

        private int amountLeft;
        private int burstIndex;

        public Wave(List<Burst> bursts)
        {
            this.bursts = bursts;
            burstIndex = 0;
            amountLeft = bursts[0].amount;
            state = State.Ready;
        }

        public bool GetEnemy(out Enemy enemy)
        {
            state = State.Ready;
            if (--amountLeft <= 0)
            { // Kör nästa burst eller wave
                if (bursts.Count > ++burstIndex)
                {
                    amountLeft = bursts[burstIndex].amount;
                    state = State.Waiting;
                }
                else
                { // Slut på denna wave
                    burstIndex = 0;
                    enemy = bursts[burstIndex].GetEnemyDuplicate();
                    return true;
                }
            }
            enemy = bursts[burstIndex].GetEnemyDuplicate();
            return false;
        }
        public float GetCurrentTimeInterval()
        {
            return bursts[burstIndex].timeInterval;
        }
        public enum State
        {
            Ready, Waiting
        }
    }
}
