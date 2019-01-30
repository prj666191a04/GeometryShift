using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionHandler : MonoBehaviour
{

    public SimpleObjectPool descriptionPool;
    public Transform descContenetPanel;


    public void loadDescription(TestItem item)
    {
        GameObject newDescPoolObj = descriptionPool.GetObject();
        newDescPoolObj.transform.SetParent(descContenetPanel);

        descriptionBox newDesc = newDescPoolObj.GetComponent<descriptionBox>();
        newDesc.setup(item);
    }



}
