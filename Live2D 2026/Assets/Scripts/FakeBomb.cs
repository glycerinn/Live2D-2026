using UnityEngine;
using System.Collections;

public class FakeBomb : MonoBehaviour, IBombInteraction
{
    bool activated;

    public void Interact(Bomb bomb)
    {
        if (activated)
            return;

        activated = true;

        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        Debug.Log("3");

        yield return new WaitForSeconds(1);

        Debug.Log("2");

        yield return new WaitForSeconds(1);

        Debug.Log("1");

        yield return new WaitForSeconds(1);

        Debug.Log("Game Over");

        // Kill player
    }
}