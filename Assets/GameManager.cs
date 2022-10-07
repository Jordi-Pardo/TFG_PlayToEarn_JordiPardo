using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Character playerPrefab;
    [SerializeField] private Character enemyPrefab;

    [SerializeField] private List<CharacterConfigSO> characterConfigSOs;

    [SerializeField] private GameObject characterSelection;
    [SerializeField] private GameObject winMessage;
    [SerializeField] private GameObject loseMessage;
    [SerializeField] private GameObject playAgainButton;
    private int selected = 0;

    [SerializeField] Transform playersParent;

    
    Character player;
    Character enemy;

    public void StartFight()
    {

        player = Instantiate(playerPrefab, playersParent);
        enemy = Instantiate(enemyPrefab, playersParent);
        player.gameObject.SetActive(true);
        enemy.gameObject.SetActive(true);
       
        StartCoroutine(StartFightCO());

    }

    public IEnumerator StartFightCO()
    {
        player.Setup(characterConfigSOs[selected]);
        enemy.Setup(characterConfigSOs[0]);
        int round = 0;

        while (!player.IsDead && !enemy.IsDead)
        {
            yield return new WaitForSeconds(1f);
            player.Attack();
            enemy.RecieveDamage(player.strength);
            if (enemy.IsDead)
                break;
            yield return new WaitForSeconds(1f);
            enemy.Attack();
            player.RecieveDamage(enemy.strength);
            round++;
        }

        if (playerPrefab.IsDead)
        {
            loseMessage.SetActive(true);
        }
        else
        {
            winMessage.SetActive(true);
        }

        playAgainButton.SetActive(true);
    }

    public void SetSelected(int i)
    {
        selected = i;
    }

    public void OpenCharacterSelection()
    {
        characterSelection.SetActive(true);
        loseMessage.SetActive(false);
        winMessage.SetActive(false);
        playAgainButton.SetActive(false);
        List<GameObject> toDelete = new List<GameObject>();

        for (int i = 0; i < playersParent.childCount; i++)
        {
            toDelete.Add(playersParent.GetChild(i).gameObject);
        }
        foreach (var item in toDelete)
        {
            Destroy(item);
        }
        toDelete.Clear();
    }
}
