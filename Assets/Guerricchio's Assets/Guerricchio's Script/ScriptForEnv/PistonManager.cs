using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonManager : MonoBehaviour
{
    [SerializeField] private List<PistonUpDown> pistons;
    [SerializeField] private float delayBetweenActivations = 1f;

    void Start()
    {
        StartCoroutine(ActivatePistonsRandomly());
    }

    IEnumerator ActivatePistonsRandomly()
    {
        while (true)
        {
            // Seleziona un pistone casuale da attivare
            int randomIndex = Random.Range(0, pistons.Count);
            pistons[randomIndex].ActivatePiston();

            // Aspetta prima di attivarne un altro
            yield return new WaitForSeconds(delayBetweenActivations);
        }
    }
}
