using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapMenu : MonoBehaviour
{
    public int numTrapsToGenerate;
    public GameObject trapPrefab;
    public Transform contentTraps;

    private List<GameObject> traps;

    public GameObject FlashPrefab;
    public GameObject GluePrefab;

    public GameObject LaserPrefab;

    public GameObject TapettePrefab;


 
    public List<TrapData> trapInfos;

    public bool isRandom = false;

    private void Start()
    {
        traps = new List<GameObject>();
        if (!isRandom)
        {
            GenerateTraps();
        }


    }

    public void GenerateTraps()
    {
        foreach (TrapData trapInfo in trapInfos)
        {
            for (int i = 0; i < trapInfo.nb; i++)
            {
                GameObject newTrap = Instantiate(trapPrefab, contentTraps);
                newTrap.name = trapInfo.trap.ToString();
                newTrap.GetComponentInChildren<Image>().sprite = trapInfo.sprite;
                newTrap.GetComponent<Button>().onClick.AddListener(() => { RemoveTrap(newTrap); });
                traps.Add(newTrap);
            }
        }
    }

    public void GenerateRandomTraps(int nbTrap)
    {
        for (int i = 0; i < nbTrap; i++)
        {
            GameObject newTrap = Instantiate(trapPrefab, contentTraps);

            int random = Random.Range(0, trapInfos.Count);
            Sprite randomSprite = trapInfos[random].sprite;
            Trap randomTrap = trapInfos[random].trap;
            Debug.Log(randomTrap);
            newTrap.name = randomTrap.ToString();

            newTrap.GetComponentInChildren<Image>().sprite = randomSprite;

            newTrap.GetComponent<Button>().onClick.AddListener(() => { RemoveTrap(newTrap); });

            traps.Add(newTrap);
        }
    }

    private void RemoveTrap(GameObject trapToRemove)
    {
        traps.Remove(trapToRemove);
        GameObject trap;
        switch (trapToRemove.name)
        {
            case "FLASH":
                trap = Instantiate(FlashPrefab);
                break;
            case "GLUE":
                trap = Instantiate(GluePrefab);
                break;
            case "LASER":
                trap = Instantiate(LaserPrefab);
                break;
            case "TAPETTE":
                trap = Instantiate(TapettePrefab);
                break;
            default:
                trap = null;
                break;
        }
        TrapSelectionMenu.Instance.SelectTrap(trap);
        Destroy(trapToRemove);
    }

}


public enum Trap
{
    FLASH,
    GLUE,
    LASER,
    TAPETTE
}
[System.Serializable]
public class TrapData
{
    public Trap trap;
    public Sprite sprite;
    public int nb;
}

