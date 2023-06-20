using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeatherCounter : MonoBehaviour
{
    public int CalculateDestiny()
    {
        int destiny = 0;

        var f = FeatherManager.allFeathers.GroupBy(x =>  x == 0  ) ;
        destiny += f.Count();

        f = FeatherManager.allFeathers.GroupBy(x =>  x == 1 );
        destiny -= f.Count();

        return destiny;
    }
}
