using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public static float NormalTime = 0.8f;
    public static int height = 24;
    public static int width = 10;
    public Spawner spawnerScript;
    public Player player;
    public Transform[,] blockGrid;

    public GameObject[] LevelUpBlocks;
    private bool firstRun = true;
    public AudioSource ClearLine;
    public AudioSource start;
    public AudioSource background;
    public AudioSource rotate;
    public Text scoreBoard;
    private int loss_height = 20;
    private Color dark;
    private Color light;
    private int NUMBER_FOR_LEVEL_UP = 6;
    void Start()
    {
        start.Play();
        blockGrid = new Transform[width,height];
        SpawnBlock();
        background.Play();
        scoreBoard.text = player.score.ToString();
        dark = new Color(48, 48, 48);
        light = new Color(132, 132, 132);
// LevelUpAddition();
    }

    // Update is called once per frame
    void Update()
    {
        //EvaluateGrid();
    }

    public void LevelUpAddition()
    {
        int CurrentLevelUp = player.GetLinesMade();
        
        if(player.GetLinesMade() < this.NUMBER_FOR_LEVEL_UP)
        {
            // Debug.Log("Before:" + CurrentLevelUp);
            GameObject block = LevelUpBlocks[CurrentLevelUp];
            player.SetLinesMade(CurrentLevelUp+=1);
// Debug.Log(player.GetLinesMade());
            block.transform.GetComponent<SpriteRenderer>().color = light;

        }
        else
        {
            //Debug.Log("mWore");
            player.SetLinesMade(0); 
            
            for(int i = 0; i < LevelUpBlocks.Length; i++)
            {
                Debug.Log("After");
                 GameObject block = LevelUpBlocks[i];
                block.transform.GetComponent<SpriteRenderer>().color = dark;
            }

           
        }
    }
    public void Initialise_Blocks()
    {
        GameObject block = spawnerScript.NewBlock();
        player.SetBlock(block);
        player.SetNewBlock(false);
        player.SetTetrisBlock((TetrisBlock)block.GetComponent(typeof(TetrisBlock)));
    }
    public void SpawnBlock()
    {
        GameObject block;
        if (firstRun)
        {
            firstRun = false;
             block = spawnerScript.InitialSpawner();
        }
        else
        {
            block = spawnerScript.NewBlock();
        }
        
        
        player.SetBlock(block);
        player.SetNewBlock(false);
        player.SetTetrisBlock((TetrisBlock)block.GetComponent(typeof(TetrisBlock)));
        
    }

    public bool CheckLoss()
    {

        for(int col = 0; col < width; col++)
        {
            if(blockGrid[col, loss_height-1] != null)
            {
                return true;
            }
        }

        return false;


    }

    public void AddToGrid(GameObject block)
    {
        foreach(Transform child in block.transform)
        {
            blockGrid[Mathf.RoundToInt(child.transform.position.x), Mathf.RoundToInt(child.transform.position.y)] = child;
        }
        
    }
    public bool CheckEmpty(int x, int y)
    {
        if (blockGrid[x, y] != null)
        {
            return false;
        }
        return true;
        
    }

    public void EvaluateGrid()
    {
       

        int row = 0;

        int ScoreMulti = 0;
        /*for (int row = 0; row < height; row++)
        {
            

            
                if (IsRowFull(row))
                {
                    ClearLine.Play();


                    DeleteRow(row);
                    LineGravity(row);
                    ScoreMulti += 1;
                    
                }
            
            
            
            
        }
        */

        while (row < height)
        {
            if (IsRowFull(row))
            {
                ClearLine.Play();


                DeleteRow(row);
                LineGravity(row);
                ScoreMulti += 1;
                LevelUpAddition();
                Debug.Log("while");


            }
            else
            {
                row += 1;
            }
        }



        ScoreMultiplier(ScoreMulti);
    }

    public void DeleteRow(int row)
    {
        for(int column = 0; column < width; column++)
        {
            Destroy(blockGrid[column, row].gameObject);
        }
    }


    
    public void LineGravity(int pos)
    {

        for(int row = pos; row < height-1; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (blockGrid[col, row + 1] != null)
                {
                    blockGrid[col, row] = blockGrid[col, row + 1];
                    blockGrid[col, row].gameObject.transform.position -= new Vector3(0, 1, 0);
                    blockGrid[col, row + 1] = null;
                }
                else
                {
                    blockGrid[col, row] = null;
                }
                
            }
        }

    }


    bool IsRowFull(int row)
    {
        for(int col = 0; col < width;col++)
        {
            if(blockGrid[col, row] == null)
            {
                return false;
            }
            
        }
        return true;
    }

    public void ScoreMultiplier(int ScoreMultiplier)
    {
        float Score = 0;
        switch (ScoreMultiplier)
        {

            case 1:
                Score = 40;
                break;

            case 2:
                Score = 100;
                break;

            case 3:
                Score = 300;
                break;

            case 4:
                Score = 1200;
                break;

            default:
                Score = 0;
                break;

        }
        player.score += Score;
        scoreBoard.text = player.score.ToString();
    }




}
