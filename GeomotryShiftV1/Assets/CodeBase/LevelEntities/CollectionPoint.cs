//Author Atilla puskas
//Description: Sample demontration of how to compleete a level by non conventional means


using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CollectionPoint : MonoBehaviour
{
    public List<CollectionPointItem> objects;
    private int collected = 0;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (CollectionPointItem item in objects)
        {
            if(!item.collected)
            {
                if(item.objectRef != null && collision.gameObject == item.objectRef)
                {
                    item.collected = true;
                    Destroy(item.objectRef);
                    collected++;
                }
            }
        }
        CheckCompletion();
    }

    void CheckCompletion()
    {
        if (collected >= objects.Count)
        {
            LevelBase.instance.AcknowledgeLevelCompletion();
        }
    }

}

[System.Serializable]
public class CollectionPointItem
{
    public GameObject objectRef;
    public bool collected = false;
}
