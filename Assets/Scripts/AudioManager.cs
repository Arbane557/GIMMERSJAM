using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource SFXSource;

    [Header("AudioClip")]
    public AudioClip clickSound;
    public AudioClip cornAdSound;
    public AudioClip cowScreenSound;
    public AudioClip emailNotificationSound;
    public AudioClip errorSound;
    public AudioClip gacorJupiterAdSound;
    public AudioClip haloDogAdSound;
    public AudioClip hotMilkAdSound;
    public AudioClip incognitoPopupSound;
    public AudioClip jumpscareAdSound;

    private void PlaySFX(AudioClip clip)
    {
     SFXSource.PlayOneShot(clip);   
    }
}
