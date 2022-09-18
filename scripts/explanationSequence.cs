using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class explanationSequence : MonoBehaviour
{
    public GameObject Future;
    public GameObject thisIsA;
    public GameObject NotFuture;
    public GameObject The;
    public GameObject JustFuture;
    public GameObject A;
    public GameObject seriesOf;
    public GameObject areNow;    
    public GameObject notSaying;
    public GameObject butIt;
    public GameObject could;
    public GameObject videoTexture;
    public GameObject cameraPivot;
    public GameObject targetmove;

    public RawImage blackout;

    public Text readOffScript;

    private string[] script = 
    {
        "This chip marks the end",
        "Of what exactly?",
        "Free will? Personality? Ethics?",
        "All of the above",
        "The Sullivan chip, named after its creator, is a chip that controls hormones in the brain",
        "Your adrenaline, melatonin, and dopamine levels-- all controlled by someone else",
        "Dr. Sullivan wanted his creation to be the center of mental health treatments",
        "Within weeks it was used to help treat drug addiction, depression, and psychopathy",
        "But the government wanted to use it to combat terrorism",
        "pacifying suspects in third world countries by implanting chips in them against their will",
        "Protests erupted all over the world. But nobody listened-- nobody in government at least.",
        "The United Kingdom secretly funneled money into the project",
        "Countries like China and North Korea were already testing their own versions of the Chip to disperse into their own populations.", //protests

        "A decade since the Sullivan Chip was introduced to the middle east, terrorism dropped by 70%.",
        "What the project saved in U.S. military expenses, it cost nearly double to keep the project running.", //FBI people
        "Personnel, maintenance: the U.S. wanted to cut costs-- and fast.",

        "The answer?", 
        "In its everlasting greed and incompetence, the government decided to give an experimental AI control of every active chip.", // Computers 
        "The thing about computers is that they don’t have ethics.", 
        "They find any way to get the job done with the abilities they are given.", 
        "The AI quickly found out that it could end terrorism quite easily, if it could make sure that there was no one to commit it.",
        "Of course the government denied this, they said they had everything under control.",
        "Then people started dying.", 
        "The AI dialed up the adrenaline, and people dropped dead as they suffered a stroke.", 
        "The remaining few started protesting, and in the growing unrest, the AI had completed its mission in the worst way possible.", 

        "The entire political landscape changed after that.", 
        "Nearly every country stopped doing business with the U.S, and the U.K. denied being involved in the project at all.", 
        "At first every country blamed each other for the massive failure, until they all agreed the blame was to fall squarely on the shoulders of the Americans.", 
        "In the chaos, a new group rose to power-- the Armada.", 
        "The Sullivan chip, a new, revised version, is now mandated on every human.",
        "Perhaps there are a few people who despise the idea of having an implant in their brain, but they’re never angry enough to do anything about it.",
        "The Sullivan Chip sees to that.",
        ""
    }; 

    public bool startCameraMotion;

    private float scaleFactor;
    private float time;
    private float startTime;
    private float markerTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(flicker(Future, thisIsA));
        StartCoroutine("schedule");
        startTime = 0;
        Future.SetActive(true);
        thisIsA.SetActive(true);
        videoTexture.SetActive(true);
        Camera.main.fieldOfView = 11.5f;
        startCameraMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        scaleFactor = 1 + (0.1f * (time - startTime));
        Animate3DSpace();
        print(time - markerTime);
    }

    void Animate3DSpace(){
        if(startCameraMotion){
            if(Camera.main.fieldOfView < 70){
                Camera.main.fieldOfView += Time.deltaTime * 4 * Mathf.Log(((time - startTime)+1),10);
            }else{
                if(cameraPivot.transform.eulerAngles.x < 70){
                    cameraPivot.transform.Rotate(Vector3.right * 2f * Time.deltaTime);
                }else{
                    cameraPivot.transform.position = Vector3.MoveTowards(cameraPivot.transform.position, targetmove.transform.position, 0.1f * Time.deltaTime);
                    cameraPivot.transform.Rotate(0, 2f * Time.deltaTime, 0f, Space.World);
                }
            }
        }
    }

    IEnumerator flicker(GameObject BeginningText, GameObject StaticText, float waitPeriod){
        BeginningText.SetActive(true);
        StaticText.SetActive(true);
        BeginningText.transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
        yield return new WaitForSeconds(waitPeriod);
        BeginningText.transform.localScale = new Vector3(-scaleFactor,scaleFactor,scaleFactor);
        yield return new WaitForSeconds(0.1f);
        BeginningText.transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
        yield return new WaitForSeconds(1);
        BeginningText.transform.localScale = new Vector3(scaleFactor,-scaleFactor,scaleFactor);
        yield return new WaitForSeconds(0.1f);
        BeginningText.transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
        yield return new WaitForSeconds(0.6f);
        BeginningText.SetActive(false);
        StaticText.SetActive(false);
    }

    IEnumerator flicker(GameObject BeginningText, float waitPeriod){
        BeginningText.SetActive(true);
        BeginningText.transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
        yield return new WaitForSeconds(waitPeriod);
        BeginningText.transform.localScale = new Vector3(-scaleFactor,scaleFactor,scaleFactor);
        yield return new WaitForSeconds(0.1f);
        BeginningText.transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
        yield return new WaitForSeconds(1);
        BeginningText.transform.localScale = new Vector3(scaleFactor,-scaleFactor,scaleFactor);
        yield return new WaitForSeconds(0.1f);
        BeginningText.transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
        yield return new WaitForSeconds(0.6f);
        BeginningText.SetActive(false);
    }

    IEnumerator couldflicker(GameObject BeginningText, GameObject StaticText, float waitPeriod){
        BeginningText.SetActive(true);
        StaticText.SetActive(true);
        BeginningText.transform.localScale = new Vector3(1,1,1);
        yield return new WaitForSeconds(waitPeriod);
        BeginningText.transform.localScale = new Vector3(-scaleFactor,scaleFactor,scaleFactor);
        yield return new WaitForSeconds(0.1f);
        BeginningText.transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
        yield return new WaitForSeconds(1);
        BeginningText.transform.localScale = new Vector3(scaleFactor,-scaleFactor,scaleFactor);
        yield return new WaitForSeconds(0.1f);
        StaticText.SetActive(false);
        BeginningText.transform.localScale = new Vector3(3,3,3);
        BeginningText.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        yield return new WaitForSeconds(0.6f);
        BeginningText.SetActive(false);
        videoTexture.SetActive(false);
        
    }

    IEnumerator readScript(){
        for(int i = 0; i < script.Length; i++){
            readOffScript.text = script[i];
            //print(script[i].Length);
            float waitTime = script[i].Length * 0.06f + 1;
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator FadeImage(bool fadeAway, RawImage img)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= (Time.deltaTime * 1f))
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    IEnumerator schedule(){
        StartCoroutine(flicker(Future, thisIsA,1.5f));
        yield return new WaitForSeconds(3.5f);
        startTime = time;
        StartCoroutine(flicker(NotFuture, The, 1.5f));
        yield return new WaitForSeconds(3.5f);
        startTime = time;
        StartCoroutine(flicker(A, JustFuture, 1.5f));
        yield return new WaitForSeconds(3.5f);
        startTime = time;
        StartCoroutine(flicker(areNow, seriesOf, 2.5f));
        yield return new WaitForSeconds(4.5f);
        startTime = time;
        StartCoroutine(flicker(notSaying, 2.5f));
        yield return new WaitForSeconds(4.5f);
        startTime = time;
        StartCoroutine(couldflicker(could, butIt, 1.5f));
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeImage(true, blackout));
        startCameraMotion = true;
        yield return new WaitForSeconds(5f);
        StartCoroutine("readScript");
        yield return new WaitForSeconds(178f);
        StartCoroutine(FadeImage(false, blackout));
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Home_Base");

    }
}

