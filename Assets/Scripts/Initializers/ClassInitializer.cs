using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassInitializer
{

    public class GridFaceIDFaceData : SerializableDictionaryBase<GridCubeFaceID , GridCubeFaceData> { };

}
[System.Serializable]
public class GridFaceIDFaceData : SerializableDictionaryBase<GridCubeFaceID, GridCubeFaceData> { };

[System.Serializable]
public class GridFaceIDMaterial : SerializableDictionaryBase<GridCubeFaceID , Material> { };

[System.Serializable]
public class GridCubeFaceIDToObj: SerializableDictionaryBase<GridCubeFaceID , GameObject> { };

[System.Serializable]
public class VerticeOrientationToValue : SerializableDictionaryBase<VerticeOrientation , float> { };