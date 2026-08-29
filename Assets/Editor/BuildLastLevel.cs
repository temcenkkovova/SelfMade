using System;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class BuildLastLevel
{
    private const string ScenePath = "Assets/SelfMade/Scenes/LastLevel.unity";
    private const string RootName = "CODEX_RooftopLevel";
    private static Sprite whiteSprite;

    [MenuItem("Tools/SelfMade/Build LastLevel Rooftop")]
    public static void Build()
    {
        Scene scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        whiteSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");

        GameObject previous = GameObject.Find(RootName);
        if (previous != null) UnityEngine.Object.DestroyImmediate(previous);

        DisableLegacyGroup("Grounds");
        DisableLegacyGroup("Walls");
        DisableLegacyGroup("Columns");
        DisableLegacyGroup("Clusters");
        DisableLegacyGroup("Platforms");
        DisableLegacyGroup("Buttons");
        DisableLegacyGroup("DeadZones");
        DisableLegacyGroup("Environment");

        Transform root = new GameObject(RootName).transform;
        Transform architecture = Child(root, "01_Architecture");
        Transform traversal = Child(root, "02_Traversal");
        Transform traps = Child(root, "03_Traps_And_Decoys");
        Transform decoration = Child(root, "04_Decoration");
        Transform markers = Child(root, "05_Level_Markers");

        string groundPrefab = "Assets/SelfMade/Prefabs/Sprites/ThirdLevel/Prefabs/GroundPrefab.prefab";
        string wallPrefab = "Assets/SelfMade/Prefabs/Sprites/ThirdLevel/Prefabs/wallPrefab.prefab";
        string stairPrefab = "Assets/SelfMade/Prefabs/Sprites/ThirdLevel/Prefabs/StairPrefab.prefab";
        string condPrefab = "Assets/SelfMade/Prefabs/Sprites/ThirdLevel/Prefabs/CondPrefab.prefab";
        string cond2Prefab = "Assets/SelfMade/Prefabs/Sprites/ThirdLevel/Prefabs/Cond2Prefab.prefab";
        string envPrefab = "Assets/SelfMade/Prefabs/Sprites/ThirdLevel/Prefabs/Env-prefab.prefab";
        string platformPrefab = "Assets/SelfMade/Prefabs/Sprites/ThirdLevel/Prefabs/PlatformPrefab.prefab";

        // Main route, kept within a single wide camera frame.
        Prefab(groundPrefab, "Start_Roof", new Vector3(-12.7f, -5.8f), new Vector3(2.25f, 1f, 1f), architecture);
        Prefab(groundPrefab, "Landing_A", new Vector3(-7.2f, -3.45f), new Vector3(1.15f, .75f, 1f), architecture);
        Prefab(groundPrefab, "Landing_B", new Vector3(-3.5f, -1.1f), new Vector3(1.25f, .75f, 1f), architecture);
        Prefab(groundPrefab, "Lift_Base", new Vector3(.2f, -4.95f), new Vector3(1.35f, .75f, 1f), architecture);
        Prefab(groundPrefab, "Upper_Roof_A", new Vector3(5.2f, 1.7f), new Vector3(1.7f, .75f, 1f), architecture);
        Prefab(groundPrefab, "Upper_Roof_B", new Vector3(10.1f, 3.85f), new Vector3(1.2f, .75f, 1f), architecture);
        Prefab(groundPrefab, "Goal_Roof", new Vector3(13.25f, 5.65f), new Vector3(1.5f, .75f, 1f), architecture);

        // Vertical walls create shafts, silhouettes and obvious dead ends.
        Prefab(wallPrefab, "Wall_Start", new Vector3(-15.2f, -2.8f), new Vector3(1.1f, 2.25f, 1f), architecture);
        Prefab(wallPrefab, "Wall_Central", new Vector3(-1.2f, -2.15f), new Vector3(.85f, 2.1f, 1f), architecture);
        Prefab(wallPrefab, "Wall_Lift", new Vector3(2.25f, -.5f), new Vector3(.7f, 2.7f, 1f), architecture);
        Prefab(wallPrefab, "Wall_Final", new Vector3(14.9f, 2.0f), new Vector3(.85f, 2.5f, 1f), architecture);

        Prefab(stairPrefab, "Stairs_To_Final", new Vector3(8.15f, 2.65f), new Vector3(1.25f, 1.25f, 1f), architecture);

        // First jump sequence over spikes.
        Solid("Step_1", new Vector3(-10.0f, -4.65f), new Vector3(1.5f, .45f), new Color(.72f,.74f,.73f), traversal);
        Solid("Step_2", new Vector3(-8.75f, -4.05f), new Vector3(1.45f, .45f), new Color(.72f,.74f,.73f), traversal);
        Solid("Step_3", new Vector3(-5.45f, -2.25f), new Vector3(1.5f, .4f), new Color(.72f,.74f,.73f), traversal);

        // Button-controlled lift.
        PlatformMoving lift = MovingLift(platformPrefab, new Vector3(1.35f, -3.3f), new Vector3(1.35f, .65f), traversal);
        Button("Lift_Button", new Vector3(-3.65f, -.15f), lift, traversal);

        // A corpse-friendly shortcut platform and a risky upper detour.
        Solid("Corpse_Step", new Vector3(3.85f, -.25f), new Vector3(1.6f, .4f), new Color(.65f,.68f,.68f), traversal);
        Solid("Narrow_Bridge", new Vector3(6.7f, 2.65f), new Vector3(1.7f, .32f), new Color(.74f,.75f,.72f), traversal);

        // Three clear hazards and one deceptive floor.
        Hazard("Spike_Pit_Start", new Vector3(-9.35f, -5.45f), new Vector3(4.35f, .55f), traps);
        Hazard("Lift_Shaft", new Vector3(1.35f, -5.55f), new Vector3(2.15f, .45f), traps);
        Hazard("Final_Shaft", new Vector3(8.65f, .15f), new Vector3(2.0f, .45f), traps);

        Solid("False_Floor_Visual", new Vector3(-7.7f, .1f), new Vector3(3.4f, .35f), new Color(.76f,.77f,.74f), traversal);
        Hazard("False_Floor_Trap", new Vector3(-7.7f, .25f), new Vector3(3.0f, .3f), traps, new Color(.76f,.77f,.74f,0.04f));
        Solid("Dead_End_Ledge", new Vector3(-10.6f, 1.65f), new Vector3(2.2f, .4f), new Color(.67f,.69f,.68f), traversal);

        Label("TRUST ME →", new Vector3(-9.25f, 2.55f), new Color(.62f,.12f,.1f), markers, 4.2f);
        Label("EXIT?", new Vector3(-11.4f, 2.2f), new Color(.16f,.18f,.19f), markers, 3.4f);
        Label("WORTH EVERY DEATH?", new Vector3(11.7f, 7.15f), new Color(.2f,.18f,.15f), markers, 3.0f);

        // Rooftop machinery and visual rhythm.
        Prefab(condPrefab, "HVAC_A", new Vector3(-13.5f, -4.25f), new Vector3(.65f,.65f,1), decoration);
        Prefab(cond2Prefab, "HVAC_B", new Vector3(4.8f, 2.95f), new Vector3(.7f,.7f,1), decoration);
        Prefab(envPrefab, "Rooftop_Equipment", new Vector3(10.3f, 5.0f), new Vector3(.65f,.65f,1), decoration);
        Railing(new Vector3(-12.7f, -4.55f), 4.2f, decoration);
        Railing(new Vector3(5.1f, 2.95f), 3.1f, decoration);
        Railing(new Vector3(13.25f, 6.75f), 2.8f, decoration);

        // Reuse the scene's goal, diploma and player management.
        MoveObject("RespawnPos", new Vector3(-13.4f, -4.55f));
        MoveObject("Goal", new Vector3(13.35f, 6.85f));
        MoveObject("FinishPhrasePos", new Vector3(12.1f, 7.35f));
        MoveObject("StartPhrasePos", new Vector3(-12.0f, -3.8f));

        Camera camera = Camera.main;
        if (camera != null)
        {
            camera.orthographic = true;
            camera.orthographicSize = 8.4f;
            camera.transform.position = new Vector3(0f, .6f, -10f);
            camera.backgroundColor = new Color(.48f,.77f,.92f);
        }

        // No sound requested.
        foreach (AudioSource source in UnityEngine.Object.FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            source.mute = true;

        EditorSceneManager.MarkSceneDirty(scene);
        bool saved = EditorSceneManager.SaveScene(scene, ScenePath, false);
        AssetDatabase.SaveAssets();
        File.WriteAllText(Path.Combine(Application.dataPath, "CodexBuildReport.txt"),
            $"DataPath={Application.dataPath}\nScenePath={scene.path}\nSaved={saved}\nTime={DateTime.Now:O}\n");
        Selection.activeGameObject = root.gameObject;
        Debug.Log("LastLevel rooftop generated successfully.");
    }

    private static Transform Child(Transform parent, string name)
    {
        GameObject go = new GameObject(name); go.transform.SetParent(parent); return go.transform;
    }

    private static void DisableLegacyGroup(string name)
    {
        GameObject go = GameObject.Find(name);
        if (go != null) go.SetActive(false);
    }

    private static GameObject Prefab(string path, string name, Vector3 pos, Vector3 scale, Transform parent)
    {
        GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (asset == null) { Debug.LogWarning("Missing prefab: " + path); return null; }
        GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(asset);
        go.name = name; go.transform.SetParent(parent); go.transform.position = pos; go.transform.localScale = scale;
        return go;
    }

    private static GameObject Solid(string name, Vector3 pos, Vector2 size, Color color, Transform parent)
    {
        GameObject go = VisualBox(name, pos, size, color, parent);
        BoxCollider2D col = go.AddComponent<BoxCollider2D>(); col.size = Vector2.one;
        go.tag = "Ground";
        return go;
    }

    private static GameObject VisualBox(string name, Vector3 pos, Vector2 size, Color color, Transform parent)
    {
        GameObject go = new GameObject(name); go.transform.SetParent(parent); go.transform.position = pos; go.transform.localScale = new Vector3(size.x,size.y,1);
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>(); sr.sprite = whiteSprite; sr.color = color; sr.sortingOrder = 3;
        return go;
    }

    private static void Hazard(string name, Vector3 pos, Vector2 size, Transform parent, Color? tint = null)
    {
        GameObject go = VisualBox(name, pos, size, tint ?? new Color(.63f,.1f,.08f), parent);
        BoxCollider2D col = go.AddComponent<BoxCollider2D>(); col.isTrigger = true;
        go.AddComponent<DeadZoneCollision>();
        // Simple triangular teeth communicate danger without animation.
        for (int i=0;i<Mathf.Max(1,Mathf.FloorToInt(size.x/.42f));i++)
        {
            GameObject tooth=VisualBox("Spike",new Vector3(pos.x-size.x/2+.21f+i*.42f,pos.y+size.y*.62f,pos.z),new Vector2(.22f,.65f),new Color(.8f,.18f,.12f),go.transform);
            tooth.transform.rotation=Quaternion.Euler(0,0,45);
        }
    }

    private static PlatformMoving MovingLift(string prefabPath, Vector3 start, Vector3 end, Transform parent)
    {
        // Build from scratch so legacy prefab scripts cannot run before the Rigidbody exists.
        GameObject go=VisualBox("Button_Lift",start,new Vector2(2.4f,.45f),new Color(.92f,.62f,.06f),parent);
        Rigidbody2D rb=go.AddComponent<Rigidbody2D>(); rb.bodyType=RigidbodyType2D.Kinematic; rb.gravityScale=0; rb.freezeRotation=true;
        BoxCollider2D col=go.AddComponent<BoxCollider2D>(); col.isTrigger=false;
        go.tag="Platform";
        for(int i=0;i<5;i++)
        {
            GameObject stripe=VisualBox("HazardStripe",new Vector3(start.x-0.88f+i*.44f,start.y,start.z-.01f),new Vector2(.16f,.46f),new Color(.12f,.12f,.1f),go.transform);
            stripe.transform.rotation=Quaternion.Euler(0,0,-22);
        }
        Transform target=new GameObject("EndPoint").transform; target.SetParent(parent); target.position=end;
        PlatformMoving moving=go.GetComponent<PlatformMoving>() ?? go.AddComponent<PlatformMoving>();
        SerializedObject so=new SerializedObject(moving); so.FindProperty("rb").objectReferenceValue=rb; so.FindProperty("endPoint").objectReferenceValue=target; so.FindProperty("moveDuration").floatValue=2.6f; so.ApplyModifiedPropertiesWithoutUndo();
        return moving;
    }

    private static void Button(string name, Vector3 pos, PlatformMoving lift, Transform parent)
    {
        GameObject go=VisualBox(name,pos,new Vector2(.8f,.25f),new Color(.8f,.12f,.1f),parent);
        BoxCollider2D col=go.AddComponent<BoxCollider2D>(); col.isTrigger=true; col.size=new Vector2(1.25f,2f);
        ActivePlatformButton button=go.AddComponent<ActivePlatformButton>(); button.platformMoving=lift;
    }

    private static void Railing(Vector3 pos,float width,Transform parent)
    {
        GameObject top=VisualBox("Railing",pos,new Vector2(width,.09f),new Color(.25f,.28f,.29f),parent);
        for(int i=0;i<4;i++) VisualBox("Post",new Vector3(pos.x-width/2+i*width/3,pos.y-.42f),new Vector2(.07f,.9f),new Color(.25f,.28f,.29f),top.transform);
    }

    private static void Label(string text,Vector3 pos,Color color,Transform parent,float size)
    {
        GameObject go=new GameObject("Sign_"+text); go.transform.SetParent(parent); go.transform.position=pos;
        TextMeshPro tmp=go.AddComponent<TextMeshPro>(); tmp.text=text; tmp.fontSize=size; tmp.color=color; tmp.alignment=TextAlignmentOptions.Center; tmp.sortingOrder=20; tmp.rectTransform.sizeDelta=new Vector2(6,1);
    }

    private static void MoveObject(string name, Vector3 pos)
    {
        GameObject go=GameObject.Find(name); if(go!=null) go.transform.position=pos;
    }
}
