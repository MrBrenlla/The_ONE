using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinalFunctions : MonoBehaviour
{
    public GameObject[] feathers;

    public Material[] materials;

    private void Start()
    {
        if(FeatherManager.allFeathers==null || feathers.Length == 0)
        {
            
            foreach (GameObject feather in feathers) RenderMat(feather, materials[2]);
        }
        else for(int i = 0;i < feathers.Length && i<4;i++)
        {
            RenderMat(feathers[i],materials[FeatherManager.allFeathers.ToArray()[i]]);
        }
    }

    private void RenderMat(GameObject f, Material m)
    {
        Renderer[] renderers = f.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers) r.material=m;
    }

    public int CalculateDestiny()
    {
        if(FeatherManager.allFeathers==null) return 2;
        int destiny = 0;
        int[] feathers = FeatherManager.allFeathers.ToArray();
        
        foreach(int f in feathers) if(f == 0) destiny++;
            else if (f == 1) destiny--;


        print("destino: "+destiny);

        if (destiny < 0)return 1;
        if (destiny > 0) return 3;
        return 2;
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("Completed",1);
        LoadScene.SceneLoad("Inicio");
    }

}
