using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MirrorAbilityHolder : MonoBehaviour
{
    public List<MirrorAbilityBase> abilities = new List<MirrorAbilityBase> ();
    private MenuElementManager[] menuObjects;

    private void Start()
    {
        LayerMask layer = LayerMask.GetMask("AbilityLayer");
        menuObjects = GetAllChildrenWithLayer(layer);
        SetComponents();
    }

    private void SetComponents() {
        int currentIndex = 0;
        foreach (MenuElementManager menuElement in menuObjects) {
            menuElement.SetAbility(abilities[currentIndex]);
            currentIndex++;
        }
    }

    private MenuElementManager[] GetAllChildrenWithLayer(LayerMask layer) {
        var result = new List<MenuElementManager>();

        foreach (Transform child in transform) {
            if (((1 << child.gameObject.layer) & layer) != 0) { 
                result.Add(child.gameObject.GetComponent<MenuElementManager>());
            }
        }
        return result.ToArray();
    }

    public MirrorAbilityBase RetrieveAbilityByIndex(int index) {
        if (index > 0 && index < abilities.Count) {
            return abilities[index];
        }
        return null;
    }


}
