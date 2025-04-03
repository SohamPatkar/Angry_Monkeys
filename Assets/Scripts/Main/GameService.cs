using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ServiceLocator.Player;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService playerService { get; private set; }

        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        // Start is called before the first frame update
        void Start()
        {
            playerService = new PlayerService(playerScriptableObject);
        }

        // Update is called once per frame
        void Update()
        {
            playerService.Update();
        }

    }
}

