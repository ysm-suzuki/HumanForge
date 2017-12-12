using System.Collections.Generic;

namespace GameMain
{
    public class Buff
    {
        public delegate void EventHandler();
        public event EventHandler OnFinished;

        public int id;
        public string name = "No name";

        public class Parameter
        {
            public float life = 0;
            public float attack = 0;
            public float defense = 0;
            public float moveSpeed = 0;
            public float penetrationRate = 0;
            
            public float sightRange = 0;

            public float attackPower = 0;
            public float attackRange = 0;
            public float attackCoolDownSeconds = 0;

            
            public static Parameter operator *(Parameter Pparameter, float ratio)
            {
                Pparameter.life *= ratio;
                Pparameter.attack *= ratio;
                Pparameter.defense *= ratio;
                Pparameter.moveSpeed *= ratio;
                Pparameter.penetrationRate *= ratio;
                Pparameter.sightRange *= ratio;
                Pparameter.attackPower *= ratio;
                Pparameter.attackRange *= ratio;
                Pparameter.attackCoolDownSeconds *= ratio;
                return Pparameter;
            }
        }
        public Parameter parameter = new Parameter();
        public BuffDuration duration = new BuffDuration();
        public List<ArmorBuff> armors = new List<ArmorBuff>();

        public Buff()
        {
            duration.OnFinished += () => 
            {
                if (OnFinished != null)
                    OnFinished();
            };
        }

        public void Tick(float delta)
        {
            duration.Tick(delta);
        }

        public bool IsSame(Buff buff)
        {
            return id == buff.id;
        }


        public bool isOnce
        {
            get { return duration.type == BuffDuration.Type.Once; }
        }
        public bool isSeconds
        {
            get { return duration.type == BuffDuration.Type.Seconds; }
        }
    }
}