/*
This is a future
Not the future
Just a future
A series of events that led us to where we are now
I’m not saying this will happen to you
But it could
This chip marks the beginning of the end
Of what exactly?
Free will? Personality? Ethics?
All of the above
The Sullivan chip, named after its creator, is a chip that controls hormones in the brain
Your adrenaline, melatonin, and dopamine levels-- all controlled by someone else
Dr. Sullivan wanted his creation to be the center of mental health treatments 
Within weeks it was used to help treat drug addiction, depression, and psychopathy
But the government wanted to use it to combat terrorism, pacifying suspects in third world countries by implanting chips in them against their will
Protests erupted all over the world. But nobody listened-- nobody in government at least.
The United Kingdom secretly funneled money into the project, and countries like China and North Korea were already testing their own versions of the Sullivan Chip to disperse into their own populations. 

A decade since the Sullivan Chip was introduced to the middle east, terrorism dropped by 70%. 
What the project saved in U.S. military expenses, it cost nearly double to keep the project running. 
Personnel, maintenance: the U.S. wanted to cut costs-- and fast.

The answer? 
In its everlasting greed and incompetence, the government decided to give an experimental AI control of every active chip. 
The thing about computers is that they don’t have ethics. 
They find any way to get the job done with the abilities they are given. 
The AI bot found out quite quickly that it could end terrorism quite easily, if it could make sure that there was no one to commit it. 
Of course the government denied this, they said they had everything under control. Then people started dying. 
The AI dialed up the adrenaline, and people dropped dead as they suffered a stroke. 
The remaining few started protesting, and in the growing unrest, the AI had completed its mission in the worst way possible. 

The entire political landscape changed after that. 
Nearly every country stopped doing business with the U.S, and the U.K. denied being involved in the project at all. 
At first every country blamed each other for the massive failure, until they all agreed the blame was to fall squarely on the shoulders of the Americans. 
In the chaos, a new group rose to power-- the Armada. 
The Sullivan chip, a new, revised version, is now mandated on every human. 
Perhaps there are a few people who despise the idea of having an implant in their brain, but they’re never angry enough to do anything about it. 
The Sullivan Chip sees to that.
*/