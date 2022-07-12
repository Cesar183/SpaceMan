using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager sharedInstance;
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();
    public Transform levelStartPosition;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    void Start()
    {
        GenerateInitialLevelBlocks();
    }
    void Update()
    {
        
    }
    public void AddLevelBlock()
    {

    }
    public void RemoveLevelBlock()
    {

    }
    public void RemoveAllBlock()
    {

    }
    public void GenerateInitialLevelBlocks()
    {
        for(int i = 0; i<2; i++)
        {
            AddLevelBlock();
        }
    }
}
