using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class FieldObject : Model
    {
        public event EventHandler OnLifeUpdated;
        public event EventHandler OnDead;
        public event EventHandler OnRemoved;

        protected int _teamId = -1;
        protected List<int> _friendlyTeamIds = new List<int>();

        protected float _maxLife = 0;
        protected float _life = 0;

        protected Position _velocity = Position.Create(0, 0);
        protected List<Position> _shapePoints = new List<Position>();

        protected BoardUI _ui = null;

        virtual public void Tick(float delta)
        {

        }


        public void AddFriendlyTeam(int teamId)
        {
            if (_friendlyTeamIds.Contains(teamId))
                return;

            _friendlyTeamIds.Add(teamId);
        }

        public void RemoveFriendlyTeam(int teamId)
        {
            var newIds = new List<int>();
            foreach (var id in _friendlyTeamIds)
                if (teamId != id)
                    newIds.Add(id);

            _friendlyTeamIds = newIds;
        }

        public bool IsSameTeam(FieldObject target)
        {
            if (teamId == -1
                || target.teamId == -1)
                return false;

            return teamId == target.teamId;
        }
        public bool IsFriendlyTeam(FieldObject target)
        {
            if (teamId == -1
                || target.teamId == -1)
                return false;
            
            return teamId == target.teamId
                || _friendlyTeamIds.Contains(target.teamId);
        }

        public bool isAlive
        {
            get { return life > 0; }
        }



        public float maxLife
        {
            get { return _maxLife; }
            set
            {
                _maxLife = value;

                if (life > _maxLife)
                    life = _maxLife;
            }
        }

        public float life
        {
            get { return _life; }
            set
            {
                float newLife = value;
                if (newLife > maxLife)
                    newLife = maxLife;

                if (_life == newLife)
                    return;


                _life = newLife;

                if (OnLifeUpdated != null)
                    OnLifeUpdated();
                if (!isAlive
                    && OnDead != null)
                    OnDead();
            }
        }

        public BoardUI ui
        {
            get { return _ui; }
            set { _ui = value; }
        }

        public void Remove()
        {
            if (OnRemoved != null)
                OnRemoved();
        }

        public Position velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public List<Position> shapePoints
        {
            get { return _shapePoints; }
            set { _shapePoints = value; }
        }

        public int teamId
        {
            get { return _teamId; }
            set { _teamId = value; }
        }
    }
}