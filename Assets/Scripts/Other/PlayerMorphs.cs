using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMorphs : MonoBehaviour, IResetable
{

    public void Reset(GameObject eraserPrefab) {
        Instantiate(eraserPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void AddToResetableList(){
        LevelManagerScript.Instance.resetables.Add(this);
    }
}
