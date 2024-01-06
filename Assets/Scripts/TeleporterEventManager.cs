using UnityEngine;
using UnityEngine.Events;

public class TeleporterEventManager : MonoBehaviour
{
    public UnityEvent<Teleporter> onTeleporterDiscovered;

    public static TeleporterEventManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to call when a teleporter is discovered
    public void DiscoverTeleporter(Teleporter discoveredTeleporter)
    {
        onTeleporterDiscovered.Invoke(discoveredTeleporter);
    }
}
