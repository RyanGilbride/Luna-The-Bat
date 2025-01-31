using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{
    public Button button; // Assign in Inspector
    public float cooldownTime = 1.5f; // Set cooldown duration

    private bool isCooldown = false;

    public void OnButtonPressed()
    {
        if (!isCooldown)
        {
            StartCoroutine(StartCooldown());
        }
    }

    private IEnumerator StartCooldown()
    {
        isCooldown = true;
        button.interactable = false; // Disable button

        yield return new WaitForSeconds(cooldownTime); // Wait for cooldown

        button.interactable = true; // Re-enable button
        isCooldown = false;
    }
}
