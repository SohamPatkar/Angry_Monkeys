using ServiceLocator.Events;
using ServiceLocator.Main;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MapButton : MonoBehaviour
    {
        private EventService eventService;
        [SerializeField] private int MapId;

        public void Init(EventService eventService)
        {
            this.eventService = eventService;
            GetComponent<Button>().onClick.AddListener(OnMapButtonClicked);
        }

        // To Learn more about Events and Observer Pattern, check out the course list here: https://outscal.com/courses
        private void OnMapButtonClicked() => eventService.OnMapSelected.InvokeEvent(MapId);
    }
}