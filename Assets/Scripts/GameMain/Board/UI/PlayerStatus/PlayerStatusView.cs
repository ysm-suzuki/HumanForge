using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityMVC;

namespace GameMain
{
    public class PlayerStatusView : View
    {
        [SerializeField]
        private Text _lifeText;
        [SerializeField]
        private Text _attackText;
        [SerializeField]
        private Text _defenseText;

        private static string PrefabPath = "Prefabs/GameMain/Board/UI/PlayerStatus";

        new private Player _model;

        public static PlayerStatusView Attach(GameObject parent)
        {
            var view = View.Attach<PlayerStatusView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public PlayerStatusView SetModel(Player model)
        {
            _model = model;

            _model.OnLifeUpdated += () => 
            {
                UpdateLife();
            };
            _model.OnAuraUpdated += () => 
            {
                UpdateStatus();
            };

            UpdateLife();
            UpdateStatus();

            return this;
        }


        private void UpdateStatus()
        {
            _attackText.text = "こうげき: " + _model.attack;
            _defenseText.text = "ぼうぎょ : " + _model.defense;
        }

        private void UpdateLife()
        {
            _lifeText.text = "HP : " + _model.life + " / " + _model.maxLife;
        }
    }
}