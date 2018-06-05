using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Json_Importer : MonoBehaviour {

    public TextAsset Jsonfile;
    // Use this for initialization


    public GameObject tracker_GO;
    public List<Vector3> Vertex_Vector_List = new List<Vector3>();
    public List<int> Face_Int_List = new List<int>();
    void Start() {
     //   ProcessJson();
     //   OnDrawGizmos();
    }

    // Update is called once per frame
    void Update() {

    }
    private void Awake()
    {
        ProcessJson();
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.black;
        for (int i = 0; i < Vertex_Vector_List.Count; i++)
        {
            Gizmos.DrawSphere(Vertex_Vector_List[i], 0.01f);
        }
    }



    public void create_trackes()
    {
        foreach (Vector3 v in Vertex_Vector_List)
        {
            GameObject g = Instantiate(tracker_GO, transform);
            g.transform.position = v;
        }
    }




    public void ProcessJson() {
        var N = JSON.Parse(Jsonfile.text);

          JSONArray Verticesarray = N["geometries"][5]["data"]["vertices"].AsArray;
          JSONArray Facearray = N["geometries"][5]["data"]["vertices"].AsArray;

       // JSONArray Verticesarray = N["geometries"][1]["VertexCoords"].AsArray;
       // JSONArray Facearray = N["geometries"][1]["VertexIndices"].AsArray;

        // Debug.Log(N[""][0]["data"]["VertexCoords"].Value);

        for (int i = 0; i < Verticesarray.Count;) {
            Vertex_Vector_List.Add(new Vector3(Verticesarray[i].AsFloat/1000f, Verticesarray[i + 1].AsFloat/1000f, Verticesarray[i + 2].AsFloat/1000f));
            i = i + 3;
        }

        for (int i = 0; i < Facearray.Count - Facearray.Count;)
        {
            Face_Int_List.Add(Facearray[i + 1]);
            Face_Int_List.Add(Facearray[i + 2]);
            Face_Int_List.Add(Facearray[i + 3]);
            i = i + 4;
        }

           this.GetComponent<MeshFilter>().mesh.vertices=Vertex_Vector_List.ToArray();
           this.GetComponent<MeshFilter>().mesh.SetTriangles(Face_Int_List.ToArray(),0);


        create_trackes();
    }






}


