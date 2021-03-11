using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pivot;
    public GameObject[] Tetrominoes;
    GameObject nextBlock;
    GameObject block;

    public void Start()
    {
        
    }

    public GameObject InitialSpawner()
    {
        int randomIndex = Random.Range(0, Tetrominoes.Length);
        block = Instantiate(Tetrominoes[randomIndex], transform.position, Quaternion.identity);
        randomIndex = Random.Range(0, Tetrominoes.Length);
        nextBlock = Instantiate(Tetrominoes[randomIndex], pivot.transform.position, Quaternion.identity);
        return block;
    }









    public void NextBlock() 
    {
        
    }
    public  GameObject NewBlock()
    {
        block = nextBlock;
        block.transform.position = transform.position;

        int randomIndex = Random.Range(0, Tetrominoes.Length);
        nextBlock = Instantiate(Tetrominoes[randomIndex], pivot.transform.position, Quaternion.identity);

        return block;



    }
}
