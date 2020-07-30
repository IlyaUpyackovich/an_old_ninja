using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Survive : MonoBehaviour
{
    public RandomMobSpawner mobSpawner;
    public GameObject jumpText;
    public GameObject finish;
    public int[] intervals;
    public float timer = 60f;

    private StateMachine stateMachine;
    private AudioSource audioSource;
    private Overlay overlay;
    private int currentStep;

    private void Start()
    {
        overlay = FindObjectOfType<Overlay>();
        audioSource = GetComponent<AudioSource>();
        stateMachine = FindObjectOfType<StateMachine>();

        Init();
    }

    private void Init()
    {
        jumpText.SetActive(false);
        finish.SetActive(false);

        overlay.Show();
        overlay.line1.text = "Survive!";
        overlay.line2.text = Mathf.FloorToInt(timer).ToString();

        timer = 60f;
        currentStep = intervals.Length - 1;
    }

    private void Update()
    {
        if (stateMachine.currentState.stateName != "Play") return;

        timer -= Time.deltaTime;
        overlay.line2.text = Mathf.FloorToInt(timer).ToString();

        if (currentStep > 0 && currentStep < intervals.Length)
        {
            if (timer < intervals[currentStep])
            {
                mobSpawner.interval--;
                currentStep--;
            }
        }

        if (timer <= 8 && !audioSource.isPlaying)
            audioSource.Play();

        if (timer <= 0)
        {
            jumpText.SetActive(true);
            finish.SetActive(true);
            overlay.Hide();
        }
    }

    public void Clean()
    {
        Init();
        audioSource.Stop();
        mobSpawner.interval = 5f;
    }
}
