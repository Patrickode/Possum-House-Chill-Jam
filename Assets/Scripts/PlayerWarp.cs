using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWarp : MonoBehaviour
{
	public enum WarpType
	{
		Death,
		Win
	}

    public WarpType warpType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (warpType)
            {
                case WarpType.Death:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;

                case WarpType.Win:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    break;

                default:
                    break;
            }
        }
    }
}
