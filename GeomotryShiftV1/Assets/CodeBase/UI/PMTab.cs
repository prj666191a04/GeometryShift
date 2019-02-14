using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMTab : MonoBehaviour
{
    public int position;

    public GameObject subMenue;
    public Transform subContainer;

    public static PMTab selected;

    // Start is called before the first frame update
    void Start()
    {
        if(position == 0)
        {
            selected = this;
        }
    }


    public virtual void Select()
    {
        if (PMTopLevel.loadedSubMenue != null)
             Destroy(PMTopLevel.loadedSubMenue);

        if (subMenue != null)
            PMTopLevel.loadedSubMenue = Instantiate(subMenue, subContainer);
        else
            Debug.LogWarning("subContainer not set");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
