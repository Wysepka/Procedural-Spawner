using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCubeMeshHandler : MonoBehaviour
{
    
    //Static variables, declared for shortcut
    static float CubeXDim { get { return GridMasterManager.GridSettings.GridCubeDimensions.x; } }
    static float CubeYDim { get { return GridMasterManager.GridSettings.GridCubeDimensions.y; } }
    static float CubeZDim { get { return GridMasterManager.GridSettings.GridCubeDimensions.z; } }

    #region  Generating Grid Cube Face Mesh

    //Generating gridCubeFace based on informations passed in paramethers
    //New Obj is instantiated, with default Cube Components
    //Approporiate FaceMesh is then applied to this obj
    public static GameObject GenerateGridCubeFace(GridCubeFaceID faceID , GameObject parent , GridCubeNoise cubeNoise)
    {
        GameObject cubeFace;
        MeshRenderer cubeFaceRenderer;
        MeshFilter cubeFaceMeshFilter;
        GenerateFaceObjSettings(faceID, parent, out cubeFace, out cubeFaceRenderer, out cubeFaceMeshFilter);
        Mesh faceMesh = new Mesh();

        if (faceID == GridCubeFaceID.DOWN)
        {
            faceMesh = GenerateGridCubeDownFace(cubeNoise);
            //cubeFace.transform.localRotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
        }
        else if (faceID == GridCubeFaceID.LEFT)
        {
            faceMesh = GenerateGridCubeLeftFace(cubeNoise);
        }
        else if (faceID == GridCubeFaceID.RIGHT)
        {
            faceMesh = GenerateGridCubeRightFace(cubeNoise);
        }
        else if (faceID == GridCubeFaceID.FORWARD)
        {
            faceMesh = GenerateGridCubeForwardFace(cubeNoise);
        }
        else if (faceID == GridCubeFaceID.BACKWARD)
        {
            faceMesh = GenerateGridCubeBackwardFace(cubeNoise);
        }
        
        else if(faceID == GridCubeFaceID.UP)
        {
            faceMesh = GenerateGridCubeTopFace(cubeNoise);
        }
        

        ApplyCubeFaceMeshSettings(faceID, cubeFaceRenderer, cubeFaceMeshFilter, faceMesh);

        cubeFace.isStatic = true;

        return cubeFace;
    }

    //Assigning FaceMesh to spawned Obj, and Material defined previously in Singleton
    private static void ApplyCubeFaceMeshSettings(GridCubeFaceID faceID, MeshRenderer cubeFaceRenderer, MeshFilter cubeFaceMeshFilter, Mesh faceMesh)
    {
        cubeFaceMeshFilter.sharedMesh = faceMesh;
        cubeFaceRenderer.sharedMaterial = GridMasterManager.GridSettings.GridCubeInitializeData.GridFaceIDMaterial[faceID];
    }

    //Generating FaceObj settings, what is important here that Components are const. defined in GridSettings
    private static void GenerateFaceObjSettings(GridCubeFaceID faceID, GameObject parent, out GameObject cubeFace, out MeshRenderer cubeFaceRenderer, out MeshFilter cubeFaceMeshFilter)
    {
        cubeFace = new GameObject(faceID.ToString(), GridMasterManager.GridSettings.GridCubeInitializingComponents);
        cubeFace.transform.parent = parent.transform;
        cubeFaceRenderer = cubeFace.GetComponent<MeshRenderer>();
        cubeFaceMeshFilter = cubeFace.GetComponent<MeshFilter>();
    }

    //Not in use currently
    Vector3[] downFaceVerts = new Vector3[4]
    {
        new Vector3(-CubeXDim / 2f, 0,  - CubeXDim / 2f),
        new Vector3(-CubeXDim /2f, 0, CubeXDim / 2f),
        new Vector3(CubeXDim / 2f, 0, CubeXDim/2f),
        new Vector3(CubeXDim / 2f, 0, -CubeXDim / 2f),
    };

    
    //Not used anymore
    public static GameObject GenerateGridCubeTopFaceObj(GridCubeNoise cubeNoise , GameObject parent)
    {
        GameObject cubeFace;
        MeshRenderer cubeFaceRenderer;
        MeshFilter cubeFaceMeshFilter;
        GenerateFaceObjSettings(GridCubeFaceID.UP, parent, out cubeFace, out cubeFaceRenderer, out cubeFaceMeshFilter);

        Mesh topMesh = new Mesh();
        Vector3[] verts = new Vector3[4];

        float leftDown = cubeNoise.OrientationToValue[VerticeOrientation.LeftDown];
        float leftUp = cubeNoise.OrientationToValue[VerticeOrientation.LeftUp];
        float rightUp = cubeNoise.OrientationToValue[VerticeOrientation.RightUp];
        float rightDown = cubeNoise.OrientationToValue[VerticeOrientation.RightDown];

        verts[0] = new Vector3(-CubeXDim / 2f, CubeYDim / 2f + leftDown, -CubeXDim / 2f);
        verts[1] = new Vector3(-CubeXDim / 2f, CubeYDim / 2f + leftUp, CubeXDim / 2f);
        verts[2] = new Vector3(CubeXDim / 2f, CubeYDim / 2f + rightUp, CubeXDim / 2f);
        verts[3] = new Vector3(CubeXDim / 2f, CubeYDim / 2f + rightDown, -CubeXDim / 2f);

        //verts[0] = new Vector3(0, 0, 0);
        //verts[1] = new Vector3(1, 0, 0);
        //verts[2] = new Vector3(1, 0, 1);
        //verts[3] = new Vector3(0, 0, 1);

        topMesh.vertices = verts;

        Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

        topMesh.uv = uvs;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 3;
        triangles[3] = 3;
        triangles[4] = 1;
        triangles[5] = 2;

        topMesh.triangles = triangles;

        topMesh.RecalculateNormals();
        topMesh.RecalculateTangents();
        topMesh.RecalculateBounds();

        ApplyCubeFaceMeshSettings(GridCubeFaceID.UP, cubeFaceRenderer, cubeFaceMeshFilter, topMesh);
        cubeFace.transform.localPosition = Vector3.zero;
        return cubeFace;
    }

    #region Generating Desired Face Mesh Obj

    //Generating desired face mesh for face Obj
    //Most of variables such as vertex positions for top vertices are pre prefined by Noise Sampler
    //Each face has two vertices located at the top
    //And for every cube there is GridCubeNoise data which keep data about specific (x,y) GridCube
    public static Mesh GenerateGridCubeTopFace(GridCubeNoise cubeNoise)
    {

        Mesh topMesh = new Mesh();
        Vector3[] verts = new Vector3[4];

        float leftDown = cubeNoise.OrientationToValue[VerticeOrientation.LeftDown];
        float leftUp = cubeNoise.OrientationToValue[VerticeOrientation.LeftUp];
        float rightUp = cubeNoise.OrientationToValue[VerticeOrientation.RightUp];
        float rightDown = cubeNoise.OrientationToValue[VerticeOrientation.RightDown];

        verts[0] = new Vector3(-CubeXDim / 2f, CubeYDim / 2f + leftDown, -CubeZDim / 2f);
        verts[1] = new Vector3(-CubeXDim / 2f, CubeYDim / 2f + leftUp, CubeZDim / 2f);
        verts[2] = new Vector3(CubeXDim / 2f, CubeYDim / 2f + rightUp, CubeZDim / 2f);
        verts[3] = new Vector3(CubeXDim / 2f, CubeYDim / 2f + rightDown, -CubeZDim / 2f);

        //verts[0] = new Vector3(0, 0, 0);
        //verts[1] = new Vector3(1, 0, 0);
        //verts[2] = new Vector3(1, 0, 1);
        //verts[3] = new Vector3(0, 0, 1);

        topMesh.vertices = verts;

        Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

        topMesh.uv = uvs;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 3;
        triangles[3] = 3;
        triangles[4] = 1;
        triangles[5] = 2;

        topMesh.triangles = triangles;

        topMesh.RecalculateNormals();
        topMesh.RecalculateTangents();
        topMesh.RecalculateBounds();

        return topMesh;
    }

    public static Mesh GenerateGridCubeDownFace(GridCubeNoise cubeNoise)
    {

        Mesh downMesh = new Mesh();
        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(-CubeXDim / 2f, -CubeYDim / 2f,  - CubeZDim / 2f);
        verts[1] = new Vector3(-CubeXDim /2f, -CubeYDim / 2f, CubeZDim / 2f);
        verts[2] = new Vector3(CubeXDim / 2f, -CubeYDim / 2f, CubeZDim/2f);
        verts[3] = new Vector3(CubeXDim / 2f, -CubeYDim / 2f, -CubeZDim / 2f);

        //verts[0] = new Vector3(0, 0, 0);
        //verts[1] = new Vector3(1, 0, 0);
        //verts[2] = new Vector3(1, 0, 1);
        //verts[3] = new Vector3(0, 0, 1);

        downMesh.vertices = verts;

        Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

        downMesh.uv = uvs;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;
        triangles[3] = 3;
        triangles[4] = 2;
        triangles[5] = 1;

        downMesh.triangles = triangles;

        downMesh.RecalculateNormals();
        downMesh.RecalculateTangents();
        downMesh.RecalculateBounds();

        return downMesh;
    }

    public static Mesh GenerateGridCubeLeftFace(GridCubeNoise cubeNoise)
    {

        Mesh downMesh = new Mesh();
        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(-CubeXDim / 2f, -CubeYDim / 2f, CubeZDim / 2f);
        verts[1] = new Vector3(-CubeXDim / 2f, CubeYDim / 2f + cubeNoise.OrientationToValue[VerticeOrientation.LeftUp], CubeZDim / 2f);
        verts[2] = new Vector3(-CubeXDim / 2f, CubeYDim / 2f + cubeNoise.OrientationToValue[VerticeOrientation.LeftDown], -CubeZDim / 2f);
        verts[3] = new Vector3(-CubeXDim / 2f, -CubeYDim / 2f, -CubeZDim / 2f);

        //verts[0] = new Vector3(0, 0, 0);
        //verts[1] = new Vector3(1, 0, 0);
        //verts[2] = new Vector3(1, 0, 1);
        //verts[3] = new Vector3(0, 0, 1);

        downMesh.vertices = verts;

        Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

        downMesh.uv = uvs;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 3;
        triangles[3] = 3;
        triangles[4] = 1;
        triangles[5] = 2;

        downMesh.triangles = triangles;

        downMesh.RecalculateNormals();
        downMesh.RecalculateTangents();
        downMesh.RecalculateBounds();

        return downMesh;
    }

    public static Mesh GenerateGridCubeRightFace(GridCubeNoise cubeNoise)
    {

        Mesh downMesh = new Mesh();
        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(CubeXDim / 2f, -CubeYDim / 2f, CubeZDim / 2f);
        verts[1] = new Vector3(CubeXDim / 2f, CubeYDim / 2f + cubeNoise.OrientationToValue[VerticeOrientation.RightUp], CubeZDim / 2f);
        verts[2] = new Vector3(CubeXDim / 2f, CubeYDim / 2f + cubeNoise.OrientationToValue[VerticeOrientation.RightDown], -CubeZDim / 2f);
        verts[3] = new Vector3(CubeXDim / 2f, -CubeYDim / 2f, -CubeZDim / 2f);

        //verts[0] = new Vector3(0, 0, 0);
        //verts[1] = new Vector3(1, 0, 0);
        //verts[2] = new Vector3(1, 0, 1);
        //verts[3] = new Vector3(0, 0, 1);

        downMesh.vertices = verts;

        Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

        downMesh.uv = uvs;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;
        triangles[3] = 3;
        triangles[4] = 2;
        triangles[5] = 1;

        downMesh.triangles = triangles;

        downMesh.RecalculateNormals();
        downMesh.RecalculateTangents();
        downMesh.RecalculateBounds();

        return downMesh;
    }

    public static Mesh GenerateGridCubeForwardFace(GridCubeNoise cubeNoise)
    {

        Mesh downMesh = new Mesh();
        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(CubeXDim / 2f, -CubeYDim / 2f, CubeZDim / 2f);
        verts[1] = new Vector3(CubeXDim / 2f, CubeYDim / 2f + cubeNoise.OrientationToValue[VerticeOrientation.RightUp], CubeZDim / 2f);
        verts[2] = new Vector3(-CubeXDim / 2f, CubeYDim / 2f + cubeNoise.OrientationToValue[VerticeOrientation.LeftUp], CubeZDim / 2f);
        verts[3] = new Vector3(-CubeXDim / 2f, -CubeYDim / 2f, CubeZDim / 2f);

        //verts[0] = new Vector3(0, 0, 0);
        //verts[1] = new Vector3(1, 0, 0);
        //verts[2] = new Vector3(1, 0, 1);
        //verts[3] = new Vector3(0, 0, 1);

        downMesh.vertices = verts;

        Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

        downMesh.uv = uvs;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 3;
        triangles[3] = 3;
        triangles[4] = 1;
        triangles[5] = 2;

        downMesh.triangles = triangles;

        downMesh.RecalculateNormals();
        downMesh.RecalculateTangents();
        downMesh.RecalculateBounds();

        return downMesh;
    }

    public static Mesh GenerateGridCubeBackwardFace(GridCubeNoise cubeNoise)
    {

        Mesh downMesh = new Mesh();
        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(CubeXDim / 2f, -CubeYDim / 2f, -CubeZDim / 2f);
        verts[1] = new Vector3(CubeXDim / 2f, CubeYDim / 2f + cubeNoise.OrientationToValue[VerticeOrientation.RightDown], -CubeZDim / 2f);
        verts[2] = new Vector3(-CubeXDim / 2f, CubeYDim / 2f + cubeNoise.OrientationToValue[VerticeOrientation.LeftDown], -CubeZDim / 2f);
        verts[3] = new Vector3(-CubeXDim / 2f, -CubeYDim / 2f,-CubeZDim / 2f);

        //verts[0] = new Vector3(0, 0, 0);
        //verts[1] = new Vector3(1, 0, 0);
        //verts[2] = new Vector3(1, 0, 1);
        //verts[3] = new Vector3(0, 0, 1);

        downMesh.vertices = verts;

        Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

        downMesh.uv = uvs;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;
        triangles[3] = 3;
        triangles[4] = 2;
        triangles[5] = 1;

        downMesh.triangles = triangles;

        downMesh.RecalculateNormals();
        downMesh.RecalculateTangents();
        downMesh.RecalculateBounds();

        return downMesh;
    }

    #endregion

    #endregion

    //Made for Optimisation reasons
    //This method combines each Face obj in GridCube to one obj ( so 6 cube faces as 1 child obj with combined MeshFilter)
    public static void CombineGridCubeFaceMeshes(GameObject gridParentObj , GridCubeFaceIDToObj cubeFaceToObj)
    {
        if (GridMasterManager.GridSettings.GridPreferences.SeperateState == GridSeperateState.PerCubeFace) return;

        GridCubeFaceID topFaceID = GridMasterManager.GridSettings.GridCubeFaceIDs[0];
        MeshFilter topMeshFilter = gridParentObj.GetComponent<MeshFilter>();

        List<CombineInstance> combineInstances = new List<CombineInstance>();

        for (int i = 0; i < GridMasterManager.GridSettings.GridCubeFaceIDs.Length; i++)
        {
            GridCubeFaceID faceID = GridMasterManager.GridSettings.GridCubeFaceIDs[i];

            CombineInstance combineInstance = new CombineInstance();
            combineInstance.mesh = cubeFaceToObj[faceID].GetComponent<MeshFilter>().mesh;
            combineInstance.transform = cubeFaceToObj[faceID].GetComponent<MeshFilter>().transform.localToWorldMatrix;

            combineInstances.Add(combineInstance);
            cubeFaceToObj[faceID].SetActive(false);
            GameObject.Destroy(cubeFaceToObj[faceID].gameObject);
        }

        topMeshFilter.mesh = new Mesh();
        topMeshFilter.mesh.CombineMeshes(combineInstances.ToArray());

    }

}
