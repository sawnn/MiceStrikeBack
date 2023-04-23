using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapMenu : MonoBehaviour
{
    public int numTrapsToGenerate;
    public GameObject trapPrefab;
    public GridLayoutGroup gridLayout;

    private List<GameObject> traps;

    public GameObject FlashPrefab;
    public GameObject GluePrefab;

    public GameObject LaserPrefab;

    public GameObject TapettePrefab;


 
    public List<TrapData> trapInfos;

    private void Start()
    {
        traps = new List<GameObject>();
        GenerateTraps();
    }

    private void GenerateTraps()
    {
        for (int i = 0; i < numTrapsToGenerate; i++)
        {
            GameObject newTrap = Instantiate(trapPrefab, gridLayout.transform);

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
}

