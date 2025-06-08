using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    bool _canDestroy = false;

    public void HandleMine()
    {
        if (!_canDestroy) return;
        Destroy(gameObject);
    }


    public void SetCanDestroy() {
        _canDestroy = true;
    }

}


