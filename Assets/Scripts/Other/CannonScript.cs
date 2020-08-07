using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour, IResetable
{
    public GameObject projectilePrefab;
    public int shots;
    public Vector2 force;

    public Vector2 shootPosition;

    public float delay;


    private List<GameObject> activeProjectiles;
    void Start()
    {
        activeProjectiles = new List<GameObject>();
        AddToResetableList();
        StartCoroutine("Shoot", false);
    }

    public void Reset(GameObject eraserPrefab) {
        foreach(GameObject obj in activeProjectiles){
            Instantiate(eraserPrefab, obj.transform.position, Quaternion.identity);
            Destroy(obj);
        }
        activeProjectiles.Clear();
        StopCoroutine("Shoot");
        StartCoroutine("Shoot", true);
    }

    public void AddToResetableList(){
        LevelManagerScript.Instance.resetables.Add(this);
    }

    IEnumerator Shoot(bool toDelay){
        if(toDelay) yield return new WaitForSeconds(EraserScript.duration);
        for(int i = 0; i < shots; ++i){
            yield return new WaitForSeconds(delay);

            GameObject fired = Instantiate(projectilePrefab, transform.position + (Vector3)shootPosition, Quaternion.identity);
            fired.GetComponent<Rigidbody2D>().velocity = force;

            AudioManager.Instance.PlayCannonSound();
            
            activeProjectiles.Add(fired);
        }
    }
}
