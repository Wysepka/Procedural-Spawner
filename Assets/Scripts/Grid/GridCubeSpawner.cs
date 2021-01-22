using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCubeSpawner : MonoBehaviour
{
    //Spawning gridCubes class, shortcut method, maybe for future purposes, or additional
    //methods etc...
    public static GridCube[,] ReturnGridCubes(float[,] noise)
    {
        GridCube[,] gridCubes = new GridCube[noise.GetLength(0), noise.GetLength(1)];

        for (int x = 0; x < noise.GetLength(0); x++)
        {
            for (int y = 0; y < noise.GetLength(1); y++)
            {
                GridCube gridCube = new GridCube(x , y);

                gridCubes[x, y] = gridCube;
            }
        }

        return gridCubes;
    }


}
