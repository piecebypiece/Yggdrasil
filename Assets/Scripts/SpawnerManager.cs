using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yggdrasil
{
    public class SpawnerManager : MonoBehaviour
    {
        CharacterManager charactermanager => CharacterManager.Instance;

        void SpawnCharacter()
        {
            int index = Random.Range(10001, 10003);
            charactermanager.AddCharacter(index);
        }

        void Start()
        {
            SpawnCharacter();
        }
    }
}