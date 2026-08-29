#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public static class ExitDoorPrefabBuilder
{
    private const string Folder = "Assets/SelfMade/Prefabs/Sprites/ThirdLevel/ExitDoor";
    private const string SpritePath = Folder + "/ExitDoor.png";
    private const string PrefabPath = Folder + "/ExitDoor.prefab";

    [MenuItem("Tools/SelfMade/Build Exit Door Prefab")]
    public static void Build()
    {
        Directory.CreateDirectory(Folder);
        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

        var importer = AssetImporter.GetAtPath(SpritePath) as TextureImporter;
        if (importer == null)
            throw new FileNotFoundException("Door sprite was not imported", SpritePath);

        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.spritePixelsPerUnit = 256f;
        importer.alphaIsTransparency = true;
        importer.mipmapEnabled = false;
        importer.filterMode = FilterMode.Bilinear;
        importer.textureCompression = TextureImporterCompression.CompressedHQ;
        importer.spritePivot = new Vector2(0.5f, 0f);
        importer.SaveAndReimport();

        var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(SpritePath);
        if (sprite == null)
            throw new System.InvalidOperationException("Could not load imported door sprite.");

        var root = new GameObject("ExitDoor");
        try
        {
            var renderer = root.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingOrder = 10;

            var trigger = root.AddComponent<BoxCollider2D>();
            trigger.isTrigger = true;
            trigger.size = new Vector2(sprite.bounds.size.x * 0.55f, sprite.bounds.size.y * 0.78f);
            trigger.offset = new Vector2(0f, sprite.bounds.size.y * 0.39f);

            var goal = root.AddComponent<ExitDoorGoal>();
            var serializedGoal = new SerializedObject(goal);
            serializedGoal.FindProperty("doorRenderer").objectReferenceValue = renderer;
            serializedGoal.ApplyModifiedPropertiesWithoutUndo();

            PrefabUtility.SaveAsPrefabAsset(root, PrefabPath);
        }
        finally
        {
            Object.DestroyImmediate(root);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Created exit-door prefab at " + PrefabPath);
    }
}
#endif
