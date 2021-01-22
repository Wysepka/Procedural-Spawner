using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseSampler
{
    //Returning noise Array for X width and Y length
    public static float[,] ReturnNoiseArray(int width , int length)
    {
        float[,] noiseArray = new float[width, length];
        float scaler = GridMasterManager.GridSettings.GridNoiseScaler;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                noiseArray[x, y] = Mathf.PerlinNoise(x * scaler, y * scaler);
            }
        }

        return noiseArray;
    }

    //Returning Noise Array with offset
    public static float[,] ReturnNoiseArray(int width, int length , Vector2 offset)
    {
        float[,] noiseArray = new float[width, length];
        float scaler = GridMasterManager.GridSettings.GridNoiseScaler;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                noiseArray[x, y] = Mathf.PerlinNoise(x * scaler * offset.x, y * scaler * offset.y);
            }
        }

        return noiseArray;
    }

    //Returning gridCubeNoise
    //What is important here is that each Cube has 4 vertices at the top
    //In case where we have two cubes stationaring next to eachother, two of their indices are in the same position
    //So it is important to share this value across
    //And this is what this method below does, calculating NoiseArray for X width and Y length Grid, 
    //which is +1 in each direction greater
    public static GridCubeNoise[,] ReturnGridCubeNoise(int width , int length)
    {
        GridCubeNoise[,] gridCubeNoises = new GridCubeNoise[width, length];

        float[,] noiseMapValues = ReturnNoiseArray(width + 1, length + 1);

        int noiseMapWidth = noiseMapValues.GetLength(0);
        int noiseMapLength = noiseMapValues.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {

                float leftDown = noiseMapValues[x, y];
                float leftUp = noiseMapValues[x, y + 1];
                float rightUp = noiseMapValues[x + 1, y + 1];
                float rightDown = noiseMapValues[x + 1, y];

                GridCubeNoise cubeNoise = new GridCubeNoise(leftDown , leftUp , rightUp , rightDown);

                gridCubeNoises[x, y] = cubeNoise;

            }
        }

        return gridCubeNoises;
    }

    //Same as above with offset
    public static GridCubeNoise[,] ReturnGridCubeNoise(int width, int length , Vector2 offset)
    {
        GridCubeNoise[,] gridCubeNoises = new GridCubeNoise[width, length];

        float[,] noiseMapValues = ReturnNoiseArray(width + 1, length + 1 , offset);

        int noiseMapWidth = noiseMapValues.GetLength(0);
        int noiseMapLength = noiseMapValues.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {

                float leftDown = noiseMapValues[x, y];
                float leftUp = noiseMapValues[x, y + 1];
                float rightUp = noiseMapValues[x + 1, y + 1];
                float rightDown = noiseMapValues[x + 1, y];

                GridCubeNoise cubeNoise = new GridCubeNoise(leftDown, leftUp, rightUp, rightDown);

                gridCubeNoises[x, y] = cubeNoise;

            }
        }

        return gridCubeNoises;
    }

}

//GridCubeNoise class, which keep information about top Cube vertice offset
public struct GridCubeNoise
{
    float[] noiseValues;
    VerticeOrientationToValue orientationToValue;

    //==================================================================================//

    public VerticeOrientationToValue OrientationToValue { get { return orientationToValue; } }

    //==================================================================================//

    public GridCubeNoise(float leftDown , float leftUp , float rightUp , float rightDown)
    {
        noiseValues = new float[4]
        {
            leftDown,
            leftUp ,
            rightUp,
            rightDown
        };

        orientationToValue = new VerticeOrientationToValue();
        orientationToValue.Add(VerticeOrientation.LeftDown, leftDown);
        orientationToValue.Add(VerticeOrientation.LeftUp, leftUp);
        orientationToValue.Add(VerticeOrientation.RightUp, rightUp);
        orientationToValue.Add(VerticeOrientation.RightDown, rightDown);

        

    }

}


public enum MeshBodyType { Underfed , Strong , Normal , Fat}
public enum MeshMorphBodyKind { Eyes , Tongue , Body , Head}
[System.Serializable]
public class MeshMorhping
{
    [System.Serializable]
    public struct MeshMorphData
    {
        public GameObject topParentObj; // White_Man_Normal

        [SerializeField]
        Dictionary<MeshMorphBodyKind, Mesh> bodyKindToPrefabMesh;
        

    }

    [SerializeField]
    MeshMorphData[] meshMorphDatas;




}