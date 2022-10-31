using Assets.Script.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    private string dataPath = "CSVData/GunData.csv";
    private List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
    private List<GameObject> mGunBase = new List<GameObject>();

    public void InitializeGun()
    {
        data = CSVReader.Read(dataPath);

        for (var i = 0; i < data.Count; i++)
        {
            string n = data[i]["Name"].ToString();
            float range = ParseFloat(data[i]["Range"]);
            float damage = ParseFloat(data[i]["Damage"]);
            float rate = ParseFloat(data[i]["Rate"]);
            GunBase gb = Resources.Load<GunBase>("Guns/" + n);

            mGunBase.Add(Instantiate(gb.gameObject));
            if (TryGetComponent<GunBase>(out var thisGB)) thisGB.InitializeGun(n, range, damage, rate);

            //mGunBase[i].SetActive(false);
            mGunBase[i].transform.SetParent(PlayerManager.GetInstance.PlayerObject.transform);
        }

    }

    public List<GameObject> GetGunList()
    {
        return this.mGunBase;
    }

    private float ParseFloat(object obj)
    {
        if (float.TryParse(obj.ToString(), out float f)) return f;
        else return 0.0f;
    }

    private int ParseInt(object obj)
    {
        if (int.TryParse(obj.ToString(), out int i)) return i;
        else return 0;
    }

}
