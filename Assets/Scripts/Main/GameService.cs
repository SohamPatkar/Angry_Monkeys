using ServiceLocator.Player;
using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Sound;
using UnityEngine;
using ServiceLocator.UI;
using ServiceLocator.Wave;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        public PlayerService playerService { get; private set; }
        public MapService mapService { get; private set; }
        public SoundService soundService { get; private set; }
        public WaveService waveService { get; private set; }
        public EventService eventService { get; private set; }
        [SerializeField] private UIService uIServiceInstance;
        public UIService uIService => uIServiceInstance;

        [Header("Player Service")]
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        [Header("Map Service")]
        [SerializeField] private MapScriptableObject mapScriptableObject;

        [Header("Sound Service")]
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private AudioSource audioEffects;
        [SerializeField] private AudioSource backgroundMusic;

        [Header("Wave Service")]
        [SerializeField] private WaveScriptableObject waveScriptableObject;

        // Start is called before the first frame update
        void Start()
        {
            eventService = new EventService();
            playerService = new PlayerService(playerScriptableObject);
            mapService = new MapService(mapScriptableObject);
            soundService = new SoundService(soundScriptableObject, audioEffects, backgroundMusic);
            waveService = new WaveService(waveScriptableObject);
            Init();
        }

        private void Init()
        {
            playerService.Init(uIService, mapService, soundService);
            mapService.Init(eventService);
            waveService.Init(eventService, uIService, soundService, mapService, playerService);
            uIService.Init(eventService, waveService, playerService);
        }

        // Update is called once per frame
        void Update()
        {
            playerService.Update();
        }

    }
}

