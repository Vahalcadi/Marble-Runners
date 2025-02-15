using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Button resetButton;

    void Start()
    {
        resetButton.onClick.AddListener(ResetProgress); // Aggiungi il listener per il click
    }

    void ResetProgress()
    {
        PlayerPrefs.DeleteAll(); // Reset tutti i salvataggi
        PlayerPrefs.Save(); // Salva i cambiamenti
        // Aggiungi codice per riavviare il livello o tornare al menu principale
        Debug.Log("Salvataggi resettati!");
    }
}
