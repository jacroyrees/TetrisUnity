using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = 0f;
    public GameController Controller;
    public GameObject block;
    public TetrisBlock blockScript;
    private bool NewBlock;
    public float score = 0f;
    private int LinesMade;


    public void Start()
    {
        this.LinesMade = 0;
    }

    public void Update()
    {
        MoveBlock();
    }

    public int GetLinesMade()
    {
        return this.LinesMade;
    }

    public void SetLinesMade(int _lines)
    {
        this.LinesMade = _lines;
    }

    public void SetNewBlock(bool NewBlock)
    {
        this.NewBlock = NewBlock;
    }
    public void SetTetrisBlock(TetrisBlock block)
    {
        this.blockScript = block;
    }

    public void SetBlock(GameObject block)
    {
        this.block = block;
    }
    
    public bool GetNewBlock()
    {
        return NewBlock;
    }

    public GameObject GetBlock()
    {
        return this.block;
    }

    // Update is called once per frame
    


    public void MoveBlock()
    {
        timer += Time.deltaTime;
            //Check if the down key is in use -> If it is make the block fall at a rate 10 times faster
            if (timer > (Input.GetKey(KeyCode.DownArrow) ? GameController.NormalTime / 10 : GameController.NormalTime))
            {
                block.transform.position -= new Vector3(0, 1, 0);

                
                
                if (!ValidMove())
                {
                block.transform.position += new Vector3(0, 1, 0);
                if (Controller.CheckLoss())
                {
                    Debug.Log("Hello");
                    Application.Quit();
                }
                    Controller.AddToGrid(block);
                    Controller.EvaluateGrid();
                    Controller.SpawnBlock();
                    
                // SpawnerScript.NewBlock();

            }
                timer = 0;
            }


            //Left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
            block.transform.position -= new Vector3(1, 0, 0);
                if (!ValidMove())
                {
                block.transform.position += new Vector3(1, 0, 0);
                }
            }

            //Right
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
            block.transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                {
                block.transform.position -= new Vector3(1, 0, 0);
                }
            }


            //Rotate
            if (Input.GetKeyDown(KeyCode.Space))
            {
                block.transform.RotateAround(block.transform.TransformPoint(blockScript.GetRotationPoint()), new Vector3(0, 0, 1), -90);
                if (!ValidMove())
                {
                    block.transform.RotateAround(block.transform.TransformPoint(blockScript.GetRotationPoint()), new Vector3(0, 0, 1), 90);
            }
            else
            {
                Controller.rotate.Play();
            }
            }

        
        
      

    }
    

    bool ValidMove()
    {

        foreach (Transform child in block.transform)
        {

            

            int roundedX = Mathf.RoundToInt(child.transform.position.x);
            int roundedY = Mathf.RoundToInt(child.transform.position.y);
            
            if (roundedX < 0 || roundedX >= GameController.width || roundedY < 0 || roundedY >= GameController.height)
            {
                return false;
            }

           if(!Controller.CheckEmpty(roundedX, roundedY))
            {
                return false;
            }

        }
        return true;
    }
}



