using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeath : MonoBehaviour
{
    public void Die()
    {
        if (GameFlow.Instance.isBoss)
        {
            SceneManager.LoadScene("WinScene");
        }

        Destroy(gameObject);
    }
}
