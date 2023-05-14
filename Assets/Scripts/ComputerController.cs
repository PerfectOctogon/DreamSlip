using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class ComputerController : MonoBehaviour
{
    //MUSIC PLAYER
    [SerializeField]
    Button musicBackButton;
    [SerializeField]
    Button musicPlayerButton;
    [SerializeField]
    GameObject musicPlayer;
    [SerializeField]
    Player p;
    [SerializeField]
    AudioSource speaker;
    [SerializeField]
    AudioClip lullaby;
    [SerializeField]
    GameObject computerScreen;
    [SerializeField]

    //DECODER
    Button decoderButton;
    [SerializeField]
    GameObject decoder;
    [SerializeField]
    Button decoderBackButton;
    [SerializeField]
    InputField decoderInput;
    [SerializeField]
    Button base32Button;
    [SerializeField]
    Button base64Button;
    [SerializeField]
    Button convertButton;
    [SerializeField]
    GameObject codeDisplayer;
    [SerializeField]
    Text code;
    [SerializeField]
    Button codeDisplayerBackButton;
    int currentDecoder = 0;

    //BROWSER
    [SerializeField]
    Button browserButton;
    [SerializeField]
    Button browserExitButton;
    [SerializeField]
    GameObject browser;
    [SerializeField]
    Button monsterDreamsButton;
    [SerializeField]
    GameObject monsterDreams;
    [SerializeField]
    Button proximityButton;
    [SerializeField]
    Button proximityExitButton;
    [SerializeField]
    GameObject proximity;
    [SerializeField]
    GameObject proximityVisual;
    [SerializeField]
    AudioClip staticSound;
    [SerializeField]
    AudioClip beep;
    [SerializeField]
    DoorMonster dm;
    bool isUsingProx = false;
    bool isBeeping = false;
    float proximityTimer = 0;
    [SerializeField]
    Text chatter;
    [SerializeField]
    GameObject chat;

    //MonsterDreams
    [SerializeField]
    GameObject stepContainer;

    //AUTHENTICATOR
    [SerializeField]
    GameObject authenticator;

    //SPAWNER
    [SerializeField]
    GameObject spawner;

    // Start is called before the first frame update

    void Start()
    {
        musicBackButton.onClick.AddListener(ExitMusicPlayer);
        musicPlayerButton.onClick.AddListener(EnterMusicPlayer);
        decoderButton.onClick.AddListener(EnterDecoder);
        decoderBackButton.onClick.AddListener(ExitDecoder);
        base32Button.onClick.AddListener(SetBase32);
        base64Button.onClick.AddListener(SetBase64);
        convertButton.onClick.AddListener(Decode);
        codeDisplayerBackButton.onClick.AddListener(ExitCodeDisplayer);
        browserButton.onClick.AddListener(EnterBrowser);
        browserExitButton.onClick.AddListener(ExitBrowser);
        proximityButton.onClick.AddListener(EnterProximity);
        proximityExitButton.onClick.AddListener(ExitProximity);
        monsterDreamsButton.onClick.AddListener(EnterMonsterDream);
    }

    private void Update()
    {
        if (isBeeping) {
            proximityTimer -= Time.deltaTime;
            if (proximityTimer <= 0) {
                speaker.Play();
                proximityTimer = 0.1f + (dm.GetWaitTime() / dm.GetMaxWaitTime());
            }
        }
    }

    public void ExitComputer() {
        computerScreen.SetActive(false);
        ExitMusicPlayer();
        ExitProximity();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EnterComputer() {
        computerScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitMusicPlayer() {
        musicPlayer.SetActive(false);
        p.isPlayingMusic = false;
        speaker.Stop();
    }

    public void EnterMusicPlayer() {
        musicPlayer.SetActive(true);
        p.isPlayingMusic = true;
        speaker.clip = lullaby;
        speaker.Play();
    }

    public void EnterDecoder() {
        decoder.SetActive(true);
    }

    public void ExitDecoder() {
        decoder.SetActive(false);
    }

    public void SetBase32() {
        currentDecoder = 0;
    }

    public void SetBase64() {
        currentDecoder = 1;
    }

    public void Decode() {
        string myString = "Not valid";
        string decoderString = decoderInput.text;
        
        if (currentDecoder == 1) {
            try
            {
                byte[] data = System.Convert.FromBase64String(decoderString);
                myString = System.Text.Encoding.UTF8.GetString(data);
            }
            catch (System.Exception e) { 
                //Not valid code
            }
        }
        else {
            try
            {
                byte[] data = ToBytes(decoderString);
                myString = System.Text.Encoding.UTF8.GetString(data);
            }
            catch (System.Exception e) { 
                //Not valid code
            }
        }


        if (myString.Length <= 5)
        {
            code.text = myString;
            codeDisplayer.SetActive(true);
        }

        else {
            code.text = "Not valid";
            codeDisplayer.SetActive(true);
        }
    }

    public void ExitCodeDisplayer() {
        codeDisplayer.SetActive(false);
    }


    public static byte[] ToBytes(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new System.ArgumentNullException("input");
        }

        input = input.TrimEnd('='); //remove padding characters
        int byteCount = input.Length * 5 / 8; //this must be TRUNCATED
        byte[] returnArray = new byte[byteCount];

        byte curByte = 0, bitsRemaining = 8;
        int mask = 0, arrayIndex = 0;

        foreach (char c in input)
        {
            int cValue = CharToValue(c);

            if (bitsRemaining > 5)
            {
                mask = cValue << (bitsRemaining - 5);
                curByte = (byte)(curByte | mask);
                bitsRemaining -= 5;
            }
            else
            {
                mask = cValue >> (5 - bitsRemaining);
                curByte = (byte)(curByte | mask);
                returnArray[arrayIndex++] = curByte;
                curByte = (byte)(cValue << (3 + bitsRemaining));
                bitsRemaining += 3;
            }
        }

        //if we didn't end with a full byte
        if (arrayIndex != byteCount)
        {
            returnArray[arrayIndex] = curByte;
        }

        return returnArray;
    }

    private static int CharToValue(char c)
    {
        int value = (int)c;

        //65-90 == uppercase letters
        if (value < 91 && value > 64)
        {
            return value - 65;
        }
        //50-55 == numbers 2-7
        if (value < 56 && value > 49)
        {
            return value - 24;
        }
        //97-122 == lowercase letters
        if (value < 123 && value > 96)
        {
            return value - 97;
        }

        throw new System.ArgumentException("Character is not a Base32 character.", "c");
    }

    public void EnterBrowser() {
        browser.SetActive(true);
    }

    public void ExitBrowser() {
        browser.SetActive(false);
    }

    public void EnterMonsterDream() {
        monsterDreams.SetActive(true);
    }

    public void EnterProximity() {
        isUsingProx = true;
        proximity.SetActive(true);
        speaker.clip = staticSound;
        speaker.Play();
        float waitTime = Random.Range(4, 10);
        Invoke("Proximity", waitTime);
    }

    public void ExitProximity() {
        isUsingProx = false;
        isBeeping = false;
        proximityVisual.SetActive(false);
        proximity.SetActive(false);
        speaker.Stop();
    }

    public void Proximity() {
        if (isUsingProx) {
            isBeeping = true;
            proximityVisual.SetActive(true);
            speaker.clip = beep;
        }
    }

    public void ExitMonsterDreams() {
        monsterDreams.SetActive(false);
    }

    public void ExitStepWindow() {
        for (int i = 0; i < 7; i++)
        {
            stepContainer.GetComponentInChildren<RectTransform>().GetChild(i).gameObject.SetActive(false);
        }
    }

    public void EnterChatter() {
        chat.SetActive(true);
    }

    public void ExitChatter()
    {
        chat.SetActive(false);
    }

    public void EnterAuthenticator() {
        authenticator.SetActive(true);
    }

    public void ExitAuthenticator() {
        authenticator.SetActive(false);
    }

    public void EnterSpawner() {
        spawner.SetActive(true);
    }

    public void ExitSpawner() {
        spawner.SetActive(false);
    }
}
