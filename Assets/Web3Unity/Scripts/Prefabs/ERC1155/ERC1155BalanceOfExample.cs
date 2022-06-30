using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ERC1155BalanceOfExample : MonoBehaviour
{
    [System.Serializable]
    public struct Golem
    {
        public GameObject golemGO;
        public string tokenId;

    }

    public Golem grayGolem; // Gray Golem
    public Golem brownGolem; // Brown Golem

    private List<Golem> golems;


    void Start()
    {
        golems = new List<Golem>();
        golems.Add(grayGolem);
        golems.Add(brownGolem);


        foreach (Golem golem in golems)
        {
            CheckGolems(golem);
        }
    }

    async public void CheckGolems(Golem golem)
    {
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x88B48F654c30e99bc2e4A1559b4Dcf1aD93FA656";
        string account = PlayerPrefs.GetString("Account");
        string tokenId = golem.tokenId;
        BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
        print(balanceOf);
        if(balanceOf > 0)
        {
            golem.golemGO.SetActive(true);
        }
    }


}
