using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetable
{
    void Reset(GameObject eraserPrefab);

    void AddToResetableList();
}
