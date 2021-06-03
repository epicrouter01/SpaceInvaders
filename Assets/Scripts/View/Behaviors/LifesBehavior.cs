using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesBehavior : MonoBehaviour
{
    [SerializeField] private GameObject lifePrefab = null;
    [SerializeField] private int maxLifes = 0;
    
    private List<GameObject> lifes = new List<GameObject>();
    private int lifesCount = 0;

    public int LifesCount { 
        get => lifesCount; 
        set {
            lifesCount = value;
            updateLifes();
        }
    }

    public int MaxLifes { get => maxLifes; set => maxLifes = value; }

    private void createLifes()
    {
        GameObject life;
        for (int i = 0; i < maxLifes; i++)
        {
            life = Instantiate(lifePrefab);
            life.transform.position = transform.position + new Vector3(3, 0, 0) * i;
            lifes.Add(life);
        }
    }

    private void updateLifes()
    {
        for (int i = 0; i < lifes.Count; i++)
        {
            lifes[i].SetActive(i <= lifesCount - 1);
        }
    }

    public void onGameStarted()
    {
        removeLifes();
        createLifes();
        LifesCount = maxLifes;
    }

    private void removeLifes()
    {
        foreach (GameObject life in lifes)
            Destroy(life);

        lifes.RemoveAll((GameObject o) => true);
    }
}
