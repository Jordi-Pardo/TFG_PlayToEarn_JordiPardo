using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig",fileName ="New Character Config")]
public class CharacterConfigSO : ScriptableObject
{
    public Sprite sprite;
    public int health;
    public int strength;
}
