using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonInteractable : MonoBehaviour, IInteractable
{
    public delegate void OnBoonInteract(List<AbilityScriptableObject> abilities);
    public static event OnBoonInteract Interacted;

    private BoonAbilitiesContainer container;
    private GameObject player;
    [SerializeField] private float closeAreaRadius = 5f;

    private void Start()
    {
        gameObject.SetActive(false);
        container = GetComponent<BoonAbilitiesContainer>();
        player = PlayerTracker.instance.player;
        SpawnBoon.SpawnAbilityBoon += Spawn;
    }

    private void Spawn() {
        gameObject.SetActive(true);
        CalculateSpawnPosition();
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        if (container != null && PlayerTracker.instance.player != null && Interacted != null) {
            List<AbilityScriptableObject> abilityObjects = container.RetrieveAbility();
            Interacted(abilityObjects);
        }
    }

    public string InteractionPrompt()
    {
        return "Interact";
    }

    private void CalculateSpawnPosition()
    {
        Vector3 randomPosition;
        while (true)
        {
            randomPosition = player.transform.position + Random.insideUnitSphere * closeAreaRadius;

            randomPosition.y = Mathf.Max(randomPosition.y, 0);

            RaycastHit hit;
            Vector3 direction = (player.transform.position - randomPosition).normalized;
            if (Physics.Raycast(randomPosition, direction, out hit, closeAreaRadius))
            {
                if (hit.point.y < randomPosition.y)
                {
                    continue;
                }
            }
            break;
        }

        gameObject.transform.position = randomPosition;
    }
}
